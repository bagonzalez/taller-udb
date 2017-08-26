package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Animation;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.math.Rectangle;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.physics.box2d.Body;
import com.badlogic.gdx.physics.box2d.BodyDef;
import com.badlogic.gdx.physics.box2d.Fixture;
import com.badlogic.gdx.physics.box2d.PolygonShape;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Actor;

import static com.eduardo.chavez.game.Constants.IMPULSE_JUMP;
import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;
import static com.eduardo.chavez.game.Constants.PLAYER_SPEED;

/**
 * Created by eduardo3150 on 21/8/17.
 */

public class MainActor extends Actor {
    //Constants rows and colums
    private static final int FRAME_COLS = 6, FRAME_ROWS = 5;
    Texture walkSheet;
    Animation<TextureRegion> walkAnimation;
    TextureRegion currentFrame;


    private World world;
    private Body body;
    private Fixture fixture;

    private boolean alive = true, jumping = false;

    //Track elapsed time for animation
    float stateTime;

    private boolean moveRight = false, moveLeft = false, moveUp = false;

    public boolean isKeepPlaying() {
        return keepPlaying;
    }

    public void setKeepPlaying(boolean keepPlaying) {
        this.keepPlaying = keepPlaying;
    }

    private boolean keepPlaying = true;

    public boolean isMustJump() {
        return mustJump;
    }

    private boolean mustJump;

    public float getCurrentSpeed() {
        return currentSpeed;
    }

    public void setCurrentSpeed(float currentSpeed) {
        this.currentSpeed = currentSpeed;
    }

    private float currentSpeed;

    public boolean isAlive() {
        return alive;
    }

    public void setAlive(boolean alive) {
        this.alive = alive;
    }


    public boolean isJumping() {
        return jumping;
    }

    public void setJumping(boolean jumping) {
        this.jumping = jumping;
    }

    public MainActor(World world, Texture walkSheet, Vector2 position) {
        this.walkSheet = walkSheet;
        this.world = world;
        this.walkSheet = walkSheet;

        BodyDef def = new BodyDef();
        def.position.set(position);
        def.type = BodyDef.BodyType.DynamicBody;
        body = world.createBody(def);

        PolygonShape shape = new PolygonShape();
        shape.setAsBox(0.5f, 0.5f);
        fixture = body.createFixture(shape, 3);
        fixture.setUserData("player");
        shape.dispose();

        setSize(PIXELS_IN_METER, PIXELS_IN_METER);

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

        if (jumping) {
            body.applyForceToCenter(0, -IMPULSE_JUMP * 0.5f, true);
        }
    }


    @Override
    public void draw(Batch batch, float parentAlpha) {
        stateTime += Gdx.graphics.getDeltaTime();
        currentFrame = walkAnimation.getKeyFrame(stateTime, keepPlaying);
        setPosition((body.getPosition().x - 0.5f) * PIXELS_IN_METER,
                (body.getPosition().y - 0.5f) * PIXELS_IN_METER);

        batch.draw(currentFrame, getX(), getY(), getWidth(), getHeight());

    }


    public void detach() {
        body.destroyFixture(fixture);
        world.destroyBody(body);
    }

    public void jump() {

        if (!jumping && alive) {
            jumping = true;
            Vector2 position = body.getPosition();
            body.applyLinearImpulse(0, IMPULSE_JUMP, position.x, position.y, true);
        }
    }

    public void moveToRight() {
        float velocidadY = body.getLinearVelocity().y;
        setCurrentSpeed(PLAYER_SPEED);
        body.setLinearVelocity(PLAYER_SPEED, velocidadY);
    }

    public void moveToLeft() {
        float velocidadY = body.getLinearVelocity().y;
        setCurrentSpeed(-PLAYER_SPEED);
        body.setLinearVelocity(-PLAYER_SPEED, velocidadY);
    }


    public void standBy() {
        float velocidadY = body.getLinearVelocity().y;
        setCurrentSpeed(0);
        body.setLinearVelocity(0, velocidadY);
    }

}
