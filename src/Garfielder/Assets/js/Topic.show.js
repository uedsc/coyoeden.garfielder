Garfielder.M("TopicShow", (function () {
	return {
		init: function (opts) {
			this.opts = opts;
		},
		onLoad: function () {
			var $items = $("#gallery .gallery-item"), $curMenu = null;
			//page tabs
			var $menus = $("#pager a").each(function (i, o) {
				(function (i1, $o) {
					if (i1 == 0) {
						$curMenu = $o;
					};
					$o.click(function () {
						if ($o.hasClass("on")) return false;
						if ($curMenu) {
							$curMenu.removeClass("on");
						};
						$curMenu = $o.addClass("on");
						$items.hide().eq(i1).fadeIn();
						return false;
					});
				})(i, $(o));
			});
			// items click event
			if ($items.length > 1) {
				$items.click(function () {
					var idx = $items.index(this);
					if (idx == ($items.length - 1)) {
						idx = 0;
					} else {
						idx = idx + 1;
					};
					$menus.eq(idx).trigger("click");
				});
			};
		} //onload
	};
} ()));