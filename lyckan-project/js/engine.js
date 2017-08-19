$(document).ready(
    function() {
        // Propiedades iniciales
        var canvas = $("canvas")[0];
        var contexto = canvas.getContext("2d");


        contexto.fillStyle = "blue";
        contexto.fillRect(0,0,100, 100);
    }
)