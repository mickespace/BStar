import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { UserMainComponent } from './user-main/user-main.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { UserAppsComponent } from './user-apps/user-apps.component';
import { AuthGuard } from './auth-guard';
import { userRoutes } from './user.routes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    RouterModule.forChild(userRoutes)
  ],
  declarations: [
    UserInfoComponent,
    UserMainComponent,
    UserAppsComponent
  ],
  providers: [
    AuthGuard,
  ]
})
export class UserModule { }
