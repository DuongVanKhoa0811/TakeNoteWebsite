function add_diary(content) {
    var tmp = content;
    while (tmp.includes("&lt;"))
        tmp = tmp.replace("&lt;", "<");
    while (tmp.includes("&gt;"))
        tmp = tmp.replace("&gt;", ">");
    $("#controlDiary").html(tmp);
}
function save_star() {
    if ($(".starEntry").children(":first").css("color") == "rgb(255, 255, 0)") {
        $(".starEntry").children(":first").css("color", "gray");
    }
    else {
        $(".starEntry").children(":first").css("color", "yellow");
    }
}
function save_entry() {
    val1 = $("#controlDiary").html();
    val2 = $("#titleEntry").html();
    val3 = false
    if ($(this).children().css("color") == "rgb(255, 255, 0)") {
        val3 = true
    }

    $.ajax({
        type: "POST",
        url: "/Main/SaveEntry",
        data: { content: val1, title: val2, star: val3 },
        dataType: "text",
        success: function (result) {
            $(".alert-success").css("animation-name", "showandhide");
        },
        error: function (req, status, error) {
            $(".alert-danger").css("animation-name", "showandhide");
        }
    });
}
function sad_emotion() {
    $("#diary").html($("#diary").html() + "😒")
}
function neural_emotion() {
    $("#diary").html($("#diary").html() + "😐")
}
function smile_emotion() {
    $("#diary").html($("#diary").html() + "😂")
}