
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
    //$('link[title="main"]').attr('href', theme);
    if (supports_storage) {
        localStorage.theme = theme;
    }
    //console.log(localStorage.theme);
}

/* On load, set theme from local storage */
if (supports_storage) {
    var themesheet = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
    themesheet.appendTo('head');
    //console.log(localStorage.theme);
    var theme = localStorage.theme;
    if (theme) {
        themesheet.attr('href', theme);
    } else {
        /* Don't annoy user with options that don't persist */
        //$('#theme-dropdown').hide();
    }
}

function getDataFromForm() {
    let data = {
        data: simplemde.value(),
        id: $('input[name=Id]').val(),
        description: $('input[name=Description]').val(),
        specialty: $('input[name=Specialty]').val(),
        name: $('input[name=Name]').val(),
        tags: $('input[name=Tags]').tagsinput('items')
    };
    return data;
}

$("#sibmit_button").click(function () {
    let data = getDataFromForm();
    sendRequest("/Manage/CreateArticle", data, function (href) {
        window.location.href = href;
    });
});
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
            if (callback!= null) {
                callback(data);
            }
        }
    });
}

$('#username').editable({
    type: 'text',
    pk: 1,
    name: 'username',
    url: '/Manage/ChangeUserName',
    title: 'Enter username'
});