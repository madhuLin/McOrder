$(function () {
    var apiurl = location.href.split("/")[0];
    alert(apiurl);
    function fnDelete() {
        
        var r = confirm("確定要刪除嗎?");
        if (r) {
            var url = location.href.split("/")[0];
            
            var id = $("#id").val();
            $.ajax({
                url: apiurl + "?id" + encodeURI(id),
                type: "DELETE",
                success: function (result) {
                    if (result != 0) {
                        alert("刪除成功");
                        location.reload();
                    }
                    else {
                        alert("刪除失敗");
                    }
                }
            });
        }
    }
});