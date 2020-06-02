window.onload = function () {
    setSrc($("#resumePath").val());
};

//修改iframe的读取路径并刷新
function setSrc(path) {
    //此句必须在前面
    $("#iframe").get(0).contentWindow.location.reload(true);
    var iframe = $("#iframe").get(0);
    iframe.src = path;
}