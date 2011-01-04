<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMCampTopicList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListTopic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 id="scSubhead">Topics<%:Html.ActionLink("Add New", "EditTopic", "Camp",null, new { @class = "btn" })%></h2>
	<!--#topicList-->
	<div id="topicList" class="listView">
    <form id="frmSearch" name="frmSearch" action="/">
		<div class="row acts1">
			<ul class="l">
				<li><a href="#" class="now">All<span class="cnt">&nbsp;(<%:Model.TopicList.Count %>)&nbsp;</span></a> | </li>
				<li><a href="#">Published<span class="cnt">&nbsp;(<%:Model.TopicList.Count %>)&nbsp;</span></a></li>
			</ul>
			<div class="r sbox">
				<label for="txtTopicSearch" class="txtForScreen">Search Topics</label>
				<input type="text" id="txtTopicSearch" name="sterm"/>
				<input type="submit" class="btn" value="Search Topics"/>	
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
				<select name="m">
					<option value="0" selected="selected">Show all dates</option>
					<option value="<%:DateTime.Now.ToString("yyyyMM") %>"><%:DateTime.Now.ToString("yyyy/MM") %></option>
					<option value="<%:DateTime.Now.AddMonths(-1).ToString("yyyyMM")%>"><%:DateTime.Now.AddMonths(-1).ToString("yyyy/MM") %></option>
					<option value="<%:DateTime.Now.AddMonths(-2).ToString("yyyyMM") %>"><%:DateTime.Now.AddMonths(-2).ToString("yyyy/MM") %></option>
				</select>
                <%if(Model.GroupList!=null&&Model.GroupList.Count>0){%>
                <% var  items = Model.GroupList.Where(x => x.Level < 3).OrderBy(x => x.Name).ToList();  %>
				<select class="postform" id="list-group" name="GroupID">
					<option value="0">View all groups</option>
                    <%items.ForEach(x =>{%>
                    <option value="<%:x.Id %>" data-level="<%:x.Level %>"><%:x.ParentName+"-"+x.Name %></option>
                    <%}); %>
				</select>
                <%}%>
				<input type="submit" class="btn" value="Filter" id="btnFilter"/>
			</div>
		</div>
		<div class="row">
			<table id="tb-list" class="widefat post fixed" cellspacing="0">
				<thead>
					<tr>
						<th scope="col" id="cb" class="manage-column column-cb check-column"><input id="cbx-toggle-topics" type="checkbox" /></th>
						<th scope="col" id="title" class="manage-column column-title">Title</th>
						<th scope="col" id="author" class="manage-column column-author">Author</th>
						<th scope="col" id="groups" class="manage-column column-groups">Groups</th>
						<th scope="col" id="tags" class="manage-column column-tags">Tags</th>
						<th scope="col" id="comments" class="manage-column column-comments num"><div class="vers"><img alt="Comments" src="<%:Url.Img("comment-grey-bubble.png") %>" /></div></th>
						<th scope="col" id="date" class="manage-column column-date">Date</th>
						<th scope="col" id="featured" class="manage-column column-featured">Featured</th>
					</tr>
				</thead>
				<tbody>
					<% var item=default(VMCampTopicEdit); %>
					<% for (var i = 0; i < Model.TopicList.Count; i++){ %>
					<% item = Model.TopicList[i]; %>
					<tr id='topic-<%:item.Id %>' class='<%:i%2==0?"alt":"" %> status-publish iedit' valign="top">
						<th scope="row" class="check-column"><input class="cbx-topicid" type="checkbox" name="TopicIDList" value="<%:item.Id %>" /></th>
						<td class="post-title column-title">
							<a class="row-title" href="#" title=""><strong><%:item.Title %></strong></a>
						</td>
						<td class="author column-author"><a href="#"><%:item.UserName %></a></td>
						<td class="groups column-groups"><a href='#'><%:item.GroupsTxt %></a></td>
						<td class="tags column-tags"><a href='#'><%:item.TagsTxt %></a></td>
						<td class="comments column-comments">
							<div class="post-com-count-wrapper">
								<a href='#' title='0 pending' class='post-com-count'><span class='comment-count'><%:item.CntComment %></span></a>		
							</div>
						</td>
						<td class="date column-date"><abbr title=""><%:item.CreateAt.ToString("yyyy-MM-dd") %></abbr><br />Published</td>					
						<td class="featured column-featured"><div class="featured_post"><a class="off"></a></div></td>
					</tr>
					<%} %>																							
				</tbody>
				<tfoot>
					<tr>
						<th scope="col"  class="manage-column column-cb check-column" ><input type="checkbox" /></th>
						<th scope="col"  class="manage-column column-title" >Title</th>
						<th scope="col"  class="manage-column column-author" >Author</th>
						<th scope="col"  class="manage-column column-groups" >Groups</th>
						<th scope="col"  class="manage-column column-tags" >Tags</th>
						<th scope="col"  class="manage-column column-comments num" ><div class="vers"><img alt="Comments" src="images/comment-grey-bubble.png" /></div></th>
						<th scope="col"  class="manage-column column-date" >Date</th>
						<th scope="col"  class="manage-column column-featured" >Featured</th>
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
    </form>    																
	</div>
	<!--#topicList-->	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
	<%this.Model.PageFlag = "topic_index"; %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
	<!--scripts-->
    <script type="text/javascript">
        Garfielder.Cfg = {};
    </script>
	<script type="text/javascript" src="http://cdn.jquerytools.org/1.2.5/tiny/jquery.tools.min.js"></script>
	<script type="text/javascript" src="<%:Url.JS("Admin.TopicList") %>"></script>
	<!--/scripts-->		
</asp:Content>