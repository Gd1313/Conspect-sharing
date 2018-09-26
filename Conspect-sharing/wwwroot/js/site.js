// Write your JavaScript code.
//$('#select_all').click(function () {
//    var c = this.checked;
//    $(':checkbox').prop('checked', c);
//});
//function Locking() {
//    $.ajax({
//        type: 'POST',
//        url: "@Url.Action("LockUser", "Account")",
//        data: { arr: getCheckedCheckBoxes() },
//        success: function (data) {
//            if (data == true) {
//                location.reload();
//            }
//            else {
//            }
//        },
//        error: function () {
//            alert("Error");
//        }
//    });
//}
//function Unlocking() {
//    $.ajax({
//        type: 'POST',
//        url: "@Url.Action("UnLockUser", "Account")",
//        data: { arr: getCheckedCheckBoxes() },
//        success: function (data) {
//            if (data == true) {
//                location.reload();
//            }
//            else {
//            }
//        },
//        error: function () {
//            alert("Error");
//        }
//    });
//}
//function Delete() {
//    $.ajax({
//        type: 'POST',
//        url: "@Url.Action("DeleteUser", "Account")",
//        data: { arr: getCheckedCheckBoxes() },
//        success: function (data) {
//            if (data == true) {
//                location.reload();
//            }
//            else {
//            }
//        },
//        error: function () {
//            alert("Error");
//        }
//    });
//}

//function getCheckedCheckBoxes() {
//    var checkboxes = document.getElementsByClassName('checkbox');
//    var checkboxesChecked = [];
//    for (var index = 0; index < checkboxes.length; index++) {
//        if (checkboxes[index].checked) {
//            checkboxesChecked.push(checkboxes[index].value);
//        }
//    }
//    return checkboxesChecked;
//}
    var themes = {

        "default": "lib/bootstrap/dist/css/bootstrap.min.css",
    "amelia": "lib/bootswatch/amelia/bootstrap.min.css",
    "cerulean": "/lib/bootswatch/cerulean/bootstrap.min.css",
    "cosmo": "lib/bootswatch/cosmo/bootstrap.min.css",
    "cyborg": "lib/bootswatch/cyborg/bootstrap.min.css",
    "flatly": "lib/bootswatch/flatly/bootstrap.min.css",
    "journal": "lib/bootswatch/journal/bootstrap.min.css",
    "readable": "lib/bootswatch/readable/bootstrap.min.css",
    "simplex": "lib/bootswatch/simplex/bootstrap.min.css",
    "slate": "lib/bootswatch/slate/bootstrap.min.css",
    "spacelab": "lib/bootswatch/spacelab/bootstrap.min.css",
    "united": "lib/bootswatch/superhero/bootstrap.min.css"
}
        $(function () {
            var themesheet = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
    themesheet.appendTo('head');
            $('.theme-link').click(function () {
                var themeurl = themes[$(this).attr('data-theme')];
    themesheet.attr('href', themeurl);
});
});
