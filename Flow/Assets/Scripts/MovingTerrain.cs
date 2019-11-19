using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    public GameObject playerToFollow;
    private float offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        offsetZ = - playerToFollow.transform.position.z + transform.position.z;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, playerToFollow.transform.position.z + offsetZ);
    }
}
