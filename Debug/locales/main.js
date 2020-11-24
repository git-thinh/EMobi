
document.oncontextmenu = function (e) {
    //console.log('RIGHT_CLICK = ', e.pageX, e.pageY);
    setTimeout(function (x, y) { pageMenu(x, y); }, 1, e.pageX, e.pageY);
    return false;
};

function loading(visable) {
    var el = document.getElementById('loading');
    if (el) {
        if (visable == true) {
            var pel = document.getElementById('i' + m_page);
            if (pel) pel.style.opacity = 0;

            document.body.style.backgroundColor = 'black';
            document.body.style.overflowY = 'hidden';
            el.style.display = 'inline-block';
        } else {
            document.body.style.backgroundColor = 'white';
            document.body.style.overflowY = 'auto';
            el.style.display = 'none';
        }
    }
}

function pageInit(page) {
    if (page == null || page == undefined) page = 0;

    var max = api.getPageTotal();
    var id = (new Date()).getTime();
    for (var i = 0; i < max; i++) {
        var el = document.getElementById('i' + i);
        if (el) document.body.removeChild(el);

        el = document.createElement('img');
        el.id = 'i' + i;
        el.setAttribute('src', 'img://i' + i + '?_=' + id);
        document.body.appendChild(el);
    }

    pageOpen(page);
}

var m_page = 0, m_total = 0;
function pageOpen(page) {
    if (page < 0) return;

    var screenWidth = api.getScreenWidth();
    var json = api.getPageInfo(page);
    var info = JSON.parse(json);

    if (info && info.Width && info.Height) {
        api.setAppWidth(info.Width, info.Height);

        if (info.Width < screenWidth)
            document.body.style.overflowX = 'hidden';
        else
            document.body.style.overflowX = 'auto';

        var el = document.getElementById('i' + page);
        if (el) {
            var old = document.getElementById('i' + m_page);
            if (old) {
                old.style.opacity = 0;
            }

            el.style.opacity = 1;

            m_page = page;
            loading(false);

            document.body.scrollLeft = 0;
            document.body.scrollTop = 0;

            api.js_page_set_current(page);
        }
    }
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
    menuHide();
    var sel = CHECK_IS_SELECTION();
    console.log('PAGE_MOUSE_UP', sel);
}

/////////////////////////////////////////////

function CHECK_IS_SELECTION() { return document.body.style.cursor == 'crosshair'; }

function menu_search(el) { }
function menu_open_document(el) { api.js_open(); }
function menu_tree_explorer(el) { }
function menu_selection(el) {
    document.body.style.cursor = 'crosshair';
    //document.body.style.cursor = "default";
}
function menu_selection_group(el) { }
function menu_selection_note(el) { }
function menu_selection_comment(el) { }
function menu_selection_link_other(el) { }
function menu_media_explorer(el) {
    api.js_media_show();
}
function menu_analytic_text(el) { }
function menu_bookmark_this_page(el) { }
function menu_like_this_page(el) { }
function menu_login_user(el) {
    window.open('local://login', '', 'width=200,height=100');
}
function menu_logout(el) { }
function menu_setting(el) { }
function menu_open_devtool(el) { api.js_open_devtool(); }
function menu_exit(el) { api.js_exit(); }

window.addEventListener('DOMContentLoaded', function (event) {
    api.mainInited();
});
