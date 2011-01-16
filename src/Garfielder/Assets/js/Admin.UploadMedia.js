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
        var me = this, prefix = "media-size-";
        if (this.opts.noFlash) {
            this.$items.css("opacity", 0.3).animate({ opacity: 1 }, 1500);
        }; //if

        //insert img into the post
        $("#media-items .media-item").live("dblclick", function (e) {
            //TODO:insert button!
            var $temp = $(this).clone();
            var srcSelected = $("#"+prefix + this.getAttribute("data-id")).find("input:checked").val();
            alert(srcSelected);
            $temp.find("img").attr("src", srcSelected);
            parent.Garfielder.TopicAdd.InsertHTML($temp.html());
            $temp = null;
        }).live("click", function (e) {
            var $temp = $(this), meta, id = this.getAttribute("data-id");
            if (!$temp.data("meta-init")) {
                meta = $temp.data("meta");
                $temp.data("meta-init", true);
                me.$sizes.children().hide().end()
                    .append(
                        $('<div id="' + prefix + id + '"/>')
                            .append(me.$tplSize.tmpl(meta.Thumbs))
                            .find("input:eq(0)").attr("checked", true)
                            .end()
                    );
            } else {
                me.$sizes.children().hide().filter(prefix + id).fadeIn();
            };
        });

    } //onload
});