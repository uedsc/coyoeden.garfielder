<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VMXFileEdit>" %>
<%@ Import Namespace="Garfielder.Models" %>
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
	<p class="upload-flash-bypass">You are using the Flash uploader.  Problems?  Try the <a href="?flash=false">Browser uploader</a> instead.</p>	
	<p class="howto">After a file has been uploaded, you can EDIT titles and descriptions in the media library.</p>
</div>
<%} %>
<%if (ViewData["flag"] != null && ViewData["flag"].ToString() == "UploadMedia"){%>
<p class="common-tip">Single Click to view choices!<button id="btn-insert" class="btn">Insert into topic</button></p>
<div id="media-sizes" class="common-tip"></div>
<script id="tpl-media-size" type="text/x-jquery-tmpl">
<input id="ObjSize-${Flag}" type="radio" name="ObjSize-${RefId}" value="${Src}" data-w="${Width}" data-h="${Height}"/>
<label for="ObjSize-${Flag}" title="${Width}x${Height}">${Flag}</label>
</script>        
<%}%> 
<div id="media-items" class="clear <%:Model.Str(Model.Error,"error") %>">
<%if (Model.Error)
  { %>
  <%:Model.Msg %>
<%}else if (Model.NoFlash && !string.IsNullOrEmpty(Model.Name))
  { %>
  <div class="media-item" data-id="<%:Model.Id %>" data-meta="<%:Html.Raw(Model.Meta) %>"><img alt="<%:Model.Name1 %>" src="<%:Model.Url(ImageFlags.S160X100) %>" /></div>
<%}%>
</div>	
<!--/upload ui-->