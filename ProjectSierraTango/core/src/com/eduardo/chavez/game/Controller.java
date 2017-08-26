package com.eduardo.chavez.game;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.scenes.scene2d.InputEvent;
import com.badlogic.gdx.scenes.scene2d.InputListener;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Image;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.utils.viewport.FitViewport;
import com.badlogic.gdx.utils.viewport.Viewport;

/**
 * Created by eduardo3150 on 21/8/17.
 */

public class Controller {
    private GameLoader gameB;
    private Viewport viewport;
    private Stage stage;
    private boolean upPressed, downPressed, leftPressed, rightPressed;
    private OrthographicCamera camera;

    public Controller(GameLoader game) {
        this.gameB = game;
        camera = new OrthographicCamera();
        viewport = new FitViewport(800, 480, camera);
        stage = new Stage(viewport, gameB.batch);


        stage.addListener(new InputListener() {
            @Override
            public boolean keyDown(InputEvent event, int keycode) {
                switch (keycode) {
                    case Input.Keys.UP:
                        upPressed = true;
                        break;
                    case Input.Keys.DOWN:
                        downPressed = true;
                        break;
                    case Input.Keys.LEFT:
                        leftPressed = true;
                        break;
                    case Input.Keys.RIGHT:
                        rightPressed = true;
                        break;
                }
                return true;
            }

            @Override
            public boolean keyUp(InputEvent event, int keycode) {
                switch (keycode) {
                    case Input.Keys.UP:
                        upPressed = false;
                        break;
                    case Input.Keys.DOWN:
                        downPressed = false;
                        break;
                    case Input.Keys.LEFT:
                        leftPressed = false;
                        break;
                    case Input.Keys.RIGHT:
                        rightPressed = false;
                        break;
                }
                return true;
            }
        });

        Gdx.input.setInputProcessor(stage);

        final Table table = new Table();
        table.left().bottom();

        final Table table1 = new Table();
        table1.setPosition(stage.getWidth() - 80,45);

        Texture arrowUp = game.getManager().get(AssetsLocation.ARROW_UP);
        Texture arrowDown = game.getManager().get(AssetsLocation.ARROW_DOWN);
        Texture arrowLeft = game.getManager().get(AssetsLocation.ARROW_LEFT);
        Texture arrowRight = game.getManager().get(AssetsLocation.ARROW_RIGHT);

        Image upImg = new Image(arrowUp);
        upImg.setSize(75, 75);

        upImg.addListener(new InputListener() {
            @Override
            public boolean touchDown(InputEvent event, float x, float y, int pointer, int button) {
                upPressed = true;
                return true;
            }

            @Override
            public void touchUp(InputEvent event, float x, float y, int pointer, int button) {
                upPressed = false;

            }
        });


        Image downImg = new Image(arrowDown);
        downImg.setSize(50, 50);
        downImg.addListener(new InputListener() {
            @Override
            public boolean touchDown(InputEvent event, float x, float y, int pointer, int button) {
                downPressed = true;
                return true;
            }

            @Override
            public void touchUp(InputEvent event, float x, float y, int pointer, int button) {
                downPressed = false;
            }
        });

        Image leftImg = new Image(arrowLeft);
        leftImg.setSize(75, 75);
        leftImg.addListener(new InputListener() {
            @Override
            public boolean touchDown(InputEvent event, float x, float y, int pointer, int button) {
                leftPressed = true;
                return true;
            }

            @Override
            public void touchUp(InputEvent event, float x, float y, int pointer, int button) {
                leftPressed = false;
            }
        });


        final Image rightImg = new Image(arrowRight);
        rightImg.setSize(75, 75);
        rightImg.addListener(new InputListener() {
            @Override
            public boolean touchDown(InputEvent event, float x, float y, int pointer, int button) {
                rightPressed = true;
                return true;
            }

            @Override
            public void touchUp(InputEvent event, float x, float y, int pointer, int button) {
                rightPressed = false;
            }
        });

        table.row().pad(5, 5, 5, 5);
        table.add(leftImg).size(leftImg.getWidth(), leftImg.getHeight());
        table.add();
        table.add(rightImg).size(rightImg.getWidth(), rightImg.getHeight());
        table.row().padBottom(5);
        table.add();
        //table.add(downImg).size(downImg.getWidth(), downImg.getHeight());
        table.add();
        table.add();

        table1.row().pad(5, 5, 5, 5);
        table1.add();
        table1.add(upImg).size(upImg.getWidth(), upImg.getHeight());
        table1.add();
        table1.row().padBottom(5);
        table1.add();
        table1.add();
        table1.add();

        stage.addActor(table);
        stage.addActor(table1);

    }

    public void draw() {
        stage.draw();
    }

    public boolean isUpPressed() {
        return upPressed;
    }

    public boolean isDownPressed() {
        return downPressed;
    }

    public boolean isLeftPressed() {
        return leftPressed;
    }

    public boolean isRightPressed() {
        return rightPressed;
    }

    public void resize(int width, int height) {
        viewport.update(width, height);
    }
}
