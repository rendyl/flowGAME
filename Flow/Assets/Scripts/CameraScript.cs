using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject playerToTarget;

    private Vector3 offset;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        offset = playerToTarget.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerToTarget.transform.position - offset, speed * Time.deltaTime);
    }
}