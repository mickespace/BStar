/**
 * 应用详情获取服务
 *auth：zsq
 *time：2017年3月31日11:57:51
 *desc：null
 */
import { Injectable } from '@angular/core';
import { Http, Response, URLSearchParams, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { AppDetail } from '../model/app-detail-model';
import { CommentPost } from '../model/app-comment-post-model';
import { AppConfig } from '../../shared/model/app-config-model';


@Injectable()
export class AppDetailService {

  public appConfig: AppConfig = new AppConfig();
  // 获取app详情url
  public appDetailUrl = this.appConfig.AppAddress + '/v1/app/user/info';
  // 评论添加url
  public addCommentUrl = this.appConfig.AppAddress + '/v1/app/user/comment/add';
  // 判定app是否已经被当前用户获取
  public appObtainedUrl = this.appConfig.AppAddress + '/v1/user/xxx';


  constructor(public http: Http) { }
  /**
   * 获取应用详情
   * @param appId appInfo id
   * @param map 字段映射
   */
  public getAppDetail(appId: string, map?: string): Observable<AppDetail> {
    const params = new URLSearchParams();
    params.set('id', appId);
    // tslint:disable-next-line:curly
    if (map === undefined)
      map = null;
    params.set('map', map);
    return this.http
      .get(this.appDetailUrl, { search: params })
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error || 'Server 错误'));
  }
  /**
   * 用户是否拥有此应用
   * @param tokenKey 用户tokenKey
   * @param appKey 应用Key
   */
  public IsAppObtained(tokenKey: string, appKey: string): Observable<any> {
    const params = new URLSearchParams();
    params.set('taokenKey', tokenKey);
    params.set('appKey', appKey);
    return this.http
      .get(this.appObtainedUrl, { search: params })
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error || 'Server 错误'));
  }
  /**
   * 添加应用评论
   * @param userToken userToken
   * @param appInfoId appInfoId
   * @param commentPost 评论comment-实体
   */
  public AddAppComment(userToken: string, appInfoId: string, commentPost: CommentPost): Observable<any> {
    const commentStr = JSON.stringify(commentPost);
    // push params into url
    const params = '?userToken=' + userToken + '&appInfoId=' + appInfoId + '&data=' + commentStr;
    this.addCommentUrl = this.addCommentUrl + params;

    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.http.post(this.addCommentUrl, options)
      .map((res) => res.json())
      .catch(this.handleError);
  }
  public handleError(error: Response | any) {
    // In a real world app, you might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }
}
