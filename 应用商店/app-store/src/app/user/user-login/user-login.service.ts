/**
 * 用户登录/登出服务
 */
import { Injectable } from '@angular/core';
import { Http, URLSearchParams, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/catch';

import { User } from '../../shared/model/user-model';
import { AppConfig } from '../../shared/model/app-config-model';


@Injectable()
export class UserLoginService {
  public appConfig: AppConfig = new AppConfig();
  public subject: Subject<User> = new Subject<User>();
  // 登录url
  public userLoginURL = this.appConfig.AppAddress + '/v1/user/login';
  // 获取userToken
  public userTokenUrl = this.appConfig.AppAddress + '/v1/user/token';

  constructor(public http: Http) {
  }

  /**
   * 用户登录服务
   * @param appKey AppKey
   * @param userName 用户名（邮箱或手机）
   * @param password 密码
   */
  public login(userName: string, password: string): Observable<any> {
    const params = new URLSearchParams();
    params.set('appKey', this.appConfig.AppStoreKey);
    params.set('username', userName);
    params.set('password', password);
    return this.http
      .get(this.userLoginURL, { search: params })
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error || 'Server 错误'));
  }
  /**
  *获取userToken
  */
  public getUserToken(tokenKey: string): Observable<any> {
    const params = new URLSearchParams();
    params.set('appKey', this.appConfig.AppStoreKey);
    params.set('tokenKey', tokenKey);
    return this.http.get(this.userTokenUrl, { search: params })
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error || 'Server 错误'));
  }
  /**
   * 退出
   */
  public logout() {
    console.log('退出登录');
  }
}
