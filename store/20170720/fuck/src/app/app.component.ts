import { Component, HostListener, ElementRef, Renderer, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './shared/model/user-model';
import { UserLoginService } from './user/user-login/user-login.service';

import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public currentUser: User;
  title = 'app works!';

  constructor(
    public router: Router,
    public renderer: Renderer,
    public elementRef: ElementRef,
    public loginService: UserLoginService,
    public toastr: ToastsManager,
    public vcf: ViewContainerRef
  ) {
    toastr.setRootViewContainerRef(vcf);
    this.currentUser = new User();
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }
  public doLogout(): void {
    this.loginService.logout();
    localStorage.clear();
    this.toastr.info('即将跳转至登录页面', '退出登录成功');
    setTimeout(() => {
      this.router.navigateByUrl('/login');
      window.location.reload();
    }, 1000);
  }
}
