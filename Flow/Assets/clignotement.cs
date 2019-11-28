using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clignotement : MonoBehaviour
{
    private bool on = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        InvokeRepeating("changeActive", 2.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeActive()
    {
        if (on)
        {
            //display text
            GetComponent<TMPro.TextMeshProUGUI>().text = "Press Enter to restart";
        }
        else
        {
            //remove text
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
        on = !on;
    }
}
