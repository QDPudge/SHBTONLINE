$(function () {
    $(".list-group-item").on('click', function () {
        $(".list-group-item").removeClass("active");
        $(this).addClass("active");
        switch ($(this).text()) {
            case "基本信息":
                $("#infoPartial").load("/Account/BaseInfo", function () {

                    //加载完之后隐藏进度条

                }); break;
            case "密码修改":
                $("#infoPartial").load("/Account/ModifyPassword", function () {

                    //加载完之后隐藏进度条

                }); break;
            case "账号安全":
                $("#infoPartial").load("/Account/BaseInfo", function () {

                    //加载完之后隐藏进度条

                }); break;
            case "社交":
                $("#infoPartial").load("/Account/Social", function () {

                    //加载完之后隐藏进度条

                }); break;
            case "游戏资源查看":
                $("#infoPartial").load("/Account/BaseInfo", function () {

                    //加载完之后隐藏进度条

                }); break;
            default: break;
        }
    });
 
});
