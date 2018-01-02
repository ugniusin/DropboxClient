$(function() {
    var tree = $('#tree');
    
    if (tree.length) {
        var url = '/Home/ListFoldersAndFiles';

        getFileSystem("", url, function (response) {
            var list = generateListHtmlForFileSystem(response, 1);
            tree.html("<ul>" + list + "</ul>");
        }, function () {});

        letToOpenFileSystemFolder(tree, url);
    }
});

function openFileSystemFolder(tree, folderElement, icon, url) {
    tree.off('click', '.folder-node');

    var destinationPath = folderElement.data("path") + '/' + folderElement.data("title");

    if (icon.data('status') === 'closed') {
        icon.data('status', 'opened');
        icon.addClass('glyphicon-folder-open').removeClass('glyphicon-folder-close');

        getFileSystem(destinationPath, url, function (response) {
            //drawTree(response);
            var list = generateListHtmlForFileSystem(response, folderElement.data("depth") + 1);
            folderElement.after(list);
        }, function () {
            letToOpenFileSystemFolder(tree, url);
        });
    } else if (icon.data('status') === 'opened') {
        icon.data('status', 'closed');
        icon.addClass('glyphicon-folder-close').removeClass('glyphicon-folder-open');
        $('li[data-path^="' + destinationPath + '"]').remove();

        letToOpenFileSystemFolder(tree, url);
    }
}

function letToOpenFileSystemFolder(tree, url) {
    tree.on('click', '.folder-node', function () {
        var folderElement = $(this).closest('li');
        var icon = $(this);
        openFileSystemFolder(tree, folderElement, icon, url);
    });
}

function generateListHtmlForFileSystem(response, depth)
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