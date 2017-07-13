/**
 * 应用列表获取服务
 *auth：zsq
 *time：2017年3月31日11:57:51
 *desc：null
 */
import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Observable } from 'rxjs/Rx';
import { AppShow } from '../model/app-list-model';

@Injectable()
export class AppListService {

  //public appListURL = 'src/mock-data/applist-mock.json';
  public appListURL = 'http://localhost:19432/api/v1/app/user/list';

  constructor(public _http: Http) {
  }

  /**
   * Http-获取应用列表
   */
  public getAppList(listparams: string): Observable<any[]> {
    let params = new URLSearchParams();
    params.set('listparams', listparams.toString());
    console.log(listparams)

    return this._http
      .get(this.appListURL, { search: params })
      .map((res) => res.json())
      .catch((error: any) => Observable.throw(error || 'Server 错误'));
  }
}
