Garfielder.M("TopicAdd", {
    init: function (opts) {
        var me = this;
        this.opts = opts;
        this.ui = {
            $title: $("#topicTitle"),
            slug: {
                $d: $("#ste_slug"),
                $input: $("#ste_slug_auto"),
                $lbl: $("#ste_slug_auto_holder"),
                $loading: $("#loading-slug")
            },
            $tabs: $("#edt_toolbar .richedt-tab"),
            $tabCT: $("#ste_richedt .richedt-tab-ct"),
            $ifFiles: $("#ifTopicFiles")

        };
        var p = {};
        p.initTabs = function () {
            var hd = $("#group-tabs li"), bd = $("#grSelector .slimtab_bd");
            $("#group-tabs a").click(function (e) {
                var $i = $(this), $li = $i.parent();
                if ($li.hasClass("slimtab_on")) return false;
                bd.hide().filter($i.attr("href")).fadeIn();
                hd.removeClass();
                $li.addClass("slimtab_on");
                return false;
            });
        };
        p.initPreInput = function () {
            me.ui.$title.preInput({ val: p._lang.lblTitle });
            if (!opts.IsNew) {
                me.ui.$title.val(opts.Title).blur();
            };
            $("#newtag-topic").preInput({ val: p._lang.lblTag });
        };
        p.initAutoSlug = function () {
            if (opts.IsNew) {
                //title input
                Garfielder.AutoSlug.Slug(me.ui.$title, {
                    data: {
                        type: 'topic'
                    },
                    begin: function () {
                        //invalid title
                        if (me.ui.$title.hasClass("ipt_default")) return false;
                        //slug has been generated
                        if ($.trim(me.ui.slug.$input.val()) !== "") return false;
                        me.ui.slug.$loading.css("visibility", "visible");
                        return true;
                    },
                    success: function (d) {
                        me.ui.slug.$loading.css("visibility", "hidden");
                        me.ui.slug.$lbl.html(d.slug);
                        me.ui.slug.$input.val(d.slug).width(me.ui.slug.$lbl.width() + 10);
                        me.ui.slug.$d.fadeIn();
                    }
                });
            };
            //slug input 
            Garfielder.AutoSlug.Slug(me.ui.slug.$input, {
                data: {
                    type: 'topic'
                },
                begin: function () {
                    me.ui.slug.$loading.css("visibility", "visible");
                    return true;
                },
                success: function (d) {
                    me.ui.slug.$loading.css("visibility", "hidden");
                    me.ui.slug.$lbl.html(d.slug);
                    me.ui.slug.$input.val(d.slug).width(me.ui.slug.$lbl.width() + 10);
                }
            });
            //edit slug input directly
            me.ui.slug.$input.keyup(function (e) {
                me.ui.slug.$lbl.html(this.value);
                me.ui.slug.$input.width(me.ui.slug.$lbl.width() + 10);
            });

        }; //initAutoSlug
        p.initAddMedia = function () {
            $("#edtbtn_img").click(function () {
                IFModal.Show(this.rel, { minH: 420 });
            });
        }; //initAddMedia

        //private area
        p.initVar = function () {
            p._lang = opts.lang || {
                lblTitle: 'Title goes here...',
                lblTag: 'Add New Tag'
            };
        };
        p.initEvents = function () {
            p.initTabs();
            p.initPreInput();
            p.initAutoSlug();
            p.initAddMedia();

            //editor tab-bar
            me.ui.$tabs.click(function (e) {
                me.ui.$tabs.removeClass("edtbtn_on").filter(this).addClass("edtbtn_on");
                me.ui.$tabCT.hide().filter("#" + this.rel).fadeIn();
                if (this.rel == "topic-files") {
                    me.ui.$ifFiles.attr("src", me.opts.URL_FILES + "/" + me.opts.ObjID + "?t=" + new Date().getTime());
                };

            });
            //auto frame height
            me.ui.$ifFiles.load(function (e) {
                if (this.contentDocument) {
                    this.height = this.contentDocument.body.offsetHeight + 35;
                } else {
                    this.height = this.contentWindow.document.body.scrollHeight;
                };
            });
        };

        //init
        p.initVar();
        p.initEvents();
        //preload img
        $.preload([Garfielder.SiteRoot + 'assets/img/overlay-close.png']);
    },
    onLoad: function () {
        this.ui.$editor = $("#edt_hoder").tinymce({
            script_url: Garfielder.SiteRoot + 'assets/js/tiny_mce/tiny_mce.js',
            theme: 'advanced',
            skin: 'lvLuna',
            width: "100%",
            relative_urls: false,
            plugins: "safari,inlinepopups,spellchecker,paste,media,fullscreen,tabfocus",
            // Theme options
            theme_advanced_buttons1: "bold,italic,strikethrough,|,bullist,numlist,blockquote,|,justifyleft,justifycenter,justifyright,|,link,unlink,|,fullscreen",
            theme_advanced_buttons2: "styleselect,formatselect,underline,justifyfull,forecolor,|,pastetext,pasteword,removeformat,|,media,charmap,|,outdent,indent,|,undo,redo",
            theme_advanced_buttons3: "",
            theme_advanced_buttons4: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,
            content_css: Garfielder.SiteRoot + 'assets/css/editor.css',
            theme_advanced_resize_horizontal: "",
            dialog_type: "modal"
        });
    }, //onLoad
    Editor: function () {
        return this.ui.$editor.tinymce();
    }, //editor
    InsertHTML: function (html) {
        this.Editor().execCommand('mceInsertContent', false, html);
    } //inserhtml
}); 