/**
 * @author levinhuang
 * @a jquery extension method preload images
 */
;(function($) {
	var imgList = [],$img0=$("<img/>");
	$.extend({
		preload: function(imgArr, option) {
			var setting = $.extend({
				init: function(loaded, total) {},
				loaded: function(img, loaded, total) {},
				loaded_all: function(loaded, total) {},
				noCache:false /* ignore browser cache when preloading */
			}, option);
			var total = imgArr.length;
			var loaded = 0,t=setting.noCache?(new Date().getTime()+""+Math.random()):"";
			
			setting.init(0, total);
			//preload single images USING the global image object
			if(typeof(imgArr)==="string"){
				imgArr=setting.noCache?(imgArr+"?t="+t):imgArr;
				$img0.unbind().load(function(){
					setting.loaded(this,1,1);
				}).attr("src",imgArr);
				return;
			};
			//preload multiple images at the same time
			for(var i=0;i<imgArr.length;i++) {
				imgList.push($("<img />")
					.attr("src", setting.noCache?(imgArr[i]+"?t="+t):imgArr[i])
					.load(function() {
						loaded++;
						setting.loaded(this, loaded, total);
						if(loaded == total) {
							setting.loaded_all(loaded, total);
						};
					})
				);
			};//for
			
		}//preload
	});
})(jQuery);
