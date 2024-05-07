using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer visual;
    private bool collected = false;

    private void Awake()
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
    }

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
            if (!collected)
            {
                collectCoin();
                Debug.Log("Coin Collected");
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/coin", 0);
            }
        }
    }

    private void collectCoin()
    {
        collected = true;
        //Play Coin Collected SFX Here
        visual.gameObject.SetActive(false);
        
    }
}
