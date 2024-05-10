
function fire_ajax(selectid, urlBase, partialDiv) {
    jQuery(function ($) {
        $("#" + selectid).change(function () {
            $.ajax({
                type: "Get",
                url: urlBase + '?type=' + $(this).val(),  //remember change the controller to your owns.
                success: function (data) {
                    $("#" + partialDiv).html("");
                    $("#" + partialDiv).html(data);
                },
                error: function (response) {
                    console.log(response.responseText);
                }
            });
        });
    });
}

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