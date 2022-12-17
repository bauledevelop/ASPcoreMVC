$(document).ready(function () {
    let Id = 0;
    $('._deleteAccount').click(handleDelItem);
    $('.btn-submit').click(handleSubmit);
    function handleSubmit() {
        console.log(Id);
        $.ajax({
            url: '/Admin/Account/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Xóa người dùng thành công");
                    $('#deleteAccount').modal('hide');
                    var item = ".item-" + Id.toString();
                    $(item).html('');
                }
            }
        })
    }
    function handleDelItem() {
        Id = $(this).data('id');
    }
})