<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMCampTopicShow>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Topic-<%:Model.IsNew?"Create":"Update" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 id="scSubhead"><%:Model.IsNew?"Add a new Topic":"Edit topic" %></h2>
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
					<div class="w_ct"><%Html.RenderPartial("ET_GroupSelector"); %></div>
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
									<input type="button" tabindex="3" value="Add" class="btn btnAddTag" id="btnAddTag" />
								</p>														
							</div>
							<p class="howto">Separate tags with commas</p>
                            <%if (Model.IsNew){%>
							<p id="tagsAdded" style="display:none"></p>
							<%}else{%>
                            <p id="tagsAdded">
                                <%for (var i = 0; i < Model.Tags.Count; i++){%>
                                <span class="tag"><input type="hidden" value="<%:Model.Tags[i].Id %>" name="TagID"/><a href="javascript://" class="delTag" rel="<%:Model.Tags[i].Id %>">X</a><%:Model.Tags[i].Name %></span>
                                <%}%>
                            </p>
                            <%}%>
                            <p id="tagTip" class="error" style="display:none"></p>
						</div>
						<p id="tagSelector">
							<a href="javascript://" id="btnTagSelect">Choose from most used tags!</a>
							<p class="the-tagcloud" id="tagcloud-topic">
							<%if (Model.TagsAll == null || Model.TagsAll.Count == 0)
							{ %>
							 No tags found!
							<%}
							else
							{%>
							<%var tags=Model.TagsAllHot.Take(10).ToList(); %>
							 <%for (var i = 0; i < tags.Count; i++)
							   {%>
							   <a class="tagitem" href="javascript://" rel="<%:tags[i].Id %>"><%:tags[i].Name %></a>
							 <%} %>   
							<%} %>
							</p>
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
                        <input id="topicId" name="Id" type="hidden" value="<%:Model.Id %>" />
						<input id="topicTitle" class="bigipt bd0" type="text" value="" tabindex="1" size="30" name="Title"/>
					</div>
					<div id="ste_slug">
						<div id="ste_slug_">
							<strong>Permanent link</strong>
                            <span id="loading-slug" class="loading-16" style="visibility:hidden"><span></span></span>
                            <em><%:Url.Action("View","Topic") %>/</em>
							<input id="ste_slug_auto" name="Slug" value="<%:Model.Slug %>"/>
                            <span id="ste_slug_auto_holder"></span>
						</div>
					</div>
				</div>
				<div id="ste_richedt" class="richedt bd0">
					<div id="edt_toolbar" class="clear">
						<a id="E-edtbtn_cover" class="richedt-tab bd0" href="javascript://" rel="topic-cover">Cover</a>
                        <a id="E-edtbtn_files" class="richedt-tab bd0" href="javascript://" rel="topic-files">Attachments</a>
						<a id="E-edtbtn_html" class="richedt-tab bd0" href="javascript://" rel="edt_main">HTML</a>
						<a id="E-edtbtn_preview" class="richedt-tab bd0 edtbtn_on" href="javascript://" rel="edt_main">Preview</a>
						<div id="edtbtn_others">
							<span>Upload / Insert</span>
							<a id="edtbtn_img" title="Add an image" class="thickbox" href="javascript://" rel="<%:Url.Action("UploadMedia","Camp",new {RefId=Model.Id}) %>"><img alt="" src="<%:Url.Img("media-button-image.gif")%>"/></a>
						</div>
					</div>
					<div id="edt_main" class="richedt-tab-ct bd0">
						<textarea id="edt_hoder" class="edt_holder" tabindex="2" name="ContentX" cols="40" rows="20"><%:Model.ContentX %></textarea>
					</div>
                    <div id="topic-files" class="richedt-tab-ct bd0">
                        <iframe id="ifTopicFiles" name="ifTopicFiles" frameborder="0" scrolling="no" width="100%" height="100%"></iframe>
                    </div>
					<div id="topic-cover" class="richedt-tab-ct bd0">
						<%if (!String.IsNullOrWhiteSpace(Model.Logo)){%>
						<img id="topic-logo" alt="cover" src="<%:Model.Logo %>" />
						<%} else{%>
						<img id="topic-logo" style="display:none" alt="cover" />
						<span id="topic-cover-tip" class="tip">No topic logo/cover image was specified!</span>
						<%} %>
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
	<link rel="stylesheet" type="text/css" href="<%:Url.CSS("overlay-apple") %>"/>
	<%this.Model.PageFlag = "topic_add";%> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
	<!--scripts-->
    <script type="text/javascript">
        Garfielder.Cfg = {
            IsNew:<%:Model.IsNew.ToString().ToLower() %>,
            ObjID:'<%:Model.Id%>',
            Title:'<%:Model.Title %>',
            URL_AUTOSLUG:'<%:Url.Action("AutoSlug","Camp") %>',
            URL_FILES:'<%:Url.Action("ListTopicFile","Camp") %>',
			URL_LOGO:'<%:Model.Logo %>'
        };
    </script>
	<script type="text/javascript" src="<%:Url.JS("jquery.tools-1.2.5.min") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("jquery.vsUtils") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("jquery.vPreload")%>"></script>
	<script type="text/javascript" src="<%:Url.JS("tiny_mce/jquery.tinymce") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("Garfielder.IFModal") %>"></script>
    <script type="text/javascript" src="<%:Url.JS("Admin.AutoSlug") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("Admin.TopicAdd") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("Admin.TopicAdd_Tag") %>"></script>
	<!--/scripts-->		
</asp:Content>