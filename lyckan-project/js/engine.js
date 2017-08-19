$(document).ready(
    function() {
        // Propiedades iniciales
        var canvas = $("#canvas")[0];
        var contexto = canvas.getContext("2d");
        console.log(contexto);

        //Graficos
        var playerImg = $("#playerImg")[0];

        //Fisicas
        var velocidad = 5;
        var direccion = velocidad;
        var iniciar = false;
        var x = 50;
        var y = 700;
        var intervalo;

        function drawPlayer(cordx, cordy) {
            canvas.width = canvas.width;
            contexto.drawImage(playerImg, cordx, cordy);
        }

        function moveAndDraw() {
            if(x > (canvas.width - 100)){
                direccion = -velocidad;
            }
            if (x < (10)) {
                direccion = velocidad;
            } 
            x+=direccion;
            drawPlayer(x,y)
        }

        function startGame(){
            intervalo = window.setInterval(function () {
                moveAndDraw();
            },20);
        }


        startGame();
    }
)