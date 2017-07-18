export class UserAppRequest {
    Page: Page;
    Map: Array<string>;
    //TODO:还有别的一些参数
}

export class Page {
    Index: number;
    Count: number;
}