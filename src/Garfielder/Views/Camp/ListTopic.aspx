<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<Garfielder.ViewModels.VMCampTopicList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ListTopic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="scSubhead">Topics<%:Html.ActionLink("Add New", "EditTopic", "Camp",null, new { @class = "btn" })%></h2>
    <!--#topicList-->
	<div id="topicList" class="listView">
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
                    <option value="201009">September 2010</option>
                    <option value="201008">August 2010</option>
                    <option value="201007">July 2010</option>
                </select>
                <select class="postform" id="cat" name="cat">
                    <option value="0">View all groups</option>
                    <option value="21">Guitar</option>
                    <option value="1">Life</option>
                    <option value="3">Music</option>
                </select>
                <input type="submit" class="btn" value="Filter" id="btnFilter">
            </div>
        </div>
		<div class="row">
			<table class="widefat post fixed" cellspacing="0">
				<thead>
					<tr>
						<th scope="col" id="cb" class="manage-column column-cb check-column" style=""><input type="checkbox" /></th>
						<th scope="col" id="title" class="manage-column column-title" style="">Title</th>
						<th scope="col" id="author" class="manage-column column-author" style="">Author</th>
						<th scope="col" id="groups" class="manage-column column-groups" style="">Groups</th>
						<th scope="col" id="tags" class="manage-column column-tags" style="">Tags</th>
						<th scope="col" id="comments" class="manage-column column-comments num" style=""><div class="vers"><img alt="Comments" src="images/comment-grey-bubble.png" /></div></th>
						<th scope="col" id="date" class="manage-column column-date" style="">Date</th>
						<th scope="col" id="featured" class="manage-column column-featured" style="">Featured</th>
					</tr>
				</thead>
				<tbody>
                    <% var item=default(VMCampTopicEdit); %>
                    <% for (var i = 0; i < Model.TopicList.Count; i++){ %>
                    <% item = Model.TopicList[i]; %>
                    <tr id='topic-<%:item.Id %>' class='<%:i%2==0?"alt":"" %> status-publish iedit' valign="top">
						<th scope="row" class="check-column"><input type="checkbox" name="topic[]" value="<%:item.Id %>" /></th>
						<td class="post-title column-title">
							<a class="row-title" href="#" title=""><strong><%:item.Title %></strong></a>
						</td>
						<td class="author column-author"><a href="#"><%:item.UserName %></a></td>
						<td class="groups column-groups"><a href='#'><%:item.Groups %></a></td>
						<td class="tags column-tags"><a href='#'><%:item.Tags %></a></td>
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
						<th scope="col"  class="manage-column column-cb check-column" style=""><input type="checkbox" /></th>
						<th scope="col"  class="manage-column column-title" style="">Title</th>
						<th scope="col"  class="manage-column column-author" style="">Author</th>
						<th scope="col"  class="manage-column column-groups" style="">Groups</th>
						<th scope="col"  class="manage-column column-tags" style="">Tags</th>
						<th scope="col"  class="manage-column column-comments num" style=""><div class="vers"><img alt="Comments" src="images/comment-grey-bubble.png" /></div></th>
						<th scope="col"  class="manage-column column-date" style="">Date</th>
						<th scope="col"  class="manage-column column-featured" style="">Featured</th>
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
    <!--#topicList-->	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
    <%this.Model.PageFlag = "topic_index"; %>
</asp:Content>
