using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMusic : MonoBehaviour
{
    public Transform charToFollow;
    bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(charToFollow.position.z);
        if(charToFollow.position.z > -90 && !launched)
        {
            launched = true;
            GetComponent<AudioSource>().Play();
        }
    }
}