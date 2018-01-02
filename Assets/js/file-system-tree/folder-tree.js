$(function() {;
    var tree = $('#folder-tree');

    if (tree.length) {
        var url = '/Home/ListFolders';

        getFileSystem("", url, function (response) {
            var list = '<li style="background:#ddf0ff;" class="list-group-item node-tree" data-path=""' +
                ' data-title="root" data-depth="0"><div class="glyphicon glyphicon-folder-open text-muted text-right"' +
                'style="width:20px;margin-right: 15px;" data-status="' + 'closed' + '"></div>' +
                '<span class="icon node-icon"></span>/' +
                '<input type="radio" name="uploadPath" class="pull-right" value="" checked="checked"></li>';
            
            list += generateListHtmlForFileTree(response, 1);
            tree.html("<ul>" + list + "</ul>");
        }, function () {});

        letToOpenFolderTreeFolder(tree, url);
    }
});

function openFolderTreeFolder(tree, folderElement, icon, url) {
    tree.off('click', '.folder-node');

    var destinationPath = folderElement.data("path") + '/' + folderElement.data("title");

    if (icon.data('status') === 'closed') {
        icon.data('status', 'opened');
        icon.addClass('glyphicon-folder-open').removeClass('glyphicon-folder-close');

        getFileSystem(destinationPath, url, function (response) {
            //drawTree(response);
            var list = generateListHtmlForFileTree(response, folderElement.data("depth") + 1);
            folderElement.after(list);
        }, function () {
            letToOpenFolderTreeFolder(tree, url);
        });
    } else if (icon.data('status') === 'opened') {
        icon.data('status', 'closed');
        icon.addClass('glyphicon-folder-close').removeClass('glyphicon-folder-open');
        $('li[data-path^="' + destinationPath + '"]').remove();

        letToOpenFolderTreeFolder(tree, url);
    }
}

function letToOpenFolderTreeFolder(tree, url) {
    tree.on('click', '.folder-node', function () {
        var folderElement = $(this).closest('li');
        var icon = $(this);
        openFolderTreeFolder(tree, folderElement, icon, url);
    });
}

function generateListHtmlForFileTree(response, depth)
{
    var list = '';
    var padding = depth * '20';

    $.each(response, function (key, object) {
        list += '<li class="list-group-item node-tree"' +
            ' data-path="' + object.path +
            '" data-title="' + object.title +
            '" data-depth="' + depth +
            '">';

        list += '<div class="folder-node glyphicon glyphicon-folder-close text-right" style="width: ' +
            padding + 'px;" data-status="' + 'closed' + '"></div>';

        list += '<span class="icon node-icon"></span>' + object.title;
        list += '<input type="radio" name="uploadPath" class="pull-right" value="' + object.path + '/' + object.title;
        list += '"></li>';
    });

    return list;
}
