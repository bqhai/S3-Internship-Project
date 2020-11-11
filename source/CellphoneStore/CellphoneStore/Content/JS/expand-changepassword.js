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