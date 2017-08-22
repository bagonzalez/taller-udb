package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Animation;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.math.Rectangle;
import com.badlogic.gdx.scenes.scene2d.Actor;

/**
 * Created by eduardo3150 on 21/8/17.
 */

public class MainActor extends Actor {
    //Constants rows and colums
    private static final int FRAME_COLS = 6, FRAME_ROWS = 5;
    Texture walkSheet;
    Animation<TextureRegion> walkAnimation;
    TextureRegion currentFrame;

    //Track elapsed time for animation
    float stateTime;
    private boolean alive;

    public MainActor(Texture walkSheet) {
        this.walkSheet = walkSheet;
        setSize(walkSheet.getWidth(), walkSheet.getHeight());
        alive = true;
        //Personaje
        TextureRegion[][] tmp = TextureRegion.split(walkSheet, walkSheet.getWidth() / FRAME_COLS,
                walkSheet.getHeight() / FRAME_ROWS);

        TextureRegion[] walkFrames = new TextureRegion[FRAME_COLS * FRAME_ROWS];
        int index = 0;

        for (int i = 0; i < FRAME_ROWS; i++) {
            for (int j = 0; j < FRAME_COLS; j++) {
                walkFrames[index++] = tmp[i][j];
            }

        }

        walkAnimation = new Animation<TextureRegion>(0.025f, walkFrames);

        stateTime = 0f;
    }

    @Override
    public void act(float delta) {

    }


    @Override
    public void draw(Batch batch, float parentAlpha) {
        stateTime += Gdx.graphics.getDeltaTime();
        currentFrame = walkAnimation.getKeyFrame(stateTime, true);
        batch.draw(currentFrame, getX(), getY());

    }

    public boolean isAlive() {
        return alive;
    }

    public void setAlive(boolean alive) {
        this.alive = alive;
    }
}
