using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class dumbThing : MonoBehaviour
{

    public TMP_Text m_MyText;
    // Start is called before the first frame update
    void Start()
    {
        //Text sets your text to say this message
        m_MyText.text = " " + PlayerMovement.timerCount;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
