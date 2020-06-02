'use strict'

// 初始化信息
window.onload = function () {
    if ($('#user_id').val() !== "") {
        if ($('#user_power').val() === "管理员") {
            $("#item_one").remove();
            $("#item_two").remove();
            $("#item_three").remove();
        } else if ($('#user_power').val() === "企业") {
            $("#item_four").remove();
            $("#item_five").remove();
            $("#item_six").remove();
            $("#child_one").append('<dd><a onclick="setSrc(\'/RecruitCenter/RecruitCenter\')"><i class="layui-icon layui-icon-release" style="font-size: 18px; color: #55ffff;"></i>&nbsp 招聘中心</a></dd>');
            $("#child_one").append('<dd><a onclick="setSrc(\'/SeekerPlaza/SeekerPlaza\')"><i class="layui-icon layui-icon-user" style="font-size: 18px; color: #55ffff;"></i>&nbsp 人才广场</a></dd>');
            $("#child_two").append('<dd><a onclick="setSrc(\'/ResumeBox/ResumeBox\')"><i class="layui-icon layui-icon-email" style="font-size: 18px; color: #55ffff;"></i>&nbsp 简历邮箱</a></dd>');
            $("#child_three").append('<dd><a onclick="setSrc(\'/CompanyInfo/CompanyInfo\')"><i class="layui-icon layui-icon-set-fill" style="font-size: 18px; color: #55ffff;"></i>&nbsp 基本资料</a></dd>');
        } else if ($('#user_power').val() === "求职者") {
            $("#item_four").remove();
            $("#item_five").remove();
            $("#item_six").remove();
            $("#child_one").append('<dd><a onclick="setSrc(\'/MyResume/MyResume\')"><i class="layui-icon layui-icon-form" style="font-size: 18px; color: #55ffff;"></i>&nbsp 我的简历</a></dd>');
            $("#child_one").append('<dd><a onclick="setSrc(\'/JobPlaza/JobPlaza\')"><i class="layui-icon layui-icon-group" style="font-size: 18px; color: #55ffff;"></i>&nbsp求职广场</a></dd>');
            $("#child_three").append('<dd><a onclick="setSrc(\'/SeekerInfo/SeekerInfo\')"><i class="layui-icon layui-icon-set-fill" style="font-size: 18px; color: #55ffff;"></i>&nbsp 基本资料</a></dd>');
        }
        setSrc("/Home/HomePage");
    } else {
        layer.msg("请重新登录！");
        location.href = "/Login/Login";
    }
};

//修改iframe的读取路径并刷新
function setSrc(path) {
    //此句必须在前面
    $("#iframeMain").get(0).contentWindow.location.reload(true);
    var iframe = $("#iframeMain").get(0);
    iframe.src = path;
}

//刷新按钮
function freshView() {
    var iframe = $("#iframeMain").get(0);
    iframe.src = iframe.src;
}