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
            area: ['600', '800'],
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

            var table2 = $("#history_max")[0];

            $("#history_max tr:not(:first)").empty(); //清空table（除了第一行以外）
            $.each(obj.result.max_values, function (k, v) {
                oneRow = table2.insertRow();
                var oTr0 = v.hero_info.hero_name;
                var oTr = v.max_type;
                var oTr2 = v.max_value;
                var oTr3 = v.win_desc;
                var record_time = new Date(parseInt(v.finish_time) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ');
                var oTr4 = record_time;
                cell0 = oneRow.insertCell();
                cell1 = oneRow.insertCell();
                cell2 = oneRow.insertCell();
                cell3 = oneRow.insertCell();
                cell4 = oneRow.insertCell();
                cell0.innerHTML = oTr0;
                cell1.innerHTML = oTr;
                cell2.innerHTML = oTr2;
                cell3.innerHTML = oTr3;
                cell4.innerHTML = oTr4;
            });
            var table3 = $("#history_match")[0];

            $("#history_match tr:not(:first)").empty(); //清空table（除了第一行以外）
            $.each(obj.result.matches, function (k, v) {
                oneRow = table3.insertRow();
                var oTr0 = v.hero_info.hero_name;
                var oTr = v.game_mode_desc;
                var oTr2 = v.skill_desc;
                var oTr3 = v.kda;
                var oTr4 = v.win_desc;
                var record_time = new Date(parseInt(v.finish_time) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ');
                var oTr5 = record_time;
                cell0 = oneRow.insertCell();
                cell1 = oneRow.insertCell();
                cell2 = oneRow.insertCell();
                cell3 = oneRow.insertCell();
                cell4 = oneRow.insertCell();
                cell5 = oneRow.insertCell();
                cell0.innerHTML = oTr0;
                cell1.innerHTML = oTr;
                cell2.innerHTML = oTr2;
                cell3.innerHTML = oTr3;
                cell4.innerHTML = oTr4;
                cell5.innerHTML = oTr5;
            });
            
            var radardata20 = [];
            var radardata100 = [];
            var radardataall = [];
            radardata20.push(obj.result.power_stats.r20.pv_all);
            radardata20.push(obj.result.power_stats.r20.pv_kda);
            radardata20.push(obj.result.power_stats.r20.pv_growth);
            radardata20.push(obj.result.power_stats.r20.pv_tower);
            radardata20.push(obj.result.power_stats.r20.pv_deatch);
            radardata20.push(obj.result.power_stats.r20.pv_damage);

            radardata100.push(obj.result.power_stats.r100.pv_all);
            radardata100.push(obj.result.power_stats.r100.pv_kda);
            radardata100.push(obj.result.power_stats.r100.pv_growth);
            radardata100.push(obj.result.power_stats.r100.pv_tower);
            radardata100.push(obj.result.power_stats.r100.pv_deatch);
            radardata100.push(obj.result.power_stats.r100.pv_damage);

            radardataall.push(obj.result.power_stats.all.pv_all);
            radardataall.push(obj.result.power_stats.all.pv_kda);
            radardataall.push(obj.result.power_stats.all.pv_growth);
            radardataall.push(obj.result.power_stats.all.pv_tower);
            radardataall.push(obj.result.power_stats.all.pv_deatch);
            radardataall.push(obj.result.power_stats.all.pv_damage);

            var color = Chart.helpers.color;
            var config = {
                type: 'radar',
                data: {
                    labels: [ "综合",  "KDA", "发育", "推进","生存","输出"],
                    datasets: [{
                        label: "最近20场",
                        backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
                        borderColor: window.chartColors.red,
                        pointBackgroundColor: window.chartColors.red,
                        data: radardata20
                    },
                    {
                        label: "最近100场",
                        backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
                        borderColor: window.chartColors.green,
                        pointBackgroundColor: window.chartColors.green,
                        data: radardata100
                    },
                    {
                        label: "生涯",
                        backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
                        borderColor: window.chartColors.blue,
                        pointBackgroundColor: window.chartColors.blue,
                        data: radardataall
                    }]
                },
                options: {
                    scaleShowLabels: false,
                }
            };

            window.onload = function () {
                window.myRadar = new Chart(document.getElementById("match_radar"), config);
            };

            var colorNames = Object.keys(window.chartColors);


        }
    });
}