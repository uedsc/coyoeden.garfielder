<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMUserEdit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit/Create User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2  id="scSubhead">Edit/Create User<%: Html.ActionLink("Back to List", "ListUser", "Camp", null, new { @class = "btn" })%></h2>
    <div id="user_info" class="editview">
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <div class="row">
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Id) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Id) %>
                    <%: Html.ValidationMessageFor(model => model.Id) %>
                </div>
            </div>

            <div class="row">
                 <div class="editor-label">
                    <%: Html.LabelFor(model => model.Name) %>
                    <span class="tip">(*)</span>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Name) %>
                    <%: Html.ValidationMessageFor(model => model.Name) %>
                </div>           
            </div>

            <div class="row">
                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Nickname) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Nickname) %>
                    <%: Html.ValidationMessageFor(model => model.Nickname) %>
                </div>            
            </div>

            <div class="row">
                 <div class="editor-label">
                    <%: Html.LabelFor(model => model.Email) %>
                    <span class="tip">(*)</span>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Email) %>
                    <%: Html.ValidationMessageFor(model => model.Email) %>
                </div>           
            </div>

            <div class="row">
                 <div class="editor-label">
                    <%: Html.LabelFor(model => model.Password) %>
                    <span class="tip">(*,twice)</span>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(model => model.Password) %><br />
                    <%: Html.TextBoxFor(model => model.Password) %>
                    <%: Html.ValidationMessageFor(model => model.Password) %>
                </div>             
            </div>
          
            <p class="submit">
                <input type="submit" value="Save" class="btn btn_g1"/>
            </p>
        </fieldset>

    <% } %>        
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "user_edit"; %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
</asp:Content>

