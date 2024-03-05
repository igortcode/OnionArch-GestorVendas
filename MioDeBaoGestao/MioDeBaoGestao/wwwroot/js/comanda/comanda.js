﻿
$(document).ready(function () {
    $('#table-comanda').DataTable({
        reposive: true,
        order: [[5, 'desc']],
        columnDefs: [
            {
                target: 4,
                visible: false
            }
        ]
    });
});

function fecharComanda(idComanda, idAberturaDia) {
    if (confirm("Confirma o fechamento da comanda?")) {
        let url = '/Comanda/FecharComanda?id=' + idComanda + "&idAberturaDia=" + idAberturaDia;
        ajaxGET(url);
    }
    else {
        alert("Comanda mantida!");
    }
}

function excluirComanda(idComanda, idAberturaDia) {
    if (confirm("Confirma a exclusão da comanda?")) {
        let url = '/Comanda/Delete?id=' + idComanda + "&idAberturaDia=" + idAberturaDia;
        ajaxGET(url);
    }
    else {
        alert("Comanda mantida!");
    }
}

function ajaxGET(url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        success: function (response) {
            alert(response);
            location.reload();
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