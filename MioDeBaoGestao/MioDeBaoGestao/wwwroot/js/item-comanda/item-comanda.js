function findProdutos() {
    let url = '/Produto/PesquisarProdutoPartial';
    getAndShowProducts(url, "produtos");
}

function mostrarModalQuantidade(idItemComanda) {
    $("#idItemComanda").val(idItemComanda);
    $("#qtd-item-comanda").modal('show');
}

function removeItemComanda() {
    let qtdItemComanda = $("#input-qtd-item-comanda").val();

    if (qtdItemComanda < 1) {
        swal("Quantidade inválida. Deve ser maior do que 0!", { icon: "warning" });
        return;
    }

    let idItemComanda = $("#idItemComanda").val();
    let idComanda = $("#idComanda").val();

    let url = "/ItensComanda/Delete?idComanda=" + idComanda + "&idItemComanda=" + idItemComanda + "&quantidade=" + qtdItemComanda;

    ajaxGET(url);
}

function addItem() {
    $("#modal-produto").modal('show');
}

function getData(page, consumidor) {
    let consum = 0;
    let divId = "";
    let search = "";

    let idComanda = $("#idComanda").val();

    if (consumidor == 'desktop') {
        divId = 'itens-comanda-desktop'; 
        search = $('#pesquisa-txt-desktop').val();
    }
    else {
        divId = 'itens-comanda-mobile';
        search = $("#pesquisa-txt-mobile").val();
        consum = 1;
    }

    let url = "/ItensComanda/PesquisarItensComandaPaginadoPartial?idComanda=" + idComanda + "&consumidor=" + consum + "&search=" + search + "&page=" + page;

    getItensComandaPartialView(url, divId);
}

function selecionarProduto(id) {
    $('#idProd').val(id);
    let qtd = Number.parseInt(prompt('Adicione a quantidade:', 1));

    if (Number.isInteger(qtd)) {
        let result = parseInt(qtd);
        $('#id-prod-qtd').val(result);
        salvaItem();
    }
    else {
        swal("Adicione um valor correto para a quantidade do produto!", { icon: "error", title: "Valor inválido" });
    }
}

function salvaItem() {

    let qtd = $('#id-prod-qtd').val();

    if (qtd <= 0) {
        swal("Quantidade deve ser maior do que 0!", { icon: "warning", title: "Alerta" });
        return;
    }

    $('#id-prod-qtd').val('');

    let idProduto = $('#idProd').val();
    let idComanda = $('#idComanda').val();

    let data = { quantidade: qtd, produtoId: idProduto, comandaId: idComanda };
    let url = "/ItensComanda/Create"
    let divId = "itens-comanda";
    let message = "Item adicionado com sucesso!";

    ajaxPost(url, data, divId, message);
}

function ajaxPost(url, dto, divId, message) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        async: false,
        data: JSON.stringify(dto),
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

function getAndShowProducts(url, divId) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        success: function (response) {
            $("#" + divId).html(response);
            $("#modal-produto").modal('show');
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


function getItensComandaPartialView(url, divId) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        success: function (response) {
            $("#" + divId).html(response);
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
