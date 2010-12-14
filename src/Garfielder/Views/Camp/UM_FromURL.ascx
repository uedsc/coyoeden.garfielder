<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<h3 class="media-title">Add media file from URL</h3>
<div id="media-items">
	<div class="media-item media-blank">
		<h4 class="media-sub-title">Insert an image from another web site</h4>
		<table class="describe">
			<tr>
				<th valign="top" style="width: 130px;" class="label" scope="row">
					<span class="alignleft"><label for="src">Image URL</label></span>
					<span class="alignright"><abbr class="required" title="required" id="status_img">*</abbr></span>
				</th>
				<td class="field"><input type="text" onblur="addExtImage.getImageData()" value="" name="src" id="src" /></td>
			</tr>
			<tr>
				<th valign="top" class="label" scope="row">
					<span class="alignleft"><label for="title">Image Title</label></span>
					<span class="alignright"><abbr class="required" title="required">*</abbr></span>
				</th>
				<td class="field"><input type="text" value="" name="title" id="title" /></td>
			</tr>
			<tr>
				<th valign="top" class="label" scope="row">
					<span class="alignleft"><label for="alt">Alternate Text</label></span>
				</th>
				<td class="field">
					<input type="text" value="" name="alt" id="alt" />
					<p class="help">Alt text for the image, e.g. “The Mona Lisa”</p>
				</td>
			</tr>
			<tr>
				<th valign="top" class="label" scope="row">
					<span class="alignleft"><label for="caption">Image Caption</label></span>
				</th>
				<td class="field"><input type="text" value="" name="caption" id="caption" /></td>
			</tr>
			<tr class="align">
				<th valign="top" class="label" scope="row"><p><label for="align">Alignment</label></p></th>
				<td class="field">
					<input type="radio" checked="checked" onclick="addExtImage.align='align'+this.value" value="none" id="align-none" name="align" />
					<label class="align image-align-none-label" for="align-none">None</label>
					<input type="radio" onclick="addExtImage.align='align'+this.value" value="left" id="align-left" name="align" />
					<label class="align image-align-left-label" for="align-left">Left</label>
					<input type="radio" onclick="addExtImage.align='align'+this.value" value="center" id="align-center" name="align" />
					<label class="align image-align-center-label" for="align-center">Center</label>
					<input type="radio" onclick="addExtImage.align='align'+this.value" value="right" id="align-right" name="align" />
					<label class="align image-align-right-label" for="align-right">Right</label>
				</td>
			</tr>
			<tr>
				<th valign="top" class="label" scope="row">
					<span class="alignleft"><label for="url">Link Image To:</label></span>
				</th>
				<td class="field"><input type="text" value="" name="url" id="url"/>
					<br/>
					<button onclick="document.forms[0].url.value=null" value="" class="btn" type="button">None</button>
					<button onclick="document.forms[0].url.value=document.forms[0].src.value" value="" class="btn" type="button">Link to image</button>
					<p class="help">Enter a link URL or click above for presets.</p>
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<input type="button" value="Insert into Post" onclick="addExtImage.insert()" id="go_button" class="btn" />
				</td>
			</tr>
		</table>
	</div>
</div>
