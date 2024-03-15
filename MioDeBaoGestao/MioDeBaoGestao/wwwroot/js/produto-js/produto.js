
function getCustomHref(page) {
    let pesquisa = $("#pesquisa-txt-desktop").val();
    if (pesquisa.length == 0)
        pesquisa = $("#pesquisa-txt-mobile").val();

    return "/Produto/Search?page=" + page + "&search=" + pesquisa
}

function getData(page) {
    location.href = getCustomHref(page);
}

function closeModal(modalId) {
    $('#' + modalId).modal('hide');
}


