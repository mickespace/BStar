import { Injectable } from '@angular/core';
import { Http, URLSearchParams, Response } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Observable } from 'rxjs/Rx';
// model
import { MyApp } from '../model/my-app-model';
import { AppConfig } from '../../shared/model/app-config-model';

@Injectable()
export class UserAppsService {
    public appConfig: AppConfig = new AppConfig();
    // 获取usertoken
    public userTokenUrl = this.appConfig.AppAddress + '/v1/user/token';
    //获取applist
    public appListURL = this.appConfig.AppAddress + '/v1/user/apps';

    constructor(public http: Http) {
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
    * Http-获取我的应用
    */
    public getMyApp(listparams: string): Observable<any[]> {
        const params = new URLSearchParams();
        const userToken = localStorage.getItem('userToken');
        params.set('userToken', userToken);
        // params.set('listparams', listparams.toString());
        console.log('888888:' + params);
        return this.http.get(this.appListURL, { search: params })
            .map((res) => res.json())
            .catch((error: any) => Observable.throw(error || 'Server 错误'));
    }
}