package com.eduardo.chavez.game;

import com.badlogic.gdx.Application;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
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
import java.util.Random;

import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;
import static com.eduardo.chavez.game.Constants.PLAYER_SPEED;


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

    private float currentNumber = 0f, min = 0, max = 100;
    Random r = new Random();

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
        Texture textureFloor = game.getManager().get("actor/floor.png");
        Texture textureOverfloor = game.getManager().get("actor/overfloor.png");
        Texture textureEnemy = game.getManager().get("actor/enemy1.png");

        mainActor = new MainActorEntity(world, textureMainActor, new Vector2(1.5f, 1.5f));

        floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, 0, 1000, 1));
        floorList.add(new FloorEntity(world, textureFloor, textureOverfloor, 12, 10, 2));

        for (int i = 0; i < 20 ; i++) {
            float random = min + r.nextFloat() * (max - min);
            enemyList.add(new GreenEnemy(world, textureEnemy, random, 1));
        }



        stage.addActor(mainActor);


        for (FloorEntity floor : floorList) {
            stage.addActor(floor);
        }

        for (GreenEnemy enemy : enemyList) {

            stage.addActor(enemy);

        }

    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClearColor(0.4f, 0.5f, 0.8f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
        if (Gdx.app.getType() == Application.ApplicationType.Android)
            controller.draw();

        if (mainActor.getX() > 150 && mainActor.isAlive()) {
            stage.getCamera().translate(mainActor.getCurrentSpeed() * delta * PIXELS_IN_METER, 0, 0);
        }
        stage.act();
        world.step(delta, 6, 2);
        handleInput();
        stage.draw();

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
