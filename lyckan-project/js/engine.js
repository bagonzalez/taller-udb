$(document).ready(
    function() {
        
        // Propiedades iniciales
        var canvas = $("#canvas")[0];
        var contexto = canvas.getContext("2d");
        var numberEnemies = 10;
        var runGame = true;
        var menuGame = true;
        var score = 0;
        var buttons = Array(5);
        buttons[0] = {x:200,y:200,width:200,height:60, text:"Iniciar"};
        buttons[1] = {x:200,y:300,width:200,height:60, text:"Score"};
        buttons[2] = {x:200,y:400,width:200,height:60, text:"Configuracion"};
        buttons[3] = {x:200,y:500,width:200,height:60, text:"Regresar"};
        buttons[4] = {x:200,y:500,width:200,height:60, text:"Ok"};
        
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

        
        
        //Menu
        function mainView() {
            contexto.save;
            var rect = {x:0,y:0,width:600,height:600};
            var backColor = "red";
            var textColor = "white";
            contexto.fillStyle = "black";
            contexto.fillRect(0, 0, 600, 600);
            for (var i = 0; i < 3; i++) {
                var button = buttons[i];
                contexto.fillStyle = backColor;
                contexto.fillRect(button.x, button.y, button.width, button.height);
                contexto.font="20px arial";
                contexto.fillStyle = textColor;
                contexto. fillText(button.text, button.x +20,button.y + 35);
            }
                
            

            
            contexto.restore;
        }

        //Elementos del menu
            //Score
        
        function scoreView(){
            contexto.save;
            var backColor = "blue";
            var textColor = "white";
            var btnColor = "green";
            contexto.fillStyle = backColor;
            contexto.fillRect(0, 0, 600, 600);
            
            contexto.fillStyle = btnColor;
            contexto.fillRect(buttons[3].x, buttons[3].y, buttons[3].width, buttons[3].height);
            contexto.font="20px arial";
            contexto.fillStyle = textColor;
            contexto. fillText(buttons[3].text, buttons[3].x +20,buttons[3].y + 35);
            contexto.restore;
        }

        function settingView(){
            contexto.save;
            var backColor = "blue";
            var textColor = "white";
            var btnColor = "green";
            
            contexto.fillStyle = backColor;
            contexto.fillRect(0, 0, 600, 600);
            
            contexto.fillStyle = btnColor;
            contexto.fillRect(buttons[4].x, buttons[4].y, buttons[4].width, buttons[4].height);
            contexto.font="20px arial";
            contexto.fillStyle = textColor;
            contexto. fillText(buttons[4].text, buttons[4].x +20,buttons[4].y + 35);
            contexto.restore;
        }

        //Seleccion de boton con mouse
        function getMousePos(canvas, event) {
            var rect = canvas.getBoundingClientRect();
            return {
                x: event.clientX - rect.left,
                y: event.clientY - rect.top
            };
        }
        function isInside(pos, rect){
            return pos.x > rect.x && pos.x < rect.x+rect.width && 
            pos.y < rect.y+rect.height && pos.y > rect.y
        }
        
        
        function myClick(event) {
            var mousePos = getMousePos(canvas, event);
            
                if (isInside(mousePos,buttons[0]) 
                ) {
                    console.log("contacto");
                    canvas.removeEventListener('click', myClick);
                    menuGame = false;
                    startGame();
                     
                }

                if (isInside(mousePos,buttons[1]) 
                ) {
                    console.log("contacto1");
                    //canvas.removeEventListener('click', myClick);
                    //menuGame = false;
                    canvas.removeEventListener('click', myClick);
                    scoreView();
                    canvas.addEventListener('click', myClick2);  
                }

                if (isInside(mousePos,buttons[2]) 
                ) {
                    console.log("contacto2");
                    //canvas.removeEventListener('click', myClick);
                    //menuGame = false;
                    canvas.removeEventListener('click', myClick);
                    scoreView();
                    canvas.addEventListener('click', settingClick); 
                    
                }

                
        }

        function myClick2(event) {
            var mousePos = getMousePos(canvas, event);
            
                if (isInside(mousePos,buttons[3]) 
                ) {
                    console.log("contacto");
                    canvas.removeEventListener('click', myClick2);
                    mainView(); 
                    canvas.addEventListener('click', myClick);
                }
                
        }

        function settingClick(event) {
            var mousePos = getMousePos(canvas, event);
            
                if (isInside(mousePos,buttons[4]) 
                ) {
                    console.log("contacto");
                    canvas.removeEventListener('click', settingClick);
                    mainView(); 
                    canvas.addEventListener('click', myClick);
                }
                
        }

        
        
        //Funciones de inicio
        function escene() {
            if (menuGame) {
                mainView(); 
                canvas.addEventListener('click', myClick);
            }
            if (runGame && !menuGame){
                prepareNewFrame();
                        armyToEnemy();
                        playerToEnemy();
                        moveAndDraw();
                        drawPlayer(player.img, player.width, player.height, 
                            player.posX, player.posY);
                        drawEnemies(enemies);
                        animation_army();
                        drawScore(score);
            } 
        }
        
        
        //Loop
        function startGame(){
            if (typeof game_loop!= "undefined") {
                clearInterval(game_loop);
            }
            game_loop = setInterval(escene, 33);
        }

        //Primer inicio

        escene();

    }
)