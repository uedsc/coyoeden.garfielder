/**
*SWFUpload module
*/
Garfielder.AddModule("SWFUpload", {
    init: function (opts) {
        this.opts = opts;
        //swfupload's default configuration
        this.swfuOpts = {
            // Backend Settings
            upload_url: '',
            post_params: null,
            // File Upload Settings
            file_size_limit: "2 MB",
            file_types: "*.jpg",
            file_types_description: "JPG Images",
            file_upload_limit: 0,    // Zero means unlimited

            // Event Handler Settings - these functions as defined in Handlers.js
            //  The handlers are not part of SWFUpload but are part of my website and control how
            //  my website reacts to the SWFUpload events.
            swfupload_preload_handler: this.controller.preLoad,
            swfupload_load_failed_handler: this.controller.loadFailed,
            file_queue_error_handler: this.controller.fileQueueError,
            file_dialog_complete_handler: this.controller.fileDialogComplete,
            upload_progress_handler: this.controller.uploadProgress,
            upload_error_handler: this.controller.uploadError,
            upload_success_handler: this.controller.uploadSuccess,
            upload_complete_handler: this.controller.uploadComplete,

            // Button settings
            button_image_url: '',
            button_placeholder_id: "spanButtonPlaceholder",
            button_width: 160,
            button_height: 22,
            button_text: '<span class="button">Select Images <span class="buttonSmall">(2 MB Max)</span></span>',
            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
            button_text_top_padding: 1,
            button_text_left_padding: 5,

            // Flash Settings
            flash_url: Garfielder.SiteRoot + "Assets/js/swfupload/swfupload.swf", // Relative to this file
            flash9_url: Garfielder.SiteRoot + "Assets/js/swfupload/swfupload_FP9.swf", // Relative to this file

            custom_settings: {
                upload_target: "divFileProgressContainer"
            },

            // Debug Settings
            debug: false
        };
    },
    onLoad: function () {
        window.swfu = new SWFUpload($.extend({}, this.swfuOpts, this.opts.swfuOpts || {}));
    },
    /**
    * swfupload controller
    */
    controller: (function () {
        //private methods
        var p = {};
        p.addImage = function (src, data) {
            var obj = document.getElementById("media-items");
            if (data && data.Error) {
                obj.innerHTML = data.Msg;
                obj.className = "error";
                p.fadeIn(obj, 0);
                return;
            };
            var newImg = document.createElement("img");
            newImg.style.margin = "5px";

            obj.appendChild(newImg);
            if (newImg.filters) {
                try {
                    newImg.filters.item("DXImageTransform.Microsoft.Alpha").opacity = 0;
                } catch (e) {
                    // If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
                    newImg.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + 0 + ')';
                };
            } else {
                newImg.style.opacity = 0;
            };

            newImg.onload = function () {
                p.fadeIn(newImg, 0);
            };
            newImg.src = Garfielder.SiteRoot + src;
        }; //addImage

        p.fadeIn = function (element, opacity) {
            var reduceOpacityBy = 5, rate = 30; // 15 fps

            if (opacity < 100) {
                opacity += reduceOpacityBy;
                if (opacity > 100) {
                    opacity = 100;
                };

                if (element.filters) {
                    try {
                        element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
                    } catch (e) {
                        // If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
                        element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
                    };
                } else {
                    element.style.opacity = opacity / 100;
                };
            };

            if (opacity < 100) {
                setTimeout(function () {
                    p.fadeIn(element, opacity);
                }, rate);
            };
        }; //fadeIn
        /* ******************************************
        *	FileProgress Object
        *	Control object for displaying file info
        * ****************************************** */

        p.FileProgress = function (file, targetID) {
            this.fileProgressID = "divFileProgress";

            this.fileProgressWrapper = document.getElementById(this.fileProgressID);
            if (!this.fileProgressWrapper) {
                this.fileProgressWrapper = document.createElement("div");
                this.fileProgressWrapper.className = "progressWrapper";
                this.fileProgressWrapper.id = this.fileProgressID;

                this.fileProgressElement = document.createElement("div");
                this.fileProgressElement.className = "progressContainer";

                var progressCancel = document.createElement("a");
                progressCancel.className = "progressCancel";
                progressCancel.href = "#";
                progressCancel.style.visibility = "hidden";
                progressCancel.appendChild(document.createTextNode(" "));

                var progressText = document.createElement("div");
                progressText.className = "progressName";
                progressText.appendChild(document.createTextNode(file.name));

                var progressBar = document.createElement("div");
                progressBar.className = "progressBarInProgress";

                var progressStatus = document.createElement("div");
                progressStatus.className = "progressBarStatus";
                progressStatus.innerHTML = "&nbsp;";

                this.fileProgressElement.appendChild(progressCancel);
                this.fileProgressElement.appendChild(progressText);
                this.fileProgressElement.appendChild(progressStatus);
                this.fileProgressElement.appendChild(progressBar);

                this.fileProgressWrapper.appendChild(this.fileProgressElement);

                document.getElementById(targetID).appendChild(this.fileProgressWrapper);
                fadeIn(this.fileProgressWrapper, 0);

            } else {
                this.fileProgressElement = this.fileProgressWrapper.firstChild;
                this.fileProgressElement.childNodes[1].firstChild.nodeValue = file.name;
            }

            this.height = this.fileProgressWrapper.offsetHeight;

        }
        p.FileProgress.prototype.setProgress = function (percentage) {
            this.fileProgressElement.className = "progressContainer green";
            this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
            this.fileProgressElement.childNodes[3].style.width = percentage + "%";
        };
        p.FileProgress.prototype.setComplete = function () {
            this.fileProgressElement.className = "progressContainer blue";
            this.fileProgressElement.childNodes[3].className = "progressBarComplete";
            this.fileProgressElement.childNodes[3].style.width = "";

        };
        p.FileProgress.prototype.setError = function () {
            this.fileProgressElement.className = "progressContainer red";
            this.fileProgressElement.childNodes[3].className = "progressBarError";
            this.fileProgressElement.childNodes[3].style.width = "";

        };
        p.FileProgress.prototype.setCancelled = function () {
            this.fileProgressElement.className = "progressContainer";
            this.fileProgressElement.childNodes[3].className = "progressBarError";
            this.fileProgressElement.childNodes[3].style.width = "";

        };
        p.FileProgress.prototype.setStatus = function (status) {
            this.fileProgressElement.childNodes[2].innerHTML = status;
        };

        p.FileProgress.prototype.toggleCancel = function (show, swfuploadInstance) {
            this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
            if (swfuploadInstance) {
                var fileID = this.fileProgressID;
                this.fileProgressElement.childNodes[0].onclick = function () {
                    swfuploadInstance.cancelUpload(fileID);
                    return false;
                };
            };
        };

        //public methods
        return {
            preLoad: function () {
                if (!this.support.loading) {
                    alert("You need the Flash Player 9.028 or above to use SWFUpload.");
                    return false;
                };
            },
            loadFailed: function () {
                alert("Something went wrong while loading SWFUpload. If this were a real application we'd clean up and then give you an alternative");
            },
            fileQueueError: function (file, errorCode, message) {
                try {
                    var imageName = "error.gif", errorName = "";
                    if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
                        errorName = "You have attempted to queue too many files.";
                    };

                    if (errorName !== "") {
                        alert(errorName);
                        return;
                    };

                    switch (errorCode) {
                        case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                            imageName = "zerobyte.gif";
                            break;
                        case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                            imageName = "toobig.gif";
                            break;
                        case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                        case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                        default:
                            alert(message);
                            break;
                    }; //switch

                    p.addImage("assets/img/" + imageName);

                } catch (ex) {
                    this.debug(ex);
                };
            },
            fileDialogComplete: function (numFilesSelected, numFilesQueued) {
                try {
                    if (numFilesQueued > 0) {
                        this.startUpload();
                    };
                } catch (ex) {
                    this.debug(ex);
                };
            },
            uploadProgress: function (file, bytesLoaded) {
                try {
                    var percent = Math.ceil((bytesLoaded / file.size) * 100);

                    var progress = new p.FileProgress(file, this.customSettings.upload_target);
                    progress.setProgress(percent);
                    if (percent === 100) {
                        progress.setStatus("Creating thumbnail...");
                        progress.toggleCancel(false, this);
                    } else {
                        progress.setStatus("Uploading...");
                        progress.toggleCancel(true, this);
                    }
                } catch (ex) {
                    this.debug(ex);
                }
            },
            uploadSuccess: function (file, serverData) {
                try {
                    var data = $.evalJSON(serverData);

                    p.addImage(data.Name1 + "_160x100" + data.Extension, data);
                    var progress = new p.FileProgress(file, this.customSettings.upload_target);
                    progress.setStatus("Proccess Completed.");
                    progress.toggleCancel(false);

                } catch (ex) {
                    this.debug(ex);
                };
            },
            uploadComplete: function (file) {
                try {
                    /*  I want the next upload to continue automatically so I'll call startUpload here */
                    if (this.getStats().files_queued > 0) {
                        this.startUpload();
                    } else {
                        var progress = new p.FileProgress(file, this.customSettings.upload_target);
                        progress.setComplete();
                        progress.setStatus("All files proccessed.");
                        progress.toggleCancel(false);
                    }
                } catch (ex) {
                    this.debug(ex);
                };
            },
            uploadError: function (file, errorCode, message) {
                var imageName = "error.gif", progress;
                try {
                    switch (errorCode) {
                        case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                            try {
                                progress = new p.FileProgress(file, this.customSettings.upload_target);
                                progress.setCancelled();
                                progress.setStatus("Cancelled");
                                progress.toggleCancel(false);
                            }
                            catch (ex1) {
                                this.debug(ex1);
                            };
                            break;
                        case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                            try {
                                progress = new p.FileProgress(file, this.customSettings.upload_target);
                                progress.setCancelled();
                                progress.setStatus("Stopped");
                                progress.toggleCancel(true);
                            }
                            catch (ex2) {
                                this.debug(ex2);
                            };
                        case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                            imageName = "uploadlimit.gif";
                            break;
                        default:
                            alert(message);
                            break;
                    }; //switch

                    p.addImage("assets/img/" + imageName);

                } catch (ex3) {
                    this.debug(ex3);
                }; //try

            }
        }; //return
    })()//controller
});