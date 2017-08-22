package com.eduardo.chavez.game;

import com.badlogic.gdx.Application;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.eduardo.chavez.game.Actores.GreenEnemy;
import com.eduardo.chavez.game.Actores.MainActor;
import com.eduardo.chavez.game.Actores.MainActorStatic;


/**
 * Created by usuario on 19/8/17.
 */

class GameScreen implements Screen {
    final GameLoader game;
    private Stage stage;

    Controller controller;
    //MainActor mainActor;
    GreenEnemy greenEnemy;
    MainActorStatic mainActor;


    //Objects used
    Texture walkSheet, enemyTexture;
    boolean isJumping;

    private float currentPosition;

    public GameScreen(GameLoader gameLoader) {
        this.game = gameLoader;
        walkSheet = new Texture(Gdx.files.internal("actor/IDLE000.png"));
        enemyTexture = new Texture(Gdx.files.internal("actor/enemy1.png"));

    }


    @Override
    public void show() {
        controller = new Controller(game);
        stage = new Stage();
        stage.setDebugAll(true);
        mainActor = new MainActorStatic(walkSheet);
        greenEnemy = new GreenEnemy(enemyTexture);
        stage.addActor(mainActor);
        stage.addActor(greenEnemy);
        mainActor.setPosition(20, 100);
        greenEnemy.setPosition(500, 100);

        currentPosition = mainActor.getX();

    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClearColor(0.4f, 0.5f, 0.8f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
        if (Gdx.app.getType() == Application.ApplicationType.Android)
            controller.draw();

        stage.act();
        handleInput();
        verifyCollisions();
        stage.draw();

    }

    private void verifyCollisions() {
        if (mainActor.isAlive() && (mainActor.getWidth() + mainActor.getX() > greenEnemy.getX())) {
            System.out.println("Colision");
            mainActor.setAlive(false);
        }
    }


    private void handleInput() {
        if (controller.isRightPressed()) {
            mainActor.setX((currentPosition += 200 * Gdx.graphics.getDeltaTime()));

        } else if (controller.isLeftPressed()) {

            mainActor.setX((currentPosition -= 200 * Gdx.graphics.getDeltaTime()));
        } else {

        }

        if (controller.isUpPressed() && (!isJumping)) {
            //playerBox.y += 200 * Gdx.graphics.getDeltaTime();

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
        walkSheet.dispose();
        stage.dispose();
    }

    @Override
    public void dispose() {
        walkSheet.dispose();
        stage.dispose();
    }
}
