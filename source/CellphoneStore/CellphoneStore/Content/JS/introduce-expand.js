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