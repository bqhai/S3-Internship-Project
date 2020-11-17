function ConfirmPassword() {
    var password = document.getElementById("newPassword");
    var confirmPassword = document.getElementById("confirmPassword");
    if (password.value != confirmPassword.value) {
        confirmPassword.setCustomValidity("Mật khẩu không trùng khớp");
    }
    else {
        confirmPassword.setCustomValidity('');
    }
}