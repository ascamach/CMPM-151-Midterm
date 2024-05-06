using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
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
            StartCoroutine(hitFlag());
        }
    }

    private IEnumerator hitFlag()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Waited 5 seconds");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
