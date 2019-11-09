using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CapsuleController : MonoBehaviour
{
    public float speed;

    public float timeJUMP;
    public float timeSLIDE;
    public float timeWALLRIDE;

    private bool wallRide;
    private bool jumping;
    private bool sliding;
    private bool moveLeft;
    private bool moveRight;

    private bool alive;

    private float timeWallRide;
    private float timeJump;
    private float timeSlide;

    private Vector3 posToMove;

    public float div;

    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;

        timeWallRide = 0f;
        wallRide = false;

        timeJump = 0f;
        jumping = false;

        timeSlide = 0f;
        sliding = false;

        moveLeft = false;
        moveRight = false;

        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        lastPos = transform.position;
        if (alive)
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

            if (moveLeft || moveRight)
            {
                transform.position += (Convert.ToInt32(moveLeft) * new Vector3(-1 / div, 0, 0)) + (Convert.ToInt32(moveRight) * new Vector3(1 / div, 0, 0));
                if (moveRight && transform.position.x > posToMove.x)
                {
                    transform.position = new Vector3(posToMove.x, transform.position.y, transform.position.z);
                    moveRight = false;
                }
                if (moveLeft && transform.position.x < posToMove.x)
                {
                    transform.position = new Vector3(posToMove.x, transform.position.y, transform.position.z);
                    moveLeft = false;
                }
            }
            else if (!wallRide)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (!jumping && !wallRide && !sliding)
                    {
                        jumping = true;
                        timeJump = timeJUMP;
                        transform.position += transform.up * 1.5f;
                        transform.localScale = new Vector3(0.9f, 0.45f, 0.9f);
                    }
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (!sliding && !wallRide && !jumping)
                    {
                        sliding = true;
                        timeSlide = timeSLIDE;
                        transform.position -= transform.up * 0.5f;
                        transform.localScale = new Vector3(0.9f, 0.45f, 0.9f);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (!wallRide && !moveRight && !moveLeft)
                    {
                        if (transform.position.x > -0.5)
                        {
                            moveLeft = true;
                            posToMove = transform.position - Vector3.right;
                        }
                        else
                        {
                            if (!jumping && !sliding)
                            {
                                wallRide = true;
                                timeWallRide = timeWALLRIDE;
                                transform.localScale = new Vector3(0.9f, 0.45f, 0.9f);
                                transform.position += transform.up * 0.5f;
                            }
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (!wallRide && !moveRight && !moveLeft)
                    {
                        if (transform.position.x < 0.5)
                        {
                            moveRight = true;
                            posToMove = transform.position + Vector3.right;
                        }
                        else
                        {
                            if (!jumping && !sliding)
                            {
                                wallRide = true;
                                timeWallRide = timeWALLRIDE;
                                transform.localScale = new Vector3(transform.localScale.x, 0.5f * transform.localScale.y, transform.localScale.z);
                                transform.position += transform.up * 0.5f;
                            }
                        }
                    }
                }
            }
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.gameObject.CompareTag("Obstacle"))
        {
            alive = false;
            transform.position = lastPos;
            Debug.Log("dead");
        }
    }
}
