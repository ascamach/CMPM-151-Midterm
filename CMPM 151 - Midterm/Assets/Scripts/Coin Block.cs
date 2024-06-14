using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private SpriteRenderer rend;
    private int randomNum;
    private bool active;
    [SerializeField] private Color newColor;

    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && active)
        {
            randomNum = Random.Range(1, 101);
            if(randomNum >= 1 && randomNum <= 25)
            {
                //Debug.Log("Life Get");
                PlayerMovement.life++;
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/life", PlayerMovement.life);
                rend.color = newColor;
                active = false;
            }
            else
            {
                PlayerMovement.score++;
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/coin", PlayerMovement.score);
                //Debug.Log("Coin Get");
            }



        }
    }
}
