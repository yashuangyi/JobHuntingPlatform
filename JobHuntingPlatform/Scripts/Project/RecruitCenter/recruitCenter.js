'use strict'

layui.config({
    base: '/Scripts/Project/RecruitCenter/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //用户列表数据表格
    table.render({
        elem: '#table_recruit',
        height: 600,
        width: 1200,
        url: '/RecruitCenter/GetRecruit', //数据接口
        page: true,//开启分页
        where:{ companyId: $('#user_id', parent.document).val()},
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Offer", title: "职位名称" },
            { field: "Number", title: "招聘人数" },
            { field: "Require", title: "应聘要求" },
            { field: "Time", title: "发布时间" },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听"发布招聘"按钮
    window.btn_addRecruit = function () {
        $('#recruitId').val(0);
        $('#companyId').val($('#user_id', parent.document).val());
        $('#time').val(null);
        layer.open({
            type: 1, //页面层
            title: "发布招聘",
            area: ['600px', '300px'],
            btn: ['保存', '取消'],
            btnAlign: 'c', //按钮居中
            content: $('#div_addRecruit'),
            success: function (layero, index) {// 弹出layer后的回调函数,参数分别为当前层DOM对象以及当前层索引
                // 解决按回车键重复弹窗问题
                $(':focus').blur();
                // 为当前DOM对象添加form标志
                layero.addClass('layui-form');
                // 将保存按钮赋予submit属性
                layero.find('.layui-layer-btn0').attr({
                    'lay-filter': 'btn_saveUserAdd',
                    'lay-submit': ''
                });
                // 表单验证
                form.verify();
                // 刷新渲染(否则开关按钮不会显示)
                form.render();
            },
            yes: function (index, layero) {// 确认按钮回调函数,参数分别为当前层索引以及当前层DOM对象
                form.on('submit(btn_saveUserAdd)', function (data) {//data按name获取
                    $.ajax({
                        type: 'post',
                        url: '/RecruitCenter/AddRecruit',
                        dataType: 'json',
                        data: data.field,
                        success: function (res) {
                            if (res.code === 200) {
                                layer.alert("发布成功!", function (index) {
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
    table.on('tool(table_recruit)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            $('#recruitId').val(data.Id);
            $('#companyId').val(data.Name);
            $('#companyId').val($('#user_id', parent.document).val());
            $('#offer').val(data.Offer);
            $('#require').val(data.Require);
            $('#number').val(data.Number);
            $('#time').val(data.Time);
            layer.open({
                type: 1, //页面层
                title: "修改招聘信息",
                area: ['600px', '300px'],
                btn: ['保存', '取消'],
                btnAlin: 'c', //按钮居中
                content: $('#div_addRecruit'),
                success: function (layero, index) {// 弹出layer后的回调函数,参数分别为当前层DOM对象以及当前层索引
                    // 解决按回车键重复弹窗问题
                    $('focus').blur();
                    // 为当前DOM对象添加form标志
                    layero.addClass('layui-form');
                    // 将保存按钮赋予submit属性
                    layero.find('.layui-layer-btn0').attr({
                        'lay-filter': 'btn_saveUserEdit',
                        'lay-submit': ''
                    });
                    // 表单验证
                    form.verify();
                    // 刷新渲染(否则开关按钮不会显示)
                    form.render();
                },
                yes: function (index, layero) {// 确认按钮回调函数,参数分别为当前层索引以及当前层DOM对象
                    form.on('submit(btn_saveUserEdit)', function (data) {//data按name获取
                        $.ajax({
                            type: 'post',
                            url: '/RecruitCenter/EditRecruit',
                            dataType: 'json',
                            data: data.field,
                            success: function (res) {
                                if (res.code === 200) {
                                    layer.alert("修改成功!", function (index) {
                                        window.location.reload();
                                    });
                                }
                            }
                        });
                    });
                }
            });
        }
        else if (obj.event === 'del') {
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
            where: { userId: $(('#user_id'), parent.document).val(), search: $('#input').val() }
        });
    });
});