Garfielder.M("Topic", (function () {
    return {
        init: function (opts) {
            this.opts = opts;
        },
        onLoad: function () {
            var $tabs = $("#gallery .gallery-tab");
            //page tabs
            $("#pager a").each(function (i, o) {
                (function (i1, $o) {
                    $o.click(function () {
                        if ($o.hasClass("on")) return;
                        $o.addClass("on");
                        $tabs.removeClass("gallery-tab-on").eq(i1).addClass("gallery-tab-on");
                    });
                })(i, $(o));
            });
        }
    };
} ()));