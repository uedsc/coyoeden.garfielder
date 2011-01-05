<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VMGroupList>" %>
<!--select tag for Groups-->
<% var items = Model.GroupList; var level = 3; var name = "ParentID";%>
<%if(items!=null&&items.Count>0){
      items = items.Where(x => x.Level < level).OrderBy(x => x.Name).ToList();    
%>
<select id="ddlGroup" name="<%:name%>">
    <option value="" data-level="-1">None</option>
    <%items.ForEach(x =>{%>
      <option value="<%:x.Id %>" data-level="<%:x.Level %>"><%:x.ParentName+"-"+x.Name %></option>
    <%}); %>
</select>
<%} %>
<!--/select tag for Groups-->
