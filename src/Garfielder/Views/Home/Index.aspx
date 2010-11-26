<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMHome>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lingzhi's Emotion Studio - Life is Good.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 	<ul class="conbox clearfix">

        <%Model.Topics.ForEach(x =>
          { %>
            <li><a href="Topic/<%:x.Id %>" title="<%:x.Title %>"><img src="<%:x.Icon %>" alt="" /></a></li>
        <%}); %>
    </ul>
    
</asp:Content>
