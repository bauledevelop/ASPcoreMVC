$(document).ready(function () {
    let Id = 0;
    $('._deletePayment').click(handleDelItem);
    $('.btn-submit').click(handleSubmit);
    function handleSubmit() {
        $.ajax({
            url: '/Admin/Payment/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Xóa thành công");
                    window.location.reload();
                }
            }
        })
    }
    function handleDelItem() {
        Id = $(this).data('id');
    }
})