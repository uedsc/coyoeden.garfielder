Garfielder.M("TopicShow", (function () {
    return {
        init: function (opts) {
            this.opts = opts;
        },
        onLoad: function () {
            var $items = $("#gallery .gallery-item"), $curMenu = null;
            //page tabs
            $("#pager a").each(function (i, o) {
                (function (i1, $o) {
                    if (i1 == 0) {
                        $curMenu = $o;
                    };
                    $o.click(function () {
                        if ($o.hasClass("on")) return false;
                        if ($curMenu) {
                            $curMenu.removeClass("on");
                        };
                        $curMenu = $o.addClass("on");
                        $items.hide().eq(i1).fadeIn();
                        return false;
                    });
                })(i, $(o));
            });

        } //onload
    };
} ()));