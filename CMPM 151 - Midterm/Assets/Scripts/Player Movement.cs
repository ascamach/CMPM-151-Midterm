using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private float horizontal;
    [SerializeField] private float speed = 3.45f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool piano = false;
    

    void Start()
    {
        //Initialize OSC Sound Here
        Application.runInBackground = true; //allows unity to update when not in focus

        //************* Instantiate the OSC Handler...
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/ready", "ready");
        //*************
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            //Play a Jump SFX Here
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchBGM();
        }
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void SwitchBGM()
    {
        if (piano == false)
        {
            piano = true;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/switch", 1);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/ready", "ready");
        } else if (piano == true)
        {
            piano = false;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/switch", 0);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/ready", "ready");
        }
    }
}
