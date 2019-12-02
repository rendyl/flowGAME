using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingMusic : MonoBehaviour
{
    bool instantiated = false;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<AudioSource>().time > 204 && !instantiated && player.alive)
        {
            instantiated = true;
            GameObject lp = Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            lp.GetComponent<LoopingMusic>().player = player;
        }

        if(GetComponent<AudioSource>().time > 230)
        {
            Destroy(gameObject);
        }
    }
}
