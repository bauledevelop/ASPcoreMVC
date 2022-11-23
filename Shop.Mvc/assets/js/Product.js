$(document).ready(function () {
    let _color = 1;
    let _size = 1;

    $('._btnSubmit').click(handleSubmit);
    $('._color').click(handleClickColor);
    $('._size').click(handleClickSize);
    $('._btnClick').click(handleClick);

    function handleClick() {
        console.log("check");
        if ($('._amount').val() <= 0) {
            $('._amount').val("1");
        }
    }
    function handleClickColor() {
        _color = $(this).data('value');
    }
    function handleClickSize() {
        _size = $(this).data('value');
    }
    function handleSubmit() {
        let _value = $('._amount').val();
        let _id = $(this).data('id');
        $.ajax({
            url: '/Cart/Insert',
            dataType: 'Json',
            type: 'POST',
            data: {
                id: _id,
                color: _color,
                size: _size,
                amount: _value
            },
            success: function (res) {
                if (res.status == true) {
                    if (res.isLogin == false) {
                        alert("Vui lòng đăng nhập trước khi mua hàng");
                    }
                    else {
                        if (res.isInsert) {
                            alert("Thêm vào giỏ hàng thành công");
                        }
                        else {
                            alert("Sản phẩm đã tồn tại trong giỏ hàng");
                        }
                    }
                } else {
                    alert("Thêm vào giỏ hàng không thành công");
                }
            },
            error: function () {
                alert("Lỗi thêm giỏ hàng");
            }
        });
    }
});