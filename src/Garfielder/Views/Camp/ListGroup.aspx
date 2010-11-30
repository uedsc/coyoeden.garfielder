<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Garfielder.ViewModels.VMGroupEdit>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ListGroup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>ListGroup</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table>
    <tr>
        <th></th>
        <th>
            Name
        </th>
        <th>
            Slug
        </th>
        <th>
            Description
        </th>
        <th>
            PageFlag
        </th>
        <th>
            IsUserAuthenticated
        </th>
        <th>
            SiteTitle
        </th>
        <th>
            MetaKeywords
        </th>
        <th>
            MetaDescription
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.Id }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.Id }) %>
        </td>
        <td>
            <%: item.Name %>
        </td>
        <td>
            <%: item.Slug %>
        </td>
        <td>
            <%: item.Description %>
        </td>
        <td>
            <%: item.PageFlag %>
        </td>
        <td>
            <%: item.IsUserAuthenticated %>
        </td>
        <td>
            <%: item.SiteTitle %>
        </td>
        <td>
            <%: item.MetaKeywords %>
        </td>
        <td>
            <%: item.MetaDescription %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
</asp:Content>

