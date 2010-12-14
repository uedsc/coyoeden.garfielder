<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMXFileList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Media Library
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 id="scSubhead">Media Library<%:Html.ActionLink("Add New", "AddMedia", "Camp",null, new { @class = "btn" })%></h2>

<div id="mediaList" class="listView">
	<div class="row acts1">
		<ul class="l">
			<li><a href="#" class="now">All<span class="cnt">&nbsp;(<%:Model.FileList.Count %>)&nbsp;</span></a> | </li>
			<li><a href="#">Image<span class="cnt">&nbsp;(<%:Model.FileList.Count %>)&nbsp;</span></a></li>
		</ul>
		<div class="r sbox">
			<label for="txtMDSearch" class="txtForScreen">Search Media</label>
			<input type="text" id="txtMDSearch" name="sterm"/>
			<input type="submit" class="btn" value="Search Media"/>	
		</div>
	</div>
    <div class="row tablenav">
        <div class="actions">
            <select name="action">
                <option selected="selected" value="-1">Bulk Actions</option>
                <option value="trash">Move to Trash</option>
            </select>
            <input type="submit" class="btn action" id="doaction" name="doaction" value="Apply"/>
            <select name="m">
                <option value="0" selected="selected">Show all dates</option>
                <option value="201009">September 2010</option>
                <option value="201008">August 2010</option>
                <option value="201007">July 2010</option>
            </select>
            <input type="submit" class="btn" value="Filter" id="btnFilter" />
        </div>
    </div>
	<div class="row">
		<table class="widefat post fixed" cellspacing="0">
			<thead>
				<tr>
					<th scope="col" id="cb" class="manage-column column-cb check-column" style=""><input type="checkbox" /></th>
					<th style="" class="manage-column column-icon" id="icon" scope="col"></th>										
					<th scope="col" id="title" class="manage-column column-title" style="">File</th>
					<th scope="col" id="author" class="manage-column column-author" style="">Author</th>
					<th scope="col" id="date" class="manage-column column-date" style="">Date</th>
				</tr>
			</thead>
			<tbody>
            <% var item=default(VMXFileEdit); %>
            <% for (var i = 0; i < Model.FileList.Count;i++ ){ %>
            <% item = Model.FileList[i]; %>
				<tr id='md-<%:item.Id %>' class='<%:i%2==0?"alt":"" %> status-publish iedit' valign="top">
					<th scope="row" class="check-column"><input type="checkbox" name="md[]" value="117" /></th>
					<td class="column-icon media-icon">				
						<a title="Edit" href="#">
							<img title="" alt="" class="attachment-64x64" src="<%:Url.Home()+item.Name1+"_64x64"+item.Extension %>"/>	
						</a>
					</td>										
					<td class="md-title column-title">
						<a class="row-title" href="#" title=""><strong><%:item.Title %></strong></a>
						<p><%:item.Extension %></p>
					</td>
					<td class="author column-author"><a href="#"><%:item.UserName %></a></td>
					<td class="date column-date"><abbr title=""><%:item.CreatedAt.ToString("yyyy/MM/dd") %></abbr></td>					
				</tr>	                 
            <% } %>																			
			</tbody>
			<tfoot>
				<tr>
					<th scope="col"  class="manage-column column-cb check-column" style=""><input type="checkbox" /></th>
					<th style="" class="manage-column column-icon" id="icon" scope="col"></th>
					<th scope="col"  class="manage-column column-title" style="">File</th>
					<th scope="col"  class="manage-column column-author" style="">Author</th>
					<th scope="col"  class="manage-column column-date" style="">Date</th>
				</tr>
			</tfoot>
		</table>							
	</div>
    <div class="row tablenav">
        <div class="actions">
            <select name="action">
                <option selected="selected" value="-1">Bulk Actions</option>
                <option value="trash">Move to Trash</option>
            </select>
            <input type="submit" class="btn action" name="doaction" value="Apply"/>
        </div>
    </div>																
</div>	

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "media_index"; %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
</asp:Content>

