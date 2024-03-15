
function getCustomHref(page) {
    let pesquisa = $("#pesquisa-txt-desktop").val();
    if (pesquisa.length == 0)
        pesquisa = $("#pesquisa-txt-mobile").val();

    let url = "/Usuarios/Search?page=" + page + "&search=" + pesquisa;

    url = normalizeUrl(url);

    return url;
}

function normalizeUrl(url) {
    let sub = $("#sub-url").val();

    if (sub.length > 0)
        url = sub + url;

    return url;
}

function getData(page) {
    location.href = getCustomHref(page);
}


function atualizarStatus(id, habilitado) {
    swal({
        title: habilitado == 'True' ? "Gostaria de desativar o usuário?" : "Gostaria de habilitar o usuário?",
        text: habilitado == 'True' ? "O usuário perderá o acesso até ser ativado novamente!" : "O usuário podera acessar a plataforma novamente!",
        icon: "warning",
        buttons: {
            cancel: "Cancelar",
            confirm: "Sim",
        }
    })
        .then((update) => {
            if (update) {
                let url = "/Usuarios/AtualizarStatus?id=" + id;
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
            swal("Status do usuário atualizado com sucesso!", {
                icon: "success"
            })
                .then((value) => {
                    location.reload();
                });
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