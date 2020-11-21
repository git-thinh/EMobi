
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
    var max = api.getPageTotal();
    for (var i = 0; i < max; i++) {
        var img = document.createElement('img');
        img.id = 'i' + i;
        img.setAttribute('src', 'img://i' + i);
        document.body.appendChild(img);
    }
    pageOpen(0);
}

var m_page = 0, m_total = 0;
function pageOpen(page) {
    if (page < 0) return;

    var json = api.getPageInfo(page);
    var info = JSON.parse(json);
    //console.log(info);

    if (info && info.Width && info.Height) {
        api.setAppWidth(info.Width, info.Height);
    }

    var el = document.getElementById('i' + page);
    if (el) {
        var old = document.getElementById('i' + m_page);
        if (old) {
            old.style.opacity = 0;
        }

        el.style.opacity = 1;

        m_page = page;
        loading(false);

        setTimeout(function () {
            document.body.scrollLeft = 0;
            document.body.scrollTop = 0;
            el.focus();
        }, 10);
    }

    //var el = document.getElementById('page');
    //if (info && el) {
    //    el.style.width = info.Width + 'px';
    //    el.style.height = info.Height + 'px';
    //    el.style.backgroundImage = 'url(img://i' + page + ')';
    //    m_page = page;
    //    loading(false);
    //    api.setAppWidth(info.Width, info.Height);               
    //    el.focus();

    //    setTimeout(function () {
    //        document.body.scrollLeft = 0;
    //        el.focus();
    //    }, 10)
    //}
}

function pagePrev() { pageOpen(m_page - 1); }
function pageNext() { pageOpen(m_page + 1); }

function pageKeyPress(event) {
    //console.log(event);
    switch (event.which) {
        case 37: // Left
            pagePrev();
            break;
        case 39: // Right
            pageNext();
            break;
    }
}


function menu_search(el) { }
function menu_open_document(el) { }
function menu_tree_explorer(el) { }
function menu_selection(el) { }
function menu_selection_group(el) { }
function menu_selection_note(el) { }
function menu_selection_comment(el) { }
function menu_selection_link_other(el) { }
function menu_media_explorer(el) { }
function menu_analytic_text(el) { }
function menu_bookmark_this_page(el) { }
function menu_like_this_page(el) { }
function menu_login_user(el) { }
function menu_logout(el) { }
function menu_setting(el) { }
function menu_exit(el) { }

window.addEventListener('DOMContentLoaded', function (event) {
    api.mainInited();
});
