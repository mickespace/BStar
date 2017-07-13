import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { AppListService } from './app-list.service';
import { CommonService } from '../../shared/service/common-service';
import { AppShow, AppShowRequest } from '../model/app-list-model';

import { User } from '../../shared/model/user-model';

import { Jsonp, URLSearchParams, Http } from '@angular/http';

@Component({
  selector: 'app-list',
  templateUrl: './app-list.component.html',
  styleUrls: ['./app-list.component.css']
})
export class AppListComponent implements OnInit {

  public code: number;
  public isOk = false;
  public applist: Array<AppShow>;
  public jsonStr: string;
  public currentUser: User = new User();

  constructor(
    public cmService: CommonService,
    public listService: AppListService,
    public titleService: Title) {
  }

  ngOnInit() {
    this.setTitle('应用商店');
    // 获取userToken
    const storUser = localStorage.getItem('currentUser');
    const storUserToken = localStorage.getItem('userToken');
    if (storUser !== null && storUserToken === null) {
      this.currentUser = JSON.parse(storUser);
      console.log(this.currentUser.TokenKey);
      this.getUserToken(this.currentUser.TokenKey);
    }
    console.log('获取应用列表');
    this.getApps();
  }
  /**
   * 获取应用列表-基础应用
   */
  getApps() {
    const request = new AppShowRequest();
    request.Map = new Array('_id', 'DisplayName', 'DisplayIcon');
    const params = JSON.stringify(request);
    return this.listService.getAppList(params)
      .subscribe(
      (res) => {
        this.applist = res['Data'];
        this.isOk = res['IsOk'];
      }, (error) => {
        console.log(error);
      });
  }
  /**
  * 获取userToken
  * @param appKey 应用key
  * @param tokenKey tokenKey
  */
  public getUserToken(tokenKey: string) {
    return this.cmService.getUserToken(tokenKey).subscribe((res) => {
      const userToken = res['Data'].UserToken;
      localStorage.removeItem('userToken');
      localStorage.setItem('userToken', userToken);
    }, (error) => {
      console.log(error);
    });
  }
  /**
   * 设置title
   * @param newTitle title
   */
  public setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }
}


