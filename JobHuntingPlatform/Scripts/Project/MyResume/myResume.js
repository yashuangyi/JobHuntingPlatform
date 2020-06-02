window.onload = function () {
    $.ajax({
        type: 'post',
        url: '/MyResume/GetStatus',
        dataType: 'json',
        data: { userId: $('#user_id', parent.document).val() },
        success: function (res) {
            if (res.code === 200) {
                $("#btn").text((res.isRelease === 0) ? "发布简历" : "取消发布");
                setSrc(res.resumePath);
            }
        }
    });
};

layui.config({
    base: '/Scripts/Project/MyResume/' //静态资源所在路径
}).use(['form'], function () {
    var form = layui.form;

    //监听"更改状态"按钮
    window.btn_changeStatus = function () {
        $.ajax({
            type: 'post',
            url: '/MyResume/ChangeStatus',
            dataType: 'json',
            data: { userId: $('#user_id', parent.document).val() },
            success: function (res) {
                if (res.code === 200) {
                    layer.alert("更改成功!", function (index) {
                        window.location.reload();
                    });
                }
            }
        });
    }
});

//修改iframe的读取路径并刷新
function setSrc(path) {
    //此句必须在前面
    $("#iframe").get(0).contentWindow.location.reload(true);
    var iframe = $("#iframe").get(0);
    iframe.src = path;
}