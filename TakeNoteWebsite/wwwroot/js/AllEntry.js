$("button").click(function () {
    var idEntry = $(this).parent().parent().attr("id");
    $("#" + idEntry).remove();
    val1 = idEntry;

    $.ajax({
        type: "POST",
        url: "/Main/DeleteEntry",
        data: { EntryID: val1 },
        dataType: "text",
        success: function (result) {
            alert("The diary had been deleted successfully!");
        },
        error: function (req, status, error) {
            alert("Failed to delete the diary for some reason!");
        }
    });
});