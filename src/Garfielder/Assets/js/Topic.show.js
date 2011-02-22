Garfielder.M("TopicShow", (function () {
    return {
        init: function (opts) {
            this.opts = opts;
        },
        onLoad: function () {
            var $items = $("#gallery .gallery-item");
            //page tabs
            $("#pager a").each(function (i, o) {
                (function (i1, $o) {
                    $o.click(function () {
                        if ($o.hasClass("on")) return false;
                        $o.addClass("on");
                        return false;
                    });
                    $items.hide().eq(i1).fadeIn();
                })(i, $(o));
            });

        } //onload
    };
} ()));