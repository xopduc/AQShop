var cart =
{
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        //binding events
        //add to cart
        $('#btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = $(this).data('id');
            cart.addItem(productId);
        });

        //delete item in cart
        $('.btnDeleteItem').off('click').on('click', function(e){
            e.preventDefault();
            var productId = $(this).data('id');
            cart.deleteItem(productId);
        });

        //delete all items in cart
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAllItem();
        });

        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });

        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });

        //update amount by quantity
        $('.txtQuantity').off('keyup').on('keyup', function (e) {
           
            var quantity = parseInt($(this).val());
            var productId = $(this).data('id');
            var price = parseFloat($(this).data('price'));

            if(isNaN(quantity) == false)
            {
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productId).text(0);
            }

            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        });

        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }

        });

    },

    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "Thanh toán tiền mặt",
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    console.log('create order ok');
                    $('#divCheckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('#cartContent').html('Cảm ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ sớm nhất.');
                    }, 2000);

                }
            }
        });
    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    deleteAllItem: function (productId) {
        $.ajax({
            url: 'ShoppingCart/DeleteAll',
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },

    deleteItem: function(productId){
        $.ajax({
            url: 'ShoppingCart/DeleteItem',
            data: { productId: productId },
            type: 'post',
            dataType: 'json',
            success: function(res)
            {
                if(res.status)
                {
                    cart.loadData();
                }
            }
        });
    },
   
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'get',
            dataType: 'json',
            success:function(res)
            {
                if(res.status)
                {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    });
                    $('#cartBody').html(html);
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        })
    },

    getTotalOrder: function()
    {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });

        return total;
    }

}
cart.init();
