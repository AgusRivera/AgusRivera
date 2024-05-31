document.getElementById('togglePassword').addEventListener('click', function () {
    togglePasswordVisibility('password', 'toggleIcon');
});

document.getElementById('toggleConfirmPassword').addEventListener('click', function () {
    togglePasswordVisibility('confirmPassword', 'toggleConfirmIcon');
});

function togglePasswordVisibility(inputId, iconId) {
    const password = document.getElementById(inputId);
    const toggleIcon = document.getElementById(iconId);
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    toggleIcon.classList.toggle('fa-eye');
    toggleIcon.classList.toggle('fa-eye-slash');
}

$(document).ready(function () {
    $('#registerForm').submit(function (event) {
        event.preventDefault();

        $.ajax({
            type: 'POST',
            url: '/User/Register',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    window.location.href = '/User/Login';
                } else {
                    // Handle errors (display error messages, etc.)
                    alert('Registration failed: ' + response.message);
                }
            },
            error: function (response) {
                // Handle errors (display error messages, etc.)
                alert('An error occurred: ' + response.responseText);
            }
        });
    });
});