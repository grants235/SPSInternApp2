// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.getElementById('loginNextStep1').addEventListener("click", e => {
    document.getElementById('loginStatus').innertext = " ";
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var data = { 'username': username, 'email': email };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/Login', requestInfo)
        .then(response => {
            if (response.ok) {

                document.getElementById('loginNextStep1').classList.add('d-none');
                document.getElementById('confirmTwoFactorButton').classList.remove('d-none');
                document.getElementById('loginUsernameEmailContainer').classList.add('d-none');
                document.getElementById('loginSecondStepFormGroup').classList.remove('d-none');
                document.getElementById('modalCancel').classList.add('d-none');
                document.getElementById('modalBack').classList.remove('d-none');
                document.getElementById('LoginPasswordFormGroup').classList.add('d-none');
                document.getElementById('PasswordSubmitButton').classList.add('d-none');
            }
            else {
                document.getElementById('loginStatus').innerText = "Login Failed";
            }
        })
        
});

document.getElementById('modalBack').addEventListener("click", e => {
    document.getElementById('loginNextStep1').classList.remove('d-none');
    document.getElementById('confirmTwoFactorButton').classList.add('d-none');
    document.getElementById('loginUsernameEmailContainer').classList.remove('d-none');
    document.getElementById('loginSecondStepFormGroup').classList.add('d-none');
    document.getElementById('modalCancel').classList.remove('d-none');
    document.getElementById('modalBack').classList.add('d-none');
    document.getElementById('LoginPasswordFormGroup').classList.add('d-none');
    document.getElementById('PasswordSubmitButton').classList.add('d-none');

});

document.getElementById('confirmTwoFactorButton').addEventListener("click", e => {
    document.getElementById('loginStatus').innertext = " ";
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var SecurityQuestion1 = document.getElementById('SecurityQuestion1').value;
    var SecurityQuestion2 = document.getElementById('SecurityQuestion2').value;
    var data = { 'username': username, 'email': email, 'SecurityQuestion1':SecurityQuestion1, "SecurityQuestion2":SecurityQuestion2};
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/SecurityQuestion', requestInfo)
        .then(response => {
            if (response.ok) {

                document.getElementById('loginNextStep1').classList.add('d-none');
                document.getElementById('confirmTwoFactorButton').classList.add('d-none');
                document.getElementById('loginUsernameEmailContainer').classList.add('d-none');
                document.getElementById('loginSecondStepFormGroup').classList.add('d-none');
                document.getElementById('modalCancel').classList.add('d-none');
                document.getElementById('modalBack').classList.add('d-none');
                document.getElementById('LoginPasswordFormGroup').classList.remove('d-none');
                document.getElementById('PasswordSubmitButton').classList.remove('d-none');
            }
            else {
                document.getElementById('loginStatus').innerText = "Login Failed";
            }
        })

});

document.getElementById('PasswordSubmitButton').addEventListener("click", e => {

    document.getElementById('loginStatus').innertext = " ";
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var SecurityQuestion1 = document.getElementById('SecurityQuestion1').value;
    var SecurityQuestion2 = document.getElementById('SecurityQuestion2').value;
    var Password = document.getElementById('passwordentry').value;
    var data = { 'username': username, 'email': email, 'SecurityQuestion1': SecurityQuestion1, "SecurityQuestion2": SecurityQuestion2, "Password":Password };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/Password', requestInfo)
        .then(response => {
            if (response.ok) {
                document.getElementById('loginStatus').classList.remove('text-danger');
                document.getElementById('loginStatus').innerText = "Login Suscessful";
                document.getElementById('LoginPasswordFormGroup').classList.add('d-none');
                document.getElementById('PasswordSubmitButton').classList.add('d-none');
                document.getElementById('modalClose').classList.remove('d-none');
                return response.json();
            }
            else {
                document.getElementById('loginStatus').innerText = "Login Failed";
            }
        })
        .then(data => {
            sessionStorage.setItem('jwt', data.token);
            document.location = '/';
        });

});

document.getElementById('LogoutButton').addEventListener("click", e => {
    sessionStorage.removeItem('jwt');
    document.location = '/';
});
