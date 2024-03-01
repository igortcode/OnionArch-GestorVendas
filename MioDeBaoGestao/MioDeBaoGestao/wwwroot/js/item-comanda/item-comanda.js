

function findProdutos() {
    let url = '/Produto/ListarProdutoPartial'
    getAndShowProducts(url, "produtos");
}

function mostrarModalQuantidade(idItemComanda) {
    $("#idItemComanda").val(idItemComanda);
    $("#qtd-item-comanda").modal('show');
}

function removeItemComanda() {
    let qtdItemComanda = $("#input-qtd-item-comanda").val();

    if (qtdItemComanda < 1) {
        alert("Quantidade inválida. Deve ser maior do que 0.");
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

function selecionarProduto(id) {
    $('#idProd').val(id);
    $("#modal-qtd").modal('show');
}

function salvaItem() {
    $("#modal-qtd").modal('hide');
    $("#modal-produto").modal('hide');

    let qtd = $('#prod-qtd').val();

    if (qtd <= 0) {
        alert("Quantidade deve ser maior do que 0!");
        return;
    }

    $('#prod-qtd').val('');

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
        dataType: "html",
        contentType: "application/json",
        data: JSON.stringify(dto),
        success: function (response) {
            alert(message);
            $("#" + divId).html(response);
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
                alert(e.responseText);
            else
                alert("Não foi possível completar a transação!");
        }
    }).done(function (results) {
    });
}
