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
            //overlay
            var ovl = {
                $loading: $("#ovl-ing"),
                $ovl: $("#ovl-wrap").overlay({
                    close: '.ovl-close',
                    onClose: function (e) {
                        ovl.$img.hide();
                        ovl.$loading.show();
                        ovl.$ovl.width(640);
                    },
                    onLoad: function (e) {
                        //fix position
                        ovl.$ovl.css("width","auto");
                        ovl.$img.fadeIn();
                    },
                    top: '18%'
                }),
                $img: $("#ovl-img")
            };
            ovl.api = ovl.$ovl.data("overlay");

            $("#gallery a").focus(function () {
                this.blur();
            });
            $("#gallery img").click(function () {
                var $i = $(this), src = $i.data("raw");
                ovl.api.load();
                if (!$i.data("preloaded")) {
                    $.preload(src, {
                        loaded: function (img) {
                            $i.data("preloaded", true);
                            ovl.$loading.hide();
                            ovl.$img.attr("src", src); //.fadeIn();

                        },
                        noCache: true
                    });
                } else {
                    ovl.$loading.hide();
                    ovl.$img.attr("src", src); //.fadeIn();
                };
            });

        } //onload
    };
} ()));