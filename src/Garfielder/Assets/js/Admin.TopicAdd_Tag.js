Garfielder.AddModule("TopicAdd_Tag", {
    init: function (opts) {
        var me = this;
        this.$tagAdder = $("#tagAdder");
        this.$tag = $("#newtag-topic");
        this.$tagTip = $("#tagTip");
        this.$btnTag = $("#btnAddTag");
        this.$tagsAdded = $("#tagsAdded");
        this.$btnTagSelect = $("#btnTagSelect");
        this.$tagCloud = $("#tagcloud-topic");

        this.tagData = { TagID: [] };

        //init events
        this.$btnTag.click(function () {
            if (me.$tag.val() == "") {
                me.$tag.addClass("error");
                return;
            };
            me.addTag(me.$tag.val());
        });

        $("#tagsAdded .delTag").live("click", function () {
            var idx = $.inArray(me.tagData.TagID, this.rel);
            me.tagData.TagID.splice(idx, 1);
            $(this).parent().fadeOut("slow", function () {
                $(this).remove();
            });
        });

        this.$btnTagSelect.click(function () {
            me.$tagCloud.slideDown();
        });

        $("#tagcloud-topic .tagitem").live("click", function () {
            if ($.inArray(me.tagData.TagID, this.rel) == -1) {
                me.tagData.TagID.push(this.rel);
            };
            $(this).fadeOut();
        });

    },
    addTag: function (tag) {
        var me = this;
        $.ajax({
            url: Garfielder.SiteRoot + "Camp/CheckTag",
            dataType: "json",
            data: tag,
            success: function (d) {
                if (d.Error) {
                    me.$tagTip.html(d.Msg).fadeIn();
                } else {
                    me.$tagTip.fadeOut();
                    me.$tagsAdded.append(Garfielder.EvalTpl('<span class="tag"><a href="javascript://" class="delTag" rel="%Id%">X</a>%Name%</span>', d));
                    if ($.inArray(me.tagData.TagID, d.Id) == -1)
                        me.tagData.TagID.push(d.Id);
                };
            }
        });
    }
});