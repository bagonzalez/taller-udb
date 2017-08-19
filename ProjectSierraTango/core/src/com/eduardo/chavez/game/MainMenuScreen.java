package com.eduardo.chavez.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;

/**
 * Created by usuario on 19/8/17.
 */

class MainMenuScreen implements Screen {
    final GameLoader gameLoader;
    OrthographicCamera camera;

    public MainMenuScreen(GameLoader gameLoader) {
        this.gameLoader = gameLoader;
        camera = new OrthographicCamera();
        camera.setToOrtho(false, 800, 480);
    }

    @Override
    public void show() {

    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClearColor(0, 0, 0.2f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

        camera.update();

        gameLoader.batch.setProjectionMatrix(camera.combined);

        gameLoader.batch.begin();
        gameLoader.font.draw(gameLoader.batch, "Bienvenido al juego", 100, 150);
        gameLoader.font.draw(gameLoader.batch, "Toca para iniciar", 100, 100);
        gameLoader.batch.end();

        if (Gdx.input.isTouched()) {
            gameLoader.setScreen(new GameScreen(gameLoader));
            dispose();
        }

    }

    @Override
    public void resize(int width, int height) {

    }

    @Override
    public void pause() {

    }

    @Override
    public void resume() {

    }

    @Override
    public void hide() {

    }

    @Override
    public void dispose() {

    }
}
