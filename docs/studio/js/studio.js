/**
 * stuio公共模块
 * @author Levin
 * @version 1.0
 */
JF.M("studio",(function($){

	var p = {},pub={};

	//导航模块
	p.nav = {
		View:{
			tpl_li:'<li id="%name%"><a href="#%name%">%name%</a></li>',
			render: function(d){
				var html="";
				for(var i =0,l=d.length;i<l;i++){
					html += JF.EvalTpl(this.tpl_li,d[i]);
				}
				$("#J_nav").html(html);
			}
		},
		Model:{
			navList:null,
			init:function(){
				this.navList = JF.studio.group;
			}
		},
		Controller:{
			render:function(){
				p.nav.View.render(p.nav.Model.navList);
			}
		},
		init:function(){
			this.Model.init();
			this.Controller.render();
		}
	};

	pub.onLoad = function(){
		p.nav.init();
	}

	return pub;

})(jQuery));