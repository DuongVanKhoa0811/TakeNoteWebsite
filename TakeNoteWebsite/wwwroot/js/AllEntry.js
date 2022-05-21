$(".buttonDeleteEntry").click(function () {
    var idEntry = $(this).parent().parent().attr("id");
    val1 = idEntry;
    $("#" + idEntry).remove();

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