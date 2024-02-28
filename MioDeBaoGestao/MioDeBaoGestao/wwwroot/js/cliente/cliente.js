$(document).ready(function () {
    $('#table-cliente').DataTable({
        reposive: true
    });
});

function selecionarCliente(id, nome) {

    $("#idCliente").val(id);
    $("#nmCliente").val(nome);

    $("#modal-cliente").modal('hide');
}

function getClienteModal() {
    $.ajax({
        url: '/Cliente/ListarClientePartial',
        type: "GET",
        dataType: "html",
        success: function (response) {
            $("#cliente-modal").html(response);
            $("#modal-cliente").modal("show");
        },
        error: function (e) {
            if (e.status === 400)
                alert(e.responseText);
            else
                alert("Não foi possível completar a transação!");

        }
    }).done(function (results) {
    });
}

