$(document).ready(
    function() {
        
        // Propiedades iniciales
        var canvas = $("#canvas")[0];
        var contexto = canvas.getContext("2d");
        var numberEnemies = 10;
        var runGame = true;
        var score = 0;
        
        //Fisicas
        var velocidad = 5;
        var game_loop;

        function numRandom(min, max) {
            return Math.round(Math.random() * (max - min) + min);
          }


        //Actores
        var player = {direccion:"", posX:250, posY:510, width:60, height:45, img:null};
        var enemies = Array(5);
        for (var i = 0; i < enemies.length; i++) {
            var image = $("#enemy"+(i+1))[0];
            enemies[i] = {direccion:"", posX:numRandom(10,(canvas.width - 70)), posY:-numRandom(100,1000), width:60, height:45, img: image};
            
        }
        var laser1 = {direccion:"", posX:0, posY:500, width:9, height:45, img:null};
        var lasers = Array();

        //Graficos
        player.img = $("#playerImg")[0];
        
        laser1.img = $("#laser1")[0];
        var damage1 = $("#damage1")[0];


        //Controles
        function moveToActor(key){
            switch(key){
                //Izquierda
                case 37:
                player.direccion = "izquierda"           
                break;
                //Derecha
                case 39:
                player.direccion = "derecha";
                break;
                //Disparo
                case 32:
                lasers.push({posX:player.posX + (player.width/2), posY:500, width:9, height:45, img:$("#laser1")[0]})
                drawArmy(lasers);
                break;
            }
        }

        $(document).keydown(function(event){
            console.log(event.which);
            moveToActor(event.which);
        
        })

        //Dibujado

        function prepareNewFrame(){
            canvas.width = canvas.width;
        }

        function setBackground(){
            
        }

        function drawPlayer(img, imgWidth, imgHeigth, cordX, cordY) {
            contexto.save;
            
            contexto.drawImage(img, cordX, cordY, imgWidth, imgHeigth);
            contexto.restore;
        }

        function drawEnemies(array) {
            contexto.save;
            for (var i = 0; i < array.length; i++) {
                var element = array[i];
                contexto.drawImage(element.img, element.posX, element.posY, element.width, element.height);
                enemies[i].posY += 1;
            }
            contexto.restore;            
        }

        function drawArmy(array){
            contexto.save;
            for (var i = 0; i < array.length; i++) {
                var element = array[i];
                if(element.posY > (-100)){
                    contexto.drawImage(element.img, element.posX, element.posY, element.width, element.height);
                    lasers[i].posY -= 10;
                }else{
                    lasers.splice(i, 1);
                }           
            }
            contexto.restore;
        }

        function moveAndDraw() {
            var desplasamiento = 0;
            if(player.posX > 10 && player.direccion == "izquierda"){
                desplasamiento = -velocidad;
            }
            if (player.posX < (canvas.width - 70) && player.direccion== "derecha") {
                desplasamiento = velocidad;
            } 
            player.posX+=desplasamiento;    
        }

        function drawScore(score){
            contexto.save;
            contexto.font="20px arial";
            contexto.fillStyle ="white";
            contexto. fillText("Score:"+score, 10, 20);
            contexto.restore;
        }

        //Motor
        
        function animation_army() {
            if(lasers.length > 0){
                drawArmy(lasers);      
            }

        }


        function armyToEnemy(){
            lasers.forEach(function(laser) {
                for (var index = 0; index < enemies.length; index++) {
                    var enemy = enemies[index];
                    if (
                        laser.posX+laser.width > enemy.posX &&
                        laser.posX<enemy.posX + enemy.width &&
                        laser.posY<enemy.posY+enemy.height && 
                        laser.posY + laser.height>enemy.posY &&
                        laser.posY > 0
                    ) {
                        enemies[index].img = damage1;
                        score += 10;
                        break;
                    }
                }
            }, this);
            

    }

        function playerToEnemy(){
                for (var index = 0; index < enemies.length; index++) {
                    var enemy = enemies[index];
                    if (
                        player.posX+player.width > enemy.posX &&
                        player.posX<enemy.posX + enemy.width &&
                        player.posY<enemy.posY+enemy.height && 
                        player.posY + player.height>enemy.posY
                    ) {
                        player.img = damage1;
                        runGame = false;
                        break;
                    }
                }

        }

        
        //Escene
        function escene() {
            if (runGame){
                prepareNewFrame();
                armyToEnemy();
                playerToEnemy();
                moveAndDraw();
                drawPlayer(player.img, player.width, player.height, player.posX, player.posY);
                drawEnemies(enemies);
                animation_army();
                drawScore(score);
            }else{

            }
            
        }
        
        
        
        //Inicio
        function startGame(){
            if (typeof game_loop!= "undefined") {
                clearInterval(game_loop);
            }
            game_loop = setInterval(escene, 33);
        }

        


        startGame();

    }
)