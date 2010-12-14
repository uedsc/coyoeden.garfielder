<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VMXFileEdit>" %>
<!--upload ui-->
<%if (Model.NoFlash)
  { %>
<div id="html-upload-ui" class="upload-ui">
	<form id="frmFile" name="frmFile" action="<%:Url.Action("AddMedia","Camp") %>" method="post" enctype="multipart/form-data">
		<input type="file" name="File" id="txtFile" size="40" />
		<input type="submit" value="Upload" class="btn" />
		<input type="hidden" name="UserID" value="<%:Model.CurrentUser.Id %>" />
		<input type="hidden" name="UserName" value="<%:Model.CurrentUser.Name %>" />
		<p class="upload-html-bypass">You are using the Browser uploader.  Try the <a href="?flash=true">Flash uploader</a> instead.</p> 
	</form>
</div>
<%}
  else
  { %>
<div id="flash-upload-ui" class="upload-ui">
	<div>
		Choose files to upload
		<span id="spanButtonPlaceholder"></span>	
		<span><input type="button" class="btn" value="Cancel Upload" onclick="cancelUpload()" disabled="disabled" id="cancel-upload"/></span>
	</div>
	<div id="divFileProgressContainer" class="upload-ui-progress"></div>
	<p class="media-upload-size">Maximum upload file size: 8MB</p>
	<p class="upload-flash-bypass">You are using the Flash uploader.  Problems?  Try the <a href="?flash=false">Browser uploader</a> instead.</p>	
	<p class="howto">After a file has been uploaded, you can add titles and descriptions.</p>
</div>
<%} %>
<div id="media-items" class="<%:Model.Str(Model.Error,"error") %>">
<%if (Model.Error)
  { %>
  <%:Model.Msg %>
<%}else if (Model.NoFlash && !string.IsNullOrEmpty(Model.Name))
  { %>
  <img alt="" src="<%:Url.Home()+Model.Name1+"_160x100"+Model.Extension %>" />
<%}%>
</div>	
<!--/upload ui-->
