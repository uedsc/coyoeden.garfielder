/**
* JS logic for adding media files
*/
Garfielder.M("UploadMedia", {
    init: function (opts) {
        this.opts = opts;
        this.$items = $("#media-items");
        this.$sizes = $("#media-sizes");
        this.$tplSize = $("#tpl-media-size");
    }, //init
    onLoad: function () {
        var me = this;
        if (this.opts.noFlash) {
            this.$items.css("opacity", 0.3).animate({ opacity: 1 }, 1500);
        }; //if

        //insert img into the post
        $("#media-items .media-item").live("dblclick", function (e) {
            var $temp = $(this).clone();
            $temp.find("img").each(function (i, o) {
                o.src = o.getAttribute("data-raw");
                o.removeAttribute("data-raw");
            });
            parent.Garfielder.TopicAdd.InsertHTML($temp.html());
            $temp = null;
        }).live("click", function (e) {
            var $temp = $(this), meta, id = this.getAttribute("data-id"), prefix = "#media-size-";
            if (!$temp.data("meta1")) {
                meta = $.evalJSON($temp.data("meta"));
                $temp.data("meta1", meta);
                me.$sizes.children().hide().end()
                    .append(
                        $('<div id="' + prefix + id + '"/>')
                            .append(me.$tplSize.tmpl(meta.MetaData.Thumbs))
                            .find("input:eq(0)").attr("checked", true)
                            .end()
                    );
            } else {
                me.$sizes.children().hide().filter(prefix + id).fadeIn();
            };
        });

    } //onload
});