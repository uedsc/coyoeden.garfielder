<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/nolayout.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMXFileList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Topic attachments <%:Model.RefTopic == null ? "" : String.Format("[{0}]",Model.RefTopic.Title)%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<span id="loading" class="tip">Loading...</span>
<%if (Model.FileList.Count == 0){%>
<p class="error"><%:Model.RefTopic == null ? "This is an unsaved topic which has no attachments!" : "No attachments available!"%></p>
<%}else{%>
<%Html.RenderPartial("ListFileView", new {mode = "thumb", files = Model.FileList});%>
<%}%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<link rel="stylesheet" type="text/css" href="/Assets/css/camp_font.css" />
<link rel="stylesheet" type="text/css" href="/Assets/css/camp_color.css" />
<link rel="stylesheet" type="text/css" href="/Assets/css/camp.css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
<script type="text/javascript" src="<%:Url.JS("Admin.TopicFileList") %>"></script>
</asp:Content>
