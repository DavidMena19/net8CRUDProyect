var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblCarrito").DataTable({
        "ajax": {
            "url": "/Cliente/ArticuloCliente/GetAll", // Asegúrate de tener este endpoint en tu backend
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "25%" },
            { "data": "cantidad", "width": "25%" },
            {
                "data": "precio",
                "render": function (data) {
                    return data ? `RD$ ${data.toFixed(2)}` : "RD$ 0.00";
                },
                "width": "20%"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    var total = (row.cantidad * row.precio).toFixed(2);
                    return `RD$ ${total}`;
                },
                "width": "25%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="#" onclick="agregarAlCarrito(${data})" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                    <i class="fa-solid fa-plus"></i> +
                                </a>
                                &nbsp;
                                <a href="#" onclick="Delete('/Cliente/Carrito/EliminarDelCarrito/${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                    <i class="far fa-trash-alt"></i> Borrar
                                </a>
                            </div>`;
                },
                "width": "25%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function agregarAlCarrito(articuloId) {
    $.ajax({
        url: '/Cliente/Carrito/AgregarAlCarrito',
        type: 'POST',  // Mejor que GET para agregar elementos
        data: { articuloId: articuloId },
        success: function (response) {
            if (response.success) {
                swal({
                    title: "¡Artículo agregado!",
                    text: "El artículo ha sido agregado al carrito.",
                    icon: "success",
                    buttons: {
                        cancel: "Seguir comprando",
                        confirm: "Ir al carrito"
                    }
                }).then((value) => {
                    if (value === "confirm") {
                        window.location.href = "/Cliente/Carrito";
                    } else {
                        dataTable.ajax.reload(); // Recargar la tabla
                    }
                });
            } else {
                swal("Error", response.message, "error");
            }
        },
        error: function () {
            swal("Error", "Ocurrió un error al agregar el artículo.", "error");
        }
    });
}

function Delete(url) {
    swal({
        title: "¿Seguro que quieres borrar?",
        text: "¡No se puede recuperar!",
        icon: "warning",
        buttons: ["Cancelar", "Sí, borrar"],
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}


//var dataTable;
//$(document).ready(function () {
//    cargarDatatable();
//});

//function cargarDatatable() {
//    dataTable = $("#tblCarrito").DataTable({
//        "ajax": {
//            "url": "/Cliente/ArticuloCliente/GetAll",
//            "type": "GET",
//            "datatype": "json"
//        },
//        "columns": [
//            { "data": "id", "width": "5%" },
//            { "data": "nombre", "width": "25%" },
//            { "data": "cantidad", "width": "25%" },
//            {
//                "data": "precio",
//                "render": function (data) {
//                    return `RD$ ${data.toFixed(2)}`;
//                },
//                "width": "20%"
//            },
//            { "data": "Total", "width": "25%" },
//            {
//                "render": function (data) {
//                    return `<div class="text-center">
//                                <a href="/Admin/ArticuloCliente/AgregarAlCarrito/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
//                                <i class="fa-solid fa-plus"></i> +
//                                </a>
//                                &nbsp;
//                                <a onclick=Delete("/Admin/Articulo/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
//                                <i class="far fa-trash-alt"></i> Borrar
//                                </a>
//                          </div>`;
//                },
//                "width": "25%"
//            }
//        ],
//        "language": {
//            "decimal": "",
//            "emptyTable": "No hay registros",
//            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
//            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
//            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
//            "infoPostFix": "",
//            "thousands": ",",
//            "lengthMenu": "Mostrar _MENU_ Entradas",
//            "loadingRecords": "Cargando...",
//            "processing": "Procesando...",
//            "search": "Buscar:",
//            "zeroRecords": "Sin resultados encontrados",
//            "paginate": {
//                "first": "Primero",
//                "last": "Ultimo",
//                "next": "Siguiente",
//                "previous": "Anterior"
//            }
//        },
//        "width": "100%"
//    });
//}
//function agregarAlCarrito(articuloId) {
//    // Realizamos la solicitud AJAX para agregar al carrito
//    $.ajax({
//        url: '/Cliente/ArticuloCliente/AgregarAlCarrito',  // Ruta de tu controlador
//        type: 'GET',
//        data: { articuloId: articuloId },
//        success: function (response) {
//            if (response.success) {
//                // Mostrar alerta de SweetAlert si la solicitud fue exitosa
//                swal({
//                    title: "¡Artículo agregado al carrito!",
//                    text: "El artículo ha sido agregado a tu carrito de compras.",
//                    icon: "success",
//                    buttons: {
//                        cancel: {
//                            text: "Seguir comprando",
//                            value: "cancel",
//                            visible: true,
//                            className: "btn btn-secondary",
//                            closeModal: true
//                        },
//                        confirm: {
//                            text: "Ir al carrito",
//                            value: "confirm",
//                            visible: true,
//                            className: "btn btn-primary",
//                            closeModal: true
//                        }
//                    }
//                }).then((value) => {
//                    if (value === "confirm") {
//                        window.location.href = "/Cliente/Carrito";  // Redirige al carrito
//                    }
//                });
//            } else {
//                // Si hubo un error, muestra un mensaje de error
//                swal("Error", response.message, "error");
//            }
//        },
//        error: function (xhr, status, error) {
//            // Manejo de errores
//            swal("Error", "Ocurrió un error al agregar el artículo al carrito.", "error");
//        }
//    });
//}
//function Delete(url) {
//    swal({
//        title: "¿Está seguro de borrar?",
//        text: "¡Este contenido no se puede recuperar!",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#DD6B55",
//        confirmButtonText: "Sí, borrar!",
//        closeOnConfirm: true
//    }, function () {
//        $.ajax({
//            type: 'DELETE',
//            url: url,
//            success: function (data) {
//                if (data.success) {
//                    toastr.success(data.message);
//                    dataTable.ajax.reload();
//                }
//                else {
//                    toastr.error(data.message);
//                }
//            }
//        });
//    });
//}
