using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/death", 1);
            player.SetActive(false);
            //play Death Sound
            StartCoroutine(reset());
        }
    }

    private IEnumerator reset()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Waited 5 seconds");
        foreach (KeyValuePair<string, ServerLog> pair in OSCHandler.Instance.Servers)
        {
            pair.Value.server.Close();
        }
        foreach (KeyValuePair<string, ClientLog> pair in OSCHandler.Instance.Clients)
        {
            pair.Value.client.Close();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
