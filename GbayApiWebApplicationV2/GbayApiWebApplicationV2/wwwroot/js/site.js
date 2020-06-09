// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("loginsubmit").addEventListener("click", e => {
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    data = { 'username': username, 'email': email };
    myFetch('/api/v1/Login', 'POST', false, data)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
                //return response.json();
            }
        })
        .then(data => {
            //sessionStorage.setItem('jwt', data.token);            
            //document.location.href = '/';
            //isLoggedIn();
            //$('#modalLogin').modal('hide');
            document.getElementById('loginButton').classList.add('d-none');
            document.getElementById('confirmTwoFactorButton').classList.remove('d-none');
            document.getElementById('loginUsernamePasswordContainer').classList.add('d-none');
            document.getElementById('loginTwoFactorFormGroup').classList.remove('d-none');
            enableLoginSpinner(false);
        })
        .catch(error => {
            console.log(error);
            document.getElementById('loginStatus').innerText = "Login Failed";
            enableLoginSpinner(false);
        });
});