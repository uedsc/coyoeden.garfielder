<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMUserList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User list
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2  id="scSubhead">Users<%:Html.ActionLink("Add New", "EditUser", "Camp",null, new { @class = "btn" })%></h2>
    <!--#userList-->
	<div id="userList" class="listView">
		<div class="row acts1">
			<ul class="l">
				<li><a href="#" class="now">All<span class="cnt">&nbsp;(<%:Model.UserList.Count %>)&nbsp;</span></a></li>
			</ul>
			<div class="r sbox">
				<label for="txtUserSearch" class="txtForScreen">Search Users</label>
				<input type="text" id="txtUserSearch" name="sterm"/>
				<input type="submit" class="btn" value="Search Users"/>	
			</div>
		</div>
        <div class="row tablenav">
            <div class="actions">
                <select name="action">
                    <option selected="selected" value="-1">Bulk Actions</option>
                    <option value="edit">Edit</option>
                    <option value="trash">Move to Trash</option>
                </select>
                <input type="submit" class="btn action" id="doaction" name="doaction" value="Apply"/>
            </div>
        </div>
		<div class="row">
			<table class="widefat post fixed" cellspacing="0">
				<thead>
					<tr>
						<th scope="col" id="cb" class="manage-column column-cb check-column" style=""><input type="checkbox" /></th>
						<th scope="col" id="name" class="manage-column column-name" style="">Name</th>
						<th scope="col" id="nickname" class="manage-column column-nickname" style="">Nickname</th>
						<th scope="col" id="email" class="manage-column column-email" style="">Email</th>
					</tr>
				</thead>
				<tbody>
                    <% var item=default(VMUserEdit); %>
                    <% for (var i = 0; i < Model.UserList.Count;i++ ){ %>
                    <% item=Model.UserList[i]; %>
					<tr id='<%:item.Id %>' class='<%:i%2==0?"alt":"" %>' valign="top">
						<th scope="row" class="check-column"><input type="checkbox" name="user[]" value="<%:item.Id %>" /></th>
						<td class="user-name column-name">
							<a class="row-name" href="#" title=""><strong><%:item.Name %></strong></a>
						</td>
						<td class="nickname column-nickname"><a href="#"><%:item.Nickname %></a></td>
						<td class="email column-email"><a href='#'> <%:item.Email %></a></td>
					</tr>
                    <%} %>																							
				</tbody>
				<tfoot>
					<tr>
						<th scope="col" class="manage-column column-cb check-column" style=""><input type="checkbox" /></th>
						<th scope="col" class="manage-column column-name" style="">Name</th>
						<th scope="col" class="manage-column column-nickname" style="">Nickname</th>
						<th scope="col" class="manage-column column-email" style="">Email</th>
					</tr>
				</tfoot>
			</table>							
		</div>
        <div class="row tablenav">
            <div class="actions">
                <select name="action">
                    <option selected="selected" value="-1">Bulk Actions</option>
                    <option value="edit">Edit</option>
                    <option value="trash">Move to Trash</option>
                </select>
                <input type="submit" class="btn action" name="doaction" value="Apply"/>
            </div>
        </div>																
	</div>
    <!--#userList-->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "user_index"; %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
</asp:Content>

