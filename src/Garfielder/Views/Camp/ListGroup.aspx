<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMGroupList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Group List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2  id="scSubhead">Groups</h2>
<div class="row">
	<div class="r sbox">
		<input type="text" value="" name="s" id="ipts-cat"/>
		<input type="submit" value="Search Groups" class="btn"/>
	</div>
</div>
<div id="catAdmin-wrap" class="multiColBox clear">
	<div id="catAdmin-wrap_L" class="l mcb_L">
		<%using (Ajax.BeginForm("EditGroup", null, new AjaxOptions
		  {
			  HttpMethod = "Post",
			  OnSuccess = "this$.OnEdit"
		  }, new{ id="frmEdit" }))
		  { %>
		<div id="catAdmin-detail" class="form-wrap">
			<h3>Add New Group</h3>
			<div class="form-field">	
				<label for="gr-name">Name<%:Html.ValidationMessageFor(x => x.Name)%></label>
				<%:Html.TextBoxFor(x => x.Name, new { id = "gr-name", size = 40 })%>
				<p>The name is how it appears on your site.</p>
			</div>	
			<div class="form-field">
				<label for="gr-slug">Slug<%:Html.ValidationMessageFor(x => x.Slug)%></label>
				<%:Html.TextBoxFor(x=>x.Slug,new{id="gr-slug",size=40}) %>
				<p>The “slug” is the URL-friendly version of the name. It is usually all lowercase and contains only letters, numbers, and hyphens.</p>
			</div>	
			<div class="form-field">
				<label for="ddlGroup">Parent</label>
				<%Html.RenderPartial("SelectGroup"); %>
				<p>Groups, unlike tags, can have a hierarchy. You might have a Jazz group, and under that have children categories for Bebop and Big Band. Totally optional.</p>
				<input type="hidden" id="gr-level" name="Level" value="0" />
			</div>
			<div class="form-field">
				<label for="grDesc">Description<%:Html.ValidationMessageFor(x => x.Description)%></label>
				<%:Html.TextAreaFor(x => x.Description, new { id = "grDesc", cols = 40, rows = 5 })%>
				<p>The description is not prominent by default; however, some themes may show it.</p>
			</div>
			<p class="submit"><input type="submit" value="Add New Group" id="gr-submit" name="grsubmit" class="button"/></p>																											
		</div><!--detail-->
		<%} %>
	</div>
	<div id="catAdmin-wrap_R" class="r mcb_R">
		<div id="catAdmin-list">
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
				<table id="tbGroupList" cellspacing="0" class="widefat cat fixed">
					<thead>
						<tr>
							<th class="manage-column column-cb check-column" id="col-cb" scope="col"><input type="checkbox"/></th>
							<th class="manage-column column-name" id="col-cat-name" scope="col">Name</th>
							<th class="manage-column column-desc" id="col-cat-desc" scope="col">Description</th>
							<th class="manage-column column-slug" id="col-cat-slug" scope="col">Slug</th>
							<th class="manage-column column-topics num" id="col-cat-topics" scope="col">Topics</th>
						</tr>
					</thead>
					<tfoot>
						<tr>
							<th class="manage-column column-cb check-column" scope="col"><input type="checkbox"/></th>
							<th class="manage-column column-name" scope="col">Name</th>
							<th class="manage-column column-desc" scope="col">Description</th>
							<th class="manage-column column-slug" scope="col">Slug</th>
							<th class="manage-column column-topics num" scope="col">Topics</th>
						</tr>
					</tfoot>
					<tbody>
						<% var item=default(VMGroupEdit); %>
						<% for (int i = 0; i < Model.GroupList.Count; i++)
						   { %>
						   <%item = Model.GroupList[i]; %>
						   <tr id="g-<%:item.Id %>" class="<%:i%2==0?"alt":"" %>">
								<th class="check-column" scope="row"> 
									<input type="checkbox" value="<%:item.Id %>" name="delete_cats[]"/>
								</th>
								<td class="name column-name">
									<strong><a title="Edit [<%:item.Name %>] " href="javascript://" class="row-title"><%:item.Name %></a></strong>
									<div class="row-actions">
										<span class="edit"><a href="javascript://">Edit</a> | </span>
										<span class="inline"><a class="editinline" href="javascript://">Quick&nbsp;Edit</a> | </span>
										<span class="delete"><a href="javascript://" class="delete-tag">Delete</a></span>
									</div>
								</td>
								<td class="desc column-desc"><%:item.Description %></td>
								<td class="slug column-slug"><%:item.Slug %></td>
								<td class="topics column-topics num"><a href="javascript://"><%:item.CntTopic %></a></td>
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
		</div><!--#catAdmin-list-->
	</div><!--#catAdmin-wrap_R-->
</div>	

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "group_index"; %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
<!--scripts-->
<script src="<%:Url.Content("~/Scripts/jquery.unobtrusive-ajax.js") %>" type="text/javascript"></script>
<script src="<%:Url.Content("~/Scripts/jquery.validate.js") %>" type="text/javascript"></script>
<script src="<%:Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>" type="text/javascript"></script>
<script type="text/javascript" src="<%:Url.JS("jquery.datalink") %>"></script>
<script type="text/javascript" src="<%:Url.JS("jquery-ui-1.8.6.effects.min") %>"></script>
<script id="tpl_group" type="text/x-jquery-tmpl">
	<tr id="g-${Id}">
		<th class="check-column" scope="row"> 
			<input type="checkbox" value="${Id}" name="delete_cats[]"/>
		</th>
		<td class="name column-name">
			<strong><a title="Edit [${Name}] " href="javascript://" class="row-title">${Name}</a></strong>
			<div class="row-actions">
				<span class="edit"><a href="javascript://">Edit</a> | </span>
				<span class="inline"><a class="editinline" href="javascript://">Quick&nbsp;Edit</a> | </span>
				<span class="delete"><a href="javascript://" class="delete-tag">Delete</a></span>
			</div>
		</td>
		<td class="desc column-desc">${Description}</td>
		<td class="slug column-slug">${Slug}</td>
		<td class="topics column-topics num"><a href="javascript://">0</a></td>
	</tr>
</script> 
<script type="text/javascript" src="<%:Url.JS("jQuery.tmpl.min") %>"></script>
<script type="text/javascript" src="<%:Url.JS("Admin.Group") %>"></script>
<script type="text/javascript">
//<![CDATA[
	this$.Init({});
//]]>
</script>
<!--/scripts-->	
</asp:Content>

