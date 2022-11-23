$(document).ready(function () {
    var _id;
    $('._settingOption').change(handleChangeOption);
    $('._clickID').mousemove(handleChangeId);
    $('._Amount').on('change', handleChangeAmount);
    $('.inc').click(handleChangeAmount);
    $('.dec').click(handleChangeAmount);
    $('._deleteItem').click(handleDeleteItem);

    function handleDeleteItem() {
        $.ajax({
            url: '/Cart/DeleteItem',
            dataType: 'Json',
            type: 'Post',
            data: {
                id: _id
            },
            success: function (res) {
                if (res.status == true) {
                    var _item = '._item-' + _id;
                    $(_item).html('');
                    $('._totalMoney').text(res.total);
                }
            }
        });
    }
    function handleChangeId() {
        _id = $(this).data('id');
    }
    function handleChangeOption() {
        var _color = $(this).val();
        $.ajax({
            url: '/Cart/ChangeColor',
            dataType: 'Json',
            type: 'POST',
            data: {
                id: _id,
                color: _color
            }
        });
    }
    function handleChangeAmount() {
        if ($('._Amount').val() <= 0) {
            $('._Amount').val("1");
        }
        else {
            submitChangeAmount($('._Amount').val());
        }
        
    }
    function submitChangeAmount(value) {
        $.ajax({
            url: '/Cart/ChangeAmount',
            dataType: 'Json',
            type: 'Post',
            data: {
                id: _id,
                amount: value
            },
            success: function (res) {
                if (res.status == true) {
                    var amountID = '._amount-' + _id;
                    var total = res.total;
                    var totalMoney = res.totalMoney;
                    $(amountID).text(total);
                    $('._totalMoney').text(totalMoney);
                }
            }
        });
    }
});