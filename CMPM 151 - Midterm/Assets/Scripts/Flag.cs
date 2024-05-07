using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool piano = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //Get the flag to restart the original state of music. I don't know a proper way to do this in unity.
        {
            if (piano == false)
            {
                piano = true;
            }
            else if (piano == true)
            {
                piano = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Debug.Log("Waiting for 5 seconds...");
            //Play a victory theme here
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/piano", 0);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/bgm", 0);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/win", 1);
            StartCoroutine(hitFlag());
        }
    }

    private IEnumerator hitFlag()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Waited 5 seconds");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/win", 0);
        if (piano == false)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/bgm", 1);
        }
        else if (piano == true)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/piano", 1);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
