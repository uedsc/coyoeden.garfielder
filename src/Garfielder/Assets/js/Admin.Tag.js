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
                $slug: $("#obj-slug")
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
        this.ui.form0.$id.val("");
        this.ui.form0.$title1.removeClass("form-tab-cur");
        this.ui.form0.$title0.addClass("form-tab-cur");
    },
    editView: function (obj) {
        this.ui.form0.$id.val(obj.i);
        this.ui.form0.$name.val(obj.n);
        this.ui.form0.$slug.val(obj.s);
        this.ui.form0.$title1.addClass("form-tab-cur");
        this.ui.form0.$title0.removeClass("form-tab-cur");
    }
});