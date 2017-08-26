package com.eduardo.chavez.game;

import com.badlogic.gdx.Application;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.MathUtils;
import com.badlogic.gdx.math.Rectangle;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.physics.box2d.Contact;
import com.badlogic.gdx.physics.box2d.ContactImpulse;
import com.badlogic.gdx.physics.box2d.ContactListener;
import com.badlogic.gdx.physics.box2d.Manifold;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.utils.Array;
import com.badlogic.gdx.utils.IntFloatMap;
import com.badlogic.gdx.utils.viewport.FitViewport;
import com.eduardo.chavez.game.Actores.FloorEntity;
import com.eduardo.chavez.game.Actores.GreenEnemyEntitiy;
import com.eduardo.chavez.game.Actores.MainActor;
import com.eduardo.chavez.game.Actores.RedEnemyEntitiy;


import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;


import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;


/**
 * Created by usuario on 19/8/17.
 */

class GameScreen implements Screen {
    private Stage stage;
    private World world;
    private Controller controller;
    //private MainActorEntity mainActor;
    private MainActor mainActor;
    private GameLoader game;
    private int scoreCounter = 0;
    private float lastScoreTime;

    //Floor utils
    private List<FloorEntity> floorList = new ArrayList<FloorEntity>();
    private Texture textureFloor;
    private Texture textureOverfloor;
    private float randomFloorPosition, randomFloorWidth, floorPosition, floorHeight, lastFloorSpawntime, randomFloorHeight, previousFloorPosition;

    //Green Enemy utils
    private List<GreenEnemyEntitiy> enemyList = new ArrayList<GreenEnemyEntitiy>();
    private Texture textureEnemy;
    private boolean firstSpawn = true;
    private float lastEnemySpawnTime, position, previousPos, randomAmount, randomHeight, height;

    private List<RedEnemyEntitiy> redEnemyEntitiys = new ArrayList<RedEnemyEntitiy>();
    Texture secondEnemy;
    private float lastSpawnFallingEnemyTime, fallinRandomSpeed, fallinEnemyPosition, fallingRandomAmount, fallingPreviousPos, fallingRandomHeight, fallingHeight;


    //Music utils
    private Music mainMusic;
    private Music secondaryMusic;
    private Music creditsMusic;

    Texture background, heartIcon;
    private int livesAmount = 3;
    Array<Rectangle> hearts;
    private boolean substractHeart = false;
    private boolean canDie = true;
    private float deadTimer;

    private Texture textureMainActor;

    public GameScreen(GameLoader gameLoader) {
        this.game = gameLoader;
        stage = new Stage(new FitViewport(800, 480));
        world = new World(new Vector2(0, -10), true);
        controller = new Controller(game);


        world.setContactListener(new ContactListener() {
            private boolean areCollided(Contact contact, Object userA, Object userB) {
                return ((contact.getFixtureA().getUserData().equals(userA) && contact.getFixtureB().getUserData().equals(userB)) ||
                        (contact.getFixtureA().getUserData().equals(userB) && contact.getFixtureB().getUserData().equals(userA)));
            }

            @Override
            public void beginContact(Contact contact) {
                if (areCollided(contact, "player", "floor")) {
                    mainActor.setJumping(false);
                }

                if (areCollided(contact, "player", "enemy")) {
                    if (mainActor.isAlive()) {
                        if (canDie) {
                            livesAmount--;
                            substractHeart = true;
                            canDie = false;
                            System.out.println("vidas" + String.valueOf(livesAmount));
                            if (livesAmount == 0) {
                                mainActor.setAlive(false);
                                mainActor.setKeepPlaying(false);
                            }
                        }
                    }
                }
            }

            @Override
            public void endContact(Contact contact) {

            }

            @Override
            public void preSolve(Contact contact, Manifold oldManifold) {

            }

            @Override
            public void postSolve(Contact contact, ContactImpulse impulse) {

            }
        });

        hearts = new Array<Rectangle>();
        makeLives();

    }

    private void makeLives() {
        Rectangle rectangleLife1 = new Rectangle();
        rectangleLife1.x = stage.getCamera().viewportWidth - 60;
        rectangleLife1.y = stage.getCamera().viewportHeight - 60;
        rectangleLife1.width = 50;
        rectangleLife1.height = 50;

        Rectangle rectangleLife2 = new Rectangle();
        rectangleLife2.x = stage.getCamera().viewportWidth - 120;
        rectangleLife2.y = stage.getCamera().viewportHeight - 60;
        rectangleLife2.width = 50;
        rectangleLife2.height = 50;

        Rectangle rectangleLife3 = new Rectangle();
        rectangleLife3.x = stage.getCamera().viewportWidth - 180;
        rectangleLife3.y = stage.getCamera().viewportHeight - 60;
        rectangleLife3.width = 50;
        rectangleLife3.height = 50;

        hearts.add(rectangleLife1);
        hearts.add(rectangleLife2);
        hearts.add(rectangleLife3);


    }


