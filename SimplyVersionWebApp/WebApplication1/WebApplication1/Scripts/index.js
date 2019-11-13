$(document).ready(function () {
    setTimeout(function () {
        $('.loader').hide();
        $('.content-temp').show();
        $('.content-switch').show();
    }, 1000);
    // read data from jsons
    var temperature = $("#dataPoint1").data("value");
    var humidity = $("#dataPoint2").data("value");
    var pressure = $("#dataPoint3").data("value");

    var temp_color = 'red';
    var hum_color = 'blue';
    var press_color = 'green';

    $('.temperature .temp').html(temperature);
    $('.humidity .temp').html(humidity);
    $('.pressure .temp').html(pressure);

    //  COLOR bg 

    if (ColorMix) {
        ColorMix.setGradient([
            {
                reference: -30,
                color: {
                    red: 20,
                    green: 0,
                    blue: 255
                }
            }, {
                reference: -27.5,
                color: {
                    red: 20,
                    green: 0,
                    blue: 255
                }
            }, {
                reference: -25,
                color: {
                    red: 20,
                    green: 0,
                    blue: 255
                }
            }, {
                reference: -22.5,
                color: {
                    red: 0,
                    green: 35,
                    blue: 255
                }
            }, {
                reference: -20,
                color: {
                    red: 0,
                    green: 35,
                    blue: 255
                }
            }, {
                reference: -17.5,
                color: {
                    red: 0,
                    green: 35,
                    blue: 255
                }
            }, {
                reference: -15,
                color: {
                    red: 0,
                    green: 35,
                    blue: 255
                }
            }, {
                reference: -12.5,
                color: {
                    red: 0,
                    green: 35,
                    blue: 255
                }
            }, {
                reference: -10,
                color: {
                    red: 0,
                    green: 60,
                    blue: 255
                }
            }, {
                reference: -7.5,
                color: {
                    red: 30,
                    green: 83,
                    blue: 255
                }
            }, {
                reference: -5,
                color: {
                    red: 30,
                    green: 128,
                    blue: 255
                }
            }, {
                reference: -2.5,
                color: {
                    red: 68,
                    green: 148,
                    blue: 253
                }
            }, {
                reference: 0,
                color: {
                    red: 95,
                    green: 163,
                    blue: 253
                }
            }, {
                reference: 2.5,
                color: {
                    red: 132,
                    green: 185,
                    blue: 255
                }
            }, {
                reference: 5,
                color: {
                    red: 132,
                    green: 209,
                    blue: 255
                }
            }, {
                reference: 7.5,
                color: {
                    red: 162,
                    green: 231,
                    blue: 255
                }
            }, {
                reference: 10,
                color: {
                    red: 195,
                    green: 247,
                    blue: 255
                }
            }, {
                reference: 12.5,
                color: {
                    red: 225,
                    green: 255,
                    blue: 252
                }
            }, {
                reference: 15,
                color: {
                    red: 255,
                    green: 255,
                    blue: 150
                }
            }, {
                reference: 17.5,
                color: {
                    red: 255,
                    green: 255,
                    blue: 88
                }
            }, {
                reference: 20,
                color: {
                    red: 255,
                    green: 250,
                    blue: 85
                }
            }, {
                reference: 22.5,
                color: {
                    red: 255,
                    green: 217,
                    blue: 85
                }
            }, {
                reference: 25,
                color: {
                    red: 255,
                    green: 200,
                    blue: 0
                }
            }, {
                reference: 27.5,
                color: {
                    red: 238,
                    green: 124,
                    blue: 60
                }
            }, {
                reference: 30,
                color: {
                    red: 241,
                    green: 160,
                    blue: 0
                }
            }, {
                reference: 32.5,
                color: {
                    red: 255,
                    green: 130,
                    blue: 0
                }
            }, {
                reference: 35,
                color: {
                    red: 255,
                    green: 100,
                    blue: 0
                }
            }, {
                reference: 37.5,
                color: {
                    red: 255,
                    green: 60,
                    blue: 0
                }
            }, {
                reference: 40,
                color: {
                    red: 255,
                    green: 0,
                    blue: 0
                }
            }
        ]);
    }

    temp_color = ColorMix.blend(temperature).toString('rgb');

    hum_color = ColorMix.blend(humidity).toString('rgb');

    press_color = ColorMix.blend(pressure).toString('rgb');

    $('.temperature p').css('border-color', temp_color);

    $('.humidity p').css('border-color', hum_color);

    $('.pressure p').css('border-color', press_color);

    /* $('.temp_in p').css('box-shadow', '0px 3px 8px ' + in_color + ', inset 0px 2px 3px #fff');*/
    /*$('.temp_out p').css('box-shadow', '0px 3px 8px ' + out_color +', inset 0px 2px 3px #fff');*/

    //chart
    //var data = {
    //    // A labels array that can contain any sort of values
    //    labels: [],
    //    // Our series array that contains series objects or in this case series data arrays
    //    series: [
    //        [],
    //        []
    //    ]
    //};
    //$.each($temperatures, function (_i, temp) {
    //    var rt = new Date(temp.recorded_time);
    //    var hours = rt.getHours() > 9 ? rt.getHours() : '0' + rt.getHours();
    //    var minutes = rt.getMinutes() > 9 ? rt.getMinutes() : '0' + rt.getMinutes();

    //    data.labels.push(hours + ':' + minutes);

    //    //inside
    //    data.series[0].push((Math.round(temp.t_in * 10) / 10));
    //    //outside
    //    data.series[1].push((Math.round(temp.t_out * 10) / 10));
    //});


    $('.switch').on('click', function () {
        $('.content-temp').toggle();
        $('.content-chart').toggle();
        setTimeout(function () {
            var chart = new Chartist.Line('.ct-chart', data, {
                height: '500px', plugins: [
                    Chartist.plugins.ctPointLabels({
                        textAnchor: 'middle'
                    })
                ]
            });
        }, 4);
    });
});

//!function (a, b) { "function" == typeof define && define.amd ? define([], function () { return a.returnExportsGlobal = b();  }) : "object" == typeof exports ? module.exports = b() : a["Chartist.plugins.ctPointLabels"] = b() }(this, function () { return function (_a, _b, c) { "use strict"; var d = { labelClass: "ct-label", labelOffset: { x: 0, y: -10 }, textAnchor: "middle", labelInterpolationFnc: c.noop }; c.plugins = c.plugins || {}, c.plugins.ctPointLabels = function (a) { return a = c.extend({}, d, a), function (b) { b instanceof c.Line && b.on("draw", function (b) { "point" === b.type && b.group.elem("text", { x: b.x + a.labelOffset.x, y: b.y + a.labelOffset.y, style: "text-anchor: " + a.textAnchor }, a.labelClass).text(a.labelInterpolationFnc(void 0 === b.value.x ? b.value.y : b.value.x + ", " + b.value.y)) }) } } }(window, document, Chartist), Chartist.plugins.ctPointLabels });

