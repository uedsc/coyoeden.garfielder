<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMCampTopicEdit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditTopic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 id="scSubhead">Add a new Topic</h2>
	<% using (Html.BeginForm()) {%>
	<div id="ste_root" class="metabox rsb clear">
		<!--#ste_sidebar-->
		<div id="ste_sidebar" class="rsb_sb">
			<div id="ste_sidebar_">
				<div id="boxsubmit" class="widget">
					<div class="w_tgl" title="click to toggle"></div>
					<h3 class="w_hd"><span>Publish</span></h3>
					<div class="w_ct">
						<div id="act_publish">
							<input id="btnPublish" class="btn btn_g1" type="submit" value="Publish"/>
						</div>
					</div>
				</div>
				<div id="boxGroup" class="widget">
					<div class="w_tgl" title="click to toggle"></div>
					<h3 class="w_hd"><span>Groups</span></h3>
					<div class="w_ct">
						<div id="catSelector">
							<ul id="group-tabs" class="slimtab">
								<li class="slimtab_on"><a href="#group-all">All groups</a></li>
								<li class=""><a href="#group-pop">Most used</a></li>
							</ul>
							<div id="group-all" class="slimtab_bd">
								<ul id="cbxlist-cat-all" class="cbxlist">
									<li><input id="cat-1" type="checkbox" value="1"/><label for="cat-1">Group1</label></li>
									<li><input id="cat-2" type="checkbox" value="2"/><label for="cat-2">Group2</label></li>
									<li><input id="cat-3" type="checkbox" value="3"/><label for="cat-3">Group3</label></li>
								</ul>
							</div>
							<div id="group-pop" class="slimtab_bd hide">
								<ul id="cbxlist-cat-pop" class="cbxlist">
									<li><input id="cat-3" type="checkbox" value="3"/><label for="cat-3">Group3</label></li>
									<li><input id="cat-2" type="checkbox" value="2"/><label for="cat-2">Group2</label></li>
									<li><input id="cat-1" type="checkbox" value="1"/><label for="cat-1">Group1</label></li>
								</ul>												
							</div>
						</div>
					</div>
				</div>
				<!--/#boxGroup-->
				<!--boxTag-->
				<div id="boxTag" class="widget">
					<div class="w_tgl" title="click to toggle"></div>
					<h3 class="w_hd"><span>Topic tags</span></h3>
					<div class="w_ct">
						<div id="tagAdder">
							<div id="tagAdder_">
								<p>
									<input type="text" value="" size="16" class="newtag form-input-tip" name="newtag_topic" id="newtag-topic"/>
									<input type="button" tabindex="3" value="Add" class="btn btnAddTag"/>
								</p>														
							</div>
							<p class="howto">Separate tags with commas</p>
						</div>
						<p id="tagSelector">
							<a href="#">Choose from most used tags</a>
						</p>
					</div>
				</div>
			</div>
		</div>
		<!--#ste_main-->
		<div id="ste_main" class="rsb_ct">
			<div id="ste_main_" class="rsb_ct_">
				<div id="ste_title">
					<div id="ste_title_">
						<input id="topicTitle" class="bigipt bd0" type="text" value="" tabindex="1" size="30" name="Title"/>
					</div>
					<div id="ste_slug">
						<div id="ste_slug_">
							<!--
							<strong>Permanent link</strong>
							<span id="ste_slug_auto"></span>
							-->
						</div>
					</div>
				</div>
				<div id="ste_richedt" class="richedt bd0">
					<div id="edt_toolbar" class="clear">
						<a id="edtbtn_html" class="bd0" href="#">HTML</a>
						<a id="edtbtn_preview" class="edtbtn_on bd0" href="#">Preview</a>
						<div id="edtbtn_others">
							<span>Upload / Insert</span>
							<a id="edtbtn_img" class="thickbox" href="javascript://" rel="<%:Url.Action("UploadMedia","Camp",null) %>"><img alt="" src="<%:Url.Img("media-button-image.gif")%>"/></a>
						</div>
					</div>
					<div id="edt_main" class="bd0">
						<textarea id="edt_hoder" class="edt_holder" tabindex="2" name="ContentX" cols="40" rows="20"></textarea>
					</div>
				</div>
			</div>
		</div><!--#ste_main-->
	</div><!--#ste_root-->	
	<% } %>

	<!--overlay ui-->
	<div id="ovl-ifr-wrap" class="ovl-apple">
		<a class="ovl-close"></a>
		<div class="ovl-ct">
			<div id="ovl-ing" class="ovl-ing"><img src="<%:Url.Img("ovl-ing.gif") %>" alt="Loading..." /></div>
			<iframe id="ovl-ifr" class="ovl-ifr" frameborder="0" scrolling="no" style="display:none;height:100%;width:100%;"></iframe>
		</div>
	</div>
	<!--/overlay ui-->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
	<link rel="stylesheet" type="text/css" href="<%:Url.CSS("overlay-apple") %>"></link> 
	<%this.Model.PageFlag = "topic_add"; this.Context.Request.MapPath("/");%> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
	<!--scripts-->
	<script type="text/javascript" src="http://cdn.jquerytools.org/1.2.5/tiny/jquery.tools.min.js"></script>
	<script type="text/javascript" src="<%:Url.JS("jquery.vsUtils") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("jquery.vPreload")%>"></script>
	<script type="text/javascript" src="<%:Url.JS("tiny_mce/jquery.tinymce") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("Garfielder.IFModal") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("Admin.TopicAdd") %>"></script>
	<!--/scripts-->		
</asp:Content>