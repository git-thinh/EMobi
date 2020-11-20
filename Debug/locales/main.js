
document.oncontextmenu = function () {
    return false;
};

function loading(visable) {
    var el = document.getElementById('loading');
    if (el) {
        if (visable == true)
            el.style.display = 'inline-block';
        else
            el.style.display = 'none';
    }
}

function pageInit() {
    //var max = api.getPageTotal();

    //for (var i = 0; i < max; i++) {
    //    var img = document.createElement('img');
    //    img.id = 'i' + i;
    //    img.setAttribute('src', 'img://i' + i);
    //    document.body.appendChild(img);
    //}
    pageOpen(0);
}

var m_page = 0, m_total = 0;
function pageOpen(page) {
    if (page < 0) return;

    var json = api.getPageInfo(page);
    var info = JSON.parse(json);
    console.log(info);

    var el = document.getElementById('page');
    if (info && el) {
        el.style.width = info.Width + 'px';
        el.style.height = info.Height + 'px';
        el.style.backgroundImage = 'url(img://i' + page + ')';
        m_page = page;
        loading(false);
    }
}

window.addEventListener('DOMContentLoaded', function (event) {
    api.mainInited();
    //pageInit();
});
