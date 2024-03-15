function getData(page, consumidor) {
    let search = "";

    if (consumidor == 'desktop') {
        search = $('#pesquisa-txt-desktop').val();
    }
    else {
        search = $("#pesquisa-txt-mobile").val();
    }

    let url = "/Cliente/Search?search=" + search + "&page=" + page;

    url = normalizeUrl(url);

    location.href = url;
}

function normalizeUrl(url) {
    let sub = $("#sub-url").val();

    if (sub.length > 0)
        url = sub + url;

    return url;
}

function selecionarCliente(id, nome) {

    $("#idCliente").val(id);
    $("#nmCliente").val(nome);

    $("#modal-cliente").modal('hide');
}

function getClienteModal(page) {
    let search = $("#pesquisa-txt").val();

    let url = "/Cliente/ListarClientePartial?search=" + search + "&page=" + page;
    url = normalizeUrl(url);

    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        success: function (response) {
            $("#cliente-modal").html(response);
            $("#modal-cliente").modal("show");
        },
        error: function (e) {
            if (e.status === 400)
                swal(e.responseText, { icon: "warning", title: "Alerta" });
            else
                swal("Não foi possível completar a transação!", { icon: "error", title: "Erro" });

        }
    }).done(function (results) {
    });
}

function excluirCliente(id) {
    swal({
        title: "Exclusão do cliente",
        text: "Confirma a exclusão do cliente?",
        icon: "warning",
        buttons: {
            cancel: "Cancelar",
            confirm: "Excluir",
        }
    })
        .then((update) => {
            if (update) {
                let url = '/Cliente/Delete?id=' + id;
                url = normalizeUrl(url);
                ajaxGET(url);
            }
        });
}

function ajaxGET(url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        success: function (response) {
            swal(response,
                {
                    icon: "success",
                    buttons: {
                        confirm: "OK",
                    }
                }).then((confirm) => { location.reload(); });
        },
        error: function (e) {
            if (e.status === 400)
                swal(e.responseText, { icon: "warning", title: "Alerta" });
            else
                swal("Não foi possível completar a transação!", { icon: "error", title: "Erro" });

        }
    }).done(function (results) {
    });
}

