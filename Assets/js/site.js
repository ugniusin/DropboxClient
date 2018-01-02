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

            self.on('addedfile', function (file) {
            });

            self.on('sending', function (file) {
                $('.meter').show();
            });

            var submitButton = document.querySelector('#upload-button');
            submitButton.addEventListener('click', function () {
                self.processQueue();
            });
        }
    };
});

function openFolder(tree, folderElement, icon, url) {
    tree.off('click', '.folder-node');

    var destinationPath = folderElement.data("path") + '/' + folderElement.data("title");

    if (icon.data('status') === 'closed') {
        icon.data('status', 'opened');
        icon.addClass('glyphicon-folder-open').removeClass('glyphicon-folder-close');

        getFileSystem(destinationPath, url, function (response) {
            //drawTree(response);
            var list = generateListHtml(response, folderElement.data("depth") + 1);
            folderElement.after(list);
        }, function () {
            letToOpenFolder(tree, url);
        });
    } else if (icon.data('status') === 'opened') {
        icon.data('status', 'closed');
        icon.addClass('glyphicon-folder-close').removeClass('glyphicon-folder-open');
        $('li[data-path^="' + destinationPath + '"]').remove();

        letToOpenFolder(tree, url);
    }
}

function letToOpenFolder(tree, url) {
    tree.on('click', '.folder-node', function () {
        var folderElement = $(this).closest('li');
        var icon = $(this);
        openFolder(tree, folderElement, icon, url);
    });
}

function generateListHtml(response, depth)
{
    var list = '';
    var padding = depth * '20';

    $.each(response, function (key, object) {
        list += '<li class="list-group-item node-tree"' +
            ' data-path="' + object.path +
            '" data-title="' + object.title +
            '" data-depth="' + depth +
            '">';

        if (object.type === 'folder') {
            list += '<div class="folder-node glyphicon glyphicon-folder-close text-right" style="width: ' +
                padding + 'px;" data-status="' + 'closed' + '"></div>';
        } else {
            list += '<div class="file-node glyphicon" style="width: ' + padding + 'px;"></div>';
        }

        list += '<span class="icon node-icon"></span>' + object.title;

        if (object.type === 'file') {
            var downloadUrl = '/Upload/DownloadFile?' + $.param({path: object.path, fileName: object.title});
            list += '<a class="pull-right" href="' + downloadUrl + '">Download</a>';
        }

        list += '</li>';
    });

    return list;
}

function getFileSystem(path, url, drawTree, letToOpenFolder) {
    $.ajax({
        url: url,
        method: 'post',
        data: { path : path },
        dataType: 'json',
        success: function (data) {
            drawTree(data);
        },
        complete: function(tree) {
            letToOpenFolder(tree);
        }
    });
}