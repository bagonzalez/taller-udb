package com.eduardo.chavez.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;

/**
 * Created by usuario on 19/8/17.
 */

class MainMenuScreen implements Screen {
    final GameLoader gameLoader;
    OrthographicCamera camera;
    Music introMusic;
    Texture iconMenu;

    public MainMenuScreen(GameLoader gameLoader) {
        this.gameLoader = gameLoader;
        camera = new OrthographicCamera();
        camera.setToOrtho(false, 800, 480);
        introMusic = gameLoader.getManager().get(AssetsLocation.MUSIC_MENU);
        iconMenu = gameLoader.getManager().get(AssetsLocation.GREEN_ENEMY);
        introMusic.setLooping(true);

    }

    @Override
    public void show() {
        introMusic.play();
    }

    @Override
    public void render(float delta) {
        Gdx.gl.glClearColor(0, 0, 0.2f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

        camera.update();

        gameLoader.batch.setProjectionMatrix(camera.combined);

        gameLoader.batch.begin();
        gameLoader.font.draw(gameLoader.batch, "Bienvenido a Impossibru", 100, 150);
        gameLoader.font.draw(gameLoader.batch, "Toca para iniciar", 100, 100);
        gameLoader.batch.draw(iconMenu, camera.viewportWidth / 2, camera.viewportHeight - 300, 200, 200);
        gameLoader.batch.end();

        if (Gdx.input.justTouched() || Gdx.input.isKeyJustPressed(Input.Keys.ANY_KEY)) {
            gameLoader.setScreen(new GameScreen(gameLoader));
            dispose();
        }

    }

    @Override
    public void resize(int width, int height) {

    }

    @Override
    public void pause() {
        introMusic.pause();
    }

    @Override
    public void resume() {
        introMusic.play();
    }

    @Override
    public void hide() {
        introMusic.pause();
    }

    @Override
    public void dispose() {
        introMusic.pause();
    }
}
