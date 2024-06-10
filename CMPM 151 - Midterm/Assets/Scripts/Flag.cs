using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();

    //[SerializeField] private bool piano = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Debug.Log("Waiting for 5 seconds...");
            //Play a victory theme here
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 0);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/win", 1);
            StartCoroutine(hitFlag());
        }
    }

    private IEnumerator hitFlag()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Waited 5 seconds");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/win", 0);

        OSCHandler.Instance.UpdateLogs();
        servers = OSCHandler.Instance.Servers;

        foreach (KeyValuePair<string, ServerLog> item in servers)
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
                }
                else
                {
                    OSCHandler.Instance.SendMessageToClient("pd", "/unity/music", 1);
                }
                //countText.text += item.Value.packets[lastPacketIndex].Data[0].ToString();


            }
        }
        foreach (KeyValuePair<string, ServerLog> pair in OSCHandler.Instance.Servers)
        {
            pair.Value.server.Close();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerMovement.score = 0;
        PlayerMovement.life = 3;

    }

}
