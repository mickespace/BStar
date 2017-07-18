/**
 * 应用Info-实体
 */
export class AppInfo {
    _id: string;
    FileId: string;
    Platform: number;
    Version: number;
    DisplayName: string;
    Description: string;
    Price: number;
    UpdateLog: string;
    State: number;
    Modules: string;
    AppKey: string;
    SubmitDate: Date;
    DownloadCount: number;
    ReviewComment: string;
    Comments: Array<AppComment>;
    DisplayIcon: string;
    ScreenshotIds: Array<string>;//截图链接 列表
}
/**
 *应用评论-实体
 */
export class AppComment {
    _id: string;
    Star: number;
    Title: string;
    Content: string;
    CreateDate: Date;
    CreateUserName: string;
}