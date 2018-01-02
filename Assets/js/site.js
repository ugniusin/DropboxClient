$(function() {
    Dropzone.options.fileUploadForm = {
        autoProcessQueue: false,
        maxFilesize: 5,
        addRemoveLinks: true,
        parallelUploads: 5,
        init:function(){
            var self = this;

            self.options.addRemoveLinks = true;
            self.options.dictRemoveFile = 'Remove';

            self.on('sending', function(file, xhr, formData) {

                var selectedLocation = $('input[name=uploadPath]:checked', '#folder-tree').val();
                
                if (selectedLocation) {
                    formData.append("uploadPath", selectedLocation);
                }
            });

            var submitButton = document.querySelector('#upload-button');
            submitButton.addEventListener('click', function () {
                self.processQueue();
            });
        }
    };
});

function getFileSystem(path, url, drawTree, letToOpenFolder) {
    $.ajax({
        url: url,
        method: 'post',
        data: { path : path },
        dataType: 'json',
        success: function (data) {
            if (data.hasOwnProperty('error')) {
                $('#folder-tree').html(
                    '<br><div class="alert alert-danger"><strong>Error! </strong>' + data.error +'</div>'
                );
            } else {
                drawTree(data);
            }
        },
        complete: function(tree) {
            letToOpenFolder(tree);
        }
    });
}