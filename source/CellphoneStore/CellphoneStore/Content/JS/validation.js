function ConfirmPassword() {
    var password = document.getElementById("newPassword");
    var confirmPassword = document.getElementById("confirmPassword");
    if (password.value != confirmPassword.value) {
        confirmPassword.setCustomValidity("Mật khẩu không trùng khớp");
    }
    else {
        confirmPassword.setCustomValidity("");
    }
}

function ValidatePassword() {
    var password = document.getElementById("password");
    if (password.value.length < 6 || password.value.length > 15) {
        password.setCustomValidity("Mật khẩu tối thiểu 6 ký tự và tối đa 15 ký tự")
    }
    else {
        password.setCustomValidity("")
    }
}