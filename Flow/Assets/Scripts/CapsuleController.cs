using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float speed;

    public float timeJUMP;
    public float timeSLIDE;
    public float timeWALLRIDE;

    private bool wallRide;
    private bool jumping;
    private bool sliding;

    private float timeWallRide;
    private float timeJump;
    private float timeSlide;

    // Start is called before the first frame update
    void Start()
    {
        timeWallRide = 0f;
        wallRide = false;

        timeJump = 0f;
        jumping = false;

        timeSlide = 0f;
        sliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Wall Ride
        if (timeWallRide > 0f)
        {
            timeWallRide -= Time.deltaTime;

        }
        else
        {
            if (wallRide)
            {
                wallRide = false;
                transform.position -= transform.up * 0.5f;
                transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
        }

        // Jump
        if (timeJump > 0f)
        {
            timeJump -= Time.deltaTime;

        }
        else
        {
            if (jumping)
            {
                jumping = false;
                transform.position -= transform.up * 1.5f;
                transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
        }

        // Slide
        if (timeSlide > 0f)
        {
            timeSlide -= Time.deltaTime;

        }
        else
        {
            if (sliding)
            {
                sliding = false;
                transform.position += transform.up * 0.5f;
                transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!jumping && !wallRide)
            {
                jumping = true;
                timeJump = timeJUMP;
                transform.position += transform.up * 1.5f;
                transform.localScale = new Vector3(0.9f, 0.45f, 0.9f);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!sliding && !wallRide)
            {
                sliding = true;
                timeSlide = timeSLIDE;
                transform.position -= transform.up * 0.5f;
                transform.localScale = new Vector3(0.9f, 0.45f, 0.9f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!wallRide)
            {
                if (transform.position.x > -0.5) transform.position -= Vector3.right;
                else
                {
                    wallRide = true;
                    timeWallRide = timeWALLRIDE;
                    transform.localScale = new Vector3(0.9f, 0.45f, 0.9f);
                    transform.position += transform.up * 0.5f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!wallRide)
            {
                if (transform.position.x < 0.5) transform.position += Vector3.right;
                else
                {
                    if (!wallRide)
                    {
                        wallRide = true;
                        timeWallRide = timeWALLRIDE;
                        transform.localScale = new Vector3(transform.localScale.x, 0.5f * transform.localScale.y, transform.localScale.z);
                        transform.position += transform.up * 0.5f;
                    }
                }
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
