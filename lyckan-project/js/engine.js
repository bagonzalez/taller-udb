$(document).ready(
    function () {

        // PROPIEDADES INICIALESs
        var canvas = $("#canvas")[0];
        var contexto = canvas.getContext("2d");
        var numberEnemies = 10;
        var runGame = true;
        var menuGame = true;
        var intro = true;
        var score = 0;
        var pointCreateEnemy = 100;
        var pointCreatePower = 200;
        var buttons = Array(5);
        var timerBossAtac = 0;
        var maxScore = 0;
        buttons[0] = { x: 200, y: 300, width: 200, height: 60, text: "Iniciar" };
        buttons[1] = { x: 200, y: 400, width: 200, height: 60, text: "Score" };
        buttons[2] = { x: 200, y: 500, width: 200, height: 60, text: "Creditos" };
        buttons[3] = { x: 200, y: 500, width: 200, height: 60, text: "Regresar" };
        buttons[4] = { x: 200, y: 500, width: 200, height: 60, text: "<3" };
        var trackFinal = false;
        var imgEnemies = Array();
        var velocidadTexto = 0;
        var posicionCaracter = 1;
        var caracterX = 30;
        var transparencia = 1;
        var cross = false;
        var transicionInicialIn = 0;
        var transicionInicialOut = 1;
        var enemyFinalLives = 5;
        var playerLives = 3;
        var speedEnemies = 2;
        var increment = 1;


        for (var i = 0; i < 21; i++) {
            imgEnemies[i] = $("#enemy" + (1 + i))[0];
        }
        //Base de datos
        if (typeof (Storage) !== "undefined") {
            if (localStorage.maxScore == "undefined") {
                localStorage.setItem("maxScore", maxScore); 
                
            }
            maxScore = localStorage.getItem("maxScore");
        } else {
            // No soporta
        }

        //FISICAS
        var velocidad = 5;
        var game_loop;

        function numRandom(min, max) {
            return Math.round(Math.random() * (max - min) + min);
        }


        //ACTORES
        var player = { direccion: "", posX: 250, posY: 510, width: 60, height: 45, img: null, lives: 3 };
        var enemies = Array();
        var powers = Array();


        var laser1 = { direccion: "", posX: 0, posY: 500, width: 13, height: 54, img: null, colision: false };
        var laser2 = { direccion: "", posX: 0, posY: 500, width: 13, height: 54, img: null, colision: false };
        var lasers = Array();
        var lasersEnemy = Array();


        var imgEnemyFinal = $("#masterEnemy" + 1)[0];
        var enemyFinal = { direccion: "1", posX: 50, posY: -350, width: 500, height: 302, img: imgEnemyFinal, lives: 4 };

        //GRAFICOS
        player.img = $("#playerImg")[0];

        laser1.img = $("#laser1")[0];
        laser2.img = $("#laser2")[0];
        var damage1 = $("#damage1")[0];

        var backImg = Array(4);
        for (var i = 0; i < 4; i++) {
            backImg[i] = $("#back" + (i + 1))[0];
        }

        //SONIDOS
        laserSnd = $("#laserSnd1")[0];

        var audio = $("#audio1")[0];
        audio.volume = 1;

        var audioFinal = $("#audio2")[0];
        audioFinal.volume = 1;

        //POWER-UPS
        var powerUpsActivate = false;

        
        

        function initialValue() {
            for (var i = 0; i < numberEnemies; i++) {
                var image = imgEnemies[numRandom(0, 20)];
                enemies.push({ direccion: "", posX: numRandom(10, (canvas.width - 70)), posY: -numRandom(100 + pointCreateEnemy, 200 + pointCreateEnemy), width: 60, height: 45, img: image, lives: 2, colision: false });
                pointCreateEnemy += 100;
            }

            for (var i = 0; i < 2; i++) {
                powers.push({ direccion: "", posX: numRandom(10, (canvas.width - 70)), posY: -numRandom(100 + pointCreatePower, 200 + pointCreatePower), width: 60, height: 45, img: $("#pw1")[0], lives: 2, colision: false });
                pointCreatePower += 400;
            }
            player.lives = 3;
            enemyFinal.lives = 4;
            player.img = $("#playerImg")[0];
            runGame = true;
            trackFinal = false;
            menuGame = true;
            cross = false;
            transparencia = 1;
            transicionInicialIn = 0;
            transicionInicialOut = 1;
            speedEnemies = 2;
            score = 0;
        }


        function nextLevel(){
            for (var i = 0; i < (numberEnemies * increment); i++) {
                var image = imgEnemies[numRandom(0, 20)];
                enemies.push({ direccion: "", posX: numRandom(10, (canvas.width - 70)), posY: -numRandom(100 + pointCreateEnemy, 200 + pointCreateEnemy), width: 60, height: 45, img: image, lives: 2, colision: false });
                pointCreateEnemy += 100;
            }

            for (var i = 0; i < 2; i++) {
                powers.push({ direccion: "", posX: numRandom(10, (canvas.width - 70)), posY: -numRandom(100 + pointCreatePower, 200 + pointCreatePower), width: 60, height: 45, img: image, lives: 2, colision: false });
                pointCreatePower += 400;
            }

            enemyFinal.lives = 4  * increment;
            player.lives = 3;
            player.img = $("#playerImg")[0];
            enemyFinal.img = imgEnemyFinal;
            runGame = true;
            trackFinal = false;
            cross = false;
            transparencia = 1;
            transicionInicialIn = 0;
            transicionInicialOut = 1;
            audio2.pause();
            audio.play();
            audio.volume = 1;
        }
        

        //CONTROLES
        function moveToActor(key) {
            switch (key) {
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
                    if (intro) {
                        intro = false;
                    }
                    lasers.push({ posX: player.posX + (player.width / 2), posY: 500, width: 9, height: 45, img: $("#laser1")[0] })
                    drawArmy(lasers);
                    laserSound();
                    break;
            }
        }

        $(document).keydown(function (event) {
            console.log(event.which);
            moveToActor(event.which);

        })

        //DIBUJADO
        function prepareNewFrame() {
            canvas.width = canvas.width;
        }

        function setBackground(img) {
            contexto.save;
            contexto.drawImage(img, 0, 0, 600, 600);
            contexto.restore;
        }

        function setBackgroundPlay(img, img2, cross, com) {
            contexto.save;
            contexto.drawImage(img, 0, 0, 600, 600);
            if (cross) {
                contexto.globalAlpha = transparencia;
                contexto.drawImage(img, 0, 0, 600, 600);
                contexto.globalAlpha = 1 - transparencia;
                contexto.drawImage(img2, 0, 0, 600, 600);
                if (transparencia >= 0) {
                    transparencia -= 0.02;
                }
                contexto.globalAlpha = 1;
            }
            contexto.restore;
        }

        function drawPlayer(img, imgWidth, imgHeigth, cordX, cordY) {
            contexto.save;
            contexto.drawImage(img, cordX, cordY, imgWidth, imgHeigth);
            contexto.restore;
        }

        function drawLives() {
            contexto.save;

            if (powerUpsActivate) {
                contexto.drawImage($("#pw1")[0], 400, 10, 37, 26);
            }
            
            if (player.lives > 2) {
                contexto.drawImage($("#livesImg")[0], 440, 10, 37, 26);
            }
            if (player.lives > 1) {
                contexto.drawImage($("#livesImg")[0], 480, 10, 37, 26);
            }
            if (player.lives > 0) {
                contexto.drawImage($("#livesImg")[0], 520, 10, 37, 26);
            }


            contexto.restore;
        }

        function drawEnemies(array) {
            contexto.save;
            for (var i = 0; i < array.length; i++) {
                var element = array[i];
                contexto.drawImage(element.img, element.posX, element.posY, element.width, element.height);
                enemies[i].posY += speedEnemies;
            }
            contexto.restore;
        }

        function drawPower(array) {
            contexto.save;
            for (var i = 0; i < array.length; i++) {
                var element = array[i];
                contexto.drawImage($("#pw1")[0], element.posX, element.posY, element.width, element.height);
                powers[i].posY += speedEnemies;
            }
            contexto.restore;
        }

        function drawArmy(array) {
            contexto.save;
            for (var i = 0; i < array.length; i++) {
                var element = array[i];
                if (element.posY > (-100)) {
                    if (powerUpsActivate) {
                        contexto.drawImage($("#laserPw")[0], element.posX, element.posY, element.width + 10, element.height+10);
                    }else{
                        contexto.drawImage(element.img, element.posX, element.posY, element.width, element.height);
                    }

                    
                    lasers[i].posY -= 10;
                } else {
                    lasers.splice(i, 1);
                }
            }
            contexto.restore;
        }

        function drawArmyEnemy(array) {
            contexto.save;
            for (var i = 0; i < array.length; i++) {
                var element = array[i];
                if (element.posY < (800)) {
                    contexto.drawImage(element.img, element.posX, element.posY, element.width, element.height);
                    lasersEnemy[i].posY += speedEnemies * 2;
                } else {
                    lasersEnemy.splice(i, 1);
                }
            }
            contexto.restore;
        }

        function moveAndDraw() {
            var desplasamiento = 0;
            if (player.posX > 10 && player.direccion == "izquierda") {
                desplasamiento = -velocidad;
            }
            if (player.posX < (canvas.width - 70) && player.direccion == "derecha") {
                desplasamiento = velocidad;
            }
            player.posX += desplasamiento;
        }

        function drawScore(score) {
            contexto.save;
            contexto.font = "20px arial";
            contexto.fillStyle = "white";
            contexto.fillText("Score:" + score, 10, 30);
            contexto.restore;
        }

        //MOTOR
        function animation_army() {
            if (lasers.length > 0) {
                drawArmy(lasers);
            }

            if (lasersEnemy.length > 0) {
                drawArmyEnemy(lasersEnemy);
            }
        }

        function armyToEnemy() {
            var num = 0;
            lasers.forEach(function (laser) {
                if (!laser.colision) {
                    for (var index = 0; index < enemies.length; index++) {
                        var enemy = enemies[index];
                        if (
                            laser.posX + laser.width > enemy.posX &&
                            laser.posX < enemy.posX + enemy.width &&
                            laser.posY < enemy.posY + enemy.height &&
                            laser.posY + laser.height > enemy.posY &&
                            laser.posY > 0 && enemy.lives >= 1
                        ) {
                            if (enemies[index].lives <= 1) {
                                enemies[index].img = damage1;
                                score += 10;
                                laser.colision = true;
                                lasers.splice(num, 1);
                                enemies[index].lives -= 1;
                                break;
                            } else {
                                if (powerUpsActivate) {
                                    enemies[index].lives -= 2;
                                    enemies[index].img = damage1;
                                    laser.colision = true;
                                    lasers.splice(num, 1);
                                }else{
                                    enemies[index].lives -= 1;
                                }
                                
                                laser.colision = true;
                                lasers.splice(num, 1);
                            }

                        }
                    }
                } else {

                }
            }, this);
        }


        function armyToEnemyFinal() {
            var num = 0;
            lasers.forEach(function (laser) {
                if (!laser.colision) {

                    if (
                        laser.posX + laser.width > enemyFinal.posX &&
                        laser.posY < enemyFinal.posY + enemyFinal.height &&
                        laser.posX < enemyFinal.posX + enemyFinal.width &&
                        laser.posY + laser.height > enemyFinal.posY &&
                        laser.posY > 0
                    ) {
                        if (enemyFinal.lives <= 1) {
                            enemyFinal.img = damage1;
                            score += 10;
                            laser.colision = true;
                            lasers.splice(num, 1);
                            enemyFinal.lives -= 1;
                            if (powerUpsActivate) {
                                enemyFinal.lives -= 2;
                            }else{
                                enemyFinal.lives -= 1
                            }
                            
                        } else {
                            enemyFinal.lives -= 1;
                            laser.colision = true;
                            lasers.splice(num, 1);
                            score += 20;
                        }

                    }

                } else {

                }
            }, this);
        }

        function armyToPlayer() {
            var num = 0;
            lasersEnemy.forEach(function (laser) {
                if (!laser.colision) {
                    
                    if (
                        laser.posX + laser.width > player.posX &&
                        laser.posY < player.posY + player.height &&
                        laser.posX < player.posX + player.width &&
                        laser.posY + laser.height > player.posY &&
                        laser.posY > 0
                    ) {
                        if (player.lives < 1) {
                            player.img = damage1;
                            laser.colision = true;
                            player.lives -= 1;
                            runGame = false;
                        } else {
                            player.lives -= 1;
                            laser.colision = true;
                            lasersEnemy.splice(num, 1);
                            powerUpsActivate = false;
                        }


                    }
                    num += 1;

                } else {
                    
                }
            }, this);
        }

        function playerToEnemy() {
            for (var index = 0; index < enemies.length; index++) {
                var enemy = enemies[index];
                if (!(enemy.lives < 1)) {
                    if (
                        player.posX + player.width > enemy.posX &&
                        player.posX < enemy.posX + enemy.width &&
                        player.posY < enemy.posY + enemy.height &&
                        player.posY + player.height > enemy.posY && !enemy.colision
                    ) {
                        if (player.lives < 1) {
                            player.img = damage1;
                            runGame = false;
                            break;
                        } else {
                            player.lives -= 1;
                            enemies[index].colision = true;
                            powerUpsActivate= false;
                        }

                    }
                }

            }
        }

        function playerToPowerUps() {
            for (var index = 0; index < powers.length; index++) {
                var pwA = powers[index];
                    if (
                        player.posX + player.width > pwA.posX &&
                        player.posX < pwA.posX + pwA.width &&
                        player.posY < pwA.posY + pwA.height &&
                        player.posY + player.height > pwA.posY && !pwA.colision
                    ) {
                            powerUpsActivate = true;
                            powers[index].colision = true;

                    }
                
            }
        }

        function exitEnemyScreen() {
            if (enemies.length > 0) {
                if (enemies[0].posY > 610) {
                    enemies.splice(0, 1);
                }
            }
        }

        function enemyFinalInit() {

            if(enemyFinal.lives > 0){
                armyToEnemyFinal();
                armyToPlayer();
                if (enemies.length <= 0) {
                    cross = true;
                    if (audio.volume > 0.02) {
                        audio.volume -= 0.02;
                    } else {
                        audio.pause();
                        if (!trackFinal) {
                            audioFinal.play();
                            trackFinal = true;
                        }
                    }
                    contexto.save;
    
                    contexto.drawImage(enemyFinal.img, enemyFinal.posX, enemyFinal.posY, enemyFinal.width, enemyFinal.height);
    
                    if (enemyFinal.posY < 100) {
                        enemyFinal.posY += 2;
                    } else {
                        
                        if (enemyFinal.direccion == 1) {
                            if (enemyFinal.posX < (300)) {
                                enemyFinal.posX += 2 * increment;
                            } else {
                                enemyFinal.direccion = -1;
                                lasersEnemy.push({ posX: enemyFinal.posX + (enemyFinal.width / 2), posY: 300, width: 85, height: 85, img: $("#laser2")[0] })
                                drawArmyEnemy(lasersEnemy);
                            }
                        }
    
                        if (enemyFinal.direccion == -1) {
                            if (enemyFinal.posX > -200) {
                                enemyFinal.posX -= 2 * increment;
                            } else {
                                enemyFinal.direccion = 1;
                                lasersEnemy.push({ posX: enemyFinal.posX + (enemyFinal.width / 2), posY: 300, width: 85, height: 85, img: $("#laser2")[0] })
                                drawArmyEnemy(lasersEnemy);
                            }
                        }
    
                        if (timerBossAtac > 500) {
                            lasersEnemy.push({ posX: enemyFinal.posX + (enemyFinal.width / 2), posY: 300, width: 85, height: 85, img: $("#laser2")[0] })
                            drawArmyEnemy(lasersEnemy);
                            timerBossAtac = 0;
                        } else {
                            timerBossAtac += 10;
                        }
                    }
                    contexto.restore;
                }
            }else{
                if (enemyFinal.posY > -350) {
                    enemyFinal.posY -= 4;
                    transparencia += 0.028;
                    contexto.drawImage(enemyFinal.img, enemyFinal.posX, enemyFinal.posY, enemyFinal.width, enemyFinal.height);
                    
                }else{
                    speedEnemies += 2;
                    increment += 1;
                    pointCreatePower += 100;
                    nextLevel();

                }
            }
        }

        //SOUND
        function laserSound() {
            laserSnd.play();
        }

        //MENU
        function mainView() {
            contexto.save;
            var rect = { x: 0, y: 0, width: 600, height: 600 };
            var backColor = "#2ecc71";
            var textColor = "black";
            contexto.fillStyle = backColor;
            contexto.fillRect(0, 0, 300, 600);

            setBackground(backImg[0]);
            for (var i = 0; i < 3; i++) {
                var button = buttons[i];
                contexto.fillStyle = backColor;
                contexto.fillRect(button.x, button.y, button.width, button.height);
                contexto.font = "20px arial";
                contexto.fillStyle = textColor;
                contexto.fillText(button.text, button.x + 20, button.y + 35);
            }

            contexto.fillStyle = backColor;
            contexto.fillRect(0, 0, 600, 250);
            contexto.font = "80px arial";
            contexto.fillStyle = "black";
            contexto.fillText("Green Space", 60, 140);

            contexto.restore;
        }

        //Elementos del menu
        //Score

        function scoreView() {
            contexto.save;
            var backColor = "#2ecc71";
            var textColor = "white";
            var btnColor = "black";
            contexto.fillStyle = backColor;
            contexto.fillRect(0, 0, 600, 600);
            contexto.fillStyle = btnColor;
            contexto.fillRect(buttons[3].x, buttons[3].y, buttons[3].width, buttons[3].height);
            contexto.font = "60px arial";
            contexto.fillStyle = textColor;
            contexto.fillText(maxScore, 200, 300);
            contexto.font = "20px arial";
            contexto.fillText(buttons[3].text, buttons[3].x + 20, buttons[3].y + 35);
            contexto.restore;
        }

        function settingView() {
            contexto.save;
            var backColor = "#2ecc71";
            var textColor = "white";
            var btnColor = "black";

            contexto.fillStyle = backColor;
            contexto.fillRect(0, 0, 600, 600);

            contexto.font = "40px arial";
            contexto.fillStyle = btnColor;
            contexto.fillText("Cesar Callejas - Lyckan", 80, 200);
            contexto.fillText("Ardillo & Ardilla", 150, 300);

            contexto.fillStyle = btnColor;
            contexto.fillRect(buttons[4].x, buttons[4].y, buttons[4].width, buttons[4].height);
            contexto.font = "20px arial";
            contexto.fillStyle = textColor;
            contexto.fillText(buttons[4].text, buttons[4].x + 80, buttons[4].y + 35);
            contexto.restore;
        }

        function gameOverView() {
            contexto.restore;
            var backColor = "#2ecc71";
            var textColor = "black";
            contexto.fillStyle = backColor;
            contexto.fillRect(buttons[4].x, buttons[4].y, buttons[4].width, buttons[4].height);
            contexto.font = "20px arial";
            contexto.fillStyle = textColor;
            contexto.fillText("Fin del Juego", buttons[4].x + 20, buttons[4].y + 35);
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

        function isInside(pos, rect) {
            return pos.x > rect.x && pos.x < rect.x + rect.width &&
                pos.y < rect.y + rect.height && pos.y > rect.y
        }


        function myClick(event) {
            var mousePos = getMousePos(canvas, event);

            if (isInside(mousePos, buttons[0])
            ) {

                canvas.removeEventListener('click', myClick);
                menuGame = false;
                runGame = true;
                startGame();

            }

            if (isInside(mousePos, buttons[1])
            ) {

                //canvas.removeEventListener('click', myClick);
                //menuGame = false;
                canvas.removeEventListener('click', myClick);
                scoreView();
                canvas.addEventListener('click', myClick2);
            }

            if (isInside(mousePos, buttons[2])
            ) {

                //canvas.removeEventListener('click', myClick);
                //menuGame = false;
                canvas.removeEventListener('click', myClick);
                settingView();
                canvas.addEventListener('click', settingClick);


            }


        }

        function myClick2(event) {
            var mousePos = getMousePos(canvas, event);

            if (isInside(mousePos, buttons[3])
            ) {
                canvas.removeEventListener('click', myClick2);
                mainView();
                canvas.addEventListener('click', myClick);
            }

        }

        function settingClick(event) {
            var mousePos = getMousePos(canvas, event);

            if (isInside(mousePos, buttons[4])
            ) {
                canvas.removeEventListener('click', settingClick);
                mainView();
                canvas.addEventListener('click', myClick);

            }

        }

        function gameOverClick(event) {
            var mousePos = getMousePos(canvas, event);

            if (isInside(mousePos, buttons[4])
            ) {
                canvas.removeEventListener('click', gameOverClick);
                menuGame = true;
                trackFinal = false;
                audio2.pause();
                escene();
                audio.volume = 1;
                audio.play();
                enemyFinal.lives = 4;
                enemyFinal.posY = -350;
                enemyFinal.img = imgEnemyFinal;
                lasersEnemy.splice(0, lasersEnemy.length);
                clearInterval(game_loop);
                

            }

        }

        function wrapText(text, x, y, maxWidth, lineHeight) {
            contexto.save;
            var words = text.split(' ');
            var line = '';

            for (var n = 0; n < words.length; n++) {
                var testLine = line + words[n] + ' ';
                var metrics = contexto.measureText(testLine);
                var testWidth = metrics.width;
                if (testWidth > maxWidth && n > 0) {
                    contexto.fillText(line, x, y);
                    line = words[n] + ' ';
                    y += lineHeight;
                }
                else {
                    line = testLine;
                }
            }
            contexto.fillText(line, x, y);
            contexto.restore;
        }

        function history() {
            contexto.save;
            contexto.globalAlpha = transicionInicialOut;
            var txt = "";
            var textos = "En el año 2500 los humanos iniciaron una dura batalla contra una nueva raza que viaja por el espacio en busca de un planeta con sustento biológico para establecer una nueva colonia, los humanos desarrollaron suficiente tecnología y luchan para defender su hogar. Un joven soldado inicia la ultima batalla contra la gran flota de invasores.";
            var tempo = 6;
            contexto.font = "20px arial";
            contexto.fillStyle = "white";
            txt = textos.substr(0, posicionCaracter);
            if (posicionCaracter < textos.length && velocidadTexto >= tempo) {
                posicionCaracter += 1;
                velocidadTexto = 0;

            }
            if (velocidadTexto < tempo) {
                velocidadTexto += 2;
            }

            if (posicionCaracter >= (textos.length - 1)) {
                if (velocidadTexto < (300)) {
                    velocidadTexto += 2;

                } else {
                    intro = false;
                }

                if (transicionInicialOut >= 0.02) {
                    transicionInicialOut -= 0.02;
                }
            }

            var maxWidth = 570;
            var lineHeight = 25;
            var x = 30;
            var y = 60;

            wrapText(txt, x, y, maxWidth, lineHeight);
            contexto.restore;

        }



        //Funciones de inicio
        function escene() {
            if (menuGame) {
                prepareNewFrame();
                mainView();
                canvas.addEventListener('click', myClick);
                enemies.splice(0, numberEnemies);
                initialValue();
                    
                
            }

            if (runGame && !menuGame && intro) {
                prepareNewFrame();
                setBackground(backImg[2]);
                history();

            }

            if (runGame && !menuGame && !intro) {
                prepareNewFrame();
                if (transicionInicialOut < 1) {
                    contexto.globalAlpha = transicionInicialOut;
                    transicionInicialOut += 0.02;
                }
                setBackgroundPlay(backImg[2], backImg[3], cross);
                exitEnemyScreen();
                armyToEnemy();
                playerToEnemy();
                playerToPowerUps();
                moveAndDraw();
                drawPlayer(player.img, player.width, player.height,
                    player.posX, player.posY);
                enemyFinalInit();
                drawEnemies(enemies);
                drawPower(powers);
                animation_army();
                drawScore(score);
                drawLives();
            }
            if (!runGame && !menuGame) {
                gameOverView();
                canvas.addEventListener('click', gameOverClick);
                if (score > maxScore) {
                    maxScore = score;
                    localStorage.maxScore = maxScore;
                    score = 0;
                }


            }
        }


        //Loop
        function startGame() {
            if (typeof game_loop != "undefined") {
                clearInterval(game_loop);
            }
            game_loop = setInterval(escene, 33);
        }

        //Primer inicio

        escene();
        audio.play();

    }
)