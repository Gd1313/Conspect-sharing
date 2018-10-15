
var themes = {

    "default": "lib/bootstrap/dist/css/bootstrap.min.css",
    "cosmo": "lib/bootswatch/cosmo/bootstrap.min.css",
    "united": "lib/bootswatch/superhero/bootstrap.min.css"
}

$(function () {
    var themesheet = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
    themesheet.appendTo('head');
    $('.theme-link').click(function () {
        var themeurl = themes[$(this).attr('data-theme')];
        themesheet.attr('href', themeurl);
        if (supports_storage) {
            localStorage.theme = themeurl;
        }
    });
});

function supports_html5_storage() {
    try {
        return 'localStorage' in window && window['localStorage'] !== null;
    } catch (e) {
        return false;
    }
}

var supports_storage = supports_html5_storage();

function set_theme(theme) {

    if (supports_storage) {
        localStorage.theme = theme;
    }
  
}


if (supports_storage) {
    var themesheet = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
    themesheet.appendTo('head');

    var theme = localStorage.theme;
    if (theme) {
        themesheet.attr('href', theme);
    } else {
  
    }
}



$('#username').editable({
    type: 'text',
    pk: 1,
    name: 'username',
    url: '/Manage/ChangeUserName',
    title: 'Enter username'
});