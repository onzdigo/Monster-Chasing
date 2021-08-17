using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movimentX;
    
    [SerializeField]
    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string WALK_ANIMATON = "Walk"; 

    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private string ENEMY_TAG = "Enemy";

    private void Awake()
    {

        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame
    void Start ()
    {

    }
    // update is called once per frame
    void Update() 
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        
    }

    private void FixedUpdate(){
        PlayerJump();
    }

    void PlayerMoveKeyboard() {

        movimentX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movimentX, 0f, 0f) * moveForce * Time.deltaTime;
    }

    void AnimatePlayer(){

            // we are going to the rigth side
        if(movimentX > 0){
            anim.SetBool(WALK_ANIMATON, true); 
            sr.flipX = false;
        }
            //we are going to the left side
        else if(movimentX < 0){
            anim.SetBool(WALK_ANIMATON, true);
            sr.flipX = true;
        }
        else {
            anim.SetBool(WALK_ANIMATON, false);
        }
    }

    void PlayerJump(){
        if (Input.GetButton("Jump") && isGrounded){
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = true;
            Debug.Log(" wee landed on the Ground");
        }

        if ( collision.gameObject.CompareTag(ENEMY_TAG)){
            Destroy(gameObject);
        }

    }   

     private void OnTriggerEnter2D(Collider2D collision){
        
        if (collision.CompareTag(ENEMY_TAG)){
            Destroy(gameObject);
        }
    }

    

}
