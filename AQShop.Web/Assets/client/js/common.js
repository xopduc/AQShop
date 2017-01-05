var common =
{
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListNameProduct",
                    dataType: "json",
                    data: {
                        keyword: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);

                return false;
            }
        })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
          .append("<a>" + item.label + "</a>")
          .appendTo(ul);
    };
        $('#btnLogout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmLogout').submit();
        });
        $('#btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = $(this).data('id');
            common.addItem(productId);
        });
    },
    addItem: function (productId) {
        $.ajax({
            url: 'ShoppingCart/Add',
            data: { productId: productId },
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    alert('Thêm sản phẩm thành công');
                }
            }
        })
    }
};
common.init();