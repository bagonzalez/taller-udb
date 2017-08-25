package com.eduardo.chavez.game;

import com.badlogic.gdx.Application;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.MathUtils;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.physics.box2d.Contact;
import com.badlogic.gdx.physics.box2d.ContactImpulse;
import com.badlogic.gdx.physics.box2d.ContactListener;
import com.badlogic.gdx.physics.box2d.Manifold;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.utils.viewport.FitViewport;
import com.eduardo.chavez.game.Actores.FloorEntity;
import com.eduardo.chavez.game.Actores.GreenEnemy;
import com.eduardo.chavez.game.Actores.MainActorEntity;

import java.util.ArrayList;
import java.util.List;


import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;


/**
 * Created by usuario on 19/8/17.
 */

class GameScreen implements Screen {
    private Stage stage;
    private World world;
    private Controller controller;
    private MainActorEntity mainActor;
    GameLoader game;
    private List<FloorEntity> floorList = new ArrayList<FloorEntity>();
    private List<GreenEnemy> enemyList = new ArrayList<GreenEnemy>();


    private Texture textureFloor;
    private Texture textureOverfloor;
    private float randomFloorPosition, randomFloorWidth, floorPosition, floorHeight, lastFloorSpawntime, randomFloorHeight, previousFloorPosition;

    //Enemy utils
    Texture textureEnemy;
    private boolean firstSpawn = true;
    private float lastEnemySpawnTime, position, previousPos, randomAmount, randomHeight, height;


    public GameScreen(GameLoader gameLoader) {
        this.game = gameLoader;
        stage = new Stage(new FitViewport(640, 360));
        world = new World(new Vector2(0, -10), true);
        controller = new Controller(game);
        stage.setDebugAll(true);

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
                    mainActor.setAlive(false);
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

    }


    @Override
    public void show() {
        Texture textureMainActor = game.getManager().get("actor/IDLE000.png");
        textureFloor = game.getManager().get("actor/floor.png");
        textureOverfloor = game.getManager().get("actor/overfloor.png");
        textureEnemy = game.getManager().get("actor/enemy1.png");

        mainActor = new MainActorEntity(world, textureMainActor, new Vector2(1.5f, 1.5f));
        floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, 0, 10000, 1));
        stage.addActor(mainActor);

    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClearColor(0.4f, 0.5f, 0.8f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
        if (Gdx.app.getType() == Application.ApplicationType.Android)
            controller.draw();

        if ((mainActor.getX() > stage.getWidth() / 2 && mainActor.isAlive())) {
            stage.getCamera().translate(mainActor.getCurrentSpeed() * delta * PIXELS_IN_METER, 0, 0);
        }

        stage.act();

        if (firstSpawn) {
            spawnEnemy();
            spawnFloor();
        }
        if (lastEnemySpawnTime >= 1f) {
            spawnEnemy();
            lastEnemySpawnTime = 0;
        } else {
            lastEnemySpawnTime += delta;
        }

        if (lastFloorSpawntime >= 3.5f) {
            spawnFloor();
            lastFloorSpawntime = 0;
        } else {
            lastFloorSpawntime += delta;
        }

        for (FloorEntity floor : floorList) {
            stage.addActor(floor);
        }

        for (GreenEnemy enemy : enemyList) {
            stage.addActor(enemy);
        }

        world.step(delta, 6, 2);
        handleInput();
        stage.draw();


    }

    private void spawnEnemy() {
        randomAmount = MathUtils.random(0, 15);
        randomHeight = MathUtils.random(1, 4);
        if (firstSpawn) {
            position = 20;
            height = 1;
            previousPos = 10;
        } else {
            position = previousPos + randomAmount;
            previousPos = position;
            height = randomHeight;
        }
        enemyList.add(new GreenEnemy(world, textureEnemy, position, height));
        System.out.println(String.valueOf(position));
        firstSpawn = false;

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
            floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, floorPosition + 1, randomFloorWidth - 2, floorHeight));
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

    }

    @Override
    public void resume() {

    }

    @Override
    public void hide() {
        stage.dispose();
        mainActor.detach();
        mainActor.remove();

        for (FloorEntity floor : floorList) {
            floor.detach();
            floor.remove();
        }

        for (GreenEnemy enemy : enemyList) {
            enemy.detach();
            enemy.remove();
        }
    }

    @Override
    public void dispose() {
        stage.dispose();
        world.dispose();
    }
}
