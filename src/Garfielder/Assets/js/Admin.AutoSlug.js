Garfielder.M("AutoSlug", {
    init: function (opts) {
        this.url = opts.URL_AUTOSLUG;
    },
    /**
    * Make the specified input elements sluggable...
    * @param {string||JQuery dom} target
    * @param {Object} opts
    */
    Slug: function (target, opts) {
        var me = this;
        opts = $.extend({
            evt: 'blur',
            begin: function () { return true; },
            error: function () { },
            success: function () { },
            data: null
        }, opts || {});

        if (typeof (target) === "string") {
            target = $(cssSelector);
        };

        target.each(function (i, o) {
            o = $(o);
            o.bind(opts.evt, function (e) {
                var val = $.trim(this.value);
                if (val == "") return;
                var goOn = true;
                if (opts.begin) {
                    var goOn = opts.begin($(this));
                };
                if (!goOn) return;
                //get slug via ajax
                $.getJSON(me.url, $.extend({ src: this.value }, opts.data || {}), function (d) {
                    if (opts.success) {
                        opts.success(d);
                    };
                });
            });
        });
    } //slug
});