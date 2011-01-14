﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/nolayout.Master" Inherits="Garfielder.Web.ViewPageX<VMUploadMedia>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Upload a Media file for a specified topic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="media-upload-header">
	<ul id="sideMenu">
		<li><a href="?RelId=<%:Model.RelId %>&Src=local" <%:Html.Raw(Str(Model.Src=="local","class=\"now\"")) %>>From Computer</a></li>
		<li><a href="?RelId=<%:Model.RelId %>&Src=url&flash=false" <%:Html.Raw(Str(Model.Src=="url","class=\"now\"")) %>>From URL</a></li>
		<li><a href="?RelId=<%:Model.RelId %>&Src=lib&flash=false" <%:Html.Raw(Str(Model.Src=="lib","class=\"now\"")) %>>From Media Library</a></li>
	</ul>
</div>
<%if (Model.Src == "url")
  {  %>
<!--url ui-->
<% Html.RenderPartial("UM_FromURL"); %>

<%}else if (Model.Src == "lib"){%>
<!--lib ui-->
<% Html.RenderPartial("ListFileView",new{mode=Model.ViewMode,files=Model.FileList}); %>

<%}else{%>
<!--local ui-->
<%Html.RenderPartial("UploadUI", Model); %>
		
<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<link rel="stylesheet" type="text/css" href="/Assets/css/camp_font.css" />
<link rel="stylesheet" type="text/css" href="/Assets/css/camp_color.css" />
<link rel="stylesheet" type="text/css" href="/Assets/css/camp.css" />
<% Model.PageFlag = "media_upload"; %>
<%if (!Model.NoFlash)
  { %>
<script type="text/javascript" src="<%:Url.JS("swfupload/swfupload","2") %>"></script> 
<script type="text/javascript" src="<%:Url.JS("jquery.json-2.2.min") %>"></script>
<%} %> 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
<script type="text/javascript" src="<%:Url.JS("Admin.AddMedia","2") %>"></script>
<script type="text/javascript">
	Garfielder.Cfg={
		noFlash:<%:Model.NoFlash.ToString().ToLower() %>,
		swfuOpts:{
			upload_url:'<%:Url.Action("SaveMedia","Camp") %>',
			post_params:{"UserID": "<%:Model.CurrentUser.Id %>","UserName":"<%:Model.CurrentUser.Name %>"},
			button_image_url:'<%:Url.Img("XPButtonNoText_160x22.png") %>'
		}
	};
</script> 
<%if (!Model.NoFlash)
  { %>
<script type="text/javascript" src="<%:Url.JS("Admin.SWFUploadController","2") %>"></script>
<%} %>
</asp:Content>
