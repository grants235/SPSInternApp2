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
            if (response.status == 200) {

                document.getElementById('loginNextStep1').classList.add('d-none');
                document.getElementById('confirmTwoFactorButton').classList.remove('d-none');
                document.getElementById('loginUsernameEmailContainer').classList.add('d-none');
                document.getElementById('loginSecondStepFormGroup').classList.remove('d-none');
                document.getElementById('modalCancel').classList.add('d-none');
                document.getElementById('modalBack').classList.remove('d-none');
                document.getElementById('LoginPasswordFormGroup').classList.add('d-none');
                document.getElementById('PasswordSubmitButton').classList.add('d-none');
                document.getElementById('loginStatus').innerText = "";
            }
            else if (response.status == 204) {
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
    var data = { 'username': username, 'email': email, 'SecurityQuestion1': SecurityQuestion1, "SecurityQuestion2": SecurityQuestion2 };
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
    var data = { 'username': username, 'email': email, 'SecurityQuestion1': SecurityQuestion1, "SecurityQuestion2": SecurityQuestion2, "Password": Password };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/Password', requestInfo)
        .then(response => {
            if (response.status == 200) {
                document.getElementById('loginStatus').classList.remove('text-danger');
                document.getElementById('loginStatus').innerText = "Login Suscessful";
                document.getElementById('LoginPasswordFormGroup').classList.add('d-none');
                document.getElementById('PasswordSubmitButton').classList.add('d-none');
                document.getElementById('modalClose').classList.remove('d-none');
                return response.json();
            } else if (response.status == 204) {
                document.getElementById('loginNextStep1').classList.add('d-none');
                document.getElementById('confirmTwoFactorButton').classList.add('d-none');
                document.getElementById('loginUsernameEmailContainer').classList.add('d-none');
                document.getElementById('loginSecondStepFormGroup').classList.add('d-none');
                document.getElementById('modalCancel').classList.add('d-none');
                document.getElementById('modalBack').classList.remove('d-none');
                document.getElementById('LoginPasswordFormGroup').classList.add('d-none');
                document.getElementById('PasswordSubmitButton').classList.add('d-none');
                document.getElementById('loginSetupSecondStepFormGroup').classList.remove('d-none');
                document.getElementById('SetupSecondStep').classList.remove('d-none');
                document.getElementById('loginStatus').innerText = "";
            }
            else {
                document.getElementById('loginStatus').innerText = "Login Failed";
            }
        })
        .then(data => {
            if (typeof data !== 'undefined') {
                sessionStorage.setItem('jwt', data.token);
                document.location = '/';
            }
        });

});

document.getElementById('LogoutButton').addEventListener("click", e => {
    sessionStorage.removeItem('jwt');
    document.location = '/';
});

document.getElementById('RegisterSubmit').addEventListener("click", e => {
    var username = document.getElementById('CreateUsername').value;
    var email = document.getElementById('CreateEmail').value;
    var password = document.getElementById('CreatePassword').value;
    var confirmPassword = document.getElementById('CreateConfirmPassword').value;
    var buyerCheck = document.getElementById('BuyerCheckbox').checked;
    var sellerCheck = document.getElementById('SellerCheckbox').checked;
    if (password != confirmPassword) {
        document.getElementById('RegisterStatus').innerText = "Passwords do not match";
    } else {
        var data = { 'username': username, 'email': email, 'password': password, 'confirmPassword': confirmPassword, 'buyer': buyerCheck, 'seller': sellerCheck };
        var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
        fetch('/api/Register', requestInfo)
            .then(response => {
                if (response.ok) {
                    document.getElementById('RegisterFormGroup').classList.add('d-none');
                    document.getElementById('modalCancelRegistration').classList.add('d-none');
                    document.getElementById('modalCloseRegistration').classList.remove('d-none');
                    document.getElementById('RegisterEmailConfirmation').classList.remove('d-none');
                    document.getElementById('RegisterSubmit').classList.add('d-none');
                    document.getElementById('RegisterStatus').innerText = "";
                }
                else {
                    document.getElementById('RegisterStatus').innerText = "Registration Failed";
                }
            });
    }

});


$(document).ready(function () {

    if (window.location.href.indexOf('#LoginModal') != -1) {
        $('#LoginModal').modal('show');
    };
});

$(document).ready(function () {

    if (window.location.href.indexOf('#ResetPasswordModal') != -1) {
        $('#ResetPasswordModal').modal('show');
    };
});


document.getElementById('SetupSecondStep').addEventListener("click", e => {
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var SecurityQuestion1 = document.getElementById('SetupSecurityQuestion1').value;
    var SecurityQuestion2 = document.getElementById('SetupSecurityQuestion2').value;
    var Password = document.getElementById('passwordentry').value;
    var data = { 'username': username, 'email': email, 'SecurityQuestion1': SecurityQuestion1, "SecurityQuestion2": SecurityQuestion2, "Password": Password };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/SetupSecurityQuestions', requestInfo)
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
                document.getElementById('loginStatus').innerText = "Setup Failed";
            }
        })
        .then(data => {
            sessionStorage.setItem('jwt', data.token);
            document.location = '/';
        });
});

