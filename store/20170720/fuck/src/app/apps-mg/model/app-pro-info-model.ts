export class ProductInfo {
    _id: string;
    AppKey: string;
    AppId: string;
    AppType: number;
    Developer: string;
    Price: number;
    Platform: number;
    SubmitDate: Date;
    FileId: string;
    FileSize: number;
    DownloadCount: number;
    Star: number;
    Version: number;
    DisplayName: string;
    DisplayIcon: string;
    Screenshots: Array<string>;
    Description: string;
    UpdateLog: string;
    State: number; // 产品状态：0-未审核；1-审核未通过；2-审核通过未发布；3-已发布
    Distro: number; // 发行版：0-测试版；1-预览版；2-稳定版
    ReviewComment: string; // 审核评论
    ApprovedDate: Date; // 产品审核通过时的日期
    PublishDate: Date; // 产品的发布日期
    Comments?: Array<AppComment2>;
    Modules?: Array<AppModuleInfo>; // 只有Type<10时才会具有以下字段
    User: AppCreateUser; // 自创User字段
}
/**
 *应用评论-实体
 */
export class AppComment2 {
    _id: string;
    Star: number;
    Content: string;
    CreateDate: Date;
    CreateUserName: string;
    UserId: string;  // 评论人Id
}
export class AppModuleInfo {
    Key: string;
    Name: string;
    Group: string;
    EveryoneAccess: boolean;
    Description: string;
}
export class AppCreateUser {
    _id: string;
    RealName: string;
}
