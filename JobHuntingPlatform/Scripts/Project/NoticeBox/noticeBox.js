'use strict'

layui.config({
    base: '/Scripts/Project/NoticeBox/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //用户列表数据表格
    table.render({
        elem: '#table_notice',
        height: 600,
        width: 1800,
        url: '/NoticeBox/GetNotice', //数据接口
        page: true,//开启分页
        where: { userId: $('#user_id', parent.document).val(), userPower: $('#user_power', parent.document).val() },
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Time", title: "时间", width: 200 },
            { field: "Content", title: "内容", width: 800 },
            { field: "Type", title: "通知类型", width: 150 },
            { field: "IsReply", title: "是否已回复", templet: '#statusbar' },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听表格工具栏
    table.on('tool(table_notice)', function (obj) {
        var data = obj.data;
        if (obj.event === 'invite') {
            layer.confirm('确认对该求职者发出面试邀请?', function () {
                $.getJSON('/SeekerPlaza/Invite', { userId: data.SourceId, companyId: $('#user_id', parent.document).val() }, function (res) {
                    if (res.code === 200) {
                        layer.alert("发出邀请成功!请注意查看消息中心~", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("邀请失败!");
                    }
                });
            });
        }
        else if (obj.event === 'seeker_refuse') {
            layer.confirm('确认拒绝该求职者?', function () {
                $.getJSON('/NoticeBox/RefuseSeeker', { userId: data.SourceId, companyId: $('#user_id', parent.document).val() }, function (res) {
                    if (res.code === 200) {
                        layer.alert("拒绝成功!", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("拒绝失败!");
                    }
                });
            });
        }
        else if (obj.event === 'company_refuse') {
            layer.confirm('确认拒绝该企业发出的面试邀请?', function () {
                $.getJSON('/NoticeBox/RefuseCompany', { userId: data.SourceId, seekerId: $('#user_id', parent.document).val() }, function (res) {
                    if (res.code === 200) {
                        layer.alert("拒绝成功!", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("拒绝失败!");
                    }
                });
            });
        }
        else if (obj.event === 'agree') {
            layer.confirm('确认答应该面试?', function () {
                $.getJSON('/NoticeBox/Agree', { userId: data.SourceId, seekerId: $('#user_id', parent.document).val() }, function (res) {
                    if (res.code === 200) {
                        layer.alert("确认成功!请留意短信通知", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("确认失败!");
                    }
                });
            });
        }
    });

    //监听查询按钮
    $("#search").click(function () {
        table.reload('table_notice', {
            where: { userId: $('#user_id', parent.document).val(), userPower: $('#user_power', parent.document).val(), search: $('#input').val() }
        });
    });
});