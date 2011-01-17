/**
* JS logic for adding media files
*/
Garfielder.M("UploadMedia", {
    init: function (opts) {
        this.opts = opts;
        this.ui = {
            $items: $("#media-items"),
            $curItem: null,
            $sizes: $("#media-sizes"),
            $curSizes: null,
            $tplSize: $("#tpl-media-size"),
            $insert: $("#btn-insert")
        };

    }, //init
    onLoad: function () {
        var me = this, prefix = "media-size-";
        if (this.opts.noFlash) {
            this.ui.$items.css("opacity", 0.3).animate({ opacity: 1 }, 1500);
        }; //if

        //insert img into the post
        me.ui.$insert.click(function (e) {
            if (!me.ui.$curItem) {
                alert("Nothing selected!Please select a media file firstly!");
                return;
            };
            var $temp = me.ui.$curItem.clone(), $radio = me.ui.$curSizes.find("input:checked");
            $temp.find("img").removeAttr("style").attr("src", $radio.val()).attr("width", $radio.attr("data-w")).attr("height", $radio.attr("data-h"));
            parent.Garfielder.TopicAdd.InsertHTML($temp.html());
            $temp = null;
            $radio = null;
        });
        $("#media-items .media-item").live("click", function (e) {
            var $temp = $(this), meta, id = this.getAttribute("data-id");
            //item has beed selected
            if ($temp.hasClass("on")) return;
            //de-select last item
            if (me.ui.$curItem) {
                me.ui.$curItem.removeClass("on");
            };
            //set current item
            me.ui.$curItem = $temp.addClass("on");
            //show sizes
            if (!$temp.data("meta-init")) {
                meta = $temp.data("meta");
                // field id
                $.each(meta.Thumbs, function (i0, o0) {
                    o0.RefId = id;
                });
                $temp.data("meta-init", true);
                me.ui.$sizes.children().hide().end()
                    .append(
                        $('<div id="' + prefix + id + '" class="media-size-choice"/>')
                            .append(me.ui.$tplSize.tmpl(meta.Thumbs))
                            .find("input:eq(0)").attr("checked", true)
                            .end()
                    );
            } else {
                me.ui.$sizes.children().hide().filter("#" + prefix + id).fadeIn();
            };
            //set current sizes
            me.ui.$curSizes = $("#" + prefix + id);
        });

    } //onload
});