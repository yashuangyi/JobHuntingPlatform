'use strict'

layui.config({
    base: '/Scripts/Project/CompanyManage/' //静态资源所在路径
}).use(['form', 'table'], function () {
    var form = layui.form
        , table = layui.table;

    //用户列表数据表格
    table.render({
        elem: '#table_company',
        height: 600,
        width: 1200,
        url: '/CompanyManage/GetCompany', //数据接口
        page: true,//开启分页
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Account", title: "账号" },
            { field: "Password", title: "密码" },
            { field: "Name", title: "名字" },
            { field: "Type", title: "性质" },
            { field: "Address", title: "地址" },
            { field: "Phone", title: "联系方式" },
            { field: "IsPass", title: "状态", templet: '#statusbar' },
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听"企业注册"按钮
    window.btn_addCompany = function () {
        $('#companyId').val(0);
        $('#companyIsPass').val(0);
        $('#companyAccount').attr("readonly", false);
        layer.open({
            type: 1, //页面层
            title: "企业用户注册",
            area: ['600px', '500px'],
            btn: ['保存', '取消'],
            btnAlign: 'c', //按钮居中
            content: $('#div_addCompany'),
            success: function (layero, index) {// 弹出layer后的回调函数,参数分别为当前层DOM对象以及当前层索引
                // 解决按回车键重复弹窗问题
                $(':focus').blur();
                // 为当前DOM对象添加form标志
                layero.addClass('layui-form');
                // 将保存按钮赋予submit属性
                layero.find('.layui-layer-btn0').attr({
                    'lay-filter': 'btn_saveCompanyAdd',
                    'lay-submit': ''
                });
                // 表单验证
                form.verify();
                // 刷新渲染(否则开关按钮不会显示)
                form.render();
            },
            yes: function (index, layero) {// 确认按钮回调函数,参数分别为当前层索引以及当前层DOM对象
                form.on('submit(btn_saveCompanyAdd)', function (data) {//data按name获取
                    if (data.field.password != data.field.pwAgain) {
                        layer.msg("两次密码不一致，请重新输入!");
                        return false;
                    }
                    $.ajax({
                        type: 'post',
                        url: '/Login/AddCompany',
                        dataType: 'json',
                        data: data.field,
                        success: function (res) {
                            if (res.code === 200) {
                                layer.alert("注册成功，待管理审核通过后即可登录系统！", function (index) {
                                    window.location.reload();
                                });
                            }
                            else if (res.code === 402) {
                                layer.alert("已存在该账号!");
                            }
                        }
                    });
                    return false;
                });
            }
        });
    }

    //监听表格工具栏
    table.on('tool(table_company)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            $('#companyId').val(data.Id);
            $('#companyName').val(data.Name);
            $('#companyPw').val(data.Password);
            $('#companyPwAgain').val(data.Password);
            $('#companyAccount').val(data.Account);
            $('#companyType').val(data.Type);
            $('#companyAddress').val(data.Address);
            $('#companyPhone').val(data.Phone);
            $('#companyIsPass').val(data.IsPass);
            $('#companyAccount').attr("readonly", true);
            layer.open({
                type: 1, //页面层
                title: "修改用户信息",
                area: ['600px', '400px'],
                btn: ['保存', '取消'],
                btnAlin: 'c', //按钮居中
                content: $('#div_addCompany'),
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
                        if (data.field.password != data.field.pwAgain) {
                            layer.msg("两次密码不一致，请重新输入!");
                            return false;
                        }
                        $.ajax({
                            type: 'post',
                            url: '/CompanyManage/EditCompany',
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
            layer.confirm('确认删除该账户?', function () {
                $.getJSON('/CompanyManage/DelCompany', { userId: data.Id }, function (res) {
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
        else if (obj.event === 'check') {
            layer.confirm('是否通过审核?', function () {
                $.getJSON('/CompanyManage/CheckCompany', { userId: data.Id }, function (res) {
                    if (res.code === 200) {
                        layer.alert("审核通过成功!该企业用户现可登录", function success() {
                            window.location.reload();
                        });
                    }
                    else {
                        layer.alert("审核通过失败!");
                    }
                });
            });
        }
    });

    //监听查询按钮
    $("#search").click(function () {
        table.reload('table_company', {
            where: { search: $('#input').val() }
        });
    });
});