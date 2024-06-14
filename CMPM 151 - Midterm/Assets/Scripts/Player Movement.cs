using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityOSC;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private float horizontal;
    [SerializeField] private float speed = 3.45f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool setMusic = false;
    public static int score = 0;
    public static int life = 3;
    private int lms = -2;
    private Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();

    public TMP_Text m_MyText;
    public float targetTime = 1.0f;
    public static int timerCount = 999;
    public int jumpPitch;


    public bool lowTimer;


    void Start()
    {
        jumpPitch = 57;
        //m_MyText.text = "This is my text";
        //Initialize OSC Sound Here
        Application.runInBackground = true; //allows unity to update when not in focus

        //************* Instantiate the OSC Handler...



        OSCHandler.Instance.Init();
        OSCHandler.Instance.UpdateLogs();
        servers = OSCHandler.Instance.Servers;

        /*foreach (KeyValuePair<string, ServerLog> item in servers)
        {
            // If we have received at least one packet,
            // show the last received from the log in the Debug console
            Debug.Log("test1");
            if (item.Value.log.Count > 0)
            {
                //Debug.Log("test2");
                int lastPacketIndex = item.Value.packets.Count - 1;
                //get address and data packet
                //countText = item.Value.packets[lastPacketIndex].Address.ToString();
                string countText = item.Value.packets[lastPacketIndex].Data[0].ToString();
                Debug.Log(countText);
                if (countText == "2")
                {
                    OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 2);
                } else
                {
                    OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 1);
                }
                //countText.text += item.Value.packets[lastPacketIndex].Data[0].ToString();


            }
        }*/

        lowTimer = false;
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/lowTimer", 0);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            //Play a Jump SFX Here
            
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/jump", jumpPitch);
            
            
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchBGM();
        }



        targetTime -= Time.deltaTime;
        //Debug.Log(targetTime);
        //Debug.Log(Time.deltaTime);
        

        if (targetTime <= 0.0f)
        {
            timerCount--;
            if (Random.Range(0, 2) == 0)
            {
                jumpPitch += Random.Range(3,5);
            }
            else
            {
                jumpPitch -= Random.Range(2,4);
                if (jumpPitch < 57)
                {
                    jumpPitch = 57;
                }
            }
            
            targetTime = 1.0f;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/timer", timerCount);
        }
        m_MyText.text = " " + timerCount;

        if (targetTime < 60.0f && !lowTimer)
        {
            lowTimer = true;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/lowTimer", 1);
        }
    }

    private void FixedUpdate()
    {


        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);


        if (!setMusic)
        {
            
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 7);
            OSCHandler.Instance.UpdateLogs();

            //servers = OSCHandler.Instance.Servers;
            foreach (KeyValuePair<string, ServerLog> item in servers)
            {
                // If we have received at least one packet,
                // show the last received from the log in the Debug console
                if (item.Value.log.Count > 0)
                {
                    int lastPacketIndex = item.Value.packets.Count - 1;
                    if (lastPacketIndex != lms)
                    {
                        lms = lastPacketIndex;
                        //get address and data packet
                        //countText = item.Value.packets[lastPacketIndex].Address.ToString();
                        string countText = item.Value.packets[lastPacketIndex].Data[0].ToString();
                        //Debug.Log(countText);
                        if (countText == "2")
                        {
                            OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 2);
                            setMusic = true;

                        }
                        else
                        {
                            OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 1);
                            setMusic = true;

                        }
                        
                        //countText.text += item.Value.packets[lastPacketIndex].Data[0].ToString();

                    }


                }
            }
        }

         
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void SwitchBGM()
    {

        OSCHandler.Instance.UpdateLogs();
        servers = OSCHandler.Instance.Servers;

        foreach (KeyValuePair<string, ServerLog> item in servers)
        {
            // If we have received at least one packet,
            // show the last received from the log in the Debug console
            //Debug.Log("test1");
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 7);
            if (item.Value.log.Count > 0)
            {
                //Debug.Log("test2");
                int lastPacketIndex = item.Value.packets.Count - 1;
                //get address and data packet
                //countText = item.Value.packets[lastPacketIndex].Address.ToString();
                string countText = item.Value.packets[lastPacketIndex].Data[0].ToString();
                //Debug.Log(countText);
                if (countText == "2")
                {
                    OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 1);
                }
                else
                {
                    OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 2);
                }
                //countText.text += item.Value.packets[lastPacketIndex].Data[0].ToString();


            }
        }
    }




}
