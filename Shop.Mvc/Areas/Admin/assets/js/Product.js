$(document).ready(function () {
    let Id = 0;
    $('._deleteProduct').click(handleDelItem);
    $('.btn-submit').click(handleSubmit);
    function handleSubmit() {
        $.ajax({
            url: '/Admin/Product/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Xóa người dùng thành công");
                    var item = ".item-" + Id;
                    $(item).html('');
                }
            }
        })
    }
    function handleDelItem() {
        Id = $(this).data('id');
    }
})