/**
 * Created by NieFZ on 2017-3-26.
 */
'use strict';
var lib = {
    getParams: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null && r.toString().length) {
            return decodeURI(r[2]);
        }
    }
};