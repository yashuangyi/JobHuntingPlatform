'use strict'

layui.config({
    base: '/Scripts/Project/SeekerPlaza/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //用户列表数据表格
    table.render({
        elem: '#table_user',
        height: 600,
        width: 1200,
        url: '/SeekerPlaza/GetSeeker', //数据接口
        page: true,//开启分页
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Name", title: "姓名" },
            { field: "Sex", title: "性别" },
            { field: "Age", title: "年龄" },
            { field: "Phone", title: "联系方式" },
            { field: "Address", title: "意向地址" },
            { field: "Offer", title: "意向职位" },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听表格工具栏
    table.on('tool(table_user)', function (obj) {
        var data = obj.data;
        if (obj.event === 'watch') {
            
        }
        else if (obj.event === 'invite') {
            layer.confirm('确认对该求职者发出面试邀请?', function () {
                $.getJSON('/SeekerPlaza/Invite', { userId: data.Id, companyId: $('#user_id', parent.document).val() }, function (res) {
                    if (res.code === 200) {
                        layer.alert("发出邀请成功!请注意查看消息中心~", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("您已邀请过了!");
                    }
                });
            });
        }
    });

    //监听查询按钮
    $("#search").click(function () {
        table.reload('table_user', {
            where: { search: $('#input').val() }
        });
    });
});