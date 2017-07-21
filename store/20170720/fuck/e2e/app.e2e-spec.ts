import { AppStoreNewPage } from './app.po';

describe('app-store-new App', () => {
  let page: AppStoreNewPage;

  beforeEach(() => {
    page = new AppStoreNewPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
