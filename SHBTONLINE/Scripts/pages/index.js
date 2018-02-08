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
        area: ['533px', '500px'],
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
    debugger;
    $.ajax({
        type: 'POST',
        data: {},
        url: "/PersonOperation/Attendance/GetAttendance",
        async: false,
        success: function (signList) {
            debugger;
            //获取当前时间
            var current = new Date();
            var str = calUtil.drawCal(current.getFullYear(), current.getMonth() + 1, signList);
            $("#calendar").html(str);
        }
    });
}

var calUtil = {
    getDaysInmonth: function (iMonth, iYear) {
        var dPrevDate = new Date(iYear, iMonth, 0);
        return dPrevDate.getDate();
    },
    bulidCal: function (iYear, iMonth) {
        var aMonth = new Array();
        aMonth[0] = new Array(7);
        aMonth[1] = new Array(7);
        aMonth[2] = new Array(7);
        aMonth[3] = new Array(7);
        aMonth[4] = new Array(7);
        aMonth[5] = new Array(7);
        aMonth[6] = new Array(7);
        var dCalDate = new Date(iYear, iMonth - 1, 1);
        var iDayOfFirst = dCalDate.getDay();
        var iDaysInMonth = calUtil.getDaysInmonth(iMonth, iYear);
        var iVarDate = 1;
        var d, w;
        aMonth[0][0] = "日";
        aMonth[0][1] = "一";
        aMonth[0][2] = "二";
        aMonth[0][3] = "三";
        aMonth[0][4] = "四";
        aMonth[0][5] = "五";
        aMonth[0][6] = "六";
        for (d = iDayOfFirst; d < 7; d++) {
            aMonth[1][d] = iVarDate;
            iVarDate++;
        }
        for (w = 2; w < 7; w++) {
            for (d = 0; d < 7; d++) {
                if (iVarDate <= iDaysInMonth) {
                    aMonth[w][d] = iVarDate;
                    iVarDate++;
                }
            }
        }
        return aMonth;
    },
    ifHasSigned: function (signList, day) {
        var signed = false;
        $.each(signList, function (index, item) {
            var date = new Date(item.signDate);
            if (date.getDate() == day) {
                signed = true;
                return false;
            }
        });
        return signed;
    },
    drawCal: function (iYear, iMonth, signList) {
        var currentYearMonth = iYear + "年" + iMonth + "月";
        var myMonth = calUtil.bulidCal(iYear, iMonth);
        var htmls = new Array();
        htmls.push("<div class='sign_main' id='sign_layer'>");
        htmls.push("<div class='sign_succ_calendar_title'>");
        htmls.push("<div class='calendar_month_span'>" + currentYearMonth + "</div>");
        htmls.push("</div>");
        htmls.push("<div class='sign' id='sign_cal'>");
        htmls.push("<table class='table'>");
        htmls.push("<tr>");
        htmls.push("<th>" + myMonth[0][0] + "</th>");
        htmls.push("<th>" + myMonth[0][1] + "</th>");
        htmls.push("<th>" + myMonth[0][2] + "</th>");
        htmls.push("<th>" + myMonth[0][3] + "</th>");
        htmls.push("<th>" + myMonth[0][4] + "</th>");
        htmls.push("<th>" + myMonth[0][5] + "</th>");
        htmls.push("<th>" + myMonth[0][6] + "</th>");
        htmls.push("</tr>");
        var d, w;
        for (w = 1; w < 7; w++) {
            htmls.push("<tr>");
            for (d = 0; d < 7; d++) {
                var ifHasSigned = calUtil.ifHasSigned(signList, myMonth[w][d]);
                if (ifHasSigned) {
                    htmls.push("<td class='on'>" + (!isNaN(myMonth[w][d]) ? myMonth[w][d] : " ") + "</td>");
                } else {
                    htmls.push("<td>" + (!isNaN(myMonth[w][d]) ? myMonth[w][d] : " ") + "</td>");
                }
            }
            htmls.push("</tr>");
        }
        htmls.push("</table>");
        htmls.push("</div>");
        htmls.push("</div>");
        return htmls.join('');
    }
};