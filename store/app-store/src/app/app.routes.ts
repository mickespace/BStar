import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AppListComponent } from './apps-mg/app-list/app-list.component';
import { AppDetailComponent } from './apps-mg/app-detail/app-detail.component';

import { UserLoginComponent } from './user/user-login/user-login.component';

export const appRoutes = [
    {
        path: '',
        redirectTo: 'applist',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: UserLoginComponent
    },
    {
        path: 'applist',
        component: AppListComponent
    },
    {
        path: 'appdetail/:appId',
        component: AppDetailComponent
    },
    {
        path: 'user',
        loadChildren: './user/user.module#UserModule'
    }

]