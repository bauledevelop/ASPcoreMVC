$(document).ready(function () {
    let Id = 0;
    $('._deleteOrder').click(handleDelItem);
    $('._clickFile').click(handleClickFile);
    $('.btn-submit').click(handleSubmit);
    function handleSubmit() {
        $.ajax({
            url: '/Admin/Order/Delete',
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
                    $('._renderData').css("display", "none");
                }
            }
        })
    }
    function handleClickFile() {
        var id = $(this).data('id');
        console.log(id);
        $.ajax({
            url: '/Admin/OrderDetail/GetDataByIDOrder',
            dataType: 'Json',
            type: 'POST',
            data: {
                IDOrder: id
            },
            success: function (res) {
                if (res.status == true) {
                    $('._renderData').css("display", "block");
                    var data = res.data;
                    let getData = document.getElementsByClassName('_getData')[0];
                    getData.innerHTML = '';
                    $.each(data, function (i, item) {
                        var tr = document.createElement('tr');
                        var tdName = document.createElement('td');
                        var tdQuantity = document.createElement('td');
                        var tdSize = document.createElement('td');
                        var tdColor = document.createElement('td');
                        var tdTotal = document.createElement('td');
                        tdName.innerHTML = item.name;
                        tdQuantity.innerHTML = item.quantity;
                        tdSize.innerHTML = item.size;
                        tdColor.innerHTML = item.color;
                        tdTotal.innerHTML = item.total;
                        tr.appendChild(tdName);
                        tr.appendChild(tdQuantity);
                        tr.appendChild(tdSize);
                        tr.appendChild(tdColor);
                        tr.appendChild(tdTotal);
                        getData.appendChild(tr);
                    })
                }
                else {
                    $('._renderData').css("display", "none");
                    alert("Không có dữ liệu cho sản phẩm này");
                }
            }
        });
    }
    function handleDelItem() {
        Id = $(this).data('id');
    }
})