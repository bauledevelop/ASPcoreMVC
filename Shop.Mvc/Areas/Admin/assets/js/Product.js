    $(document).ready(function () {
    let Id = 0;
    $('._clickFile').click(handleClickFile);
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
                    $('._renderData').css("display", "none");
                }
            }
        })
    }
    function handleClickFile() {
        var id = $(this).data('id');
        console.log(id);
        $.ajax({
            url: '/Admin/File/GetDataByIDProduct',
            dataType: 'Json',
            type: 'POST',
            data: {
                IDProduct: id
            },
            success: function (res) {
                if (res.status == true) {
                    $('._renderData').css("display", "block");
                    var data = res.data;
                    let getData = document.getElementsByClassName('_getData')[0];
                    getData.innerHTML = '';
                    $.each(data, function (i, item) {
                        var tr = document.createElement('tr');
                        var tdType = document.createElement('td');
                        var tdContent = document.createElement('td');
                        var tdUpdate = document.createElement('td');
                        var tdCreate = document.createElement('td');
                        var img = document.createElement('img');
                        tdType.innerHTML = (item.type == 1) ? "Hình ảnh" : "Video";
                        img.src = item.fileContent;
                        img.classList.add('pic', 'pic__size');
                        tdContent.appendChild(img);
                        var m = new Date(item.createdDate);
                        var dateString = m.getUTCFullYear() + "/" + (m.getUTCMonth() + 1) + "/" + m.getUTCDate() + " " + m.getUTCHours() + ":" + m.getUTCMinutes() + ":" + m.getUTCSeconds();
                        + m.getUTCSeconds();
                        tdCreate.innerHTML = dateString;
                        m = new Date(item.updatedDate);
                        dateString = m.getUTCFullYear() + "/" + (m.getUTCMonth() + 1) + "/" + m.getUTCDate() + " " + m.getUTCHours() + ":" + m.getUTCMinutes() + ":" + m.getUTCSeconds();
                        tdUpdate.innerHTML = dateString;
                        tr.appendChild(tdType);
                        tr.appendChild(tdContent);
                        tr.appendChild(tdUpdate);
                        tr.appendChild(tdCreate);
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