$(document).ready(function () {
    let Id = 0;
    let IdImage = 0;
    $('._clickFile').click(handleClickFile);
    $('._deleteProduct').click(handleDelItem);
    $('.btn-submit').click(handleSubmit);
    $('.btn-click-delete').click(handleClickDeleteImage);
    function handleClickDeleteImage() {
        console.log(IdImage);
        $.ajax({
            url: '/Admin/File/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: IdImage
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Xóa hình ảnh thành công");
                    $('._renderData').css("display", "none");
                    $('#deleteImage').modal('hide');
                }
            }
        });
    }
    
    function handleSubmit() {
        console.log(Id);
        $.ajax({
            url: '/Admin/Product/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Xóa sản phẩm thành công");
                    var item = ".item-" + Id;
                    $(item).html('');
                    $('._renderData').css("display", "none");
                    $('#deleteProduct').modal('hide');
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
                        var tool = document.createElement('td');
                        var span = document.createElement('span');
                        var iDelete = document.createElement('i');
                        iDelete.classList.add('fa-solid', 'fa-trash');
                        iDelete.classList.add('color-trash');
                        span.append(iDelete);
                        span.setAttribute('data-bs-toggle', 'modal');
                        span.setAttribute('data-bs-target', '#deleteImage');
                        span.setAttribute('data-id', item.id);
                        span.setAttribute('class', '_deleteImage');
                        tool.appendChild(span);
                        tdType.innerHTML = (item.type == 1) ? "Hình ảnh" : "Video";
                        img.src = "/uploadFiles/" + item.fileContent;
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
                        tr.appendChild(tool);
                        getData.appendChild(tr);
                    })
                    $('._deleteImage').click(handleDelImage);
                    function handleDelImage() {
                        IdImage = $(this).data('id');
                    }
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