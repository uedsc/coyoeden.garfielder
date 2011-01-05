/**
 * @author Levin
 */
var this$ = (function ($) {
    var p = {}, pub = {};
    //private area
    p.initVar = function (opts) {
        p._lang = opts.lang || {
            lblTitle: 'Title goes here...',
            lblTag: 'Add New Tag'
        };
        p._$tpl = $("#tpl_group");
        p._$tb = $("#tbGroupList");
        p._$ddlGroup = $("#ddlGroup");
        p._$opts = p._$ddlGroup.children();
        p._$level = $("#gr-level");
        p._model = {};
    };
    p.onLoaded = function () {

    };
    p.initEvents = function (opts) {
        $(document).ready(p.onLoaded);
        $("#frmEdit").link(p._model);
        p._$ddlGroup.change(function () {
            var level = parseInt(p._$opts.filter(":selected").data("level"));
            p._$level.val(level + 1);
        });
    };
    //public area
    pub.Init = function (opts) {
        p.initVar(opts);
        p.initEvents(opts);
    };
    pub.OnEdit = function (d) {
        if (!d) {
            alert("error!pls try again!"); return;
        };
        var $d = p._$tpl.tmpl(d);
        p._$tb.find("tbody").append($d);
        if (!$d.prev().is(".alt")) {
            $d.addClass("alt");
        };
        $d.effect("highlight",1000);
    };
    return pub;
})(jQuery); 