'use strict'

// 初始化信息
window.onload = function () {
    $.ajax({
        type: 'get',
        url: '/SeekerInfo/GetMyData',
        dataType: 'json',
        data: { userId: $('#user_id', parent.document).val() },
        success: function (res) {
            if (res.code === 200) {
                $('#userId').val(res.data.id);
                $('#userName').val(res.data.name);
                $('#userPw').val(res.data.password);
                $('#userPwAgain').val(res.data.password);
                $('#userAccount').val(res.data.account);
                $('#resumePath').val(res.data.resumePath);
                $('#userIsRelease').val(res.data.isRelease);
                $('#userOffer').val(res.data.offer);
                $('#userAddress').val(res.data.address);
                $('#userPhone').val(res.data.phone);
                $('#userAge').val(res.data.age);
                var select = document.getElementsByName("sex");
                for (let i = 0; i < select.length; i++) {
                    if (select[i].value === res.data.sex) {
                        select[i].checked = true;
                        break;
                    }
                }
            }
        }
    });
}

layui.config({
    base: '/Scripts/Project/SeekerInfo/' //静态资源所在路径
}).use(['form', 'upload'], function () {
    var form = layui.form
        , upload = layui.upload;

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

    //监听修改按钮
    form.on('submit(lay-submit)', function (data) {
        if ($('#userPw').val() != $('#userPwAgain').val()) {
            layer.msg("两次密码不一致，请重新输入!");
            return false;
        }
        $.ajax({
            url: "/SeekerManage/EditSeeker",
            dataType: "json",
            type: "post",
            data: data.field,
            success: function (res) {
                if (res.code === 200) {
                    layer.alert("修改成功!", function (index) {
                        window.location.reload();
                    });
                }
                else if (res.code === 402) {
                    layer.alert("修改失败!");
                }
            },
        });
    })
});