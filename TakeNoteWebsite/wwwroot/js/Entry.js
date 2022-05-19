function add_diary(content) {
    var tmp = content;
    while (tmp.includes("&lt;"))
        tmp = tmp.replace("&lt;", "<");
    while (tmp.includes("&gt;"))
        tmp = tmp.replace("&gt;", ">");
    $("#controlDiary").html(tmp);
}
function save_star() {
    if ($(".starEntry").css("color") == "rgb(255, 255, 0)") {
        $(".starEntry").css("color", "gray");
    }
    else {
        $(".starEntry").css("color", "yellow");
    }
}
function save_image() {
    var files = document.getElementById("uploadFileModal").files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    var i = 0;
    for (var value of formData.values()) {
        alert("Value");
        i += 1;
    }
    $.ajax({
        url: "/Main/NewImage",
        data: formData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (data) {
            alert("Success - The image had been save successfully!");
        },
        error: function (req, status, error) {
            alert("Failed - The image had not been saved for some reason!")
        }
    });
}
function save_entry() {
    val1 = $("#controlDiary").html();
    val2 = $("#titleEntry").html();
    val3 = false
    if ($(this).children().css("color") == "rgb(255, 255, 0)") {
        val3 = true
    }

    if (val2 == "") {
        $(".alert-danger-forgot-title").css("animation-name", "showandhide");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Main/SaveEntry",
            data: { content: val1, title: val2, star: val3 },
            dataType: "text",
            success: function (result) {
                $(".alert-success-entry").css("animation-name", "showandhide");
            },
            error: function (req, status, error) {
                $(".alert-danger-entry").css("animation-name", "showandhide");
            }
        });
    }
}
function prevent_long_title(event) {
    event = event || window.event;
    var tmp = $("#titleEntry").html();
    if (tmp.length > 75) {
        event.preventDefault();
        alert("The title is too long!");
    }
}
function new_entry() {
    location.replace("/Main/Entry/?userID=0");
}
function create_new_folder() {
    val1 = $("#folderModalName").val();
    $.ajax({
        type: "POST",
        url: "/Main/CreateNewFolder",
        data: { folderName : val1 },
        dataType: "text",
        success: function (result) {
            alert("Success - The folder had been saved successfully!");
        },
        error: function (req, status, error) {
            alert("Failed - The folder had not been saved for some reasons!");
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