'use strict'

layui.config({
    base: '/Scripts/Project/ArticleManage/' //静态资源所在路径
}).use(['form', 'table', 'upload'], function () {
    var form = layui.form
        , upload = layui.upload
        , table = layui.table;

    //上传文章功能
    upload.render({
        elem: '#btn_select',
        url: '/ArticleManage/UploadArticle',
        auto: false,//不自动上传
        accept: 'file',
        acceptMime: 'file/pdf',
        exts: 'pdf',
        bindAction: '#btn_upload', //触发上传的按钮
        before: function () {
            layer.load();
        },
        done: function (res) {
            layer.closeAll('loading');
            $('#btn_select').html("<i class=''layui-icon layui-icon-upload-drag'></i>重新选择");
            if (res.code === 200) {
                layer.msg(res.msg);
                $('#article').html("<i class='layui-icon layui-icon-file'></i> <a href='" + res.resumePath + "'>" + res.resumeName + "<a/>");
                $('#articlePath').val(res.resumePath);
            } else {
                layer.msg(res.msg);
                $('#article').html("");
                $('#articlePath').val("");
            }
        },
    });

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
            { field: "IsTop", title: "状态", templet: '#statusbar' },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 300 }
        ]]
    });

    //监听"新增文章"按钮
    window.btn_addArticle = function () {
        layer.open({
            type: 1, //页面层
            title: "新增文章",
            area: ['600px', '300px'],
            btn: ['保存', '取消'],
            btnAlign: 'c', //按钮居中
            content: $('#div_addArticle'),
            success: function (layero, index) {// 弹出layer后的回调函数,参数分别为当前层DOM对象以及当前层索引
                // 解决按回车键重复弹窗问题
                $(':focus').blur();
                // 为当前DOM对象添加form标志
                layero.addClass('layui-form');
                // 将保存按钮赋予submit属性
                layero.find('.layui-layer-btn0').attr({
                    'lay-filter': 'btn_saveArticleAdd',
                    'lay-submit': ''
                });
                // 表单验证
                form.verify();
                // 刷新渲染(否则开关按钮不会显示)
                form.render();
            },
            yes: function (index, layero) {// 确认按钮回调函数,参数分别为当前层索引以及当前层DOM对象
                form.on('submit(btn_saveArticleAdd)', function (data) {//data按name获取
                    if ($("#articlePath").val() == null || $("#articlePath").val() == "") {
                        layer.msg("请上传文章!");
                        return false;
                    }
                    $.ajax({
                        type: 'post',
                        url: '/ArticleManage/AddArticle',
                        dataType: 'json',
                        data: { adminId: $('#user_id', parent.document).val(), articlePath:$("#articlePath").val(), title:$("#title").val() },
                        success: function (res) {
                            if (res.code === 200) {
                                layer.alert("添加成功!", function (index) {
                                    window.location.reload();
                                });
                            }
                        }
                    });
                    return false;
                });
            }
        });
    }

    //监听表格工具栏
    table.on('tool(table_article)', function (obj) {
        var data = obj.data;
        if (obj.event === 'view') {
            window.location.href = "/ArticleManage/ArticleView?articleId=" + data.Id;
        }
        else if (obj.event === 'top') {
            layer.confirm('确认置顶该文章?', function () {
                $.getJSON('/ArticleManage/TopArticle', { articleId: data.Id }, function (res) {
                    if (res.code === 200) {
                        layer.alert("置顶成功!", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("置顶失败!");
                    }
                });
            });
        }
        else if (obj.event === 'no-top') {
            layer.confirm('确认取消置顶?', function () {
                $.getJSON('/ArticleManage/CancelTopArticle', { articleId: data.Id }, function (res) {
                    if (res.code === 200) {
                        layer.alert("取消成功!", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("取消失败!");
                    }
                });
            });
        }
        else if (obj.event === 'del') {
            layer.confirm('确认删除该文章?', function () {
                $.getJSON('/ArticleManage/DelArticle', { articleId: data.Id }, function (res) {
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
        table.reload('table_article', {
            where: { search: $('#input').val() }
        });
    });
});