'use strict'

layui.config({
    base: '/Scripts/Project/Login/'
}).use(['layer', 'form', 'upload'], function () {
    var layer = layui.layer
        , upload = layui.upload
        , form = layui.form;

    //监听登录按钮
    $("#btn_login").click(function () {
        $.ajax({
            url: "/Login/Check",
            dataType: "json",
            type: "post",
            data: {
                account: $('#account').val(),
                password: $('#password').val(),
            },
            success: function (res) {
                if (res.code === 200) {
                    layer.open({
                        title: '欢迎管理员~'
                        , content: '登录成功!'
                        , end: function () {
                            location.href = "/Home/Home";
                        }
                    });
                }
                else if (res.code === 201) {
                    layer.open({
                        title: '求职者您好，祝您求职顺利~'
                        , content: '登录成功!'
                        , end: function () {
                            location.href = "/Home/Home";
                        }
                    });
                }
                else if (res.code === 202) {
                    layer.open({
                        title: '企业用户您好，注意查看邮箱哦~'
                        , content: '登录成功!'
                        , end: function () {
                            location.href = "/Home/Home";
                        }
                    });
                }
                else if (res.code === 402) {
                    layer.open({
                        title: '企业用户您好'
                        , content: '请耐心等待管理员的审核结果!'
                    });
                }
                else if (res.code === 401) {
                    layer.open({
                        title: 'Fail'
                        , content: '请输入账号及密码!'
                    });
                }
                else if (res.code === 404) {
                    layer.open({
                        title: 'Fail'
                        , content: '账号或密码错误，请重新输入!'
                    });
                    $('#password').val("");
                }
            },
        });
    });

    //监听“回车键”
    $(document).keyup(function (event) {
        if (event.keyCode === 13) {
            $("#btn_login").click();
        }
    });

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

    //监听"用户注册"按钮
    window.btn_addUser = function () {
        $('#userId').val(0);
        $('#userIsRelease').val(0);
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
                    if (data.field.resumePath == "") {
                        layer.msg("请上传简历!");
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

    //监听"企业注册"按钮
    window.btn_addCompany = function () {
        $('#companyId').val(0);
        $('#companyIsPass').val(0);
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
                                layer.alert("注册成功，待管理审核通过后可登录系统！", function (index) {
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
});