/**
 * 用户登录/登出服务
 */
import { Injectable } from '@angular/core';
import { Http, URLSearchParams, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/catch';

import { User } from '../model/user-model';
import { AppConfig } from '../model/app-config-model';


@Injectable()
export class CommonService {
    public appConfig: AppConfig = new AppConfig();

    //获取userToken
    public userTokenUrl = this.appConfig.AppAddress + '/v1/user/token';
    constructor(public http: Http) {
    }

    /**
    *获取userToken
    */
    public getUserToken(tokenKey: string): Observable<any> {
        var params = new URLSearchParams();
        params.set('appKey', this.appConfig.AppStoreKey);
        params.set('tokenKey', tokenKey);
        return this.http.get(this.userTokenUrl, { search: params })
            .map((res: Response) => res.json())
            .catch((error: any) => Observable.throw(error || 'Server 错误'));
    }
}
