import { AppInfo } from './app-info-model';
/**
 * 应用详情-实体
 */
export class AppDetail {
    _id: number;
    UserId: string;
    AppKey: string;
    AppName: string;
    Developer: string;
    Description: string;
    SupportedPlatform: number;
    Icon: string;//应用图标
    CreateDate: Date;
    ApiLevel: number;//应用访问Api级别
    User: AppCreateUser;
    FileSize: number;
    Screenshots: Array<string>;
    AppInfo: AppInfo;
}
/**
 * 创建者-实体
 */
export class AppCreateUser {
    _id: string;
    RealName: string;
}
