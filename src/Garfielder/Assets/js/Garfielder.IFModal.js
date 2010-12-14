/**
*   IFrame modal dialog.
    @dependency jquery.js jquery.tools.js
*/
var IFModal = (function ($) {
    var pub = {}, p = {};
    p.init = function () {
        p.$if = $("#ovl-ifr").load(function () {
            p.$loading.hide();
            p.$if.show();
            if (p.$if.height() < p.opts.minH) {
                p.$if.height(p.opts.minH);
            };
            if (p.opts.after) {
                p.opts.after.call(p);
            };
        });
        p.$loading = $("#ovl-ing");

        p.$ovl = $("#ovl-ifr-wrap").overlay({
            close: '.ovl-close',
            onClose: function (e) {
                p.$if.hide();
                if (p.opts.hide) {
                    p.opts.hide(e);
                };
            }
        });
        p.ovl = p.$ovl.data("overlay");
    };
    p.init();

    pub.Show = function (url, opts) {
        if (!url || url == "") return;
        p.opts = $.extend({ minH: 200 }, opts || {});
        p.$loading.show();
        if (p.opts.before) {
            p.opts.before.call(p);
        };
        p.$if.attr("src", url);

        p.ovl.load();

    };
    return pub;
})(jQuery);