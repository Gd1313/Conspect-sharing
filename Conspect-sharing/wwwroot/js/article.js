

function createArticle(userId) {
    let data = getFormData();
    $('#sibmit_button').prop("disabled", true);
    data.userId = userId;
    sendRequest("/Manage/CreateArticle", data, function (href) {
        window.location.href = href;
    });
}

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

function getDataFrom() {
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