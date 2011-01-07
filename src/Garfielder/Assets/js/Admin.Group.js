Garfielder.M("AdminGroup", {
    init: function (opts) {
        var me = this;
        //dom references
        this.ui = {
            $tpl: $("#tpl_item"),
            $tb: $("#tbItemList"),
            $toggleAll0: $("#cbx-toggle-all0"),
            $toggleAll1: $("#cbx-toggle-all1"),
            form0: {//detail form
                $title0: $("#form-title0"),
                $title1: $("#form-title1"),
                $id: $("#obj-id"),
                $name: $("#gr-name"),
                $slug: $("#gr-slug"),
                $ddlGroup: $("#ddlGroup"),
                $level: $("#gr-level"),
                $desc: $("#gr-desc"),
                $optsGroup: $("#ddlGroup").children(),
                $submit: $("#btn-submit")
            },
            form1: {//filter form
                $btnAct: $("#do-action0"),
                $actType: $("#frm-filter .act-type")
            }
        };
        //i18 resources
        this.i18 = opts.i18 || {
            lblTitle: 'Title goes here...',
            lblTag: 'Add New Tag'
        };

        //early events binding
        //add new tag
        this.ui.form0.$title0.click(function () {
            me.addView();
            me.mode = "add";
        });
        this.ui.form0.$ddlGroup.change(function () {
            var level = parseInt(me.ui.form0.$optsGroup.filter(":selected").data("level"));
            me.ui.form0.$level.val(level + 1);
        });
        //mode
        this.mode = "add";
    },
    onLoad: function () {
        var me = this;
        //toggle all checkbox
        this.ui.$toggleAll0.click(function () {
            var $ids = $("#tbItemList .cbx-objid");
            if (me.ui.$tb.data("all")) {
                $ids.attr("checked", false);
                me.ui.$tb.data("all", false);
                me.ui.$toggleAll1.attr("checked", false);
            } else {
                $ids.attr("checked", true);
                me.ui.$tb.data("all", true);
                me.ui.$toggleAll1.attr("checked", true);
            };
        });
        this.ui.$toggleAll1.click(function () {
            me.ui.$toggleAll0.trigger("click");
        });
        //delete event
        $("#tbItemList .act-del").live("click", function (e) {
            var $tr = $(this).parents("tr");
            me.Delete(this.rel, {
                onOK: function (msg) {
                    $tr.fadeOut("slow", function () {
                        $tr.remove();
                    });
                }
            });
        });
        //edit event
        $("#tbItemList .act-edit").live("click", function (e) {
            var $i=$(this), $tr = $i.parents("tr");
            $("#tbItemList tr").removeClass("cur");
            $tr.addClass("cur");
            if (this.rel != "") {
                me.editView($.evalJSON(this.rel));
            } else {
                me.editView($i.data("data"));
            };
            me.mode = "edit";
        });
        //batch operation event
        me.ui.form1.$btnAct.click(function (e) {
            if (me.ui.form1.$actType.val() == "-1") {
                alert("No action selected!");
                me.ui.form1.$actType.focus();
                return false;
            };
            if ($("#tbItemList .cbx-objid:checked").size() == 0) {
                alert("No items selected!");
                return false;
            };
            return true;
        });
    },
    OnEdit: function (d) {
        if (d.Error) {
            Garfielder.AdminCommon.ShowTip(d.Body, true, 3000); return;
        };
        //delete the old row
        $("#g-" + d.Id).remove();
        var $d = Garfielder.AdminGroup.ui.$tpl.tmpl(d), $btnEdit = $d.find(".act-edit");
        $btnEdit.data("data", d);
        Garfielder.AdminGroup.ui.$tb.find("tbody").prepend($d);
        if (!$d.next().is(".alt")) {
            $d.addClass("alt");
        };
        $d.effect("highlight", 1000);
        if (Garfielder.AdminGroup.mode == "edit") {
            $btnEdit.trigger("click");
        };
    },
    Delete: function (id, opts) {
        if (!confirm("Are you sure to delete this Group [" + id + "]?")) return;
        $.ajax({
            url: Garfielder.SiteRoot + "Camp/DeleteGroup",
            type: 'POST',
            dataType: 'json',
            data: { id: id },
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
                alert("Server Error,Please try again later or contact the site developer!");
            }
        });

    },
    addView: function () {
        this.ui.form0.$id.val("00000000-0000-0000-0000-000000000000");
        this.ui.form0.$name.val("");
        this.ui.form0.$slug.val("");
        this.ui.form0.$level.val(1);
        this.ui.form0.$ddlGroup.val(-1);
        this.ui.form0.$desc.val("");
        this.ui.form0.$title1.removeClass("form-tab-cur");
        this.ui.form0.$title0.addClass("form-tab-cur");
        this.ui.form0.$submit.val("Add new Group");
        $("#tbItemList tr").removeClass("cur");
    },
    editView: function (obj) {
        this.ui.form0.$id.val(obj.i || obj.Id);
        this.ui.form0.$name.val(obj.n || obj.Name);
        this.ui.form0.$slug.val(obj.s || obj.Slug);
        this.ui.form0.$level.val(obj.l || obj.Level);
        this.ui.form0.$ddlGroup.val(obj.p || obj.ParentID);
        this.ui.form0.$desc.val(obj.d || obj.Description);
        this.ui.form0.$title1.addClass("form-tab-cur");
        this.ui.form0.$title0.removeClass("form-tab-cur");
        this.ui.form0.$submit.val("Edit Group");
    }
});