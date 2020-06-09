// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("loginsubmit").addEventListener("click", e => {
    document.getElementById('loginStatus').innertext = " ";
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var data = { 'username': username, 'email': email };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/Login', requestInfo)
        .then(response => {
            if (response.ok) {

                document.getElementById('loginsubmit').classList.add('d-none');
                document.getElementById('confirmTwoFactorButton').classList.remove('d-none');
                document.getElementById('loginUsernameEmailContainer').classList.add('d-none');
                document.getElementById('loginTwoFactorFormGroup').classList.remove('d-none');

            }
            else {
                document.getElementById('loginStatus').innerText = "Login Failed";
            }
        })
        
});
