<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Garfielder.Models" %>
<form id="filter" method="get" action="">
	<div id="mediaList" class="listView">
		<div class="row acts1">
			<ul class="l">
				<li><a href="javascript://">All<span class="cnt">&nbsp;(<%:Model.files.Count %>)&nbsp;</span></a> | </li>
				<li><a href="javascript://" class="now">Image<span class="cnt">&nbsp;(<%:Model.files.Count%>)&nbsp;</span></a></li>
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
            <%if (Model.mode == "list"){%>
			<table id="mediaList-main" class="widefat fixed" cellspacing="0">
				<thead>
					<tr>
						<th class="manage-column column-icon" id="icon" scope="col"></th>										
						<th scope="col" class="manage-column column-title">File</th>
						<th scope="col" class="manage-column col-meta">Meta</th>
                        <th scope="col" class="manage-column col-acts">Actions</th>
					</tr>
				</thead>
				<tbody>
				<%
                  var item = default(VMXFileEdit);%>
				<%
                  for (var i = 0; i < Model.files.Count; i++)
                  {%>
				<%
                      item = Model.files[i];%>
					<tr id='md-<%:item.Id%>' class='<%:i%2 == 0 ? "alt" : ""%> status-publish iedit' valign="top">
						<td class="column-icon media-icon">				
							<a title="" href="javascript://">
								<img title="" alt="" class="attachment-64x64" src="<%:item.Url(ImageFlags.S64X64)%>"/>	
							</a>
						</td>										
						<td class="md-title column-title">
							<a class="row-title" href="javascript://" title=""><strong><%:item.Title%></strong></a>
							<p><%:item.Extension%></p>
                            <p class="tip"><%:item.CreatedAt.ToString("yyyy/MM/dd")%></p>
						</td>
						<td class="col-meta">
                            <div id="file-meta-<%:item.Id %>">
                                Sizes:
                                <select class="file-meta-sizes">
                                <%item.MetaData.Thumbs.ForEach(x =>{%>
                                    <option value="<%:x.Src %>" data-w="<%:x.Width %>" data-h="<%:x.Height %>"><%:x.Flag %></option>
                                <%});%>
                                </select>
                            </div>
                        </td>
                        <td class="col-acts">
                            <a href="javascript://" class="btn btn-insert" title="Insert into the topic">Insert</a>
							<a href="javascript://" class="btn btn-logo" title="Set as topic cover">As Logo</a>
							<a href="javascript://" class="btn btn-attach" title="Set as an attachment!" rel="<%:item.Id %>">Attachment</a>
                        </td>
					</tr>	                 
				<%
                  }%>																			
				</tbody>
				<tfoot>
					<tr>
						<th class="manage-column column-icon" id="icon1" scope="col"></th>
						<th scope="col"  class="manage-column column-title">File</th>
						<th scope="col" class="manage-column col-meta">Meta</th>
                        <th scope="col" class="manage-column col-acts">Actions</th>
					</tr>
				</tfoot>
			</table>							
		    <%}else{%>
            <ul id="list-file" class="pp list-thumb">
				<%
                  var item = default(VMXFileEdit);%>
				<%
                  for (var i = 0; i < Model.files.Count; i++)
                  {%>
				<%item = Model.files[i];%>
                <li>
                    <a class="box-c" title="" href="javascript://">
					    <img title="<%:item.Title%>" alt="" class="attachment-64x64" src="<%:item.Url(ImageFlags.S64X64)%>"/>	
					</a>
                    <span class="file-ext"><%:item.Extension%></span>
                    <span class="file-date"><%:item.CreatedAt.ToString("yyyy/MM/dd")%></span>
                    <div class="file-acts">
                        <a href="javascript://" class="file-del" rel="<%:item.Id %>">Delete</a>
                    </div>
                </li>                 
				<%
                  }%>	                
            </ul>
            <%}%>
        </div>															
	</div>	   
</form>

