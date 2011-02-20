<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/nolayout.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMTopicFull>" %>
<%@ Import Namespace="Garfielder.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lingzhi's Emotion Studio - <%:Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header">
    	<div class="title">
            <div class="info">
        	    <h1><%:Model.Title%></h1>
                <p><%:Model.DateCreated%></p>
                <p><%:Html.Raw(Model.Desc)%></p>
                <%if (Model.Files.Count > 1)
				  {%>
                <ul id="pager" class="page">
                <%for (var i = 0; i < Model.Files.Count; i++){%>
                   <li><a href="#item<%:Model.Files[i].Id %>">[<%:i+1 %>]</a></li> 
                <%}%>
                </ul>
                <%}%>
            </div>
            <p class="back"><a href="<%:Model.UrlGoBack %>" id="btn-back">back</a></p> 
        </div>
    </div>
    <div id="gallery" class="gallery">
		<%for (var i = 0; i < Model.Files.Count; i++){%>
        <div id="item<%:Model.Files[i].Id %>" class="gallery-item">
           <img  alt=""  src="<%:Model.Files[i].Url(ImageFlags.RAW) %>"/>
        </div>
        <%}%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<link href="<%:Url.CSS("main") %>" rel="stylesheet" type="text/css" />
<link href="<%:Url.CSS("detail") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
</asp:Content>
