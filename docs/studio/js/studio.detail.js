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
			tpl_pageNav:'<li><a href="#%idx0%">[%idx%]</a></li>',
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
					p.goback();
					return false;
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
							idx0:i,
							idx:(i+1)
						});
					}
					view.$pager.empty().html(pagerHtml).fadeIn();
				}

				//render other info
				view.$desc.html(album.desc).fadeIn();
			}
		}
	};

	p.gallery = {
		View:{},
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
						$.extend(this,d);
					}
				}
			}
		},
		Controller:{
			init:function(){
				if (p.gallery.Model.isEmpty)
				{
					p.goback();
					return;
				}
				p.info.View.init();
				p.info.Controller.render(p.gallery.Model);
				this.render();
			},
			render:function(){

			}
		}
	};

	//share data
	p.data={};

	p.goback = function(){
		try{
			window.history.back();
		}catch(e){
			window.location.href = 'index.html';
		}
	};

	pub.onLoad = function(){
		//get albumName
		p.data.albumName = document.location.search.replace('?','');
		if (p.data.albumName.length==0)
		{
			p.goback();
			return;
		}

		p.gallery.Model.init(p.data.albumName);
		p.gallery.Controller.init();
	};

	return pub;

})(jQuery));