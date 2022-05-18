// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".settingButton").click(function () {
    if ($(".leftMain").css("width") == "0px") {
        $(".leftMain").animate({
            width: "25%"
        });
        $(".rightMain").animate({
            width: "75%"
        });
    }
    else {
        $(".leftMain").animate({
            width: "0%"
        });
        $(".rightMain").animate({
            width: "100%"
        });
    }
    $(".rightMain").animate({ left: '250px' });
});
$(".bold").click(function () {
    if ($("#controlDiary").html().includes("<b>")) {
        $("#controlDiary").html($("#controlDiary").html().replace("<b>", "").replace("</b>", ""));
    }
    else {
        $("#controlDiary").html("<b>" + $("#controlDiary").html() + "</b>");
    }
});
$(".italic").click(function () {
    if ($("#controlDiary").html().includes("<i>")) {
        $("#controlDiary").html($("#controlDiary").html().replace("<i>", "").replace("</i>", ""));
    }
    else {
        $("#controlDiary").html("<i>" + $("#controlDiary").html() + "</i>");
    }
});
$(".underline").click(function () {
    if ($("#controlDiary").html().includes("<u>")) {
        $("#controlDiary").html($("#controlDiary").html().replace("<u>", "").replace("</u>", ""));
    }
    else {
        $("#controlDiary").html("<u>" + $("#controlDiary").html() + "</u>");
    }
});
$(".strikeThrough").click(function () {
    if ($("#controlDiary").html().includes("<s>")) {
        $("#controlDiary").html($("#controlDiary").html().replace("<s>", "").replace("</s>", ""));
    }
    else {
        $("#controlDiary").html("<s>" + $("#controlDiary").html() + "</s>");
    }
});
$(".timestamp").click(function () {
    $("#diary").html($("#diary").html() + formatDate(new Date()).toString() + " ");
});
function formatDate(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    var day = date.toString().split(" ");
    var tmp = day[0].toString() + ". ";
    return (tmp + (date.getMonth() + 1).toString() + "/" + date.getDate() + "/" + date.getFullYear() + " at " + strTime);
}
$("#tooltipTextColourRed").click(function () {
    $("#diary").css("color", "red");
});
$("#tooltipTextColourGreen").click(function () {
    $("#diary").css("color", "green");
});
$("#tooltipTextColourBlue").click(function () {
    $("#diary").css("color", "blue");
});
$("#tooltipTextColourBlack").click(function () {
    $("#diary").css("color", "black");
});
$(".background-colour").click(function () {
    alert("This function will be update later!");
});
$("#font1").click(function () {
    $("#diary").css("font-family", "Macondo");
});
$("#font2").click(function () {
    $("#diary").css("font-family", "Oleo Script Swash Caps");
});
$("#font3").click(function () {
    $("#diary").css("font-family", "Mukta");
});
$("#font4").click(function () {
    $("#diary").css("font-family", "Quicksand");
});
$("#size12px").click(function () {
    $("#diary").css("font-size", "12px");
});
$("#size14px").click(function () {
    $("#diary").css("font-size", "14px");
});
$("#size16px").click(function () {
    $("#diary").css("font-size", "16px");
});
$("#size18px").click(function () {
    $("#diary").css("font-size", "18px");
});
$(".numbered-list").click(function () {
    var tmp = $("#diary").html();
    if (tmp.includes("<ul>")) {
        list("<ul>", "</ul>");
        list("<ol>", "</ol>");
    } else {
        list("<ol>", "</ol>");
    }
});
$(".bulleted-list").click(function () {
    var tmp = $("#diary").html();
    if (tmp.includes("<ol>")) {
        list("<ol>", "</ol>");
        list("<ul>", "</ul>");
    }
    else {
        list("<ul>", "</ul>");
    }
});
function list(startList, endList) {
    var tmp = $("#diary").html();
    if (tmp.includes(startList)) {
        tmp = tmp.replace(startList, "");
        tmp = tmp.replace(endList, "");
        var listString = tmp.split("<li>");
        if (listString.length > 1) {
            var result = listString[0].replace("</li>", "");
            var result = listString[1].replace("</li>", "");
            for (let i = 2; i < listString.length; i++) {
                listString[i] = listString[i].replace("</li>", "");
                result += "<div>" + listString[i] + "</div>";
            }
            $("#diary").html(result);
        }
    }
    else {
        var listString = tmp.split("<div>");
        if (listString.length != 0) {
            var result = startList + "<li>" + listString[0] + "</li>";
            for (let i = 1; i < listString.length; i++) {
                listString[i] = listString[i].replace("</div>", "");
                result += "<li>" + listString[i] + "</li>";
            }
            result += endList;
            $("#diary").html(result);
        }
    }
}