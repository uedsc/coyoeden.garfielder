/**
 * stuio.index
 * @author Levin
 * @version 1.0
 */
JF.M("sindex",(function($){

	var p = {}, pub = {};

	//中间内容模块
	p.main={
		View:{
			tpl_li:'<li><a href="detail.html?%name%"><img src="%logo%" alt="%title%"/></a></li>',
			render:function(d){
				var html="";
				for(var i =0,l=d.length;i<l;i++){
					if (!d[i].isElected)
					{
						continue;
					}
					html += JF.EvalTpl(this.tpl_li,d[i]);
				}
				$("#J_content").html(html);
			}
		},
		Model:{
			albumList:null,
			init:function(){
				this.albumList = JF.studio.album;
			},
			getAlbumsElected: function(){
				var retVal = $.grep( this.albumList, function(v,i){
					return ( v.isElected === true );
				} );

				return retVal;
			},
			getAlbumsByGroup: function(groupId){
				if (!groupId)
				{
					return this.getAlbumsElected();
				}

				var retVal = $.grep(this.albumList,function(v,i){
					return ( $.inArray( groupId ,v.groups ) > -1 );
				});

				return retVal;
			}

		},
		Controller:{
			render:function(groupId){
				var d = p.main.Model.getAlbumsByGroup( groupId );
				p.main.View.render( d );
			}
		},
		init:function(){
			this.Model.init();
			this.initEvts();
		},
		initEvts:function(){
			//监听nav模块的onNavClick事件
			$(JF.studio).bind('onNavClick',function(evt,data){
				var groupId = data.rel;
				p.main.Controller.render(groupId);
			});

			//hashchange
			// Bind an event to window.onhashchange that, when the hash changes, gets the
			// hash and adds the class "selected" to any matching nav link.
			$(window).hashchange( function(){

				// parse the group id based on the hash.
				var gid = location.hash.replace( /^#/, '' ) || false;

				JF.studio.selectNav(gid);
				//load data
				p.main.Controller.render(gid);
			})

			// Since the event is only triggered when the hash changes, we need to trigger
			// the event now, to handle the hash the page may have loaded with.
			$(window).hashchange();


		}
	};


	pub.onLoad = function(){
		p.main.init();
	}

	return pub;

})(jQuery));