document.getElementById('GoToForgotPasword').addEventListener("click", e => {
    $('#LoginModal').modal('hide');
    $('#ForgotPasswordModal').modal('show');
});

document.getElementById('ForgotPasswordSubmit').addEventListener("click", e => {
    var email = document.getElementById('ForgotPasswordEmail').value;
    var data = { 'email': email };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/ForgotPassword', requestInfo)
        .then(response => {
            if (response.ok) {
                document.getElementById('ForgotPaswordModalClose').classList.remove('d-none');
                document.getElementById('ForgotPasswordOk').classList.remove('d-none');
                document.getElementById('ForgotPasswordSubmit').classList.add('d-none');
                document.getElementById('ForgotPasswordCancel').classList.add('d-none');
                document.getElementById('ForgotPasswordFormGroup').classList.add('d-none');
            }
        });

});

document.getElementById('ResetPasswordSubmit').addEventListener("click", e => {
    var userId = document.getElementById('userId').value;
    var token = document.getElementById('token').value;
    var password = document.getElementById('Password').value;
    var confirmPassword = document.getElementById('ConfirmPassword').value;
    if (password == confirmPassword) {
        var data = { 'userId': userId, 'token': token, 'password': password, 'confirmPassword': confirmPassword };
        var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
        fetch('/api/ResetPassword', requestInfo)
            .then(response => {
                if (response.ok) {
                    document.getElementById('loginStatus').innerText = "Password Reset Suscessful";
                    document.location = '/#LoginModal';
                } else {
                    document.getElementById('ResetPasswordStatus').innerText = "Password Reset Failed";
                }
            });
    } else {
        document.getElementById('ResetPasswordStatus').innerText = "Passwords do not match";
    }
});

document.addEventListener("DOMContentLoaded", e => {
    var jwt = sessionStorage.getItem('jwt');
    if (jwt != null && jwt != " ") {
        var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
        fetch('/api/MyAccount', requestInfo)
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
            })
            .then(data => {
                document.getElementById('MyAccountUsername').innerText = data.username;
                document.getElementById('MyAccountEmail').innerText = data.email;
            })
            .catch(error => {
                console.log(error);
            });
    }

});

document.getElementById('EditResetPasswordLink').addEventListener('click', e => {
    var email = document.getElementById('EditEmail').value;
    var data = { 'email': email };
    var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
    fetch('/api/ForgotPassword', requestInfo)
        .then(response => {
            if (response.ok) {
                document.getElementById('EditResetPasswordLink').innerText = "Suscessfully sent email!"
            }
        });
});

document.getElementById('EditSubmit').addEventListener('click', e => {
    var id = document.getElementById('EditId').value;
    var username = document.getElementById('EditUsername').value;
    var email = document.getElementById('EditEmail').value;
    var securityQuestion1 = document.getElementById('EditSecurityQuestion1').value;
    var securityQuestion2 = document.getElementById('EditSecurityQuestion2').value;
    var buyer = document.getElementById('EditBuyerCheckbox').checked;
    var seller = document.getElementById('EditSellerCheckbox').checked;
    var moderator = document.getElementById('EditModeratorCheckbox').checked;
    var admin = document.getElementById('EditAdminCheckbox').checked;
    var jwt = sessionStorage.getItem('jwt');
    if (jwt != null && jwt != " ") {
        var data = { 'id' : id, 'username': username, 'email': email, 'securityquestion1': securityQuestion1, 'securityQuestion2': securityQuestion2, 'buyer': buyer, 'seller': seller, 'moderator': moderator, 'administrator': admin }
        var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json', 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
        fetch('/api/EditUser', requestInfo)
            .then(response => {
                if (response.ok) {
                    $("#EditUserModal").modal('hide');
                    document.location = '/Administration/ManageUsers'
                } else {
                    document.getElementById('EditStatus').innerText = "Error";
                }
            });
    }
});

