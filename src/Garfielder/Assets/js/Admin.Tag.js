Garfielder.M("AdminTag", {
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
                $name: $("#obj-name"),
                $slug: $("#obj-slug"),
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
            var $i = $(this);
            var $tr = $i.parents("tr");
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
        //keyword this is referenced to the ajax xmlhttprequest object!
        if (d.Error) {
            Garfielder.AdminCommon.ShowTip(d.Body, true, 3000); return;
        };
        //delete the old row
        $("#tag-" + d.Id).remove();
        var $d = Garfielder.AdminTag.ui.$tpl.tmpl(d), $btnEdit = $d.find(".act-edit");
        $btnEdit.data("data", d);
        Garfielder.AdminTag.ui.$tb.find("tbody").prepend($d);
        if (!$d.next().is(".alt")) {
            $d.addClass("alt");
        };
        $d.effect("highlight", 1000);
        if (Garfielder.AdminTag.mode == "edit") {
            $btnEdit.trigger("click");
        };
    },
    Delete: function (id, opts) {
        if (!confirm("Are you sure to delete this tag [" + id + "]?")) return;
        $.ajax({
            url: Garfielder.SiteRoot + "Camp/DeleteTag",
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
        this.ui.form0.$title1.removeClass("form-tab-cur");
        this.ui.form0.$title0.addClass("form-tab-cur");
        this.ui.form0.$submit.val("Add new Tag");
        $("#tbItemList tr").removeClass("cur");
    },
    editView: function (obj) {
        this.ui.form0.$id.val(obj.i || obj.Id);
        this.ui.form0.$name.val(obj.n || obj.Name);
        this.ui.form0.$slug.val(obj.s || obj.Slug);
        this.ui.form0.$title1.addClass("form-tab-cur");
        this.ui.form0.$title0.removeClass("form-tab-cur");
        this.ui.form0.$submit.val("Edit Tag");
    }
});