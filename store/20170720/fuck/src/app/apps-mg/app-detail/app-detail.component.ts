import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
// service
import { AppDetailService } from './app-detail.service';
// model
import { UserAppRequest } from '../../user/model/user-app-request-model';
// import { AppInfo, AppComment } from '../model/app-info-model';
import { ProductInfo, AppComment2, AppModuleInfo, AppCreateUser } from '../model/app-pro-info-model';
import { CommentPost } from '../model/app-comment-post-model';
import { User } from '../../shared/model/user-model';

import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-detail',
  templateUrl: './app-detail.component.html',
  styleUrls: ['./app-detail.component.css']
})
export class AppDetailComponent implements OnInit {

  // !首先进行初始化，才能赋值
  // public app: AppDetail;
  public appKey: string;
  // appInfo信息
  // public appInfo: AppInfo;

  // product-info初始化
  public productInfo: ProductInfo;

  // 应用截图
  public appScreensUrl: Array<string>;
  // map调用
  public appRequest: UserAppRequest;
  // app创建者信息
  public appCreater: AppCreateUser;
  // app评论信息
  public userComments: Array<AppComment2>;
  // app Modules产品模块 集合信息
  public proModules: Array<AppModuleInfo>;

  // 是否存在评论
  public isExistCts = false;
  public aveStar = '5';
  // 当前应用评价数
  public commentCount = 0;
  // 文件大小
  public fileSize: string;

  // 用户是否登录
  public isLogined = false;
  public currentUser: User = new User();
  public isAppObtained = false;
  public userToken: string;

  // 用户评论
  public max = 5;
  public star = 5;
  public commentPost: CommentPost;
  public isAdding = false;

  public isReadonly = false;

  constructor(
    public activateRoute: ActivatedRoute,
    public service: AppDetailService,
    public titleService: Title,
    public toastr: ToastsManager,
    public vcf: ViewContainerRef) {
    toastr.setRootViewContainerRef(vcf);
  }

  ngOnInit() {

    // this.app = new AppDetail();

    // 初始化信息
    this.productInfo = new ProductInfo();

    this.appScreensUrl = new Array<string>();
    this.appCreater = new AppCreateUser();

    this.userComments = new Array<AppComment2>();

    this.appRequest = new UserAppRequest();
    // 获取当前登录用户信息
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    // 获取userToken信息
    this.userToken = localStorage.getItem('userToken');
    if (this.currentUser) {
      this.isLogined = true;
      // 判断当前用户是否已经拥有此app
      // this.getAppIsObtained();
      this.isAppObtained = true;
    }
    // 评论信息初始化
    this.commentPost = new CommentPost();
    this.commentPost.Star = 5;
    console.log('获取应用详情');
    this.activateRoute.params.subscribe(
      params => this.getAppDetail(params['appId']),
      par => console.log(par['appId'])
    );
  }

  /**
   * 获取应用详情
   * @param appId AppInfo-_id
   */
  public getAppDetail(appId: string) {
    this.service.getAppDetail(appId)
      .subscribe(
      (res) => {
        // 将返回的数据存在this.productInfo
        this.productInfo = res['Data'];
        this.appCreater = this.productInfo.User;
        console.log('Create::' + this.appCreater);
        // this.appInfo = this.app.AppInfo;
        const transPro = JSON.stringify(this.productInfo);
        console.log('COMMENTS:' + this.productInfo + 'trans:::' + transPro);
        this.fileSize = (this.productInfo.FileSize / 1024).toFixed(1);
        this.userComments = this.productInfo.Comments;
        if (this.productInfo.DisplayIcon === undefined) {
          this.productInfo.DisplayIcon = null;
          console.log('DisplayIcon:' + this.productInfo.DisplayIcon);
        }
        if (this.productInfo.Comments === undefined) {
          this.productInfo.Comments = null;
          console.log('Comments:' + this.productInfo.Comments);
        } else {
          // 格式化显示评论用户名
          this.userComments.forEach(c => {
            c.CreateUserName = this.formatUserName(c.CreateUserName);
          });
        }
        // 遍历截图数组
        if (this.productInfo.Screenshots) {
          const screensImgs = this.productInfo.Screenshots;
          if (screensImgs.length >= 1) {
            for (let i = 0; i <= screensImgs.length && i < 4; i++) {
              this.appScreensUrl.push(screensImgs[i]);
            }
          }
        }
        this.CalculateStar(this.userComments);
        this.setTitle(this.productInfo.DisplayName + '-详情');
      },
      (error) => {
        console.log(error);
      });
  }
  /**
   * 计算应用平均Star
   * @param commonts 评论集合
   */
  public CalculateStar(commonts: Array<AppComment2>): void {
    if (commonts === undefined) {
      commonts = null;
    } else {
      const count = commonts.length;
      if (count > 0) {
        this.isExistCts = true;
        this.commentCount = commonts.length;
        let sumStar = 0;
        commonts.forEach(element => {
          sumStar = sumStar + element.Star;
        });
        const ave = sumStar / count;
        this.aveStar = ave.toFixed(1).toString();
      }
    }
  }
  /**
   * 当前用户是否已经拥有此app
   */
  public getAppIsObtained() {
    return this.service.IsAppObtained(this.currentUser.TokenKey, this.productInfo.AppKey)
      .subscribe(
      (res) => {
        this.isAppObtained = res['Data'];
      }, (error) => {
        console.log(error);
      });
  }
  /**
   * 添加应用评论
   */
  public addComment() {
    this.isAdding = true;
    this.commentPost.CreateDate = new Date();
    this.commentPost.CreateUserName = this.currentUser.RealName;
    // 添加评论
    if (!this.userToken) {
      console.log('usertoken不存在，无法评论');
      return;
    }
    return this.service.AddAppComment(this.userToken, this.productInfo._id, this.commentPost)
      .subscribe((res) => {
        console.log('res:' + res['Data']);
        const comment = res['Data'];
        if (res['IsOk']) {
          this.toastr.info('添加评论成功', '添加成功');
          // 刷新评论
          this.commentPost = new CommentPost();
          this.commentPost.Star = 5;
          console.log('ccccccccc:' + this.userComments);
          this.userComments.push(res['Data']);
          this.CalculateStar(this.userComments);
        } else {
          this.toastr.info('添加评论失败', '添加失败');
        }
      }, (error) => {
        console.log(error);
      });
  }

  public hoveringOver(value: number): void {
    this.commentPost.Star = value;
  }
  /**
   * 格式用户名
   * @param name 格式的字符串
   */
  public formatUserName(name: string): string {
    const reg = /^(.).+(.)$/g;
    return name.replace(reg, '$1**');
  }

  /**
   * 设置title
   * @param newTitle title
   */
  public setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }
}
