/**
 * 获取用的应用
 */
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
// import { AppListService } from '../../apps-mg/app-list/app-list.service';
import { UserAppsService } from './user-apps.service';
import { Observable } from 'rxjs/Observable';
import { UserAppRequest, Page } from '../model/user-app-request-model';
// import { UserApp } from '../model/user-applist-model';
import { AppConfig } from '../../shared/model/app-config-model';

@Component({
  selector: 'app-user-apps',
  templateUrl: './user-apps.component.html',
  styleUrls: ['./user-apps.component.css']
})
export class UserAppsComponent implements OnInit {

  public applist: Array<AppConfig>;
  public appRequest: UserAppRequest;
  // public pageParams: Page;

  constructor(
    public listService: UserAppsService,
    public titleService: Title
  ) { }

  ngOnInit() {
    this.setTitle('我的应用');
    this.applist = new Array<AppConfig>();
    this.appRequest = new UserAppRequest();
    // this.pageParams = new Page();
    this.getApps();
  }
  /**
   * 获取我的应用
   */
  public getApps() {
    // this.pageParams.Index = 0;
    // this.pageParams.Count = 20;
    // this.appRequest.Page = this.pageParams;
    this.appRequest.Map = new Array('_id', 'Name', 'Icon', 'Price');
    // tslint:disable-next-line:prefer-const
    let params = JSON.stringify(this.appRequest);
    return this.listService.getMyApp(params)
      .subscribe((res) => {
        if (res['IsOk']) {
          this.applist = res['Data'];
          console.log(this.applist);
        }
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
