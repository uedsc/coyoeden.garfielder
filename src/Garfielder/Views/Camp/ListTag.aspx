<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMTagList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tags
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2 id="scSubhead">Tags</h2>
<div class="row">
	<div class="r sbox">
		<input type="text" value="" name="s" id="ipts-cat"/>
		<input type="submit" value="Search Groups" class="btn"/>
	</div>
</div>
<div id="tagAdmin-wrap" class="multiColBox clear">
	<div id="tagAdmin-wrap_L" class="l mcb_L">
        <%using (Ajax.BeginForm("EditTag", new AjaxOptions { 
            HttpMethod="Post",OnSuccess="function(){alert('hi');}"
          }))
          { %>
		<div id="tagAdmin-detail" class="form-wrap">
			<h3>Add New Tag</h3>
			<div class="form-field form-required">
				<label for="tag-name">Name</label>
				<input type="text" size="40" value="" id="tag-name" name="Name"/>
				<p>The name is how it appears on your site.</p>
			</div>	
			<div class="form-field">
				<label for="tag-slug">Slug</label>
				<input type="text" size="40" value="" id="tag-slug" name="Slug"/>
				<p>The “slug” is the URL-friendly version of the name. It is usually all lowercase and contains only letters, numbers, and hyphens.</p>
			</div>	
			<div class="form-field">
				<label for="tag-desc">Description</label>
				<textarea cols="40" rows="5" id="tag-desc" name="Description"></textarea>
				<p>The description is not prominent by default; however, some themes may show it.</p>
			</div>
			<p class="submit"><input type="submit" value="Add New Tag" id="tag-submit" name="tagsubmit" class="button"/></p>																											
		</div><!--detail-->
        <%} %>
	</div>
	<div id="tagAdmin-wrap_R" class="r mcb_R">
		<div id="tagAdmin-list">
			<div class="row tablenav">
				<div class="actions">
					<select name="action">
						<option selected="selected" value="">Bulk Actions</option>
						<option value="delete">Delete</option>
					</select>
					<input type="submit" class="btn action" id="doaction" name="doaction" value="Apply"/>
				</div>								
			</div>
			<div class="row">
				<table cellspacing="0" class="widefat tag fixed">
					<thead>
						<tr>
							<th class="manage-column column-cb check-column" id="col-cb" scope="col"><input type="checkbox"/></th>
							<th class="manage-column column-name" id="col-cat-name" scope="col">Name</th>
							<th class="manage-column column-slug" id="col-cat-slug" scope="col">Slug</th>
							<th class="manage-column column-topics num" id="col-cat-topics" scope="col">Topics</th>
						</tr>
					</thead>
					<tfoot>
						<tr>
							<th class="manage-column column-cb check-column" scope="col"><input type="checkbox"/></th>
							<th class="manage-column column-name" scope="col">Name</th>
							<th class="manage-column column-slug" scope="col">Slug</th>
							<th class="manage-column column-topics num" scope="col">Topics</th>
						</tr>
					</tfoot>
					<tbody>
                        <% var item=default(VMTagEdit); %>
                        <% for (int i = 0; i < Model.TagList.Count; i++)
                           { %>
                           <%item = Model.TagList[i]; %>
                           <tr id="tag-<%:item.Id %>" class="<%:i%2==0?"alt":"" %>">
                                <th class="check-column" scope="row"> 
								    <input type="checkbox" value="<%:item.Id %>" name="delete_tags[]"/>
							    </th>
							    <td class="name column-name">
								    <strong><a title="Edit [<%:item.Name %>] " href="javascript://" class="row-title"><%:item.Name %></a></strong>
								    <div class="row-actions">
									    <span class="edit"><a href="javascript://">Edit</a> | </span>
									    <span class="inline"><a class="editinline" href="javascript://">Quick&nbsp;Edit</a> | </span>
									    <span class="delete"><a href="javascript://" class="delete-tag">Delete</a></span>
								    </div>
							    </td>
							    <td class="slug column-slug"><%:item.Slug %></td>
							    <td class="topics column-topics num"><a href="javascript://">2</a></td>
                           </tr>
                        <%} %>											
					</tbody>
				</table>									
			</div>
			<div class="row tablenav">
				<div class="actions">
					<select name="action">
						<option selected="selected" value="">Bulk Actions</option>
						<option value="delete">Delete</option>
					</select>
					<input type="submit" class="btn action" id="doaction1" name="doaction" value="Apply"/>
				</div>								
			</div>								
		</div><!--#tagAdmin-list-->
	</div><!--#tagAdmin-wrap_R-->
</div>	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "tag_index"; %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
</asp:Content>
