using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
   
   void Awake()
   {
    //grab reference to rigidbody and animator from project
       body = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
   }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
         body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
       //flip charchter
        if(horizontalinput>0.01f)
        transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        else if(horizontalinput<-0.01f)
        transform.localScale = new Vector3(-0.8f,0.8f,0.8f);

        if (Input.GetKey(KeyCode.Space)&&grounded)
            Jump();

            //set animator parameters
            anim.SetBool("run",horizontalinput !=0);
            anim.SetBool("ground", grounded);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
    public bool canAttack()
    {
        float horizontalinput = Input.GetAxis("Horizontal");

        return horizontalinput == 0 && grounded;
    }
}
