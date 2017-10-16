using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DControllerCustom : MonoBehaviour {


    private Animator animation_body;
	private Rigidbody2D player_body;

    [SerializeField]
	private float speed = 6f;

    private bool attack;
    private bool slide;
    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;
    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    void Awake(){
        facingRight = true;
		animation_body = GetComponent<Animator> ();
		player_body = GetComponent<Rigidbody2D> ();
	}


	void FixedUpdate()
    {

		float lh = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleMovement (lh);
        FlipCharacter(lh);
        HandleAttacks();
        HandleLayers();

        ResetValues();
	}

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            slide = true;
        }

    }

    private void HandleAttacks() {
        if (attack && isGrounded&& !this.animation_body.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            animation_body.SetTrigger("Attack");
            player_body.velocity = Vector2.zero;
        }
    }

	private void HandleMovement(float horizontal)
    {
        if (player_body.velocity.y < 0) {
            animation_body.SetBool("Land", true);
        }


        if (!this.animation_body.GetBool("IsRunning") && !this.animation_body.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            player_body.velocity = new Vector2(horizontal * speed, player_body.velocity.y);


        } else if (this.animation_body.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            player_body.velocity = new Vector2(0f, player_body.velocity.y);
        }


        if (isGrounded && jump) {
            isGrounded = false;
            player_body.AddForce(new Vector2(0f, jumpForce));
            animation_body.SetTrigger("Jump");
        }


        if (slide && !this.animation_body.GetCurrentAnimatorStateInfo(0).IsName("ninja_slide"))
        {
            animation_body.SetBool("IsRunning", true);
        }
        else if (!this.animation_body.GetCurrentAnimatorStateInfo(0).IsName("ninja_slide")) {
            animation_body.SetBool("IsRunning", false);
        }



        animation_body.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void FlipCharacter(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
            facingRight = !facingRight;            
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    private void ResetValues()
    {
        attack = false;
        slide = false;
        jump = false;
    }

    private bool IsGrounded() {
        if (player_body.velocity.y <= 0) {
            foreach (Transform point in groundPoints) {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++) {
                    if (colliders[i].gameObject != gameObject) {
                        animation_body.ResetTrigger("Jump");
                        animation_body.SetBool("Land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            animation_body.SetLayerWeight(1, 1);
        }
        else {
            animation_body.SetLayerWeight(1, 0);
        }
        
    }
}
