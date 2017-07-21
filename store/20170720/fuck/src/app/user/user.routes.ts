import { RouterModule } from '@angular/router';
import { UserAppsComponent } from './user-apps/user-apps.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { UserMainComponent } from './user-main/user-main.component';
import { AuthGuard } from './auth-guard';

export const userRoutes = [
    {
        path: '',
        component: UserMainComponent,
        canActivate: [AuthGuard],
        children: [
            { path: '', redirectTo: 'userinfo', pathMatch: 'full' },
            { path: 'userinfo', component: UserInfoComponent, canActivate: [AuthGuard] },
            { path: 'userapps', component: UserAppsComponent, canActivate: [AuthGuard] }
        ]
    }
]