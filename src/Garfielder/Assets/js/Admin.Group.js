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
                $id: $("#gr-id"),
                $name: $("#gr-name"),
                $slug: $("#gr-slug"),
                $ddlGroup: $("#ddlGroup"),
                $level: $("#gr-level"),
                $desc: $("#gr-desc"),
                $optsGroup: this.$ddlGroup.children()
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
        });
        this.ui.form0.$ddlGroup.change(function () {
            var level = parseInt(me.ui.form0.$optsGroup.filter(":selected").data("level"));
            me.ui.form0.$level.val(level + 1);
        });
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
            var $tr = $(this).parents("tr");
            $("#tbItemList tr").removeClass("cur");
            $tr.addClass("cur");
            me.editView($.evalJSON(this.rel));
        });
    },
    OnEdit: function (d) {
        if (!d) {
            alert("Error!Please try again later!"); return;
        };
        var $d = this.ui.$tpl.tmpl(d);
        this.ui.$tb.find("tbody").append($d);
        if (!$d.prev().is(".alt")) {
            $d.addClass("alt");
        };
        $d.effect("highlight", 1000);
    },
    Delete: function (id, opts) {
        if (!confirm("Are you sure to delete this tag [" + id + "]?")) return;
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
        this.ui.form0.$id.val("");
        this.ui.form0.$title1[0].className = "";
        this.ui.form0.$title0[0].className = "cur";
    },
    editView: function (obj) {
        this.ui.form0.$id.val(obj.i);
        this.ui.form0.$name.val(obj.n);
        this.ui.form0.$slug.val(obj.s);
        this.ui.form0.$level.val(obj.l);
        this.ui.form0.$ddlGroup.val(obj.p);
        this.ui.form0.$desc.val(obj.d);
        this.ui.form0.$title1[0].className = "cur";
        this.ui.form0.$title0[0].className = "";
    }
});