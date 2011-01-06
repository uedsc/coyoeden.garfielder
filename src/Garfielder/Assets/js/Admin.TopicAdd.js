Garfielder.AddModule("TopicAdd", {
    init: function (opts) {
        this.opts = opts;
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
            var $title = $("#topicTitle").preInput({ val: p._lang.lblTitle });
            if (!opts.IsNew) {
                $title.val(opts.Title).blur();
            };
            $("#newtag-topic").preInput({ val: p._lang.lblTag });
        };
        p.initAddMedia = function () {
            $("#edtbtn_img").click(function () {
                IFModal.Show(this.rel, { minH: 420 });
            });
        };

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
            p.initAddMedia();
        };

        //init
        p.initVar();
        p.initEvents();
        //preload img
        $.preload([Garfielder.SiteRoot + 'assets/img/overlay-close.png']);
    },
    onLoad: function () {
        $("#edt_hoder").tinymce({
            script_url: Garfielder.SiteRoot + 'assets/js/tiny_mce/tiny_mce.js',
            theme: 'advanced',
            skin: 'lvLuna',
            width: "100%",
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
    } //onLoad
}); 