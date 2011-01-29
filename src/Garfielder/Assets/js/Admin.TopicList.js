Garfielder.AddModule("TopicList", {
    init: function (opts) {
        this.uiForm = {
            $form0: $("#frm-search"),
            $form1: $("#frm-filter"),
            $published: $("#t-published"),
            $actType: $("#frm-filter .act-type"),
            $btnAct: $("#do-action0,#do-action1"),
            $btnFilter: $("#btnFilter"),
            $toggleTopics0: $("#cbx-toggle-topics0"),
            $toggleTopics1: $("#cbx-toggle-topics1")
        };

        this.ACTS_TOPIC = ["trash", "pub", "unpub"];

    },
    onLoad: function () {
        var me = this;
        //全选
        this.$topicIDs = $("#tb-list .cbx-topicid");
        me.uiForm.$toggleTopics0.click(function () {
            if (me.uiForm.$form1.data("all")) {
                me.$topicIDs.attr("checked", false);
                me.uiForm.$form1.data("all", false);
                me.uiForm.$toggleTopics1.attr("checked", false);
            } else {
                me.$topicIDs.attr("checked", true);
                me.uiForm.$form1.data("all", true);
                me.uiForm.$toggleTopics1.attr("checked", true);
            };
        });
        me.uiForm.$toggleTopics1.click(function () {
            me.uiForm.$toggleTopics0.trigger("click");
        });
        //全部/发布
        $("#btnShowAll,#btnShowPublished").click(function () {
            me.uiForm.$published.val(this.rel);
            me.submit("0");
        });
        //批量操作行为
        me.uiForm.$btnAct.click(function (e) {
            if (me.uiForm.$actType.val() == "-1") {
                alert("No action selected!");
                me.uiForm.$actType.focus();
                return false;
            };
            if ($.inArray(me.uiForm.$actType.val(), me.ACTS_TOPIC) && $("#tb-list .cbx-topicid:checked").size() == 0) {
                alert("No topics selected!");
                return false;
            };
            return true;
        });
        //筛选行为
        me.uiForm.$btnFilter.click(function (e) {
            me.uiForm.$actType.val("-1");
            return true;
        });

        //delete action
        $("#tb-list .act-del").click(function () {
            var $tr = $(this).parents("tr");
            me.del(this.rel, {
                onOK: function (msg) {
                    $tr.fadeOut("slow", function () {
                        $tr.remove();
                    });
                }
            });
        });
        //star action

        $("#tb-list .act-star").click(function () {
            var $i = $(this);
            if ($i.hasClass("on")) return;
            $.ajax({
                url:me.opts.url_star,
                type: 'POST',
                dataType: 'json',
                data: { id: $i[0].rel },
                success: function (msg) {
                    if (msg.Error) {
                        Garfielder.AdminCommon.ShowTip(msg.Body, true, 2000);
                    } else {
                        $i.removeClass("off").addClass("on");
                    };
                },
                error: function (xhr, status) {
                    Garfielder.AdminCommon.ShowTip("Server error!", true, 3000);
                }
            });
        });
    },
    /**
    **submit the form
    **
    **/
    submit: function (form) {
        this.uiForm["$form" + form].submit();
    },
    /**
    ** delete the specified topic
    **/
    del: function (id, opts) {
        if (!confirm("Are you sure to delete this topic?")) {
            return;
        };
        $.ajax({
            url: Garfielder.SiteRoot + "Camp/DeleteTopic",
            data: { id: id },
            type: 'POST',
            success: function (msg) {
                if (msg.Error) {
                    alert(msg.Body);
                    return;
                };
                if (opts.onOK) {
                    opts.onOK(msg);
                };
            },
            error: function (xhr, txtStatus) {
                alert("Server hanged!Please try again later!");
            }
        });
    }
});