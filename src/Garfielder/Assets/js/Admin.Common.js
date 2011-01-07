/**
* Common logic component for admin camp
*/
Garfielder.M("AdminCommon", {
    init: function (opts) {
        this._opts = opts;
        this.ui = {
            $tip: $("#scTip-dynamic")
        };
        this.timer_tip = null;
    },
    onLoad: function () {
        if (this._opts.tip) {
            this.ShowTip(null,null,4000);
        };
    },
    ShowTip: function (txt, isError, interval) {
        var me = this;
        window.clearTimeout(this.timer_tip);
        if (!this.ui.$tip.$ct) {
            this.ui.$tip.$ct = this.ui.$tip.find(".site-tip-ct");
        };
        if (txt) {
            this.ui.$tip.$ct.html(txt);
        };
        if (isError===true) {
            this.ui.$tip.$ct.addClass("site-tip-error");
        } else if(isError===false) {
            this.ui.$tip.$ct.removeClass("site-tip-error");
        };
        //effect
        this.ui.$tip.hide().slideDown("slow", function () {
            if (interval) {
                me.timer_tip = window.setTimeout(function () {
                    me.HideTip.call(me);
                }, interval);
            };
        });

    },
    HideTip: function () {
        if (this.ui.$tip.$ct)
            this.ui.$tip.$ct.html("");

        this.ui.$tip.slideUp();
    }
});