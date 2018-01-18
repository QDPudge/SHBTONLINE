$(function () {
    //$("#src").css("width", document.body.offsetWidth);
    //$("#src").css("height", document.body.clientHeight);
    //$("#video1").css("width", document.body.offsetWidth);

});
//刷新Frame页面
function RefreshFrame(url) {
    var me = this;
    if (url != "") {
        me.Url = url;
        if (document.body.clientWidth <= 992) {
            $('.left-sidebar').slideUp(300);
        }
        if (url.substring(0, 4).toLowerCase() == "http") {
            me.LoadHttpUrl(url);
        } else {
            me.openIFrame(url, false);
        }
    }
}
function LoadHttpUrl(url) {
    $.ajax({
        method: 'POST',
        url: SZH.Root + "Web/GetLoginInfo",
        data: { url: url },
        success: function (data) {
            var src = data.LoginPage + "?userName=" + data.EmpCode + "&password=" + data.Token;
            $.ajax({
                async: false,
                url: src,
                dataType: 'jsonp',
                jsonp: 'callback',//自定义参数名称
                timeout: 5000,
                success: function (json) {
                    var resultArray = eval(json);
                    window.open(data.SkipPage);
                }
            });

        }
    });

}
/*
iframe 跳转
@parm 
@url 跳转地址 
@iscache 是否缓存 false 不需要缓存
*/
function openIFrame(url, iscache) {
    $("#mainFrame").hide();
    $("#mainFrame").attr("src", "#");

    if (url.substring(0, 4) != "http") {

        $("#i_loading").css("top", $(".left-sidebar").height() / 2).css("left", $("#mainFrame").width() / 2);
        $("#i_loading").show();
    }

    if (iscache == false) {
        //不需要缓存，动态添加参数
        var ts = (new Date).getTime();
        var ret = url.replace(/([?&])_=[^&]*/, '$1_=' + ts);
        url = ret + ((ret === url) ? (/\?/.test(url) ? '&' : '?') + '_=' + ts : '');

    }
    $("#mainFrame").attr("src", url);
    $("#mainFrame").show();

}