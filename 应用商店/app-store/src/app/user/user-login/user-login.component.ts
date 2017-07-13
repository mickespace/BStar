import { Component, OnInit, Input, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, ActivatedRouteSnapshot, RouterState, RouterStateSnapshot } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { UserLoginService } from './user-login.service';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import { LoginRequest } from '../model/user-login-model';
import { User } from '../../shared/model/user-model';

import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  public userRe: LoginRequest = new LoginRequest();
  public subject: Subject<User> = new Subject<User>();
  public loading = false;
  public userName: string;
  public password: string;

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public loginService: UserLoginService,
    public titleService: Title,
    public toastr: ToastsManager,
    public vcf: ViewContainerRef
  ) {
    this.toastr.setRootViewContainerRef(vcf);
  }

  ngOnInit() {
    this.setTitle('登录');
  }

  public doLogin() {
    this.loading = true;
    this.userName = this.userRe.userName;
    this.password = this.userRe.password;
    this.loginService
      .login(this.userName, this.password)
      .subscribe(
      (data) => {
        if (data['IsOk'] === true) {
          const activatedRouteSnapshot: ActivatedRouteSnapshot = this.activatedRoute.snapshot;
          const routerState: RouterState = this.router.routerState;
          const routerStateSnapshot: RouterStateSnapshot = routerState.snapshot;
          localStorage.setItem('currentUser', JSON.stringify(data['Data']));
          this.toastr.success('即将跳转应用商店', '登录成功！');
          setTimeout(() => {
            this.router.navigateByUrl('/applist');
            window.location.reload(false);
          }, 1000);

        } else {
          this.toastr.error('账号或密码有误，请重新输入', '登录失败！');
          this.loading = false;
        }

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
    return this.loginService.getUserToken(tokenKey).subscribe((res) => {
      const userToken = res['Data'].UserToken;
      console.log('userToken：' + userToken);
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
