document.getElementById('CreateProductSubmit').addEventListener("click", e => {
    var jwt = sessionStorage.getItem('jwt');
    if (jwt != null && jwt != " ") {
        var productName = document.getElementById('CreateProductName').value;
        var productDescription = document.getElementById('CreateProductDescription').value;
        var productPrice = document.getElementById('CreateProductPrice').value;
        var productImgUrl = document.getElementById('CreateProductImageUrl').value;
        var requestInfo = { 'method': 'POST', headers: { 'Content-Type': "application/json", 'Authorization': 'bearer ' + jwt, 'ProductName': productName, 'ProductDescription': productDescription, 'ProductPrice': productPrice, 'ProductImgUrl': productImgUrl }, credentials: 'same-origin' };
        fetch('/api/CreateProduct', requestInfo)
            .then(response => {
                if (response.ok) {
                    $("#CreateProduct").modal('hide');
                    document.location = '/Administration/ManageProducts'
                } else {
                    document.getElementById('EditProductStatus').innerText = "Error";
                }
            });
    }
});