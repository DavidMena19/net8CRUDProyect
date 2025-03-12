var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblCarrito").DataTable({
        "ajax": {
            "url": "/Cliente/ArticuloCliente/GetAll", 
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "30%" },
            { "data": "cantidad", "width": "10%" },
            {
                "data": "precio",
                "render": function (data) {
                    return data ? `RD$ ${data.toFixed(2)}` : "RD$ 0.00";
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                               <a href="/Cliente/ArticuloCliente/AgregarAlCarrito/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-plus"></i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Cliente/ArticuloCliente/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-trash-alt"></i> 
                                </a>
                            </div>`;
                },
                "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No hay artículos en el carrito",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ artículos",
            "lengthMenu": "Mostrar _MENU_ artículos",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron artículos",
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
