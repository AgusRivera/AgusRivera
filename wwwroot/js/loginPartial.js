$(document).ready(function () {
    $('#logoutLink').on('click', function (e) {
        e.preventDefault(); // Previene el comportamiento predeterminado del enlace

        // Envía una solicitud POST al endpoint Logout del controlador User
        $.post('/User/Logout')
            .done(function (data) {
                window.location.href = '/User/Login';
            })
            .fail(function (xhr, status, error) {
                console.error('Error al cerrar sesión:', xhr.responseText);
            });
    });
});