<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VMUploadMedia>" %>
<form id="filter" method="get" action="">
	<div id="mediaList" class="listView">
		<div class="row acts1">
			<ul class="l">
				<li><a href="javascript://">All<span class="cnt">&nbsp;(<%:Model.FileList.Count %>)&nbsp;</span></a> | </li>
				<li><a href="javascript://" class="now">Image<span class="cnt">&nbsp;(<%:Model.FileList.Count %>)&nbsp;</span></a></li>
			</ul>
			<div class="r sbox">
				<label for="txtMDSearch" class="txtForScreen">Search Media</label>
				<input type="text" id="txtMDSearch" name="sterm"/>
				<input type="submit" class="btn" value="Search Media"/>	
			</div>
		</div>
		<div class="row tablenav">
			<div class="actions">
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
						<th style="" class="manage-column column-icon" id="icon" scope="col"></th>										
						<th scope="col" class="manage-column column-title" style="">File</th>
						<th scope="col" id="date" class="manage-column column-date" style="">Date</th>
						<th scope="col" class="manage-column column-act" style="">&nbsp;</th>
					</tr>
				</thead>
				<tbody>
				<% var item=default(VMXFileEdit); %>
				<% for (var i = 0; i < Model.FileList.Count;i++ ){ %>
				<% item = Model.FileList[i]; %>
					<tr id='md-<%:item.Id %>' class='<%:i%2==0?"alt":"" %> status-publish iedit' valign="top">
						<td class="column-icon media-icon">				
							<a title="Edit" href="#">
								<img title="" alt="" class="attachment-64x64" src="<%:Url.Home()+item.Name1+"_64x64"+item.Extension %>"/>	
							</a>
						</td>										
						<td class="md-title column-title">
							<a class="row-title" href="#" title=""><strong><%:item.Title %></strong></a>
							<p><%:item.Extension %></p>
						</td>
						<td class="date column-date"><abbr title=""><%:item.CreatedAt.ToString("yyyy/MM/dd") %></abbr></td>					
						<td class="act"><a href="javascript://">Show</a></td>
					</tr>	                 
				<% } %>																			
				</tbody>
				<tfoot>
					<tr>
						<th style="" class="manage-column column-icon" id="icon1" scope="col"></th>
						<th scope="col"  class="manage-column column-title" style="">File</th>
						<th scope="col"  class="manage-column column-date" style="">Date</th>
						<th scope="col" class="manage-column column-act" style="">&nbsp;</th>
					</tr>
				</tfoot>
			</table>							
		</div>															
	</div>	   
</form>
