import { Injectable } from '@angular/core';
import { Route, Router, CanLoad, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from "rxjs/Observable";


@Injectable()
export class AuthGuard implements CanLoad, CanActivate {
  constructor() {
    console.log("守卫构造！");
  }
  canLoad(route: Route): boolean | Observable<boolean> | Promise<boolean> {
    console.log("我让你访问：" + route.path);
    return true;
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    console.log("我让你激活");
    return true;
  }
}
