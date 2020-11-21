
document.oncontextmenu = function (e) {
    //console.log('RIGHT_CLICK = ', e.pageX, e.pageY);
    setTimeout(function (x, y) { pageMenu(x, y); }, 1, e.pageX, e.pageY);
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

function pageClick(event) {
    //console.log('PAGE_CLICK = ', event);
    menuHide();
}

var PAGE_MENU_TIMER = null;
function pageMenu(x, y) {
    if (PAGE_MENU_TIMER != null) {
        clearTimeout(PAGE_MENU_TIMER);
        PAGE_MENU_TIMER = null;
    }

    var el = document.getElementById('menu');
    if (el) {
        el.style.top = '7px';
        el.style.left = x + 'px';
        el.style.display = 'inline-block';
        PAGE_MENU_TIMER = setTimeout(menuHide, 5000);
    }
}

function menuHide() {
    if (PAGE_MENU_TIMER != null) {
        clearTimeout(PAGE_MENU_TIMER);
        PAGE_MENU_TIMER = null;
    }

    var el = document.getElementById('menu');
    if (el) el.style.display = 'none';
}

function pageMouseDown(event) {
    var sel = CHECK_IS_SELECTION();
    console.log('PAGE_MOUSE_DOWN', sel);
}

function pageMouseMove(event) {
    var sel = CHECK_IS_SELECTION();
    console.log('PAGE_MOUSE_MOVE', sel);
}

function pageMouseUp(event) {
    var sel = CHECK_IS_SELECTION();
    console.log('PAGE_MOUSE_UP', sel);
}

/////////////////////////////////////////////

function CHECK_IS_SELECTION() { return document.body.style.cursor == 'crosshair'; }

function menu_search(el) { }
function menu_open_document(el) { }
function menu_tree_explorer(el) { }
function menu_selection(el) {
    document.body.style.cursor = 'crosshair';
    //document.body.style.cursor = "default";
}
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
