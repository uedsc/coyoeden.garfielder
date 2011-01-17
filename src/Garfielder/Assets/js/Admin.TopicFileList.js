Garfielder.M("TopicFileList", {
    init: function (opts) {
        this.opts = opts;
        this.ui = {
            $list: $("#list-file"),
            $curItem: null
        };
    },
    onLoad: function () {
        $("#loading").remove();
        var me = this;
        //events
        this.ui.$list.find("li").hover(function () {
            var $me = $(this), $act = $me.data("acts");
            if (!$act) {
                $me.data("acts", $me.find(".file-acts").slideDown("fast"));
            } else {
                $act.slideDown();
            };
            $act = null;
            if ($me.hasClass("on")) return;
            if (me.ui.$curItem) {
                me.ui.$curItem.removeClass("on");
            };
            me.ui.$curItem = $me.addClass("on");
        }, function () {
            $(this).data("acts").hide();
        });
        //delete events
        this.ui.$list.find(".file-del").click(function (e) {
            var $me = $(this);
            me.DetachFile(this.rel, {
                onOK: function (msg) {
                    $me.parent().parent().remove();
                }
            });
        });
    },
    DetachFile: function (fid, opts) {
        if (!window.confirm("Are you sure to delete?")) return;

        $.ajax({
            url: this.opts.URL_DELFILE,
            type: 'POST',
            data: { tid: this.opts.RefId, fid: fid },
            success: function (msg) {
                if (msg.Error) {
                    Garfielder.AdminCommon.ShowTip(msg.Body, true);
                } else {
                    if (opts.onOK) {
                        opts.onOK(msg);
                    };
                };
            },
            error: function (xhr) {
                Garfielder.AdminCommon.ShowTip("Server hanged!Please try later!", true);
            }
        });
    } //DetachFile
});