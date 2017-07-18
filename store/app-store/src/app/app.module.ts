import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule, BaseRequestOptions, RequestOptions } from '@angular/http';
import { RouterModule } from '@angular/router';
// Angular Material
import { MaterialModule } from '@angular/material';
import { AppComponent } from './app.component';
import { APP_BASE_HREF, LocationStrategy, HashLocationStrategy } from '@angular/common';

// 服务
import { AuthGuard } from './service/auth-guard.service';
import { AppListService } from './apps-mg/app-list/app-list.service';
import { AppDetailService } from './apps-mg/app-detail/app-detail.service';
import { UserLoginService } from './user/user-login/user-login.service';
import { CommonService } from './shared/service/common-service';

import { appRoutes } from './app.routes';
import { AppListComponent } from './apps-mg/app-list/app-list.component';
import { AppDetailComponent } from './apps-mg/app-detail/app-detail.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
// 组件ng2-bootstrap
import { RatingModule } from 'ng2-bootstrap';
// 提示组件
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { ToastOptions } from 'ng2-toastr';
import { CustomOption } from './shared/model/custom-option-model';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    JsonpModule,
    MaterialModule,
    RouterModule,
    RatingModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    ToastModule.forRoot()
  ],
  declarations: [
    AppComponent,
    AppListComponent,
    AppDetailComponent,
    UserLoginComponent
  ],
  providers: [
    AuthGuard,
    AppListService,
    AppDetailService,
    UserLoginService,
    CommonService,
    Title,
    { provide: APP_BASE_HREF, useValue: '/' },
    // 防止刷新丢失页面
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: ToastOptions, useClass: CustomOption },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
