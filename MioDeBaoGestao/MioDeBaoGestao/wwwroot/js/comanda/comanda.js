
function getCustomHref(page) {
    let pesquisa = $("#pesquisa-txt-desktop").val();
    if (pesquisa.length <= 0)
        pesquisa = $("#pesquisa-txt-mobile").val();
    const idAberturaDia = $("#aberturaDia").val();

    let url = "/Comanda/Search?page=" + page + "&idAberturaDia=" + idAberturaDia + "&search=" + pesquisa;
    url = normalizeUrl(url);

    return url;
}

function normalizeUrl(url) {
    let sub = $("#sub-url").val();

    if (sub.length > 0)
        url = sub + url;

    return url;
}

function handlerOnChangePesquisa() {
    const pesquisa = $("#pesquisa-txt").val();
    if (pesquisa.length > 4) {
        getData(0);
    }
}

function getData(page) {
   location.href = getCustomHref(page);
}

function fecharComanda(idComanda, idAberturaDia) {
    swal({
        title: "Fechar comanda",
        text: "Confirma o fechamento da comanda?",
        icon: "info",
        buttons: {
            cancel: "Cancelar",
            confirm: "Sim",
        }
    })
        .then((update) => {
            if (update) {
                let url = '/Comanda/FecharComanda?id=' + idComanda + "&idAberturaDia=" + idAberturaDia;
                url = normalizeUrl(url);
                ajaxGET(url);
            }
        });
}

function excluirComanda(idComanda, idAberturaDia) {

    swal({
        title: "Exclusão da comanda",
        text: "Confirma a exclusão da comanda?",
        icon: "warning",
        buttons: {
            cancel: "Cancelar",
            confirm: "Excluir",
        }
    })
        .then((update) => {
            if (update) {
                let url = '/Comanda/Delete?id=' + idComanda + "&idAberturaDia=" + idAberturaDia;
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
