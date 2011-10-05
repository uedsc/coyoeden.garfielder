/**
 * Logic code for detail.html
 * @author Levin
 * @version 1.0
 */
JF.M("sdetail",(function($){

	var p = {},pub = {};

	p.info = {
		View:{
			tpl_title:"Lingzhi's Emotion Studio - %title%",
			tpl_pageNav:'<li><a href="#%idx0%" rel="%idx0%">[%idx%]</a></li>',
			cl_pageOn:'on',
			$title:null,
			$desc:null,
			$back:null,
			$pager:null,
			init:function(){
				this.$title = $("#J_title");
				this.$desc = $("#J_desc");
				this.$back = $("#J_btn_back");
				this.$pager = $("#J_pager");

				this.initEvts();
			},
			initEvts:function(){
				this.$back.click(function(){
					p.history.goback();
					return false;
				});

				//pager
				$("#J_pager a").live("click",function(e){
					if ( $(this).hasClass(p.info.View.cl_pageOn) )
					{
						return false;
					}
					//p.gallery.Controller.showPic(this.rel);
				});
			}
		},
		Controller:{
			render:function(album){
				var view = p.info.View;
				//render title
				document.title = JF.EvalTpl(view.tpl_title,{
					title:album.title
				});
				view.$title.html(album.title);
				//render pager
				var cntPic = album.pics.length,
					pagerHtml= "";
				if (cntPic<2)
				{
					view.$pager.empty().hide();
				}else{

					for (var i=0; i<cntPic; i++)
					{
						pagerHtml += JF.EvalTpl(view.tpl_pageNav,{
							idx0:i+'',
							idx:(i+1)+''
						});
					}
					view.$pager.empty().html(pagerHtml).fadeIn();
				}

				//render other info
				view.$desc.html(album.desc).fadeIn();
			},
			highlightPage:function(idx){
				idx = parseInt(idx);
				var view = p.info.View;
				$("#J_pager a").removeClass(view.cl_pageOn).eq(idx).addClass(view.cl_pageOn);
			},
			pageTo:function(idx){
				if ( (idx>=p.gallery.Model.cntPic) || (idx<0) )
				{
					idx = 0;
				}
				window.location.hash = idx+'';
			},
			pagePrev:function(){
				this.pageTo(p.gallery.Controller.curIdx-1);
			},
			pageNext:function(){
				this.pageTo(p.gallery.Controller.curIdx+1);
			}
		}
	};

	p.gallery = {
		View:{
			cl_item:'gallery-item',
			tpl_item:'<div id="pitem-%idx%" class="%clItem%%clXtra%">'+
						'<img  alt="%albumName%"  src="%src%"/>'+
					 '</div>',
			tpl_noresult:'<div class="no_result">Ooh!! There is no pics in the album [<strong>%albumName%</strong>]...</div>',
			$gallery:null,
			init:function(){
				this.$gallery = $("#J_gallery");
				this.initEvts();
			},
			initEvts:function(){
				$("#J_gallery "+"."+this.cl_item).live('click',function(){
					var idx = $(this).index();
					p.info.Controller.pageTo(idx+1);
				});
			}
		},
		Model:{
			isEmpty:true,
			init:function(albumName){
				var d = null;
				for (var i=0,j=JF.studio.album.length; i<j; i++)
				{
					d = JF.studio.album[i];
					if (d.name === albumName)
					{
						this.isEmpty = false;
						this.cntPic = d.pics.length;
						$.extend(this,d);
					}
				}
			}
		},
		Controller:{
			curIdx:0,
			init:function(){
				if (p.gallery.Model.isEmpty)
				{
					p.history.goback();
					return;
				}
				p.info.View.init();
				p.info.Controller.render(p.gallery.Model);
				p.gallery.View.init();
				this.render();
			},
			render:function(){
				var mod = p.gallery.Model,
					view = p.gallery.View,
					html = "";
				//no pics
				if (mod.cntPic == 0)
				{
					html = JF.EvalTpl( view.tpl_noresult,{
						'albumName':mod.name
					} );
					view.$gallery.html( html );
					return;
				}
				//has pics
				var pic = null;
				for (var i = 0; i<mod.cntPic; i++)
				{
					pic = mod.pics[i];
					pic.albumName = mod.name;
					pic.idx = i+'';
					pic.clXtra = '';
					pic.clItem = view.cl_item;
					if (i>0)
					{
						pic.clXtra = ' hide';
					};
					html += JF.EvalTpl( view.tpl_item ,pic);
				}
				view.$gallery.html( html );
			},
			showPic:function(idx){
				if (p.gallery.Model.cntPic == 0)
				{
					return;
				}
				this.curIdx = idx = parseInt(idx);
				var view = p.gallery.View;
				view.$gallery.find('.'+view.cl_item).hide().eq(idx).fadeIn();
				//pager
				p.info.Controller.highlightPage(idx);
			}
		}
	};

	//share data
	p.data={};

	p.history = {
		idx:0,
		rawLen:0,
		_parseHash:function(){
			this.idx = document.location.hash.replace( /^#/, '' );
			if (this.idx.length == 0)
			{
				this.idx = 0;
			}else{
				try{
					this.idx = parseInt(this.idx);
				}catch(e){
					this.idx = 0;
				}
			}
			if ( (this.idx>=p.gallery.Model.cntPic) || (this.idx<0) )
			{
				this.idx = 0;
			}
		},
		_initEvts:function(){
			//hashchange
			// Bind an event to window.onhashchange that, when the hash changes, gets the hash
			$(window).hashchange( function(){

				// parse the pic id based on the hash.
				p.history._parseHash();

				p.gallery.Controller.showPic( p.history.idx );

			})

			// Since the event is only triggered when the hash changes, we need to trigger
			// the event now, to handle the hash the page may have loaded with.
			$(window).hashchange();
		},
		init:function(){
			this.rawLen = window.history.length;
			this._parseHash();
			this._initEvts();
		},
		goback:function(){

			if (this.rawLen==0)
			{
				window.location.href = 'index.html';
				return;
			}

			var curLen = window.history.length,
				step = curLen -this.rawLen;

			try{
				window.history.go(-(step+1));
			}catch(e){
				window.location.href = 'index.html';
			}
		}
	};

	p.keyboard = {
		init:function(){
			$(document).keydown(function(e) {

				var key = 0;
				if (e == null) {
					key = event.keyCode;
				} else { // mozilla
					key = e.which;
				}
				switch(key) {
					case 37://left
						p.info.Controller.pagePrev();
						break;
					case 38://up
						break;
					case 39://right
						p.info.Controller.pageNext();
						break;
					case 40://down
						break;
					case 13://enter
						p.info.Controller.pageNext();
						break;
				}
			});
		}
	};

	pub.onLoad = function(){
		//get albumName
		p.data.albumName = document.location.search.replace('?','');
		if (p.data.albumName.length==0)
		{
			p.history.goback();
			return;
		}

		p.gallery.Model.init(p.data.albumName);
		p.gallery.Controller.init();
		p.history.init();
		p.keyboard.init();
	};

	return pub;

})(jQuery));