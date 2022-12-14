$(document).ready(function () {
    var id;
    $('._clickAddBag').click(HandleClickAddBag);
    $('._clickMenu').mousemove(handleMenu);
    $('._clickDelete').click(handleClickDelete);

    function handleClickDelete() {
        console.log(id);
        $.ajax({
            url: '/Cart/DeleteItem',
            dataType: 'Json',
            type: 'Post',
            data: {
                id: id
            },
            success: function (res) {
                if (res.status == true) {
                    var _item = '._item-' + id;
                    $(_item).html('');
                    $('._totalMoney').text(res.total);
                }
            }
        });
    }
    function handleMenu() {
        id = $(this).data('id');
    }
    function HandleClickAddBag() {
        var _id = $(this).data('id');
        console.log(_id);
        $.ajax({
            url: '/Cart/Insert',
            dataType: 'Json',
            type: 'POST',
            data: {
                id: _id,
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
            }
        });
    }
});