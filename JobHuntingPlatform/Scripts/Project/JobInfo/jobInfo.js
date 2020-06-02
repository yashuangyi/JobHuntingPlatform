'use strict'

layui.config({
    base: '/Scripts/Project/ArticleManage/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //文章列表数据表格
    table.render({
        elem: '#table_article',
        height: 600,
        width: 1500,
        url: '/ArticleManage/GetArticle', //数据接口
        page: true,//开启分页
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Title", title: "标题" },
            //{ field: "Name", title: "编者" },
            { field: "Time", title: "发布时间", sort: true },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听表格工具栏
    table.on('tool(table_article)', function (obj) {
        var data = obj.data;
        if (obj.event === 'view') {
            window.location.href = "/ArticleManage/ArticleView?articleId=" + data.Id;
        }
    });

    //监听查询按钮
    $("#search").click(function () {
        table.reload('table_article', {
            where: { search: $('#input').val() }
        });
    });
});