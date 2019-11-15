using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position - player.transform.forward * -5;
        //transform.position += Vector3.forward * player.GetComponent<PlayerController>().speed * Time.deltaTime;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        Destroy(other.gameObject);
    }
}
