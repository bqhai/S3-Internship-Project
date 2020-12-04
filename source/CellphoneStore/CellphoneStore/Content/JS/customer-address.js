$(document).ready(function () {
    var province = $('#province');
    province.append($("<option></option>").val('').html('Chọn Tỉnh/Thành phố'));
    $.ajax({
        //url: 'https://localhost:44356/api/API_User/GetProvinces',
        url: 'http://cellphonesapi.somee.com/api/API_User/GetProvinces',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (index, prov) {
                province.append($("<option></option>").val(prov.Id).html(prov.Name));
            });
        },
        error: function () {
            alert('Error!');  
        }
    });
    $('#province').change(function () {
        var provinceId = parseInt($(this).val());
        if (!isNaN(provinceId)) {
            var district = $('#district');
            district.empty();
            district.append($("<option></option>").val('').html('Vui lòng đợi . . .'));
            $.ajax({
                //url: 'https://localhost:44356/api/API_User/GetDistrictsByProvinceID/' + provinceId,
                url: 'http://cellphonesapi.somee.com/api/API_User/GetDistrictsByProvinceID/' + provinceId,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    district.empty();
                    district.append($("<option></option>").val('').html('Chọn Quận/Huyện'));
                    $.each(data, function (index, dis) {
                        district.append($("<option></option>").val(dis.Id).html(dis.Name));
                    });
                },
                error: function () {
                    alert('Error!');
                }
            });
        }
    });
    $('#district').change(function () {
        var districtId = parseInt($(this).val());
        if (!isNaN(districtId)) {
            var ward = $('#ward');
            ward.empty();
            ward.append($("<option></option>").val('').html('Vui lòng đợi . . .'));
            $.ajax({
                //url: 'https://localhost:44356/api/API_User/GetWardsByDistrictID/' + districtId,
                url: 'http://cellphonesapi.somee.com/api/API_User/GetWardsByDistrictID/' + districtId,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    ward.empty();
                    ward.append($("<option></option>").val('').html('Chọn Phường/Xã'));
                    $.each(data, function (index, w) {
                        ward.append($("<option></option>").val(w.Id).html(w.Name));
                    });
                },
                error: function () {
                    alert('Error!');
                }
            });
        }
    });
    $('#change-address').submit(function () {
        var selectedProvince = $('#province option:selected').text();
        $('#province option:selected').val(selectedProvince);
        var selectedDistrict = $('#district option:selected').text();
        $('#district option:selected').val(selectedDistrict);
        var selectedWard = $('#ward option:selected').text();
        $('#ward option:selected').val(selectedWard);
    });
});