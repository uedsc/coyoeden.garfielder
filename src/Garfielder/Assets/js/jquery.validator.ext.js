(function (V) {
	
	V.addMethod("alphaNumUChs", function (val, elm) {
		return this.optional || /^[a-zA-Z0-9_\u4e00-\u9fa5]+$/.test(val);
	}, "请输字母、数字、下划线或者汉字");

	V.addMethod("alphaNumU", function (val, elm) {
		return this.optional || /^[a-zA-Z0-9_]+$/.test(val);
	}, "请输字母、数字或下划线");

	V.addMethod("isMobileChs", function (val, elm) {
		return this.optional || /^(13[0-9]|15[0-9]|18[0-9])\d{8}$/.test(val);
	}, "请输有效的手机号");

	V.addMethod("isPhoneChs", function (val, elm) {
		return this.optional || /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/.test(val);
	}, "请输有效的电话号");

})(jQuery.validator);