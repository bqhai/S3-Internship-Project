function expandChangePassword() {
    var checkBox = document.getElementById("changePassword");

    var content = document.querySelectorAll("#changePasswordContainer")
    var oldPass = document.querySelector("#oldPassword");
    var newPass = document.querySelector("#newPassword");
    var confirmPass = document.querySelector("#confirmPassword");
    var btnConfirmPass = document.querySelector("#btnConfirmPass");

    if (checkBox.checked == true) {
        oldPass.disabled = newPass.disabled = confirmPass.disabled =  false;
        btnConfirmPass.style.display = "block";      
        for (var item in content) {        
            content[item].style.display = "block";
        }
    }
    else {
        oldPass.disabled = newPass.disabled = confirmPass.disabled =  true;
        btnConfirmPass.style.display = "none";        
        for (var item in content) {
            content[item].style.display = "none";
        }
    } 
}

function expandIntroduce() {
    var state = document.getElementById("introduce-expand").textContent;
    var content = document.querySelectorAll('.state');
    if (state == "Xem thêm đánh giá") {
        document.getElementById("introduce-expand").textContent = "Thu nhỏ";
        for (var item in content) {
            content[item].style.display = "block";
        }
    }
    else {
        document.getElementById("introduce-expand").textContent = "Xem thêm đánh giá";
        for (var item in content) {
            content[item].style.display = "none";
        }
    }
}

function expandTechInfo() {
    var state = document.getElementById("techinfo-expand").textContent;
    var row = document.querySelectorAll('.table tr');
    if (state == "Xem thêm") {
        document.getElementById("techinfo-expand").textContent = "Thu nhỏ";
        for (var item in row) {
            row[item].style.display = "table-row";
        }
    }
    else {
        document.getElementById("techinfo-expand").textContent = "Xem thêm";
        for (var i = 0; i < row.length; i++) {
            row[i + 10].style.display = "none";
        }
    }

}

function expandChangeAddress() {
    var content = document.querySelector("#change-address");
    content.style.display = "block";
}