document.body.addEventListener('click', e => {
    if (e.srcElement.id == 'EditUserModalOpenButton0') {
        var jwt = sessionStorage.getItem('jwt');
        if (jwt != null && jwt != " ") {
            var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
            fetch('/api/ManageUsers', requestInfo)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then(data => {
                    document.getElementById('EditUsername').value = data[0].username;
                    document.getElementById('EditEmail').value = data[0].email;
                    document.getElementById('EditSecurityQuestion1').value = data[0].securityQuestion1;
                    document.getElementById('EditSecurityQuestion2').value = data[0].securityQuestion2;
                    document.getElementById('EditId').value = data[1].id;
                    if (data[0].roles.includes("Buyers")) {
                        document.getElementById('EditBuyerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[0].roles.includes("Sellers")) {
                        document.getElementById('EditSellerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[0].roles.includes("Moderators")) {
                        document.getElementById('EditModeratorCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditModeratorCheckbox').checked = false;
                    }
                    if (data[0].roles.includes("Administrators")) {
                        document.getElementById('EditAdminCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditAdminCheckbox').checked = false;
                    }
                    $("#EditUserModal").modal('show');
                });
        }
    }
});

document.body.addEventListener('click', e => {
    if (e.srcElement.id == 'EditUserModalOpenButton1') {
        var jwt = sessionStorage.getItem('jwt');
        if (jwt != null && jwt != " ") {
            var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
            fetch('/api/ManageUsers', requestInfo)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then(data => {
                    document.getElementById('EditUsername').value = data[1].username;
                    document.getElementById('EditEmail').value = data[1].email;
                    document.getElementById('EditSecurityQuestion1').value = data[1].securityQuestion1;
                    document.getElementById('EditSecurityQuestion2').value = data[1].securityQuestion2;
                    document.getElementById('EditId').value = data[1].id;
                    if (data[1].roles.includes("Buyers")) {
                        document.getElementById('EditBuyerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[1].roles.includes("Sellers")) {
                        document.getElementById('EditSellerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[1].roles.includes("Moderators")) {
                        document.getElementById('EditModeratorCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditModeratorCheckbox').checked = false;
                    }
                    if (data[1].roles.includes("Administrators")) {
                        document.getElementById('EditAdminCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditAdminCheckbox').checked = false;
                    }
                    $("#EditUserModal").modal('show');
                });
        }
    }
});

document.body.addEventListener('click', e => {
    if (e.srcElement.id == 'EditUserModalOpenButton2') {
        var jwt = sessionStorage.getItem('jwt');
        if (jwt != null && jwt != " ") {
            var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
            fetch('/api/ManageUsers', requestInfo)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then(data => {
                    document.getElementById('EditUsername').value = data[2].username;
                    document.getElementById('EditEmail').value = data[2].email;
                    document.getElementById('EditSecurityQuestion1').value = data[2].securityQuestion1;
                    document.getElementById('EditSecurityQuestion2').value = data[2].securityQuestion2;
                    document.getElementById('EditId').value = data[2].id;
                    if (data[2].roles.includes("Buyers")) {
                        document.getElementById('EditBuyerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[2].roles.includes("Sellers")) {
                        document.getElementById('EditSellerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[2].roles.includes("Moderators")) {
                        document.getElementById('EditModeratorCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditModeratorCheckbox').checked = false;
                    }
                    if (data[2].roles.includes("Administrators")) {
                        document.getElementById('EditAdminCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditAdminCheckbox').checked = false;
                    }
                    $("#EditUserModal").modal('show');
                });
        }
    }
});

document.body.addEventListener('click', e => {
    if (e.srcElement.id == 'EditUserModalOpenButton3') {
        var jwt = sessionStorage.getItem('jwt');
        if (jwt != null && jwt != " ") {
            var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
            fetch('/api/ManageUsers', requestInfo)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then(data => {
                    document.getElementById('EditUsername').value = data[3].username;
                    document.getElementById('EditEmail').value = data[3].email;
                    document.getElementById('EditSecurityQuestion1').value = data[3].securityQuestion1;
                    document.getElementById('EditSecurityQuestion2').value = data[3].securityQuestion2;
                    document.getElementById('EditId').value = data[3].id;
                    if (data[3].roles.includes("Buyers")) {
                        document.getElementById('EditBuyerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[3].roles.includes("Sellers")) {
                        document.getElementById('EditSellerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[3].roles.includes("Moderators")) {
                        document.getElementById('EditModeratorCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditModeratorCheckbox').checked = false;
                    }
                    if (data[3].roles.includes("Administrators")) {
                        document.getElementById('EditAdminCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditAdminCheckbox').checked = false;
                    }
                    $("#EditUserModal").modal('show');
                });
        }
    }
});

document.body.addEventListener('click', e => {
    if (e.srcElement.id == 'EditUserModalOpenButton4') {
        var jwt = sessionStorage.getItem('jwt');
        if (jwt != null && jwt != " ") {
            var requestInfo = { 'method': 'GET', headers: { 'Authorization': 'bearer ' + jwt }, credentials: 'same-origin' };
            fetch('/api/ManageUsers', requestInfo)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                })
                .then(data => {
                    document.getElementById('EditUsername').value = data[4].username;
                    document.getElementById('EditEmail').value = data[4].email;
                    document.getElementById('EditSecurityQuestion1').value = data[4].securityQuestion1;
                    document.getElementById('EditSecurityQuestion2').value = data[4].securityQuestion2;
                    document.getElementById('EditId').value = data[4].id;
                    if (data[4].roles.includes("Buyers")) {
                        document.getElementById('EditBuyerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[4].roles.includes("Sellers")) {
                        document.getElementById('EditSellerCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditSellerCheckbox').checked = false;
                    }
                    if (data[4].roles.includes("Moderators")) {
                        document.getElementById('EditModeratorCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditModeratorCheckbox').checked = false;
                    }
                    if (data[4].roles.includes("Administrators")) {
                        document.getElementById('EditAdminCheckbox').checked = true;
                    }
                    else {
                        document.getElementById('EditAdminCheckbox').checked = false;
                    }
                    $("#EditUserModal").modal('show');
                });
        }
    }
});