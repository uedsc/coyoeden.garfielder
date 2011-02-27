<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Garfielder.ViewModels.VMBase>" %>
<!--campMenu-->
<ul id="scMenus">
	<li id="menu_home" class="menu_first menu_hasSub menu_open menu_top menu_top_last<%:this.Model.Str(this.Model.PageFlag=="camp_index"," menu_hotSub") %>">
		<div class="menu_bg"></div>
		<div class="menu_trigger"></div>
		<a href="javascript://" tabindex="1" class="<%:this.Model.Str(this.Model.PageFlag=="camp_index","menu_hotSub ","menu_hasSub") %> menu_top menu_first">Camper</a>
		<div class="menu_sub">
			<ul class="menu_sub_">
				<li class="menu_first<%:this.Model.Str(this.Model.PageFlag=="camp_index"," now") %>">
					<%:Html.ActionLink("Summary","Index","Camp",null,new {tabIndex="1"}) %>
				</li>
			</ul>
		</div>
	</li>
	<li class="menu_split"><a href="javascript://">&nbsp;</a></li>
	<li id="menu_topic" class="menu_hasSub menu_open menu_top menu_top_first<%:this.Model.Str(this.Model.PageFlag.In("topic_index,topic_add,tag_index")," menu_hotSub") %>">
		<div class="menu_bg"></div>
		<div class="menu_trigger"></div>
		<a href="javascript://" tabindex="2" class="<%:this.Model.Str(this.Model.PageFlag.In("topic_index,topic_add,tag_index"),"menu_hotSub ","menu_hasSub") %> menu_top menu_first">Topics</a>
		<div class="menu_sub">
			<ul class="menu_sub_">
				<li class="<%:this.Model.Str(this.Model.PageFlag=="topic_index","now") %>"><%:Html.ActionLink("Topics","ListTopic","Camp",null,new {tabIndex="2"}) %></li>
				<li class="<%:this.Model.Str(this.Model.PageFlag=="topic_add","now") %>"><%:Html.ActionLink("Add Topic", "EditTopic", "Camp", new { id = "" }, new { tabIndex = "3" })%></li>
				<li class="<%:this.Model.Str(this.Model.PageFlag=="group_index","now") %>"><%:Html.ActionLink("Groups","ListGroup","Camp",null,new {tabIndex="4"}) %></li>
				<li class="<%:this.Model.Str(this.Model.PageFlag=="tag_index","now") %>"><%:Html.ActionLink("Tags","ListTag","Camp",null,new {tabIndex="5"}) %></li>
			</ul>
		</div>                    
	</li>
	<li id="menu_media" class="menu_hasSub menu_open menu_top<%:this.Model.Str(this.Model.PageFlag.In("media_index,media_new")," menu_hotSub") %>">
		<div class="menu_bg"></div>
		<div class="menu_trigger"></div>
		<a href="javascript://" class="<%:this.Model.Str(this.Model.PageFlag.In("media_index,media_new"),"menu_hotSub ","menu_hasSub") %> menu_top">Media</a>
		<div class="menu_sub">
			<ul class="menu_sub_">
				<li class="<%:this.Model.Str(this.Model.PageFlag=="media_index","now") %>"><%:Html.ActionLink("Library","ListFile","Camp",null,null) %></li>
				<li class="<%:this.Model.Str(this.Model.PageFlag=="media_new","now") %>"><%:Html.ActionLink("Add Media","AddMedia","Camp",null,null) %></li>                            
			</ul>
		</div>
	</li>
	<li id="menu_comment" class="menu_top menu_top_last">
		<div class="menu_bg"></div>
		<a href="javascript://" class="menu_top menu_top_last">Comments</a>
	</li>
	<li class="menu_split"><a href="javascript://">&nbsp;</a></li>
	<li id="menu_user" class="menu_hasSub menu_open menu_top menu_top_first menu_top_last<%:this.Model.Str(this.Model.PageFlag.In("user_index,user_edit")," menu_hotSub") %>">
		<div class="menu_bg"></div>
		<div class="menu_trigger"></div>
		<a href="javascript://" tabindex="2" class="<%:this.Model.Str(this.Model.PageFlag.In("user_index,user_edit"),"menu_hotSub ","menu_hasSub") %>b menu_top menu_first">Users</a>
		<div class="menu_sub">
			<ul class="menu_sub_">
				<li class="<%:this.Model.Str(this.Model.PageFlag=="user_index","now") %>"><%:Html.ActionLink("Users","ListUser","Camp",null,null) %></li>
				<li class="<%:this.Model.Str(this.Model.PageFlag=="user_edit","now") %>"><%:Html.ActionLink("Add User","EditUser","Camp",null,null) %></li>
				<li class="<%:this.Model.Str(this.Model.PageFlag=="user_profile","now") %>"><%:Html.ActionLink("Your profile","UserProfile","Camp",null,null) %></li>
			</ul>
		</div>                    
	</li>
</ul>
<!--/campMenu-->
