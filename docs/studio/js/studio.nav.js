/**
 * stuio main navigation module
 * @author Levin
 * @version 1.0
 */
JF.M("snav",(function($){

	var p = {},pub={};

	//导航模块
	p.nav = {
		View:{
			cl_on:'on',
			tpl_li:'<li class="nav-%name%"><a rel="%name%" href="#%name%">%name%</a></li>',
			$curNav:null,
			render: function(d){
				var html="";
				for(var i =0,l=d.length;i<l;i++){
					html += JF.EvalTpl(this.tpl_li,d[i]);
				}

				this.$layout = $("#J_nav").html(html);

				this._initEvts();
			},
			highlightNav:function(id){
				if ( id === false )
				{
					if( this.$curNav ){
						this.$curNav.removeClass(this.cl_on);
						this.$curNav = null;
					}
					return;
				}
				id = this.$layout[0].id +"_"+id;
				if(this.$curNav){
					this.$curNav.removeClass(this.cl_on);
				}
				this.$curNav = $('#'+id).addClass(this.cl_on);
			},
			_initEvts:function(){
				var view = this;
				this.$layout.find('a').click(function(e){

					var $this = $(this);
					if( $this.hasClass(view.cl_on) ){
						return;
					}
					view.highlightNav(this.rel);
					/* we rely on the hash change!
					$(JF.snav).trigger('onNavClick',{
						"rel":this.rel
					});
					*/

				}).each(function(o,i){
					if(!this.id){
						this.id = view.$layout[0].id+'_'+this.rel;
					}
				});
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
	//首页公共事件初始化
	p.initEvts = function(){

		$(JF.snav).bind('onNavClick',JF.emptyFun);
	};


	pub.selectNav = function(id){
		p.nav.View.highlightNav(id);
	};

	pub.onLoad = function(){
		p.nav.init();
	}

	return pub;

})(jQuery));