    @Override
    public void show() {

        //Carga de texturas
        secondEnemy = game.getManager().get(AssetsLocation.GAME_FALLING_ENEMY);
        textureMainActor = game.getManager().get(AssetsLocation.MAIN_ACTOR_ANIMATED);
        background = game.getManager().get(AssetsLocation.GAME_BACKGROUND);
        heartIcon = game.getManager().get(AssetsLocation.GAME_HEART);

        textureFloor = game.getManager().get(AssetsLocation.MAIN_FLOOR);
        textureOverfloor = game.getManager().get(AssetsLocation.GAME_ALT_FLOOR);
        textureEnemy = game.getManager().get(AssetsLocation.GREEN_ENEMY);

        mainMusic = game.getManager().get(AssetsLocation.MUSIC_MAIN);
        secondaryMusic = game.getManager().get(AssetsLocation.MUSIC_SECOND);
        creditsMusic = game.getManager().get(AssetsLocation.MUSIC_CREDITS);

        mainActor = new MainActor(world, textureMainActor, new Vector2(1.5f, 1.5f));


        floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, 0, 10000, 1));


        stage.addActor(mainActor);

        mainMusic.setLooping(true);
        secondaryMusic.setLooping(true);
        creditsMusic.setLooping(true);
        mainMusic.play();

    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClearColor(0.4f, 0.5f, 0.8f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

        if ((mainActor.getX() > stage.getWidth() / 2 && mainActor.isAlive())) {
            stage.getCamera().translate(mainActor.getCurrentSpeed() * delta * PIXELS_IN_METER, 0, 0);

        }
        game.batch.begin();
        game.batch.draw(background, 0, 0, stage.getCamera().viewportWidth, stage.getCamera().viewportHeight);
        game.font.draw(game.batch, "Puntaje actual: " + scoreCounter, 10, stage.getCamera().viewportHeight - 20);
        for (Rectangle heart : hearts) {
            game.batch.draw(heartIcon, heart.x, heart.y);
        }
        game.batch.end();

        stage.act();
        handleInput();
        if (mainActor.isAlive()) {

            if (!canDie) {
                if (deadTimer >= 1.25f) {
                    canDie = true;
                    System.out.println("Now can die");
                    deadTimer = 0;
                } else {
                    deadTimer += delta;
                }
            }

            if (lastScoreTime >= 2.5f && (mainActor.getCurrentSpeed() == 5)) {
                scoreCounter += 25;
                System.out.println("score " + String.valueOf(scoreCounter) + " Player " + String.valueOf(mainActor.getCurrentSpeed() + " Player pos: " + String.valueOf(mainActor.getX() / PIXELS_IN_METER)));
                lastScoreTime = 0;
            } else {
                lastScoreTime += delta;
            }

            if (scoreCounter >= 250) {
                mainMusic.stop();
                secondaryMusic.play();

                if (lastSpawnFallingEnemyTime >= 1f) {
                    spawnFallingEnemy();
                    lastSpawnFallingEnemyTime = 0;
                } else {
                    lastSpawnFallingEnemyTime += delta;
                }

                for (RedEnemyEntitiy enemyEntitiy : redEnemyEntitiys) {
                    stage.addActor(enemyEntitiy);
                }
            }

            //APARECER UN ELEMENTO INICIAL
            if (firstSpawn) {
                spawnEnemy();
                spawnFloor();
            }

            //APARECER ENEMIGOS ESTATICOS
            if (lastEnemySpawnTime >= 1.75f) {
                spawnEnemy();
                lastEnemySpawnTime = 0;
            } else {
                lastEnemySpawnTime += delta;
            }


            //APARECER SUELOS
            if (lastFloorSpawntime >= 3.5f) {
                spawnFloor();
                lastFloorSpawntime = 0;
            } else {
                lastFloorSpawntime += delta;
            }


            //RELLENAR LA PANTALLA DE PERSONAJES
            for (FloorEntity floor : floorList) {
                stage.addActor(floor);
            }

            for (GreenEnemyEntitiy enemy : enemyList) {
                stage.addActor(enemy);
            }

        }

        //CUando muere
        if (!mainActor.isAlive()) {
            mainMusic.stop();
            secondaryMusic.stop();
            creditsMusic.play();
            game.batch.begin();
            game.font.draw(game.batch, "GAME OVER \nTu puntaje: " + scoreCounter, stage.getCamera().viewportWidth / 2 - PIXELS_IN_METER, stage.getCamera().viewportHeight / 2);
            game.batch.end();

            if (Gdx.input.justTouched() || Gdx.input.isKeyJustPressed(Input.Keys.ANY_KEY)) {
                game.setScreen(new MainMenuScreen(game));
            }

        }

        world.step(delta, 6, 2);

        stage.draw();

        if (Gdx.app.getType() == Application.ApplicationType.Android)
            controller.draw();


        Iterator<Rectangle> iter = hearts.iterator();
        while (iter.hasNext()) {
            Rectangle heart = iter.next();
            if (substractHeart) {
                iter.remove();
                substractHeart = false;
            }
        }

    }

    private void spawnEnemy() {
        randomAmount = MathUtils.random(5, 25);
        randomHeight = MathUtils.random(1, 4);
        if (firstSpawn) {
            position = (mainActor.getX() / PIXELS_IN_METER) + randomAmount;
            height = 1;

        } else {
            previousPos = (mainActor.getX() / PIXELS_IN_METER);
            position = previousPos + randomAmount;
            previousPos = position;
            height = randomHeight;
        }
        enemyList.add(new GreenEnemyEntitiy(world, textureEnemy, position, height));
        System.out.println(String.valueOf(position));
        firstSpawn = false;

    }


    private void spawnFallingEnemy() {
        fallingRandomAmount = MathUtils.random(-10f, 10f);
        fallingRandomHeight = MathUtils.random(10, 15);
        fallinRandomSpeed = MathUtils.random(4, 12);
        //Current position of character
        fallingPreviousPos = (mainActor.getX() / PIXELS_IN_METER);

        fallinEnemyPosition = fallingPreviousPos + fallingRandomAmount;
        fallingPreviousPos = fallinEnemyPosition;

        redEnemyEntitiys.add(new RedEnemyEntitiy(world, secondEnemy, fallinEnemyPosition, fallingRandomHeight, fallinRandomSpeed));
        System.out.println(String.valueOf(fallinEnemyPosition));


    }

    private void spawnFloor() {
        randomFloorPosition = MathUtils.random(0, 75);
        randomFloorWidth = MathUtils.random(2, 12);
        randomFloorHeight = MathUtils.random(2, 3);
        if (firstSpawn) {
            floorHeight = 2;
            floorPosition = 12;
        } else {
            floorPosition = previousFloorPosition + randomFloorPosition;
            previousFloorPosition = floorPosition;
            floorHeight = randomFloorHeight;
        }

        if (floorHeight == 3) {
            randomFloorWidth = randomFloorWidth - 2;

            if (randomFloorWidth == 0) {

            } else {
                floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, floorPosition + 1, randomFloorWidth, floorHeight));
            }
            randomFloorWidth = randomFloorWidth + 2;
            floorHeight = 2;
            floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, floorPosition, randomFloorWidth, floorHeight));
        } else {
            floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, floorPosition, randomFloorWidth, floorHeight));
        }
        System.out.println("Floor " + String.valueOf(floorPosition));
        System.out.print("Actor " + String.valueOf(mainActor.getX()));


        firstSpawn = false;
    }


    private void handleInput() {
        if (controller.isRightPressed()) {
            if (mainActor.isAlive()) {
                mainActor.moveToRight();
            }
        } else if (controller.isLeftPressed()) {
            if (mainActor.isAlive()) {
                mainActor.moveToLeft();
            }
        } else {
            if (mainActor.isAlive()) {
                mainActor.standBy();
            }
        }

        /**
         if (!(controller.isRightPressed() || controller.isLeftPressed())
         && (Gdx.input.justTouched() && (Gdx.app.getType() == Application.ApplicationType.Android))) {
         if (mainActor.isAlive()) {
         mainActor.jump();
         }
         }
         **/
        if (controller.isUpPressed()) {
            if (mainActor.isAlive()) {
                mainActor.jump();
            }
        }


    }

    @Override
    public void resize(int width, int height) {
        controller.resize(width, height);
    }

    @Override
    public void pause() {
        mainMusic.pause();
        creditsMusic.pause();
        secondaryMusic.pause();
    }

    @Override
    public void resume() {
        if (mainActor.isAlive() && scoreCounter >= 250) {
            secondaryMusic.play();
        }
        if (mainActor.isAlive()) {
            mainMusic.play();
        }

        if (!mainActor.isAlive()) {
            creditsMusic.pause();
        }
    }

    @Override
    public void hide() {
        stage.dispose();
        mainActor.detach();
        mainActor.remove();
        mainMusic.pause();
        secondaryMusic.pause();
        creditsMusic.pause();

        for (FloorEntity floor : floorList) {
            floor.detach();
            floor.remove();
        }

        for (GreenEnemyEntitiy enemy : enemyList) {
            enemy.detach();
            enemy.remove();
        }

        for (RedEnemyEntitiy enemy : redEnemyEntitiys) {
            enemy.detach();
            enemy.remove();
        }
    }

    @Override
    public void dispose() {
        stage.dispose();
        mainActor.detach();
        mainActor.remove();
        mainMusic.pause();
        secondaryMusic.pause();
        creditsMusic.pause();

        for (FloorEntity floor : floorList) {
            floor.detach();
            floor.remove();
        }

        for (GreenEnemyEntitiy enemy : enemyList) {
            enemy.detach();
            enemy.remove();
        }


        for (RedEnemyEntitiy enemy : redEnemyEntitiys) {
            enemy.detach();
            enemy.remove();
        }
    }
}
