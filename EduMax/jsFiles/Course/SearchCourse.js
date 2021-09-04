function checkSearchQuery() {
    var text = document.getElementById("searchCourseTxt").value;
    if (text == null || text == "") {
        return false;
    }
    return true;
}