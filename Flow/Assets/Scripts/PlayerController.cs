using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
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
    private bool win;

    private float timeWallRide;
    private float timeJump;
    private float timeSlide;

    private Vector3 posToMove;

    public float div;

    private Animator anim;
    private BoxCollider bc;

    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;

        bc = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animator>();

        timeWallRide = 0f;
        wallRide = false;

        timeJump = 0f;
        jumping = false;

        timeSlide = 0f;
        sliding = false;

        moveLeft = false;
        moveRight = false;

        alive = true;
        win = false;
    }

    void resetCollider()
    {
        bc.center = new Vector3(0, 1, 0);
        bc.size = new Vector3(0.9f, 1.8f, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
                    resetCollider();
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
                    resetCollider();
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
                    resetCollider();
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

            if (!wallRide)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (!jumping && !wallRide && !sliding)
                    {
                        jumping = true;
                        timeJump = timeJUMP;
                        bc.center += transform.up * 1.5f;
                        bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                        anim.ResetTrigger("Jumping");
                        anim.SetTrigger("Jumping");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (!sliding && !wallRide && !jumping)
                    {
                        sliding = true;
                        timeSlide = timeSLIDE;
                        bc.center -= transform.up;
                        bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                        anim.ResetTrigger("Sliding");
                        anim.SetTrigger("Sliding");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (!wallRide && !moveRight && !moveLeft)
                    {
                        if (transform.position.x > -0.5)
                        {
                            moveLeft = true;

                            if (moveRight)
                            {
                                moveRight = false;
                                posToMove = posToMove - Vector3.right;
                            }
                            else
                            {
                                posToMove = transform.position - Vector3.right;
                            }
                        }
                        else
                        {
                            if (!jumping && !sliding)
                            {
                                wallRide = true;
                                timeWallRide = timeWALLRIDE;
                                bc.center += transform.up * 0.5f;
                                bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                                anim.ResetTrigger("WallRunningL");
                                anim.SetTrigger("WallRunningL");
                            }
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (!wallRide && !moveRight && !moveLeft)
                    {
                        if (transform.position.x < 0.5)
                        {
                            moveRight = true;

                            if (moveLeft)
                            {
                                moveLeft = false;
                                posToMove = posToMove + Vector3.right;
                            }
                            else
                            {
                                posToMove = transform.position + Vector3.right;
                            }
                        }
                        else
                        {
                            if (!jumping && !sliding)
                            {
                                wallRide = true;
                                timeWallRide = timeWALLRIDE;
                                bc.center += transform.up * 0.5f;
                                bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                                anim.ResetTrigger("WallRunningR");
                                anim.SetTrigger("WallRunningR");
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
            anim.SetTrigger("Death");
            transform.position = lastPos;
            Debug.Log("dead");
        }
    }
}
