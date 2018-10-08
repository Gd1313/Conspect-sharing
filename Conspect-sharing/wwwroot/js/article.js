function getDataFromForm() {
    let data = {
        text: simplemde.value(),
        id: $('input[name=Id]').val(),
        description: $('input[name=Description]').val(),
        specialty: $('input[name=Specialty]').val(),
        name: $('input[name=Name]').val(),
        tags: $('input[name=Tags]').tagsinput('items')
    };
    return data;
}

function createArticle(userId) {
    let data = getFormData();
    $('#sibmit_button').prop("disabled", true);
    data.userId = userId;
    sendRequest("/Manage/CreateArticle", data, function (href) {
        window.location.href = href;
    });
}

//$("#sibmit_button").click(function () {
//    let data = getDataFromForm();
//    sendRequest("/Manage/CreateArticle", data, function (href) {
//        window.location.href = href;
//    });
//});
var constraints = {
    description: {
        presence: true,
        length: {
            minimum: 3,
            maximum: 20
        }
    },
    specialty: {
        presence: true,
        length: {
            minimum: 3,
            maximum: 20
        }
    },
    name: {
        presence: true,
        length: {
            minimum: 3,
            maximum: 20
        }
    }
};

//if (validate({ description: data.description, specialty: data.specialty, name: data.name }, constraints, { format: "flat" }) != undefined)
//{
//    //let dep_span = document.getElementById("des_span");
//    //let spec_span = document.getElementById("spec_span");
//    //let name_span = document.getElementById("name_span");
//    let err = validate({ description: data.description, specialty: data.specialty, name: data.name }, constraints, { format: "flat" });
//    var spans = ["des_span","spec_span", "name_span"];
//    //$("#dep_span").text(validate({ description: data.description }, constraints, { format: "flat" }));
//    //$("#spec_span").text(validate({ specialty: data.specialty }, constraints, { format: "flat" }));
//    //$("#name_span").text(validate({ name: data.name }, constraints, { format: "flat" }));
//    for (var i = 0; i < length; i++) {
//        $("#"+spans[i]).text(err[i]);
//    }
//} else {
//    console.log(validate({ description: data.description, specialty: data.specialty, name: data.name }, constraints, { format: "flat" }))

//    });
//}


function sendRequest(url, data, callback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: function (data) {
            if (callback != null) {
                callback(data);
            }
        }
    });
}