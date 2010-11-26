<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMCampHome>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Garfielder camp
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
    <%this.Model.PageFlag = "camp_index"; %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="scSubhead">Summary of the site</h2>

</asp:Content>
