$(function () {
    $("#videoBox").css("width", document.body.offsetWidth);
    $("#video1").css("width", document.body.offsetWidth);
    $("#shade").css("width", document.body.offsetWidth);
    $("#shade").css("height", document.body.offsetHeight);
    bindEvent();
    loadNameMatch();

});

function bindEvent() {
    $(window).resize(function () {
        $("#videoBox").css("width", document.body.offsetWidth);
        $("#video1").css("width", document.body.offsetWidth);
        $("#shade").css("width", document.body.offsetWidth);
        $("#shade").css("height", document.body.offsetHeight);
    });

}
function loadNameMatch() {
    var date = new Date().getTime();
    //var url = "https://api.xiaoheihe.cn/game/pubg/get_player_overview/?nickname=RubickLee&region=&season=&heybox_id=767045&imei=356156077945624&os_type=Android&os_version=7.0&version=1.1.14&_time="+date;
    $.ajax({
        type: 'POST',
        data: {},
        url: "GetUserInfo?time=" + date,
        async: false,
        success: function (data) {
            var obj = JSON.parse(data);
        }
    });
}