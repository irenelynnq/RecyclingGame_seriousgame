using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    public Animator animator;

    CircleCollider2D trashCollector;
    private float collectorOffset;

    public float speed;
    public float jump_power;
    public float slide_scale;
    private bool is_grounded = true;

    public enum State
    {
        Idle,
        Running
    }

    State state = State.Idle;
    public void ChangeState(State state) => this.state = state;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        trashCollector = GetComponentInChildren<CircleCollider2D>();
        collectorOffset = trashCollector.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //임시 start/pause key
            if (state == State.Idle)
            {
                ChangeState(State.Running);
                animator.SetBool("run_bool", true);
            }
            /*
            else if (state == State.Running)
            {
                state = State.Idle;
                animator.SetBool("run_bool", false);
            }
            */
        }

        if(state == State.Running)
        {
            transform.position += Vector3.right * speed;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(is_grounded == true)
                {
                    //공중 점프 막으려면 이 안으로 이동
                }
                rigidbody2D.velocity = Vector3.up * jump_power;
                is_grounded = false;
                animator.SetBool("jump_bool", true);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                //stop slide
                trashCollector.offset = new Vector2(0, collectorOffset);
                //animator.SetBool("slide_bool", false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //slide
                trashCollector.offset = new Vector2(0, collectorOffset - slide_scale);
                //animator.SetBool("slide_bool", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            is_grounded = true;
            animator.SetBool("jump_bool", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            is_grounded = false;
        }
    }


}
