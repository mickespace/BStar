(function(window, undefined) {
  var dictionary = {
    "cf6b6228-2440-41fd-8de9-111757ea3565": "注册页面",
    "c05e0ffb-66e3-4d62-8530-2b90fe28adb0": "案列",
    "b1b77cc9-428f-4aee-b00c-6269ce963b77": "关于我们",
    "d12245cc-1680-458d-89dd-4f0d7fb22724": "首页",
    "1ad161e1-a218-4b9c-ad09-fce62a5f7b34": "登录、注册页面",
    "02f39a12-c9a5-4aa3-ab07-e377e6e28374": "联系我们",
    "c02b21a9-25d7-4105-96f2-412972a69caf": "加入我们",
    "87db3cf7-6bd4-40c3-b29c-45680fb11462": "960 grid - 16 columns",
    "e5f958a4-53ae-426e-8c05-2f7d8e00b762": "960 grid - 12 columns",
    "f39803f7-df02-4169-93eb-7547fb8c961a": "Template 1"
  };

  var uriRE = /^(\/#)?(screens|templates|masters)\/(.*)(\.html)?/;
  window.lookUpURL = function(fragment) {
    var matches = uriRE.exec(fragment || "") || [],
        folder = matches[2] || "",
        canvas = matches[3] || "",
        name, url;
    if(dictionary.hasOwnProperty(canvas)) { /* search by name */
      url = folder + "/" + canvas;
    }
    return url;
  };

  window.lookUpName = function(fragment) {
    var matches = uriRE.exec(fragment || "") || [],
        folder = matches[2] || "",
        canvas = matches[3] || "",
        name, canvasName;
    if(dictionary.hasOwnProperty(canvas)) { /* search by name */
      canvasName = dictionary[canvas];
    }
    return canvasName;
  };
})(window);