function setupImageUpload(inputId, actionDetails) {
    'use strict';
    $(inputId).fileupload({
        autoUpload: false,
        url: actionDetails.url,
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        datatype: 'json',
        maxFileSize: 5000000,
    }).on('fileuploadadd', function (e, data) {
        data.context = $('<div/>').appendTo($(this).parent());
        $.each(data.files, function (index, file) {
            var node = $('<p/>').append($('<span/>')).text(file.name);

            if (!index) {
                node.find('button').remove();
                node.append('<br />').append(actionDetails.uploadButton.clone(true).data(data));
            }

            node.appendTo(data.context);
            node.parent().prev().remove();
        });
    }).on('fileuploadprocessalways', function (e, data) {
        var index = data.index, file = data.files[index], node = $(data.context.children()[index]);
        if (file.preview) {
            node.prepend($('<br/>')).prepend(file.preview);
        }

        if (file.error) {
            node.append($('<br/>')).append($('<span class="text-danger">').text(file.error));
        }

        if (index + 1 == data.files.length) {
            data.context.find('button').prop('disabled', !!data.files.error);
        }

    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $(actionDetails.progressId).children('div').css('width', progress + '%');
    }).on('fileuploaddone', function (e, data) {
        if (data.result.files == undefined) {
            data.result = $.parseJSON(data.result);
        }
        $.each(data.result.files, function (index, file) {
            if (file.url) {
                var link = $('<a>').attr('target', '_blank').prop('href', file.url);
                var imageName = $(data.context.children()[index]).wrap(link);

                var span = $("<span>");
                imageName = imageName.wrap(span);

                var thumbnail = $('<img />').attr('src', file.thumbnail_url).css('width', '100px');
                thumbnail.prependTo(imageName.parent()[0]);
            } else if (file.error) {
                var error = $('<span class="text-danger" />').text(file.error);
                $(data.context.children()[index]).append('<br />').append(error);
            }
        });

        actionDetails.doneCallBack(data.result.files);
    }).on('fileuploadfail', function (e, data) {
        $.each(data.files, function (index, file) {
            var error = $('<span class="text-danger" />').text('File upload failed.');
            $(data.context.children()[index]).append('<br/>').append(error);
        });
    }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
}

function readUrl(input, imgId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(imgId).attr("src", e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function setJCrop(image, imageDetails, oldApi) {
    var oldApiExists = oldApi != undefined;
    if (oldApi != undefined) {
        $(oldApi.ui.holder).fadeOut();
        oldApi.destroy();
    }

    if ($(imageDetails.x1Id).val() == "" &&
		$(imageDetails.x2Id).val() == "" &&
		$(imageDetails.y1Id).val() == "" &&
		$(imageDetails.y12d).val() == "") {

        $(imageDetails.x1Id).val(0);
        $(imageDetails.x2Id).val(0);
        $(imageDetails.y1Id).val(0);
        $(imageDetails.y12d).val(0);
    }

    var newApi;
    $(image).Jcrop(
		{
		    bgColor: 'black',
		    aspectRatio: imageDetails.ratio,
		    minSize: [imageDetails.width],
		    bgOpacity: 0.6,
		    boxWidth: 800,
		    boxHeight: 400,
		    onSelect: function (c) {
		        $(imageDetails.x1Id).val(parseInt(c.x));
		        $(imageDetails.x2Id).val(parseInt(c.x2));
		        $(imageDetails.y1Id).val(parseInt(c.y));
		        $(imageDetails.y2Id).val(parseInt(c.y2));
		    },
		    setSelect: [$(imageDetails.x1Id).val(),
						$(imageDetails.y1Id).val(),
						$(imageDetails.x2Id).val(),
						$(imageDetails.y2Id).val()]
		},
		function () {
		    newApi = this;
		    var height = $(this.ui.holder).height();

		    $(this.ui.holder).animate({
		        opacity: 0,
		        height: oldApiExists ? height : 0
		    }, 0);


		    $(this.ui.holder).show();
		    $(this.ui.holder).animate({
		        opacity: 1,
		        height: height
		    }, 800);
		}
	);

    $(image).attr("src", "");

    return newApi;
}

function getUploadButton(buttonSettings) {
    return $("<button />").text(buttonSettings.uploadText).addClass("btn btn-primary").prop("disabled", true)
				.click(function () {
				    var $this = $(this), data = $this.data();
				    $this.off("click").text(buttonSettings.abortText).on("click", function () {
				        $this.remove();
				        data.abort();
				    });

				    data.formData = buttonSettings.formData();
				    data.formData.FullImageName = data.fileInput[0].value;
				    data.submit().always(function () { $this.remove(); });
				    return false;
				});
}

function imageUploadedComplete(files, jcrop_api, imageUploadId, modelIds) {
    "use strict";
    $(imageUploadId).animate({
        opacity: 0,
        height: 0
    }, 1000);
    $(jcrop_api.ui.holder).animate({
        opacity: 0,
        height: 0
    }, 1000);
    setTimeout(function () {
        jcrop_api.destroy();
        $(imagePreviewId).hide();
    }, "1000");

    $(modelIds.ContentType).val(files[0].type);
    $(modelIds.FullImageSize).val(files[0].size);
    $(modelIds.FullImageName).val(files[0].name);
    $(modelIds.CroppedImageName).val(files[0].cropped_image_name);
    $(modelIds.CroppedImageSize).val(files[0].cropped_image_size);
}