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
			<h3><a id="form-title0" class="form-tab form-tab-cur" href="javascript://">Add New Group</a><span id="form-title1" class="form-tab">Edit Group</span></h3>
			<div class="form-field">	
				<label for="gr-name">Name<%:Html.ValidationMessageFor(x => x.Name)%></label>
				<%:Html.TextBoxFor(x => x.Name, new { id = "gr-name", size = 40 })%>
				<p>The name is how it appears on your site.</p>
                <input type="hidden" name="Id" id="gr-id"/>
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
				<label for="gr-desc">Description<%:Html.ValidationMessageFor(x => x.Description)%></label>
				<%:Html.TextAreaFor(x => x.Description, new { id = "gr-desc", cols = 40, rows = 5 })%>
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
				<table id="tbItemList" cellspacing="0" class="widefat cat fixed">
					<thead>
						<tr>
							<th class="manage-column column-cb check-column" scope="col"><input id="cbx-toggle-all0" type="checkbox"/></th>
							<th class="manage-column column-name" scope="col">Name</th>
							<th class="manage-column column-desc" scope="col">Description</th>
							<th class="manage-column column-slug" scope="col">Slug</th>
							<th class="manage-column column-topics num" scope="col">Topics</th>
						</tr>
					</thead>
					<tfoot>
						<tr>
							<th class="manage-column column-cb check-column" scope="col"><input id="cbx-toggle-all1" type="checkbox"/></th>
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
									<input type="checkbox" class="cbx-objid" value="<%:item.Id %>" name="ObjIDList"/>
								</th>
								<td class="name column-name">
									<strong><a title="Edit [<%:item.Name %>] " href="javascript://" class="row-title"><%:item.Name %></a></strong>
									<div class="row-actions">
										<span class="edit"><a href="javascript://" class="act-edit" rel='{"i":"<%:item.Id %>","n":"<%:item.Name %>","s":"<%:item.Slug %>","d":"<%:item.Description %>","p":"<%:item.ParentID %>","l":"<%:item.Level %>"}'>Edit</a> | </span>
										<span class="delete"><a href="javascript://" class="act-del" rel="<%:item.Id %>">Delete</a></span>
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
<script type="text/javascript" src="<%:Url.JS("jquery-ui-1.8.6.effects.min") %>"></script>
<script type="text/javascript" src="<%:Url.JS("jquery.json-2.2.min") %>"></script>
<script id="tpl_item" type="text/x-jquery-tmpl">
	<tr id="g-${Id}">
		<th class="check-column" scope="row"> 
			<input type="checkbox" value="${Id}" name="ObjIDList"/>
		</th>
		<td class="name column-name">
			<strong><a title="Edit [${Name}] " href="javascript://" class="row-title">${Name}</a></strong>
			<div class="row-actions">
				<span class="edit"><a href="javascript://" class="act-edit">Edit</a> | </span>
				<span class="delete"><a href="javascript://" class="act-del">Delete</a></span>
			</div>
		</td>
		<td class="desc column-desc">${Description}</td>
		<td class="slug column-slug">${Slug}</td>
		<td class="topics column-topics num"><a href="javascript://">0</a></td>
	</tr>
</script> 
<script type="text/javascript" src="<%:Url.JS("jQuery.tmpl.min") %>"></script>
<script type="text/javascript" src="<%:Url.JS("Admin.Group") %>"></script>
<!--/scripts-->	
</asp:Content>

