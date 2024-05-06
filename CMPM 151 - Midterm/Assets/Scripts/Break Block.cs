using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject block;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            //Play a Breaking Block SFX Here
            Debug.Log("Breaking Block");
            block.SetActive(false);

        }
    }
}
