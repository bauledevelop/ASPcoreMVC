$(document).ready(function () {
    $('#IDMenu').on('change', function () {
        var id = $(this).val();
        console.log(id);
        if (id != '0') {
            $('#getCategoryProduct').html('');
            getCategoryProduct(id);
        }
        else {
            $('#getCategoryProduct').html('');
        }
    });

    function getCategoryProduct(id) {
        $.ajax({
            url: '/Admin/CategoryProduct/GetCategory',
            type: "POST",
            dataType: "Json",
            data: {
                IDMenu: id
            },
            success: function (res) {
                if (res.status == true) {
                    var data = res.data;
                   
                    $('#getCategoryProduct').append(new Option("Chọn loại sản phẩm") ,"0");
                    $.each(data, function (i, item) {
                        $('#getCategoryProduct').append(new Option(item.name, item.id));
                    })
                }
            }
            
        });
    }
});

    