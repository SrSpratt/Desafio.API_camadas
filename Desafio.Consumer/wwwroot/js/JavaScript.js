
function vanilla_fire_ajax(selectid, urlBase, partialDiv) {
    document.getElementById(selectid).addEventListener('change', function () {
        var xhr = new XMLHttpRequest();
        var selectedValue = this.value;
        xhr.open('GET', urlBase + '?type=' + selectedValue);
        xhr.onload = function () {
            if (xhr.status === 200) {
                document.getElementById(partialDiv).innerHTML = xhr.responseText;
            } else {
                console.log(xhr.responseText);
            }
        };
        xhr.send();
    });
}

function vanilla_fire_ajax_search(selectid, urlBase, partialDiv) {
    document.getElementById(selectid).addEventListener('input', function () {
        var xhr = new XMLHttpRequest();
        var typedValue = this.value;
        xhr.open('GET', urlBase + '?name=' + typedValue);
        xhr.onload = function () {
            if (xhr.status === 200) {
                document.getElementById(partialDiv).innerHTML = xhr.responseText;
            } else {
                console.log(xhr.responseText);
            }
        };
        xhr.send();
    });
}

function fill_value_on_input(fromid, toid, event) {
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById(fromid).addEventListener(event, function (event) {
            document.getElementById(toid).value = event.target.value;
        })
    });
}