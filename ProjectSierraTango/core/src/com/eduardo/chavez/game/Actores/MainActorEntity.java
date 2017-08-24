package com.eduardo.chavez.game.Actores;

import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.Batch;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.physics.box2d.Body;
import com.badlogic.gdx.physics.box2d.BodyDef;
import com.badlogic.gdx.physics.box2d.Fixture;
import com.badlogic.gdx.physics.box2d.PolygonShape;
import com.badlogic.gdx.physics.box2d.World;
import com.badlogic.gdx.scenes.scene2d.Actor;

import static com.eduardo.chavez.game.Constants.PIXELS_IN_METER;
import static com.eduardo.chavez.game.Constants.IMPULSE_JUMP;
import static com.eduardo.chavez.game.Constants.PLAYER_SPEED;
/**
 * Created by eduardo3150 on 22/8/17.
 */

public class MainActorEntity extends Actor {
    private Texture mainActor;
    private World world;
    private Body body;
    private Fixture fixture;

    private boolean alive = true, jumping = false;

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

    public MainActorEntity(World world, Texture mainActor, Vector2 position) {
        this.world = world;
        this.mainActor = mainActor;

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
    }

    @Override
    public void act(float delta) {
        //Iniciar salto
        if (jumping) {
            body.applyForceToCenter(0, -IMPULSE_JUMP * 0.5f, true);
        }
        //Mover al jugador
    }

    @Override
    public void draw(Batch batch, float parentAlpha) {

        setPosition((body.getPosition().x - 0.5f) * PIXELS_IN_METER,
                (body.getPosition().y - 0.5f) * PIXELS_IN_METER);
        batch.draw(mainActor, getX(), getY(), getWidth(), getHeight());
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


    public void setMustJump(boolean mustJump) {
        this.mustJump = mustJump;
    }
}
