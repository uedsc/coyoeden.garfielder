<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/camp.Master" Inherits="System.Web.Mvc.ViewPage<VMXFileEdit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add a new Media
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 id="scSubhead">Upload New Media</h2>
<div id="flash-upload-ui">
	<div>
		Choose files to upload
        <span id="spanButtonPlaceholder"></span>	
		<span><input type="button" class="btn" value="Cancel Upload" onclick="cancelUpload()" disabled="disabled" id="cancel-upload"/></span>
	</div>
    <div id="divFileProgressContainer" style="height: 75px;"></div>
	<p class="media-upload-size">Maximum upload file size: 8MB</p>
	<p class="upload-flash-bypass">You are using the Flash uploader.  Problems?  Try the <a href="media_new.html?flash=0">Browser uploader</a> instead.</p>	
	<p class="howto">After a file has been uploaded, you can add titles and descriptions.</p>
</div>
<div id="media-items"></div>	

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHead" runat="server">
<%Model.PageFlag = "media_new"; %>
<script type="text/javascript" src="<%:Url.JS("swfupload/swfupload") %>"></script> 
<script type="text/javascript" src="<%:Url.JS("jquery.json-2.2.min") %>"></script> 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFoot" runat="server">
<script type="text/javascript" src="<%:Url.JS("Admin.AddMedia") %>"></script> 
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
            swfupload_preload_handler: preLoad,
            swfupload_load_failed_handler: loadFailed,
            file_queue_error_handler: fileQueueError,
            file_dialog_complete_handler: fileDialogComplete,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,

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
</asp:Content>
