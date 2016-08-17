$(function () {

    var checkboxes = $('.all-products input[type=checkbox]');

    $('#clear_filters').click(function (e) {
        $(checkboxes).attr('checked', false);
    });

    $('.single-product').click(function (e) {
        if ($(this).hasClass('visible')) {

            var clicked = $(e.target);

            if (clicked.hasClass('close') || clicked.hasClass('overlay')) {
                window.location.hash = '#';
            }
        }
    });

    $.ajaxSetup({
        beforeSend: function () {
            // show gif here
            $("#loading").show();
        },
        complete: function () {
            // hide gif here
            $("#loading").hide();
        }
    });

    getAllProducts();

    $(window).on('hashchange', function () {
        render(window.location.hash);
    });

    function render(url) {

        var temp = url.split('/')[0];

        $('.main-content .page').removeClass('visible');

        var map = {
            '': function () {
                $('.all-products').addClass('visible');
            },
            '#product': function () {
                var index = url.split('product/')[1].trim();

                var data = localStorage.getItem("data");
                var item = JSON.parse(data).filter(function (data) { return data.id == index })[0];

                $('.single-product').attr("id", item.id);

                $('.single-product').html('<div class="overlay"></div>' +
                                            '<div class="preview-large">' +
                                            '<h3>' + item.name + '</h3>' +
                                            '<img src="' + item.image.largeImage + '" />' +
                                            '<p>Specifications</p>' +
                                            '<div class="container">' +
                                                '<div class="row">' +
                                                    '<div class="col-xs-4 right">Manufacturer:</div>' +
                                                    '<div class="col-xs-4 left">' + item.specs.manufacturer + '</div>' +
                                                 '</div>' +
                                                '<div class="row">' +
                                                    '<div class="col-xs-4 right">Operating system:</div>' +
                                                    '<div class="col-xs-4 left">' + item.specs.os + '</div>' +
                                                '</div>' +
                                                '<div class="row">' +
                                                    '<div class="col-xs-4 right">Storage:</div>' +
                                                    '<div class="col-xs-4 left">' + item.specs.storage + '</div>' +
                                                '</div>' +
                                                '<div class="row">' +
                                                    '<div class="col-xs-4 right">Camera:</div>' +
                                                    '<div class="col-xs-4 left">' + item.specs.camera + '</div>' +
                                                '</div>' +
                                                '<div class="row">' +
                                                    '<div class="col-xs-4 right"><b>Price:</b></div>' +
                                                    '<div class="col-xs-4 left"><b>' + item.price + '&#8364</b></div>' +
                                                '</div>' +
                                            '</div>' +
                                            '<span class="close">×</span></br>' +
                                          '</div>');

                $("#" + index + ".single-product").addClass('visible');
            }
        };
        if (temp == "#")
            temp = "";
        map[temp]();
    }

    function onBuyClick() {
        alert("Out of stock!");
    }

    function getAllProducts() {
        $.ajax(
            {
                url: "api/phones",
                type: "Get",
                success: function (data) {
                    displayProducts(data);
                },
                error: function (msg) {
                    alert(msg.responseJSON.exceptionMessage)
                }
            });
    }

    function displayProducts(data) {
        localStorage.setItem("data", JSON.stringify(data));
        $("#productList").html("");
        for (var i = 0; i < data.length; i++) {
            var html = '<li id=""products-template" data-index="' + data[i].id + '">' +
            '<a href="#product/' + data[i].id + '">' +
            '<div class="product-photo"><img src="' + data[i].image.smallImage + '" height="130" alt="' + data[i].name + '" /></div>' +
            '<h2>' + data[i].name + '</h2>' +
            '<div class="product-description">' +
                '<p class="product-price">' + data[i].price + '<spna>&#8364</span></p>' +
            '</div>' +
            '<div class="highlight"></div>' +
        '</a>' +
        '<button class="btn btn-primary" id = "buy' + data[i].id + '">Buy</button>' +
        '</li>';

            $("#productList").append(html);
            $("#buy" + data[i].id).click(function (e) { onBuyClick(); });
        }
    }

    $("#filter").click(function (e) {
        var arr = $("#formFilters").serializeArray();
        var filters = {};
        $.each(arr, function () {
            if (filters[this.name] !== undefined) {
                if (!filters[this.name].push) {
                    filters[this.name] = [filters[this.name]];
                }
                filters[this.name].push(this.value || '');
            } else {
                filters[this.name] = [this.value] || '';
            }
        });
        filters = JSON.stringify(filters);
        if (arr.length > 0) {
            $.ajax({
                url: "api/phones",
                type: "POST",
                data: filters,
                contentType: "application/json",
                success: function (data) {
                    displayProducts(data);
                },
                error: function (msg) {
                    alert(msg.responseJSON.exceptionMessage)
                }
            });
        }
    })


});