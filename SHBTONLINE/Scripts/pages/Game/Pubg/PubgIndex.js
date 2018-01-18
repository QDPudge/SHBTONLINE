$(function () {
    $("#videoBox").css("width", document.body.offsetWidth);
    $("#video1").css("width", document.body.offsetWidth);
    $("#shade").css("width", document.body.offsetWidth);
    $("#shade").css("height", document.body.offsetHeight);
    bindEvent();
});

function bindEvent() {
    $(window).resize(function () {
        $("#videoBox").css("width", document.body.offsetWidth);
        $("#video1").css("width", document.body.offsetWidth);
        $("#shade").css("width", document.body.offsetWidth);
        $("#shade").css("height", document.body.offsetHeight);
    });
}