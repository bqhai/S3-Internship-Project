$(document).ready(function (e) {
    $("#province").click(function () {
        $.ajax({
            url: "https://localhost:44356/api/API_User/GetProvinces",
            method: "get",
            dataType: "json",
        }).done(function (response) {
            var text = "";
            $.each(response, function (key, val) {
                text += "<option value='" + val.Id + "'>" + val.Name + "</option>";
            });
            $('#province').html(text);
        })
    });
});
