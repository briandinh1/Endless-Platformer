using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameManager gameManager;
    private float baseSpeed;
    public float moveSpeed;
    public float speedMultiplier; // game will get faster and faster over time
    public float checkpointDistance; 
    private float nextCheckpoint; // when to increase speed multiplier
    private float baseCheckpoint;
    public float jumpForce;
    public bool grounded;
    private bool jumping;
    private bool canDoubleJump;
    public float jumpTime; // how long possible to stay in the air
    private float currentJumpTime; // counts down the jump time 
    private Rigidbody2D myRigidBody;
    public LayerMask whatIsGround;
    private Animator myAnimator;
    public Transform groundChecker;
    public float groundCheckerRadius;
    public AudioSource jumpSound;
    public AudioSource deathSound;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        currentJumpTime = jumpTime;
        baseSpeed = moveSpeed;
        baseCheckpoint = nextCheckpoint = checkpointDistance;
        canDoubleJump = true;
	}
	
	// Update is called once per frame 
	void Update () {
        grounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, whatIsGround);

        if (transform.position.x > nextCheckpoint) {
            nextCheckpoint += checkpointDistance;
            // increase checkpoints also because player reaches them faster
            checkpointDistance *= speedMultiplier; 
            moveSpeed *= speedMultiplier ;
        }
            
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        // space key or mouse click (touch screen on mobile)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            if (grounded) {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumping = true;
                jumpSound.Play();
            }

            if (!grounded && canDoubleJump) {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                currentJumpTime = jumpTime;
                canDoubleJump = false;
                jumping = true;
                jumpSound.Play();
            }     
        }

        // get extra airtime if button is held down
        // only get extra air if the jump button was pressed
        if (jumping && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))) {
            if (currentJumpTime > 0)  {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                currentJumpTime -= Time.deltaTime;
            }
        }

        // prevent players from repeatedly pressing or holding button down to fly
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) {
            currentJumpTime = 0;
            jumping = false;
        }

        if (grounded) {
            currentJumpTime = jumpTime; // reset current jump time once grounded
            canDoubleJump = true;
        }
        

        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill Box") {
            gameManager.RestartGame();
            moveSpeed = baseSpeed;
            nextCheckpoint = checkpointDistance = baseCheckpoint;
            deathSound.Play();
        }
    }
}
