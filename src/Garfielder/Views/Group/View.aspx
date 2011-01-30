<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMGroupHome>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lingzhi's Emotion Studio - Life is Good - [<%:Model.GroupData.Name %>]
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 	<ul class="conbox clearfix">
        <%Model.Topics.ForEach(x =>
          { %>
            <li><a href="<%:Url.Action("View","Topic",new{id=x.Slug}) %>" title="<%:x.Title %>"><img width="160" height="100" src="<%:x.Icon %>" alt="" /></a></li>
        <%}); %>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = String.Format("channel_{0}", Model.GroupData.Slug); %>
</asp:Content>
