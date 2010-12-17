<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VMCampTopicShow>" %>
<div id="grSelector">
	<ul id="group-tabs" class="slimtab">
		<li class="slimtab_on"><a href="#group-pop">Most used</a></li>
		<li><a href="#group-all">All groups</a></li>
	</ul>
	<div id="group-pop" class="slimtab_bd">
	<%if (Model.GroupsAll == null || Model.GroupsAll.Count == 0)
	{%>
		<div class="error">No groups data,Click <%:Html.ActionLink("Groups", "ListGroup", "Camp")%> to add some group data.</div>
	<%}
	else
	{%>
		<ul id="cbxlist-gr-pop" class="cbxlist">
			<% var tags = Model.GroupsAllHot.Take(10).ToList();%>
			<%for (var i = 0; i < tags.Count; i++)
			  {%>
			<li><input "<%:Html.Raw(Model.Str(Model.IsSelectedGroup(tags[i].Id),"checked=\"checked\"")) %>" type="checkbox" value="<%:tags[i].Id %>" name="GroupID" /><label><%:tags[i].Name%></label></li>
			<%} %>
		</ul>       
	<%} %>											
	</div>	
	<div id="group-all" class="slimtab_bd hide">
	<%if (Model.GroupsAll == null || Model.GroupsAll.Count == 0)
	{%>
		<div class="error">No groups data,Click <%:Html.ActionLink("Groups", "ListGroup", "Camp")%> to add some group data.</div>
	<%}
	else
	{%>
		<ul id="cbxlist-gr-all" class="cbxlist">
			<%for (var i = 0; i < Model.GroupsAll.Count; i++)
			  {%>
			<li><input "<%:Html.Raw(Model.Str(Model.IsSelectedGroup(Model.GroupsAll[i].Id),"checked=\"checked\"")) %>" type="checkbox" value="<%:Model.GroupsAll[i].Id %>" name="GroupID" /><label><%:Model.GroupsAll[i].Name%></label></li>
			<%} %>
		</ul>       
	<%} %>
	</div>

</div>
