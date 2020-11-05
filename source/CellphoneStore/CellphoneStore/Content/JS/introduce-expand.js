function expandIntroduce() {
    var state = document.getElementById("introduce-expand").textContent;
    var content = document.querySelectorAll('.state');
    if (state == "Xem thêm") {
        document.getElementById("introduce-expand").textContent = "Thu nhỏ";      
        for (var item in content) {
            content[item].style.display = "block";
        }
    }
    else {
        document.getElementById("introduce-expand").textContent = "Xem thêm";
        for (var item in content) {
            content[item].style.display = "none";
        }
    }
}