$(document).ready(function () {
    let Id = 0;
    $('._deleteFile').click(handleDelItem);
    $('.btn-submit').click(handleSubmit);
    function handleSubmit() {
        $.ajax({
            url: '/Admin/File/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Xóa người dùng thành công");
                    $('#deleteFile').modal('hide');
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