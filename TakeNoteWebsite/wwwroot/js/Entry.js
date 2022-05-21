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
function save_image(entryID) {
    if (typeof entryID !== "undefined") 
        entryID = preprocessEntryID(entryID);
    var files = document.getElementById("uploadFileModal").files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    var i = 0;
    for (var value of formData.values()) {
        i += 1;
    }
    if (i == 0)
        alert("You haven't selected any files yet!")
    else {
        val1 = $("#selectedFolderName").children("option:selected").val();
        formData.append("folderName", val1);
        formData.append("entryID", entryID);
        $.ajax({
            type: "POST",
            url: "/Main/NewImage",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert("Success - The image had been save successfully!");
            },
            error: function (req, status, error) {
                alert("Failed - The image had not been saved for some reason!")
            }
        });
    }
}
function save_entry(entryID) {
    if (typeof entryID !== "undefined")
        entryID = preprocessEntryID(entryID);
    var formData = new FormData();
    val1 = $("#controlDiary").html();
    val2 = $("#diary").html();
    val3 = $("#titleEntry").html();
    if (val3 == "") {
        $(".alert-danger-forgot-title").css("animation-name", "showandhide");
    } else {
        val4 = false
        if ($(".starEntry").css("color") == "rgb(255, 255, 0)") {
            val4 = true
        }
        var pathURL = "/Main/NewEntry";
        if (typeof entryID !== "undefined") {
            pathURL = "/Main/SaveEntry";
            formData.append("entryID", entryID);
        }
        formData.append("contentFormat", val1);
        formData.append("content", val2);
        formData.append("title", val3);
        formData.append("star", val4);
        $.ajax({
            type: "POST",
            url: pathURL,
            data: formData,
            processData: false,
            contentType: false,
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
function new_entry() {/*0*/
    location.replace("/Main/Entry/?entryID=");
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
function preprocessEntryID(entryID) {
    return ("0000" + entryID).slice(-5);
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