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
    
    var tree = $('#tree');
    
    getFileSystem("", function (response) {
        var list = generateListHtml(response, 1);
        tree.html("<ul>" + list + "</ul>");
    });

    tree.on('click', '.folder-node', function(element) {
        var folderElement = $(this).closest('li');
        var icon = $(this);
        
        var destinationPath = folderElement.data("path") + '/' + folderElement.data("title");
        
        if (icon.data('status') === 'closed') {
            icon.data('status', 'opened');
            icon.addClass('glyphicon-folder-open').removeClass('glyphicon-folder-close');
            
            getFileSystem(destinationPath, function (response) {
                //drawTree(response);
                var list = generateListHtml(response, folderElement.data("depth") + 1);
                folderElement.after(list);
            })
        } else if (icon.data('status') === 'opened') {
            icon.data('status', 'closed');
            icon.addClass('glyphicon-folder-close').removeClass('glyphicon-folder-open');
            $('li[data-path^="' + destinationPath + '"]').remove();
        }
    });
});

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

        if (object.type == 'folder') {
            list += '<div class="folder-node glyphicon glyphicon-folder-close text-right" style="width: ' +
                padding + 'px; margin-right:10px;" data-status="' + 'closed' + '"></div>';
        } else {
            list += '<div class="glyphicon" style="width: ' + padding + 'px; margin-right:10px;"></div>';
        }

        list += '<span class="icon node-icon"></span>' + object.title + '</li>';
    });

    list += '';
    
    return list;
}


function makeTree(tree, response) {
    
    $.each(response, function(key, object) {

        if (object.type == 'file') {
            tree.push({
                text: object.title
            });
        } else if (object.type == 'folder') {
            tree.push({
                text: object.title,
                nodes: makeTree([], object.items)
            });
        }
    });
    
    return tree;
}

function getFileSystem(path, drawTree) {

    $.ajax({
        url: '/Home/ListFoldersAndFiles',
        method: 'post',
        data: { path : path },
        dataType: 'json',
        success: function (data) {

            drawTree(data);
            
            return data;
        }
    });
}