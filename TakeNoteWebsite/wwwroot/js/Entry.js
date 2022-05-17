$(document).ready(function () {
    alert("somethkng");
    $("#settingButton").css("visibility", "visible");
    $(".hidden").css("visibility", "visible");
});
function sad_emotion() {
    $("#diary").html($("#diary").html() + "😒")
}
function neural_emotion() {
    $("#diary").html($("#diary").html() + "😐")
}
function smile_emotion() {
    $("#diary").html($("#diary").html() + "😂")
}