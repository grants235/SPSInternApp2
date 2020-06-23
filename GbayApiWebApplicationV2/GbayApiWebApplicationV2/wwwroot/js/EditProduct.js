document.body.addEventListener('click', e => {
    if (e.srcElement.id == 'EditProductModal0') {
        var jwt = sessionStorage.getItem('jwt');
        if (jwt != null && jwt != " ") {
            var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
            fetch('/api/ManageProducts', requestInfo)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then(data => {
                    document.getElementById('EditProductName').value = data.products[0].productName;
                    document.getElementById('EditProductDescription').value = data.products[0].description;
                    document.getElementById('EditProductPrice').value = data.products[0].price;
                    document.getElementById('EditProductImageUrl').value = data.products[0].imgUrl;
                    //document.getElementById('EditProductId').value = data.products[0].id;

                    $("#EditProductModal").modal('show');
                });
        }
    }
});