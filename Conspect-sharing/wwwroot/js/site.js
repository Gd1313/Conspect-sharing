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