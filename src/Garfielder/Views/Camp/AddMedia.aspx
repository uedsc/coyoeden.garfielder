<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMXFileEdit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add a new Media
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 id="scSubhead">Upload New Media</h2>
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
<div id="media-items" class="<%:Model.AssertStr(()=>{return Model.Error;},"error") %>">
<%if (Model.Error)
  { %>
  <%:Model.Msg %>
<%}else if (Model.NoFlash && !string.IsNullOrEmpty(Model.Name))
  { %>
  <img alt="" src="<%:Url.Home()+Model.Name1+"_160x100"+Model.Extension %>" />
<%}%>
</div>	

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "media_new"; %>
<%if (!Model.NoFlash)
  { %>
<script type="text/javascript" src="<%:Url.JS("swfupload/swfupload") %>"></script> 
<script type="text/javascript" src="<%:Url.JS("jquery.json-2.2.min") %>"></script>
<%} %> 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
<script type="text/javascript" src="<%:Url.JS("Admin.AddMedia","2") %>"></script>
<script type="text/javascript">
    Garfielder.Cfg={noFlash:<%:Model.NoFlash.ToString().ToLower() %>};
</script> 
<%if (!Model.NoFlash)
  { %>
<script type="text/javascript" src="<%:Url.JS("Admin.SWFUploadController","2") %>"></script>
<script type="text/javascript">
    var swfu;
    window.onload = function () {
        swfu = new SWFUpload({
            // Backend Settings
            upload_url: '<%:Url.Action("SaveMedia","Camp") %>',
            post_params: {
                "UserID": "<%:Model.CurrentUser.Id %>",
                "UserName":"<%:Model.CurrentUser.Name %>"
            },
            // File Upload Settings
            file_size_limit: "2 MB",
            file_types: "*.jpg",
            file_types_description: "JPG Images",
            file_upload_limit: 0,    // Zero means unlimited

            // Event Handler Settings - these functions as defined in Handlers.js
            //  The handlers are not part of SWFUpload but are part of my website and control how
            //  my website reacts to the SWFUpload events.
            swfupload_preload_handler: SWFUpload_Controller.preLoad,
            swfupload_load_failed_handler: SWFUpload_Controller.loadFailed,
            file_queue_error_handler: SWFUpload_Controller.fileQueueError,
            file_dialog_complete_handler: SWFUpload_Controller.fileDialogComplete,
            upload_progress_handler: SWFUpload_Controller.uploadProgress,
            upload_error_handler: SWFUpload_Controller.uploadError,
            upload_success_handler: SWFUpload_Controller.uploadSuccess,
            upload_complete_handler: SWFUpload_Controller.uploadComplete,

            // Button settings
            button_image_url: '<%:Url.Img("XPButtonNoText_160x22.png") %>',
            button_placeholder_id: "spanButtonPlaceholder",
            button_width: 160,
            button_height: 22,
            button_text: '<span class="button">Select Images <span class="buttonSmall">(2 MB Max)</span></span>',
            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
            button_text_top_padding: 1,
            button_text_left_padding: 5,

            // Flash Settings
            flash_url: "<%:Url.Home() %>Assets/js/swfupload/swfupload.swf", // Relative to this file
            flash9_url: "<%:Url.Home() %>Assets/js/swfupload/swfupload_FP9.swf", // Relative to this file

            custom_settings: {
                upload_target: "divFileProgressContainer"
            },

            // Debug Settings
            debug: false
        });
    }
</script>
<%} %>
</asp:Content>
