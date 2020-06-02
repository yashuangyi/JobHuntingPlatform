// 初始化信息
window.onload = function () {
    if ($('#user_power', parent.document).val() === "求职者") {
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/MyResume/MyResume\')">我的简历</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/JobPlaza/JobPlaza\')">求职广场</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/JobInfo/JobInfo\')">求职资讯</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/NoticeBox/NoticeBox\')">通知中心</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/SeekerInfo/SeekerInfo\')">基本资料</button>');
    } else if ($('#user_power', parent.document).val() === "企业") {
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/RecruitCenter/RecruitCenter\')">招聘中心</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/SeekerPlaza/SeekerPlaza\')">人才广场</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/JobInfo/JobInfo\')">求职资讯</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/ResumeBox/ResumeBox\')">简历邮箱</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/NoticeBox/NoticeBox\')">通知中心</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/CompanyInfo/CompanyInfo\')">基本资料</button>');
    } else if ($('#user_power', parent.document).val() === "管理员") {
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/RecruitManage/RecruitManage\')">招聘中心管理</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/ArticleManage/ArticleManage\')">资讯管理</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/CompanyManage/CompanyManage\')">企业管理</button>');
        $("#btn_list").append('<button class="layui-btn layui-btn-normal" onclick="linkBtn(\'/SeekerManage/SeekerManage\')">求职者管理</button>');
    }
}

//快速导航按钮事件
function linkBtn(url) {
    window.location.href = url;
}
