/**
 * 应用列表展示-实体
 */
export class AppShow {
    _id: string;
    DisplayName: string;
    DisplayIcon: string;
}
/**
 * 获取列表请求-实体
 */
export class AppShowRequest {
    Map: Array<string>;
    //TODO:还有别的一些参数
}