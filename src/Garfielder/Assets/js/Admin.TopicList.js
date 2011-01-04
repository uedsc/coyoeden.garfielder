Garfielder.AddModule("TopicList", {
    init: function (opts) {

    },
    onLoad: function () {
        var me = this;
        this.$topicIDs = $("#tb-list .cbx-topicid");
        $("cbx-toggle-topics").toggle(function () {
            me.$topicIDs.attr("checked", true);
        }, function () {
            me.$topicIDs.attr("checked",false);
        });
    }
});