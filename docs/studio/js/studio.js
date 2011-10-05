/**
 * stuio base module
 * @author Levin
 * @version 1.0
 */
JF.M("studio",(function($){

	var p = {},pub={};
	pub.init = function(){
		var isIE6 = ($.browser.msie && $.browser.version <7);
		if(isIE6){
			$('#site-tip').slideDown().click(function(){
				$(this).slideUp();
			}).delay(5000).slideUp();
		}
	};
	return pub;

})(jQuery));