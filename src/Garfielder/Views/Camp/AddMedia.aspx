<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMXFileEdit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add a new Media
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 id="scSubhead">Upload New Media</h2>
<% Html.RenderPartial("UploadUI", Model); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "media_new"; %>
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
			post_params:{"UserID": "<%:Model.CurrentUser.Id %>","UserName":"<%:Model.CurrentUser.Name %>","RefId":"<%:Model.RefId %>"},
			button_image_url:'<%:Url.Img("XPButtonNoText_160x22.png") %>'
		}
	};
</script> 
<%if (!Model.NoFlash)
  { %>
<script type="text/javascript" src="<%:Url.JS("Admin.SWFUploadController","2") %>"></script>
<%} %>
</asp:Content>
