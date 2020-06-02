'use strict'

layui.config({
    base: '/Scripts/Project/RecruitManage/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //用户列表数据表格
    table.render({
        elem: '#table_recruit',
        height: 600,
        width: 1600,
        url: '/RecruitManage/GetRecruit', //数据接口
        page: true,//开启分页
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Name", title: "公司名称" },
            { field: "Type", title: "公司类型" },
            { field: "Address", title: "地址" },
            { field: "Phone", title: "联系方式" },
            { field: "Offer", title: "职位名称" },
            { field: "Number", title: "招聘人数" },
            { field: "Require", title: "应聘要求" },
            { field: "Time", title: "发布时间" },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    

    //监听表格工具栏
    table.on('tool(table_recruit)', function (obj) {
        var data = obj.data;
        if (obj.event === 'del') {
            layer.confirm('确认删除该招聘信息?', function () {
                $.getJSON('/RecruitCenter/DelRecruit', { recruitId: data.Id }, function (res) {
                    if (res.code === 200) {
                        layer.alert("删除成功!", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("删除失败!");
                    }
                });
            });
        }
    });

    //监听查询按钮
    $("#search").click(function () {
        table.reload('table_recruit', {
            where: {  search: $('#input').val() }
        });
    });
});