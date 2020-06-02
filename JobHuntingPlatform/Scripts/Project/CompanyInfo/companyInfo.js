'use strict'

// 初始化信息
window.onload = function () {
    $.ajax({
        type: 'get',
        url: '/CompanyInfo/GetMyData',
        dataType: 'json',
        data: { userId: $('#user_id', parent.document).val() },
        success: function (res) {
            if (res.code === 200) {
                $('#companyId').val(res.data.id);
                $('#companyName').val(res.data.name);
                $('#companyPw').val(res.data.password);
                $('#companyPwAgain').val(res.data.password);
                $('#companyAccount').val(res.data.account);
                $('#companyIsPass').val(res.data.isPass);
                $('#companyAddress').val(res.data.address);
                $('#companyPhone').val(res.data.phone);
                $('#companyType').val(res.data.type);
            }
        }
    });
}

layui.config({
    base: '/Scripts/Project/CompanyInfo/' //静态资源所在路径
}).use(['form'], function () {
    var form = layui.form;

    //监听修改按钮
    form.on('submit(lay-submit)', function (data) {
        if ($('#userPw').val() != $('#userPwAgain').val()) {
            layer.msg("两次密码不一致，请重新输入!");
            return false;
        }
        $.ajax({
            url: "/CompanyManage/EditCompany",
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