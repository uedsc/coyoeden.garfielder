<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/nolayout.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMTopicFull>" %>
<%@ Import Namespace="Garfielder.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lingzhi's Emotion Studio - <%:Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var itemsPerPage = 10;
        var cntItem = Model.Files.Count;
        var pNum = Model.GetPageNum(itemsPerPage);
%>
    <div class="header">
    	<div class="title">
            <div class="info">
        	    <h1><%:Model.Title%></h1>
                <p><%:Model.DateCreated%></p>
                <p><%:Model.Desc%></p>
                <%if (pNum > 1){%>
                <ul id="pager" class="page">
                <%for (var i = 0; i < pNum; i++){%>
                   <li><a href="javascript://" class="<%:i==0?"on":"" %>">[<%:i+1 %>]</a></li> 
                <%}%>
                </ul>
                <%}%>
            </div>
            <p class="back"><a href="<%:Model.UrlGoBack %>" id="btn-back">back</a></p> 
        </div>
    </div>
    <div id="gallery" class="gallery">
        <%for (var i = 0; i < pNum; i++){%>
        <div class="gallery-tab clearfix<%:i==0?" gallery-tab-on":"" %>">
            <%for (var j = i * itemsPerPage; (j < (i + 1) * itemsPerPage) && (j < cntItem); j++){%>
            <div class="gallery-item">
                <img width="160" height="100" alt="" src="<%:Model.Files[j].Url(ImageFlags.S160X100) %>"/>
            </div>
            <%}%>
        </div>
        <%}%>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<link href="<%:Url.CSS("main") %>" rel="stylesheet" type="text/css" />
<link href="<%:Url.CSS("detail") %>" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%:Url.JS("jquery-1.4.4.min") %>"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
<script type="text/javascript" src="<%:Url.JS("Garfielder") %>"></script>
<script type="text/javascript" src="<%:Url.JS("Topic") %>"></script>
<script type="text/javascript">
//<![CDATA[
		Garfielder.Init({ siteRoot: '<%:Url.Home(true) %>',tip:<%:(!String.IsNullOrEmpty(Model.Msg)).ToString().ToLower() %> });
//]]>
</script>
</asp:Content>
