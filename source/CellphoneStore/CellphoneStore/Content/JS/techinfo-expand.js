function expandTechInfo() {
    var state = document.getElementById("techinfo-expand").textContent;
    if (state == "Xem thêm") {
        document.getElementById("techinfo-expand").textContent = "Thu nhỏ";
        var row = document.querySelectorAll('.table tr');
        for (var item in row) {
            row[item].style.display = "table-row";
        }
    }
    else {
        document.getElementById("techinfo-expand").textContent = "Xem thêm";
        var row = document.querySelectorAll('.table tr');
        for (var i = 0; i < row.length; i++) {
            row[i + 10].style.display = "none";
        }
    }
    
}