$(function () {
    autoheight();
    //$("#src").css("width", document.body.clientWidth);
    //$("#src").css("height", document.body.scrollHeight - 60);
    //$(window).resize(function () {
    //    $("#src").css("width", document.body.clientWidth);
    //    $("#src").css("height", document.body.scrollHeight );
    //});
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

/************************
/*	WINDOW RESIZE
/************************/

$(window).bind("resize", autoheight);

function resizeResponse() {
    if ($(window).width() < (992 - scrollWidth)) {
        if ($('.left-sidebar').hasClass('minified')) {
            $('.left-sidebar').removeClass('minified');
            $('.left-sidebar').addClass('init-minified');
        }

    } else {
        if ($('.left-sidebar').hasClass('init-minified')) {
            $('.left-sidebar')
            .removeClass('init-minified')
            .addClass('minified');
        }
    }

    autoheight();
}

/**
 * 获取滚动条宽度
 * @returns int
 */
function getScrollbarWidth () {
    var oP = document.createElement('p'),
    styles = {
        width: '100px',
        height: '100px',
        overflowY: 'scroll'
    }, i;

    for (i in styles) oP.style[i] = styles[i];
    document.body.appendChild(oP);
    var scrollbarWidth = oP.offsetWidth - oP.clientWidth;
    $(oP).remove();
    return scrollbarWidth;
}

/**
 * 自动设定高度
 */
function autoheight() {
    var minHeight = document.body.clientHeight - $('.top-bar').outerHeight(true)+68;//- $('footer.footer').outerHeight(true)
    //获取滚动条宽度
    var scrollWidth = getScrollbarWidth();
    // //菜单若虚移除滚动条 注释下行代码 left sidebar
    if ($(window).width() >= (992 - scrollWidth)) {
        //$(".left-sidebar").css({ 'height': minHeight });

        //setTimeout(function () {
        //    $('#for-slimscroll-wrapper').slimScroll({
        //        height: "auto",
        //        wheelStep: 5
        //    });
        //}, 300);
    }
    $('#src').css({ 'max-height': minHeight, height: minHeight });

}

function Attendance() {
    layer.open({
        type: 1,
        title: false,
        area: ['600', '800'],
        zIndex: 99999,
        content: $('#XXS'), 
        success: function (layero, index) {
            $("#XXS").css("display", "block");
        },
        cancel: function (index, layero) {
            $("#XXS").css("display", "none");
            layer.close(index)
        }
    });
}

function LoadAttendInfo() {


}