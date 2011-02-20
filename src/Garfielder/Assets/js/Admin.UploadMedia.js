/**
* JS logic for adding media files
*/
Garfielder.M("UploadMedia", {
	init: function (opts) {
		this.opts = opts;
		this.ui = {
			$items: $("#media-items"),
			$curItem: null,
			$sizes: $("#media-sizes"),
			$curSizes: null,
			$tplSize: $("#tpl-media-size"),
			$insert: $("#btn-insert"),
			$trLogo: null
		};
		this.data = {};
		this.data.url_topicLogo = parent.Garfielder.Cfg.URL_LOGO;

	}, //init
	onLoad: function () {
		var me = this, prefix = "media-size-";
		if (this.opts.noFlash) {
			this.ui.$items.css("opacity", 0.3).animate({ opacity: 1 }, 1500);
		}; //if

		//insert img into the post
		/* tab-from computer */
		me.ui.$insert.click(function (e) {
			if (!me.ui.$curItem) {
				alert("Nothing selected!Please select a media file firstly!");
				return;
			};
			var $radio = me.ui.$curSizes.find("input:checked");
			me.insert({
				src: $radio.val(),
				w: $radio.attr("data-w"),
				h: $radio.attr("data-h")
			});
			$radio = null;
		});
		$("#media-items .media-item").live("click", function (e) {
			var $temp = $(this), meta, id = this.getAttribute("data-id");
			//item has beed selected
			if ($temp.hasClass("on")) return;
			//de-select last item
			if (me.ui.$curItem) {
				me.ui.$curItem.removeClass("on");
			};
			//set current item
			me.ui.$curItem = $temp.addClass("on");
			//show sizes
			if (!$temp.data("meta-init")) {
				meta = $temp.data("meta");
				// field id
				$.each(meta.Thumbs, function (i0, o0) {
					o0.RefId = id;
				});
				$temp.data("meta-init", true);
				me.ui.$sizes.children().hide().end()
                    .append(
                        $('<div id="' + prefix + id + '" class="media-size-choice"/>')
                            .append(me.ui.$tplSize.tmpl(meta.Thumbs))
                            .find("input:eq(0)").attr("checked", true)
                            .end()
                    );
			} else {
				me.ui.$sizes.children().hide().filter("#" + prefix + id).fadeIn();
			};
			//set current sizes
			me.ui.$curSizes = $("#" + prefix + id);
		});

		/* tab-from lib */
		var getSelectedItem = function ($tr) {
			var $sizes, $opt;
			if (!($sizes = $tr.data("$sizes"))) {
				$tr.data("$sizes", ($sizes = $tr.find(".file-meta-sizes")));
			};
			$opt = $sizes.find("option:selected");
			return {
				src: $opt.val(),
				w: $opt.data("w"),
				h: $opt.data("h")
			};
		};
		$("#mediaList-main .btn-insert").click(function (e) {
			var item = getSelectedItem($(this).parent().parent());
			me.insert(item);
		});
		$("#mediaList-main .btn-attach").click(function (e) {
			var i = this;
			$.ajax({
				url: me.opts.url_attach,
				type: 'POST',
				dataType: 'json',
				data: { fid: i.rel, tid: me.opts.refId },
				success: function (msg) {
					if (msg.Error) {
						parent.Garfielder.AdminCommon.ShowTip(msg.Body, true);
					} else {
						parent.Garfielder.AdminCommon.ShowTip("Operation done!", false, 2000);
						$(i).fadeOut("normal", function () {
							$(this).remove();
						});
					};
				},
				error: function (xhr, txtStatus) {
					parent.Garfielder.AdminCommon.ShowTip("Server Error,Please try again later or contact the site developer!", true);
				}
			});
		});

		//btn-logo
		var isLogo = function (img) {
			if (me.data.url_topicLogo === "") return false;
			return (img.indexOf(me.data.url_topicLogo) > -1);
		};

		var $btnLogo = $("#mediaList-main .btn-logo").click(function () {
			var i = this, item = getSelectedItem($(this).parent().parent());
			//ajax
			$.ajax({
				url: me.opts.url_setlogo,
				type: 'POST',
				dataType: 'json',
				data: { img: item.src, tid: me.opts.refId },
				success: function (msg) {
					if (msg.Error) {
						parent.Garfielder.AdminCommon.ShowTip(msg.Body, true);
					} else {
						parent.Garfielder.AdminCommon.ShowTip("Operation done!", false, 2000);
						if (me.ui.$trLogo) {
							me.ui.$trLogo.removeClass("islogo");
						};
						me.ui.$trLogo=$(i).parents("tr").addClass("islogo");
						parent.Garfielder.Cfg.URL_LOGO = item.src;
					};
				},
				error: function (xhr, txtStatus) {
					parent.Garfielder.AdminCommon.ShowTip("Server Error,Please try again later or contact the site developer!", true);
				}
			});
		});

		$("#mediaList-main .file-meta-sizes").change(function () {
			var $me = $(this), $tr = $me.parents("tr").removeClass("islogo");
			var img = $me.find("option:selected").val();
			if (isLogo(img)) {
				me.ui.$trLogo=$tr.addClass("islogo");
			};
			//dispose
			$tr = null;
		}).trigger("change");



	}, //onload
	/**
	* insert a pic into the topic
	* @param {object} d pic data '{src:"xxx",w:"yyy",h:"zzz"}'
	*/
	insert: function (d) {
		var html = '<img src="' + d.src + '" width="' + d.w + '" height="' + d.h + '" alt=""/>';
		parent.Garfielder.TopicAdd.InsertHTML(html);
	}
});