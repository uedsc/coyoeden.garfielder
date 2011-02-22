<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMCampTopicList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List Topic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2 id="scSubhead">Topics<%:Html.ActionLink("Add New", "EditTopic", "Camp",null, new { @class = "btn" })%></h2>
	<!--#topicList-->
	<div id="topicList" class="listView">
        <form id="frm-search" name="frmSearch" action="" method="get">
		<div class="row acts1">
			<ul class="l">
				<li><a id="btnShowAll" href="javascript://" rel="False" <%:Html.Raw(Model.Str(!Model.Published,"class=\"now\"")) %>>All<span class="cnt">&nbsp;(<%:Model.TopicList.Count %>)&nbsp;</span></a> | </li>
				<li><a id="btnShowPublished" href="javascript://" rel="True" <%:Html.Raw(Model.Str(Model.Published,"class=\"now\"")) %>>Published<span class="cnt">&nbsp;(<%:Model.TopicList.Count %>)&nbsp;</span></a></li>
			</ul>
			<div class="r sbox">
				<label for="txtTopicSearch" class="txtForScreen">Search Topics</label>
				<input type="text" id="txtTopicSearch" name="term" value="<%:Model.Term %>"/>
				<input type="submit" class="btn" value="Search Topics"/>
                <input id="t-published" name="published" type="hidden" value="False" />	
			</div>
		</div>
        </form>
        <form id="frm-filter" name="frmFilter" action="" method="post">
		<div class="row tablenav">
			<div class="actions">
				<select  class="act-type" name="Action">
					<option selected="selected" value="-1">Bulk Actions</option>
					<option value="trash">Move to Trash</option>
                    <option value="pub">Publish</option>
					<option value="unpub">Unpublish</option>
				</select>
				<input type="submit" class="btn action" id="do-action0" name="doaction" value="Apply"/>
				<select name="Date">
					<option value="-1" selected="selected">Show all dates</option>
					<option value="<%:DateTime.Now.ToString("yyyyMM")%>" ><%:DateTime.Now.ToString("yyyy/MM") %></option>
					<option value="<%:DateTime.Now.AddMonths(-1).ToString("yyyyMM")%>" ><%:DateTime.Now.AddMonths(-1).ToString("yyyy/MM") %></option>
					<option value="<%:DateTime.Now.AddMonths(-2).ToString("yyyyMM") %>" ><%:DateTime.Now.AddMonths(-2).ToString("yyyy/MM") %></option>
				</select>
                <%if(Model.GroupList!=null&&Model.GroupList.Count>0){%>
                <% var  items = Model.GroupList.Where(x => x.Level < 3).OrderBy(x => x.Name).ToList();  %>
				<select class="postform" id="list-group" name="GroupID">
					<option value="-1">View all groups</option>
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
						<th scope="col" id="cb" class="manage-column column-cb check-column"><input id="cbx-toggle-topics0" type="checkbox" /></th>
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
						<th scope="row" class="check-column"><input class="cbx-topicid" type="checkbox" name="ObjIDList" value="<%:item.Id %>" /></th>
						<td class="post-title column-title">
							<a class="row-title" href="<%:Url.Action("EditTopic","Camp",new{id=item.Id}) %>" title="Click to edit!"><strong><%:item.Title %></strong></a>
						    <p class="acts1">
                                <a href="<%:Url.Action("Show","Topic",new{id=item.Id}) %>" target="_blank">Preview</a> | 
                                <a href="javascript://" class="act-del" title="Delete this topic" rel="<%:item.Id %>">Delete</a>
                            </p>
                        </td>
						<td class="author column-author"><a href="#"><%:item.UserName %></a></td>
						<td class="groups column-groups"><a href='#'><%:item.GroupsTxt %></a></td>
						<td class="tags column-tags"><a href='#'><%:item.TagsTxt %></a></td>
						<td class="comments column-comments">
							<div class="post-com-count-wrapper">
								<a href='javascript://' title='0 pending' class='post-com-count'><span class='comment-count'><%:item.CntComment %></span></a>		
							</div>
						</td>
						<td class="date column-date"><abbr title=""><%:item.CreateAt.ToString("yyyy-MM-dd") %></abbr><br />Published</td>					
						<td class="featured column-featured"><div class="star_topic" title="<%:Model.Str(item.Starred,"Un-star it!","Star it!") %>"><a class="act-star <%:Model.Str(item.Starred,"on","off") %>" rel="<%:item.Id %>"></a></div></td>
					</tr>
					<%} %>																							
				</tbody>
				<tfoot>
					<tr>
						<th scope="col"  class="manage-column column-cb check-column" ><input id="cbx-toggle-topics1" type="checkbox" /></th>
						<th scope="col"  class="manage-column column-title" >Title</th>
						<th scope="col"  class="manage-column column-author" >Author</th>
						<th scope="col"  class="manage-column column-groups" >Groups</th>
						<th scope="col"  class="manage-column column-tags" >Tags</th>
						<th scope="col"  class="manage-column column-comments num" ><div class="vers"><img alt="Comments" src="<%:Url.Img("comment-grey-bubble.png") %>" /></div></th>
						<th scope="col"  class="manage-column column-date" >Date</th>
						<th scope="col"  class="manage-column column-featured" >Featured</th>
					</tr>
				</tfoot>
			</table>							
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
        Garfielder.Cfg = {
            url_star:'<%:Url.Action("StarTopic","Camp") %>'
        };
    </script>
	<script type="text/javascript" src="<%:Url.JS("jquery.tools-1.2.5.min") %>"></script>
	<script type="text/javascript" src="<%:Url.JS("Admin.TopicList") %>"></script>
	<!--/scripts-->		
</asp:Content>