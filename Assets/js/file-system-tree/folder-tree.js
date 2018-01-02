$(function() {;
    var tree = $('#folder-tree');

    if (tree.length) {
        var url = '/Home/ListFolders';

        getFileSystem("", url, function (response) {
            var list = generateListHtml(response, 1);
            tree.html("<ul>" + list + "</ul>");
        }, function () {});

        letToOpenFolder(tree, url);
    }
});

/*function openFolder(tree, folderElement, icon) {
    tree.off('click', '.folder-node');

    var destinationPath = folderElement.data("path") + '/' + folderElement.data("title");

    if (icon.data('status') === 'closed') {
        icon.data('status', 'opened');
        icon.addClass('glyphicon-folder-open').removeClass('glyphicon-folder-close');

        getFileSystem(destinationPath, function (response) {
            //drawTree(response);
            var list = generateListHtml(response, folderElement.data("depth") + 1);
            folderElement.after(list);
        }, function () {
            letToOpenFolder(tree);
        });
    } else if (icon.data('status') === 'opened') {
        icon.data('status', 'closed');
        icon.addClass('glyphicon-folder-close').removeClass('glyphicon-folder-open');
        $('li[data-path^="' + destinationPath + '"]').remove();

        letToOpenFolder(tree);
    }
}

function letToOpenFolder(tree) {
    tree.on('click', '.folder-node', function () {
        var folderElement = $(this).closest('li');
        var icon = $(this);
        openFolder(tree, folderElement, icon);
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
            list += '<div class="folder-node glyphicon glyphicon-folder-close text-right" style="width:' +
                padding + 'px;" data-status="' + 'closed' + '"></div>';
        }

        list += '<span class="icon node-icon"></span>' + object.title + '</li>';
    });

    return list;
}
*/