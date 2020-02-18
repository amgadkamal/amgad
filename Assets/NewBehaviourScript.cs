using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{


    Rigidbody2D rb;
    float maxSpeed = .3f;
    float move = -1;
    Animator animator;
    int state;
    bool ishurt;
    int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = animator.GetInteger("CurrentState");
        ishurt = animator.GetBool("IsHurt");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(move * maxSpeed, 0);
        
    }



    IEnumerator delaytime()
    {
        yield return  new WaitForSecondsRealtime (150);

        
         

    }


    // Update is called once per frame
    void Update()

    {
        if (Input.GetMouseButtonDown(0))
        {
            var purple = GameObject.FindWithTag("powerup");
            if (state == 0 )
            {
                state = 1;

                animator.SetInteger("CurrentState", state);
                rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
                

            }  

            
            if (purple)
            {
                ishurt = false;
                animator.SetBool("IsHurt", ishurt);  
                rb.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
            }
        }
    }
 
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            state = 0;
            animator.SetInteger("CurrentState", state);
        }
        if (collision.gameObject.tag == "hazard")
        {
            ishurt = true;
            animator.SetBool("IsHurt", ishurt);
            
            StartCoroutine("delaytime");
            SceneManager.LoadScene("Lose");
            

        }
        

        if (collision.gameObject.tag == "goal")
        {
            SceneManager.LoadScene("Win");
            
        }
        
    }

    
    

    
}
