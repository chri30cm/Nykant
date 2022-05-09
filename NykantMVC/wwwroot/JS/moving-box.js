﻿$(document).ready(function () {
    var x, y;

    var followbox = document.getElementById('follow-box');
    var followname = document.getElementById('follow-name');
    var followprice = document.getElementById('follow-price');

    $("#follow-container").mousemove(function (e) {
        if (e.target != followbox && e.target != followname && e.target != followprice) {
            var rect = e.target.getBoundingClientRect();

            x = e.clientX - rect.left;
            y = e.clientY - rect.top;

            $("#follow-box").stop().animate({ left: x - 170, top: y - 80 }, {
                duration: 500,
                specialEasing: {
                    width: "linear",
                    height: "easeOutBounce"
                }
            });
        }
    });

    $("#follow-container").mouseleave(function (e) {
        $("#follow-box").stop().animate({ left: "35%", top: "85%" }, {
            duration: 500,
            specialEasing: {
                width: "linear",
                height: "easeOutBounce"
            }
        });
    });

    $("#follow-box").css('left', "35%");
    $("#follow-box").css('top', "85%");
});


