<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMHome>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lingzhi's Emotion Studio - Life is Good.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 	<ul class="conbox clearfix">

        <%Model.Topics.ForEach(x =>
          { %>
            <li><a href="<%:Url.Action("Show","Topic",new{id=x.Slug}) %>" title="<%:x.Title %>"><img width="160" height="100" src="<%:x.Logo %>" alt="" /></a></li>
        <%}); %>
    </ul>
    
</asp:Content>
