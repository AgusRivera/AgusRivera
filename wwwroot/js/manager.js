$(document).ready(function () {
    var deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
    var addReservationModal = new bootstrap.Modal(document.getElementById('addReservationModal'));
    var showToursModal = new bootstrap.Modal(document.getElementById('showToursModal'));
    var addTourModal = new bootstrap.Modal(document.getElementById('addTourModal'));
    var reservationIdToDelete = null;
    loadTours();

    // Función para activar el modal de agregar reserva
    function showAddReservationModal() {
        addReservationModal.show();
    }

    // Función para cerrar el modal de agregar reserva
    function cancelAddReservation() {
        addReservationModal.hide();
    }

    // Función para confirmar la creación de una reserva
    $('#confirmAddReservation').click(function () {
        var clientName = $('#clientName').val();
        var reservationDate = $('#reservationDate').val();
        var tourId = $('#tourSelect').val();

        var reservation = {
            Reservations: [{
                ClientName: clientName,
                ReservationDate: reservationDate,
                TourId: tourId
            }]
        };

        $.ajax({
            url: '/Manager/CreateReservation',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(reservation),
            success: function (response) {
                console.log(response);
                if (response.isSuccess) {
                    alert('Reserva agregada exitosamente');
                    $('#addReservationModal').modal('hide');
                    resetAddReservationForm();
                    window.location.reload();
                    
                } else {
                    alert('Error al agregar la reserva: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error en la solicitud AJAX:', error);
                alert('Error al agregar la reserva.');
            }
        });
    });

    // Función para limpiar los campos del formulario de agregar reserva
    function resetAddReservationForm() {
        $('#clientName').val('');
        $('#reservationDate').val('');
        $('#tourSelect').val('');
    }

    // Función para activar el modal de eliminar reserva
    function showDeleteModal(event) {
        reservationIdToDelete = $(this).data('id');
        deleteModal.show();
    }

    // Función para cerrar el modal de eliminar reserva y volver al cshtml
    function cancelDelete() {
        reservationIdToDelete = null;
        deleteModal.hide();
    }

    // Función para confirmar la eliminación de reserva
    function confirmDelete() {
        if (reservationIdToDelete) {
            $.ajax({
                url: `/Manager/DeleteReservation/${reservationIdToDelete}`,
                type: 'DELETE',
                success: function (result) {
                    $(`button[data-id='${reservationIdToDelete}']`).closest('tr').remove();
                    deleteModal.hide();
                    loadReservations();
                },
                error: function (xhr, status, error) {
                    alert('No se pudo eliminar la reserva. Inténtelo de nuevo más tarde.');
                }
            });
        }
    }


    ///TOURS -----------------------------------------------------------------------------------------------------------------


    // Función para activar el modal de mostrar tours
    function showToursModalFunction() {
        showToursModal.show();
    }

    // Función para activar el modal de crear tours
    function showAddToursModal() {
        resetAddTourForm();
        addTourModal.show();
    }


    // Función para confirmar la creación de un tour
    $('#confirmAddTour').click(function () {
        var tourName = $('#tourName').val();
        var destination = $('#destination').val();
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();
        var price = $('#price').val();

        var tour = {
            Tours: [{
                Name: tourName,
                Destination: destination,
                StartDate: startDate,
                EndDate: endDate,
                Price: parseFloat(price),
                Reservations: null
            }]
        };

        $.ajax({
            url: '/Manager/CreateTours', 
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(tour),
            success: function (response) {
                console.log(response);
                if (response.isSuccess) {
                    alert('Tour agregado exitosamente');
                    $('#addTourModal').modal('hide');
                    resetAddTourForm();
                    loadTours(); // Recargar las opciones de tours
                } else {
                    alert('Error al agregar el tour: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error en la solicitud AJAX:', error);
                alert('Error al agregar el tour.');
            }
        });
    });

    // Función para limpiar los campos del formulario de agregar tour
    function resetAddTourForm() {
        $('#tourName').val('');
        $('#destination').val('');
        $('#startDate').val('');
        $('#endDate').val('');
        $('#price').val('');
    }

    // Función para cargar las opciones del combo Tours
    function loadTours() {
        $.ajax({
            url: '/Manager/GetTours',
            type: 'GET',
            success: function (result) {
                console.log('Response from GetTours:', result);
                if (result && result.value && result.value.tours) {
                    $('#tourSelect').empty();
                    result.value.tours.forEach(function (tour) {
                        $('#tourSelect').append($('<option>', {
                            value: tour.id,
                            text: tour.name
                        }));
                    });
                    fillToursTable(result.value.tours);
                } else {
                    console.error('Formato de respuesta del servidor incorrecto.');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error en la solicitud AJAX:', error);
            }
        });
    }


    //Funcion para llenar la tabla de Tours en el Modal
    function fillToursTable(tours) {
        var toursTableBody = $('#toursTableBody');
        toursTableBody.empty();

        tours.forEach(function (tour) {

            var startDate = tour.startDate ? new Date(tour.startDate) : null;
            var endDate = tour.endDate ? new Date(tour.endDate) : null;

            // Format Date
            var formattedStartDate = startDate ? startDate.toLocaleDateString() : '';
            var formattedEndDate = endDate ? endDate.toLocaleDateString() : '';

            var deleteButton = $('<button>')
                .addClass('btn btn-danger delete-tour-btn')
                .text('Eliminar')
                .data('id', tour.id)
                .click(function () {
                    deleteTour(tour.id);
                });

            var row = $('<tr>').append(
                $('<td>').text(tour.id),
                $('<td>').text(tour.name),
                $('<td>').text(tour.destination),
                $('<td>').text(formattedStartDate),
                $('<td>').text(formattedEndDate),
                $('<td>').text(tour.price),
                $('<td>').append(deleteButton) // Agrega el botón de eliminar en la última celda
            );

            toursTableBody.append(row);
        });
    }

    //Funcion para eliminar Tours en el Modal
    function deleteTour(tourId) {
        if (confirm('¿Estás seguro de que deseas eliminar este tour?')) {
            $.ajax({
                url: `/Manager/DeleteTour/${tourId}`,
                type: 'DELETE',
                success: function (response) {
                    if (response.isSuccess) {
                        alert('Tour eliminado exitosamente');
                        loadTours(); // Recargar las opciones de tours
                    } else {
                        alert('Error al eliminar el tour: ' + response.message);
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error en la solicitud AJAX:', error);
                    alert('Error al eliminar el tour.');
                }
            });
        }
    }




    // Asignar eventos a los botones
    $('.delete-btn').on('click', showDeleteModal);
    $('#confirmDelete').on('click', confirmDelete);
    $('.btn-close, .btn-secondary').on('click', cancelDelete);
    $('#addReservationBtn').on('click', showAddReservationModal);
    $('#showToursBtn').on('click', showToursModalFunction);
    $('#addTourBtn').on('click', showAddToursModal);
    $('#addReservationModal').on('hidden.bs.modal', cancelAddReservation);
    $('#confirmAddReservation').on('click', confirmAddReservation);

});