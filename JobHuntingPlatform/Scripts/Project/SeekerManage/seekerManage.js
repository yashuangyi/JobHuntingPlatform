'use strict'

layui.config({
    base: '/Scripts/Project/SeekerManage/' //静态资源所在路径
}).use(['form', 'table', 'upload'], function () {
    var form = layui.form
        , upload = layui.upload
        , table = layui.table;

    //上传简历功能
    upload.render({
        elem: '#btn_select',
        url: '/Login/UploadResume',
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
                $('#userResume').html("<i class='layui-icon layui-icon-file'></i> <a href='" + res.resumePath + "'>" + res.resumeName + "<a/>");
                $('#resumePath').val(res.resumePath);
            } else {
                layer.msg(res.msg);
                $('#userResume').html("");
                $('#resumePath').val("");
            }
        },
    }); 

    //用户列表数据表格
    table.render({
        elem: '#table_user',
        height: 600,
        width: 1500,
        url: '/SeekerManage/GetSeeker', //数据接口
        page: true,//开启分页
        cols: [[
            //{ field: "Id", title: "编号", sort: "true" },
            { field: "Account", title: "账号" },
            { field: "Password", title: "密码" },
            { field: "Name", title: "姓名" },
            { field: "Sex", title: "性别" },
            { field: "Age", title: "年龄" },
            { field: "Phone", title: "联系方式" },
            { field: "Address", title: "意向地址" },
            { field: "Offer", title: "意向职位" },
            { field: "IsRelease", title: "是否发布求职", templet: '#statusbar'},
            { fixed: 'right', align: 'center', toolbar: '#toolbar', width: 200 }
        ]]
    });

    //监听"用户注册"按钮
    window.btn_addUser = function () {
        $('#userId').val(0);
        $('#userIsRelease').val(0);
        $('#userAccount').attr("readonly", false);
        layer.open({
            type: 1, //页面层
            title: "求职者注册",
            area: ['600px', '500px'],
            btn: ['保存', '取消'],
            btnAlign: 'c', //按钮居中
            content: $('#div_addUser'),
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
                    if (data.field.password != data.field.pwAgain) {
                        layer.msg("两次密码不一致，请重新输入!");
                        return false;
                    }
                    $.ajax({
                        type: 'post',
                        url: '/Login/AddSeeker',
                        dataType: 'json',
                        data: data.field,
                        success: function (res) {
                            if (res.code === 200) {
                                layer.alert("注册成功!", function (index) {
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
    table.on('tool(table_user)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            $('#userId').val(data.Id);
            $('#userName').val(data.Name);
            $('#userPw').val(data.Password);
            $('#userPwAgain').val(data.Password);
            $('#userAccount').val(data.Account);
            $('#resumePath').val(data.ResumePath);
            $('#userAge').val(data.Age);
            $('#userIsRelease').val(data.IsRelease);
            $('#userAddress').val(data.Address);
            $('#userPhone').val(data.Phone);
            $('#userOffer').val(data.Offer);
            $('#userAccount').attr("readonly", true);
            var select = document.getElementsByName("sex");
            for (let i = 0; i < select.length; i++) {
                if (select[i].value === data.Sex) {
                    select[i].checked = true;
                    break;
                }
            }
            layer.open({
                type: 1, //页面层
                title: "修改用户信息",
                area: ['600px', '400px'],
                btn: ['保存', '取消'],
                btnAlin: 'c', //按钮居中
                content: $('#div_addUser'),
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
                            url: '/SeekerManage/EditSeeker',
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
                $.getJSON('/SeekerManage/DelSeeker', { userId: data.Id }, function (res) {
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
        table.reload('table_user', {
            where: { search: $('#input').val() }
        });
    });
});