// Write your JavaScript code.

$(function() {
    Dropzone.options.fileUploadForm = {
        autoProcessQueue: false,
        maxFilesize: 5,
        addRemoveLinks: true,
        parallelUploads: 5,
        init:function(){
            var self = this;

            self.options.addRemoveLinks = true;
            self.options.dictRemoveFile = "Remove";

            self.on("addedfile", function (file) {
            });

            self.on("sending", function (file) {
                $('.meter').show();
            });

            var submitButton = document.querySelector("#upload-button");
            submitButton.addEventListener("click", function () {
                self.processQueue();
            });
        }
    };
});