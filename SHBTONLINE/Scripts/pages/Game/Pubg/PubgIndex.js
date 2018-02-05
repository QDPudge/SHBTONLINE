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
    $(".player_img").on('click', function () {
        layer.open({
            type: 1,
            title: false,
            area: ['600','500'],
            zIndex:99999,
            content: $('#player_info'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响,
            success: function(layero, index){
                $("#player_info").css("display", "block");
            },
            cancel: function (index, layero) {
                $("#player_info").css("display", "none");
                layer.close(index)
        }    
        });
    });

}
function loadNameMatch(Name) {
    var date = new Date().getTime();
    $.ajax({
        type: 'POST',
        data: {},
        url: "GetUserInfo?time=" + date + "&Name=" + Name,
        async: false,
        success: function (data) {
            debugger;
            var obj = JSON.parse(data);
            var table = $("#total_match")[0];
            $("#total_match").empty();
            $.each(obj.result.radar_score, function (k, v) {
                if (k % 2 == 0) {
                    oneRow = table.insertRow();
                }
                var oTr0 = v.desc;
                var oTr = v.value+"分";
                cell0 = oneRow.insertCell();
                cell1 = oneRow.insertCell();
                cell0.innerHTML = oTr0;
                cell1.innerHTML = oTr;
            });
            var oTr0 = obj.result.player_rank_round.desc;
            var oTr = obj.result.player_rank_round.value;
            cell0 = oneRow.insertCell();
            cell1 = oneRow.insertCell();
            cell0.innerHTML = oTr0;
            cell1.innerHTML = oTr;
            var table2 = $("#score_detail")[0];
            $("#score_detail").empty();
            $.each(obj.result.stats.modes, function (n, m) {
                oneRow = table2.insertRow();
                oneRow.style = "background-color:" + obj.result.normal_score_detail[n][0].color+";color:#fff";
                cell1 = oneRow.insertCell();
                cell1.innerHTML = m.mode_desc;
                $.each(m.values, function (k, v) {
                    if (k % 4 == 0) {
                        oneRow = table2.insertRow();
                    }
                    var oTr0 = v.desc;
                    var oTr = v.value ;
                    cell0 = oneRow.insertCell();
                    cell1 = oneRow.insertCell();
                    cell0.innerHTML = oTr0;
                    cell1.innerHTML = oTr;
                });
            });

            var table3 = $("#inventory")[0];
            $("#inventory").empty();
            $.each(obj.result.inventory, function (k, v) {
                if (k % 2 == 0) {
                    oneRow = table3.insertRow();
                }
                //oneRow.style =
                var infodiv = "<div class ='inventory'><img style='-webkit-user-select: none' src='" + v.icon_url + "'><p>" + v.name + "￥" + v.price_info.rmb_normal_price_num + "</p></div>"
                cell1 = oneRow.insertCell();
                cell1.innerHTML = infodiv;
            });
        }
    });
}