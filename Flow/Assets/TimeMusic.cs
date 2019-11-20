using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<AudioSource>().time);
    }
}

// 24
// 34
// 47
// 49
//
//
//
//
//