/**
 * 基于元信息(data-validate属性)的通用验证模块
 * @author huangyongyou
 * @dependency jquery.js
 * @version 2011.03.06
 * @ TODO:test
 */
if(!window["jqfocus"]){
	var jqfocus={};
};
jqfocus.validator=(function($){
	var p={cache:{}},pub={};
	//private
	//默认的验证方法
	p.methods={
		//字母、数字、下划线和汉字
		alphaNumUChs:function(str){
			return /^[a-zA-Z0-9_\u4e00-\u9fa5]+$/.test(str);	
		},
		//字母、数字和下划线
		alphaNumU:function(str){
			return /^[a-zA-Z0-9_]+$/.test(str);
		},
		charLength : function(str) {
			var nstr = str.replace(/[^x00-xff]/g, "JJ");
			return nstr.length;
		},
		isBlank : function(str) {
			if (str == undefined || str == "" || str == null || /^\s*$/.test(str)) {
				return true;
			}
			return false;
		},
		isNotBlank : function(str) {
			return !this.isBlank(str);
		},
		/**
		 * 字符串是否具有指定长度范围
		 */
		require : function(str,range){
			str=$.trim(str);
			var r=range;
			if(!$.isArray(r)){
				r=[range,1];
			}
			var size=this.charLength(str);
			return (size>=r[1]&&size<=r[0]);
		},
		isUrl : function(str){
			var regx= /(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?/;
			return regx.test(str);
		},
		isMail : function(str){
			var regx= /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
			return regx.test(str);
		},
		isMobile : function(str) {
			var regx=/^(13[0-9]|15[0-9]|18[0-9])\d{8}$/;
			return regx.test(str);
		},
		isPhone : function(str) {
			var regx = /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/;
			return regx.test(str);
		}		
	};
	//错误提示信息（TODO:每个验证方法对应的提示信息）
	p.msg={};
	//默认的配置项
	p.opts={
		onOk:function(obj){
			//this will be referenced to the p.M instance
			this.data.form[obj.name]=obj.value;
			$(obj).parent().removeClass("error").addClass("valid");
		},
		onError:function(obj){
			$(obj).parent().removeClass("valid").addClass("error");
		},
		evt:'blur',
		triggerAtOnce:false //是否马上触发验证
	};
	//验证逻辑类
	p.M=function($doms,opts){
		var opts0=p.opts;
		this.opts=$.extend({},opts0,opts);
		this.doms=$doms;
		this._cache={};
		this._init();
	};
	p.M.prototype={
		_init:function(){
			this.data={
				valid:{
					_init:true
				},
				form:{}
			};
			
			this.attach();
		},
		isValid:function(){
			var yes=true;
			for(var c in this.data.valid){
				if(!c) continue;
				yes = yes && this.data.valid[c];
			}
			return yes;				
		},
		_check:function(meta,str){
			var isOk=true;
			for(var c in meta){
				if(!c) {
					continue;
				}
				if(c==="_") {
					isOk=isOk || this._check(meta[c],str);
				}else if (c==="$")
				{
					isOk=isOk && this._check(meta[c],str);
				}else {
					isOk=isOk && p.methods[c].call(p.methods,str,meta[c]);
				}
			}

			return isOk;			
		},
		attach:function(){
			var me=this;
			if(!$.isArray(this.doms)){
				this.doms=[this.doms];
			}
			$.each(this.doms,function(o,i){
				//获取验证的元数据
				var meta=$.parseJSON(o.getAttribute("data-validate"));
				//匿名函数-处理meta和o的作用域
				(function(o1,meta1){
					$(o1).bind(me.opts.evt,function(evt){
						var isOk=me._check(meta1,o1.value);
						if(isOk){
							me.opts.onOk.call(me,o1);
						}else{
							me.opts.onError.call(me,o1);
						}
						me.data.valid[o1.name]=isOk;
					});
				})(o,meta);
				//默认所有域无效
				me.data.valid[o.name]=false;
				//是否马上触发验证
				if(me.opts.triggerAtOnce){
					$(o).trigger(me.opts.evt);
				}
				//cache
				me._cache[o.name]=o;
			});
		},
		/**
		 * 返回验证失败的域
		 */
		errorFields:function(){
			var retVal=[];
			for(var c in this.data.valid){
				if(c&&!this.data.valid[c]){
					retVal.push(this._cache[c]);
				}
			}
			return retVal;
		},
		/**
		 * 将焦点移至第一个验证失败的域
		 */
		focusError:function(){
			var fields=this.errorFields(),
				focusItems=[],
				tempObj=null,
				focusObj=null,
				focusFlag=null;
			if(fields.length>0){
				for(var i=0;i<fields.length;i++){
					tempObj=fields[i];
					if(pub.isVisible(tempObj)){
						focusItems.push(tempObj);
					}else if(focusFlag=$(tempObj).attr("data-validatefocus")){
						//是否有data-validatefocus属性
						if(focusFlag==="parent"){
							focusItems.push({
								a:tempObj, //原对象
								b:$(tempObj).parent() //获取焦点的代理对象
							});
						}else{
							focusItems.push({
								a:tempObj,
								b:$(focusFlag)
							});			
						}
					}
					
				}//for
			}//if
			if(focusItems.length>0){
				tempObj=focusItems[0].a||focusItems[0];
				focusObj=focusItems[0].b||$(tempObj);
				window.scrollTo(0,focusObj.position().top-15);
				$(tempObj).trigger(this.opts.evt);
				focusObj.focus();	
			}
			tempObj=focusObj=null;
		}
	};
	
	//public
	/**
	 * 添加自定义的验证方法
	 * @param {Object} key 验证名称，如isIDCard
	 * @param {Object} fun 实现验证逻辑的函数
	 */
	pub.addMethod=function(key,fun){
		if(p.methods[key]){
			alert("方法["+key+"]已经存在");
			return;
		}
		p.methods[key]=fun;
	};
	/**
	 * 对指定的dom使用验证逻辑
	 * @param {Object} doms 待验证的dom 如T.dom.query('[data-validate]');
	 * @param {Object} opts 选项 {onOk,onError}
	 * @parem {String} id  验证实例的id，默认为singleton(目的支持多实例)
	 */
	pub.validate=function(doms,opts,id){
		id=id||'singleton';
		if(p.cache[id]){
			alert("验证器"+id+"已经存在!");
			return;
		}
		p.cache[id]=new p.M(doms,opts);
		return p.cache[id];
	};
	/**
	 * 获取指定的验证实例
	 * @param {Object} id
	 */
	pub.Get=function(id){
		id=id||'singleton';
		return p.cache[id];
	};
	/**
	 * 判断指定的dom是否是可见的
	 */
	pub.isVisible=function(obj){
		if (obj == document) return true;
	    if (!obj) return false;
	    if (!obj.parentNode) return false;
		
	    if (obj.style) {
	        if (obj.style.display == 'none') return false;
	        if (obj.style.visibility == 'hidden') return false;
	    }
	
	    //Try the computed style in a standard way
	    if (window.getComputedStyle) {
	        var style = window.getComputedStyle(obj, "");
	        if (style.display == 'none') return false;
	        if (style.visibility == 'hidden') return false;
	    }
	
	    //Or get the computed style using IE's silly proprietary way
	    var style = obj.currentStyle;
	    if (style) {
	        if (style['display'] == 'none') return false;
	        if (style['visibility'] == 'hidden') return false;
	    }
	
	    return this.isVisible(obj.parentNode);

	};	
	/**
	 * 验证方法可以单独使用，所以将它们扩展给公共接口
	 */
	pub.StrUtil=p.methods;
	return pub;
})(jQuery);
