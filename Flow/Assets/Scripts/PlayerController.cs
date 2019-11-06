using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool wallRunning;

    // Start is called before the first frame update
    void Start()
    {
        wallRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (transform.position.x > -0.5) transform.position -= Vector3.right;
            else
            {
                wallRunning = true;
                transform.localScale = new Vector3(transform.localScale.x, 0.5f * transform.localScale.y, transform.localScale.z);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 0.5) transform.position += Vector3.right;
            else
            {
                wallRunning = true;
                transform.localScale = new Vector3(transform.localScale.x, 0.5f * transform.localScale.y, transform.localScale.z);
            }
        }
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.gameObject.CompareTag("Obstacle"))
        {
            speed = 0;
            Debug.Log("dead");
            Application.Quit();
        }
    }
}
