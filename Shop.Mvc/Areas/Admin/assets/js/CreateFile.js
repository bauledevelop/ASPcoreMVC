$(document).ready(function () {
    $('#IDCategory').on('change', function () {
        var id = $(this).val();
        console.log(id);
        if (id != '0') {
            $('#IDProduct').html('');
            getProduct(id);
        }
        else {
            $('#IDProduct').html('');
        }
    });

    function getProduct(id) {
        $.ajax({
            url: '/Admin/Product/GetProduct',
            type: "POST",
            dataType: "Json",
            data: {
                IDCategory: id
            },
            success: function (res) {
                if (res.status == true) {
                    var data = res.data;

                    $('#IDProduct').append(new Option("Chọn sản phẩm","0"));
                    $.each(data, function (i, item) {
                        $('#IDProduct').append(new Option(item.name, item.id));
                    })
                }
            }

        });
    }
});

