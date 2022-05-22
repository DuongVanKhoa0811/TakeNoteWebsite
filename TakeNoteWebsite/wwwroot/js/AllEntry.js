function delete_entry(entryID) {
    val1 = entryID;
    $("." + val1).remove();

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
};
function preprocessEntryID(entryID) {
    return ("0000" + entryID).slice(-5);
}