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
            area: ['600', '500'],
            zIndex: 99999,
            content: $('#player_info'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响,
            success: function (layero, index) {
                $("#player_info").css("display", "block");
            },
            cancel: function (index, layero) {
                $("#player_info").css("display", "none");
                layer.close(index)
            }
        });
    });

}
function loadNameMatch(ID) {
    var date = new Date().getTime();
    $.ajax({
        type: 'POST',
        data: {},
        url: "GetUserInfo?ID=" + ID,
        async: false,
        success: function (data) {
            debugger;
            var obj = JSON.parse(data);
            var table = $("#career_average")[0];
            $("#career_average").empty();
            $.each(obj.result.avg, function (k,v) {
                if(k%3==0){
                    oneRow = table.insertRow();
                }
                var oTr0 = v.desc;
                var oTr = v.value ;
                cell0 = oneRow.insertCell();
                cell1 = oneRow.insertCell();
                cell0.innerHTML = oTr0;
                cell1.innerHTML = oTr;
            });

            var table2 = $("#recent_game")[0];
            $("#recent_game").empty();
            $.each(obj.result.power_stats.r20, function (k,v) {
                if (k % 3 == 0) {
                    oneRow = table2.insertRow();
                }
                var oTr0 = v.desc;
                var oTr = v.value;
                cell0 = oneRow.insertCell();
                cell1 = oneRow.insertCell();
                cell0.innerHTML = oTr0;
                cell1.innerHTML = oTr;
            });
        }
    });
}