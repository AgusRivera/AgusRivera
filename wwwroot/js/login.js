document.getElementById('togglePassword').addEventListener('click', function () {
    const password = document.getElementById('password');
    const toggleIcon = document.getElementById('toggleIcon');
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    toggleIcon.classList.toggle('fa-eye');
    toggleIcon.classList.toggle('fa-eye-slash');
});

$(document).ready(function () {
    $('#loginForm').submit(function (event) {
        event.preventDefault();

        $.ajax({
            type: 'POST',
            url: '/User/Login',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    window.location.href = '/Manager/Index';
                } else {
                    alert('Login failed: ' + response.message);
                }
            },
            error: function (response) {
                alert('An error occurred: ' + response.responseText);
            }
        });
    });
});