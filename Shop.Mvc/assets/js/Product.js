$(document).ready(function () {
    let _color = 1;
    let _size = 1;
    let _page = 1;
    let _pageSize = 5;
    let _idStar;
    var _idProduct;


    getDataComment();


    $('._btnSubmit').click(handleSubmit);
    $('._color').click(handleClickColor);
    $('._size').click(handleClickSize);
    $('._btnClick').click(handleClick);
    $('._rattingComment').click(handleClickStar);
    $('.btn-submit-comment').click(handleSubmitComment);
    document.addEventListener("click", addEvent);
    function addEvent() {
        $('.btn-page').click(handleClickPage);
    }
    function handleClickPage() {
        console.log("111");
        var getPage = $(this).data('id')
        if (getPage != _page) {
            _page = getPage;
            getDataComment();
        }
    }
    function getDataComment() {
        var _listComment = $('.list-comment');
        _idProduct = _listComment.data('id');
        $.ajax({
            url: '/Comment/GetData',
            dataType: 'Json',
            type: 'Post',
            data: {
                idProduct: _idProduct,
                page: _page,
                pageSize: _pageSize,
            },
            success: function (res) {
                if (res.status == true) {
                    let _listComment = document.getElementsByClassName('list-comment')[0];
                    _listComment.innerHTML = '';
                    var data = res.listComment;
                    var pagination = res.paginationView;
                    console.log(data);
                    var _defineStar1 = '<label for="rcd1" class="rating__item"><svg class="rating__star" ><use xlink:href="#star"></use></svg ><span class="screen-reader">1</span></label>'
                    var _defineStar2 = '<label for="rcd2" class="rating__item"><svg class="rating__star" ><use xlink:href="#star"></use></svg ><span class="screen-reader">2</span></label>'
                    var _defineStar3 = '<label for="rcd3" class="rating__item"><svg class="rating__star" ><use xlink:href="#star"></use></svg ><span class="screen-reader">3</span></label>'
                    var _defineStar4 = '<label for="rcd4" class="rating__item"><svg class="rating__star" ><use xlink:href="#star"></use></svg ><span class="screen-reader">4</span></label>'
                    var _defineStar5 = '<label for="rcd5" class="rating__item"><svg class="rating__star" ><use xlink:href="#star"></use></svg ><span class="screen-reader">5</span></label>'
                    $.each(data, function (i, item) {
                        var _col = document.createElement('div');
                        var _comment = document.createElement('div');
                        var _pic = document.createElement('div');
                        var _img = document.createElement('img');
                        var _rate = document.createElement('div');
                        for (var i = 1; i <= 5; i++) {
                            let _star;
                            if (i > item.rate) {
                                _star = `<input type="radio" disabled name="rating-star${item.id}" class="rating__control screen-reader" id="rcd${i}">`
                            }
                            else {
                                _star = `<input type="radio" checked disabled name="rating-star${item.id}" class="rating__control screen-reader" id="rcd${i}">`
                            }
                            _rate.innerHTML += _star;
                        }
                        _rate.innerHTML += _defineStar1;
                        _rate.innerHTML += (_defineStar2);
                        _rate.innerHTML += (_defineStar3);
                        _rate.innerHTML += (_defineStar4);
                        _rate.innerHTML += (_defineStar5);
                        _rate.classList.add('rating', 'mt-2');
                        _img.classList.add('size-pic-comment', 'd-flex');
                        _img.src = "/Areas/Admin/assets/img/undraw_profile.svg";
                        var _contentComment = document.createElement('div');
                        _contentComment.classList.add('_content-comment', 'flex-column');
                        var _nameComment = document.createElement('div');
                        var _spanName = document.createElement('span');
                        _spanName.textContent = item.username;
                        if (item.typeUser == "1") {
                            _spanName.classList.add('txt-admin');
                        }
                        var _feedback = document.createElement('div');
                        var _spanFeedback = document.createElement('span');
                        _spanFeedback.textContent = item.content;
                        _nameComment.appendChild(_spanName);
                        _nameComment.classList.add('_name-comment');
                        _feedback.append(_spanFeedback);
                        _feedback.classList.add('mt-10', '_feedback-content-comment');
                        _contentComment.appendChild(_nameComment);
                        _contentComment.appendChild(_rate);
                        _contentComment.appendChild(_feedback);
                        _pic.appendChild(_img);
                        _pic.classList.add('_pic-comment');
                        _comment.appendChild(_pic);
                        _comment.appendChild(_contentComment);
                        _comment.classList.add('_comment', '_border-bottom');
                        _col.appendChild(_comment);
                        _col.classList.add('col-lg-12', 'mt-4');
                        _listComment.appendChild(_col);
                    })
                    if (pagination.total > 1) {
                        var _btnToolbar = document.createElement('div');
                        var _btnGroup = document.createElement('div');
                        var pageDisplay = pagination.maxPage;
                        var totalPage = pagination.totalPage;
                        var currentPage = pagination.page;
                        var start = 1;
                        var end = totalPage;

                        if (currentPage > 1) {
                            var _btnCurrent = document.createElement('button');
                            var _i = document.createElement('i');
                            _i.classList.add('fa-solid', 'fa-angle-left');
                            _btnCurrent.append(_i);
                            _btnCurrent.classList.add('btn', 'btn-outline-secondary');
                            _btnCurrent.classList.add('btn-page');
                            _btnCurrent.setAttribute('data-id', currentPage - 1);
                            _btnCurrent.setAttribute('type', 'button');
                            _btnGroup.appendChild(_btnCurrent);
                        }
                        for (var i = start; i <= end; i++) {
                            var _btnCurrent = document.createElement('button');
                            _btnCurrent.classList.add('btn', 'btn-outline-secondary');
                            _btnCurrent.setAttribute('type', 'button');
                            _btnCurrent.setAttribute('data-id', i.toString());
                            _btnCurrent.classList.add('btn-page');
                            _btnCurrent.textContent = i;
                            if (i == currentPage) {
                                _btnCurrent.classList.add('active');
                            }
                            _btnGroup.appendChild(_btnCurrent);
                        }
                        if (currentPage < totalPage) {
                            var _btnCurrent = document.createElement('button');
                            var _i = document.createElement('i');
                            _i.classList.add('fa-solid', 'fa-chevron-right');
                            console.log(_i);
                            _btnCurrent.append(_i);
                            _btnCurrent.classList.add('btn', 'btn-outline-secondary');
                            _btnCurrent.classList.add('btn-page');
                            _btnCurrent.setAttribute('data-id', currentPage + 1);
                            _btnCurrent.setAttribute('type', 'button');
                            _btnGroup.appendChild(_btnCurrent);
                        }
                        _btnGroup.classList.add('btn-group', 'me-2');
                        _btnGroup.setAttribute('role', 'group');
                        _btnToolbar.appendChild(_btnGroup);
                        _btnToolbar.classList.add('btn-toolbar', 'mt-3');
                        _btnToolbar.classList.add('pos-btn', 'mb-3');
                        _btnToolbar.setAttribute('role', 'toolbar');
                        _listComment.appendChild(_btnToolbar);
                    }
                }
            }
        });
    }
    function handleSubmitComment(e) {
        e.preventDefault();
        var _comment = $('.txt-comment').val();
        var _id = $(this).data("id");
        $.ajax({
            url: '/Comment/Insert',
            type: "Post",
            dataType: "Json",
            data: {
                idProduct: _id,
                rate: _idStar,
                comment: _comment,
            },
            success: function (res) {
                if (res.status == true) {
                    alert("Cảm ơn bạn đã đánh giá sản phẩm");
                    location.reload();
                }
                else {
                    alert("Comment thất bại");
                }
            }
        });
    }
    function handleClickStar() {
        _idStar = $(this).val();
    }
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
                            //window.postMessage(
                            //    {
                            //        event: 'ADD_PRODUCT_TO_CART',
                            //        value: {
                            //            product_id: _id,
                            //            user_id: 0
                            //        },                                    
                            //    }, location.href);
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
