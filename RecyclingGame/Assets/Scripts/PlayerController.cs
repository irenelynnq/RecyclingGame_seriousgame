using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State
{
    Idle,
    Running
}
public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    public Animator animator;

    CircleCollider2D trashCollectorArea;
    private float collectorOffset;
    public GameObject tc;
    public TrashCollector trashCollector;
    public GameObject runUIController;
    RunUI runUI;

    public float speed;
    public float jump_power;
    public float slide_scale;
    public int jump_count = 1;


    

    public State state = State.Idle;
    public void ChangeState(State state) => this.state = state;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        trashCollectorArea = GetComponentInChildren<CircleCollider2D>();
        collectorOffset = trashCollectorArea.offset.y;
        trashCollector = tc.GetComponent<TrashCollector>();
        runUI = runUIController.GetComponent<RunUI>();
        trashCollector.runUI = runUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && SceneManager.GetActiveScene().name == "RunScene")
        {
            //임시 start/pause key
            if (state == State.Idle)
            {
                ChangeState(State.Running);
                animator.SetBool("run_bool", true);
            }
        }

        if (state == State.Running)
        {
            transform.position += Vector3.right * speed;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(jump_count > 0)
                {
                    //공중 점프 막으려면 이 안으로 이동
                    //rigidbody2D.velocity = Vector3.up * jump_power;
                    rigidbody2D.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
                    jump_count -= 1;
                    animator.SetBool("jump_bool", true);
                }
                
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                //stop slide
                trashCollectorArea.offset = new Vector2(0, collectorOffset);
                animator.SetBool("slide_bool", false);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //slide
                trashCollectorArea.offset = new Vector2(0, collectorOffset - slide_scale);
                animator.SetBool("slide_bool", true);
            }
            runUI.playerPosition = transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jump_count = 1;
            animator.SetBool("jump_bool", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            
        }
    }

    public void Flicker()
    {
        StartCoroutine("FlickerEffect");
    }

    IEnumerator FlickerEffect()
    {
        int countTime = 0;

        while (countTime < 7)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            //wait update frame
            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        yield return null;
    }

    

}
