
function getCustomHref(page) {
    let pesquisa = $("#pesquisa-txt-desktop").val();
    if (pesquisa.length == 0)
        pesquisa = $("#pesquisa-txt-mobile").val();

    let url = "/Produto/Search?page=" + page + "&search=" + pesquisa
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

function closeModal(modalId) {
    $('#' + modalId).modal('hide');
}


