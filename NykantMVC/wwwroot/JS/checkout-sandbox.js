﻿$('#checkout-testing').css('display', 'block');

$(document).mouseup(function (e) {
    if ($(e.target).closest("#checkout-testing").length === 0) {
        $('#checkout-testing').css('display', 'none');
    }
});

$('#testing-close').on('click', function () {
    $('#checkout-testing').css('display', 'none');
});