'use strict'

layui.config({
    base: '/Scripts/Project/ResumeBox/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //简历列表数据表格
    table.render({
        elem: '#table_resume',
        height: 600,
        width: 1500,
        url: '/ResumeBox/GetResume', //数据接口
        page: true,//开启分页
        where: { userId: $('#user_id', parent.document).val() },
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Name", title: "求职者姓名" },
            { field: "Time", title: "投递时间" },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听表格工具栏
    table.on('tool(table_resume)', function (obj) {
        var data = obj.data;
        if (obj.event === 'watch') {
            window.location.href = "/ResumeBox/ResumeView?userId=" + data.SeekerId;
        }
    });

    //监听查询按钮
    $("#search").click(function () {
        table.reload('table_resume', {
            where: { userId: $('#user_id', parent.document).val(), search: $('#input').val() }
        });
    });
});