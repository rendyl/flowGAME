using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public bool deathOnHit;
    public GameObject goToinstantiate;

    public ParticleSystem ps1;
    public ParticleSystem ps2;

    public float speedIncrement;
    public float speedMultiplier;
    public float baseSpeed;
    public float speed;

    public float baseTimeJUMP;
    public float baseTimeSLIDE;
    public float bastTimeWALLRIDE;

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

    public float baseDiv;
    public float div;

    public ParticleSystem psFoot1;
    public ParticleSystem psFoot2;
    public ParticleSystem psBack;

    private Animator anim;

    private CapsuleCollider cc;
    // private BoxCollider bc;

    private Vector3 lastPos;

    public ComboManager comboManager;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;

        // bc = GetComponent<BoxCollider>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();

        updateSpeedMultiplier(speedMultiplier);

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

    void updateSpeedMultiplier(float value)
    {
        speedMultiplier = value;
        anim.SetFloat("speedMultiplier", speedMultiplier);
        speed = baseSpeed * speedMultiplier;
        timeJUMP = baseTimeJUMP / speedMultiplier;
        timeSLIDE = baseTimeSLIDE / speedMultiplier;
        timeWALLRIDE = bastTimeWALLRIDE / speedMultiplier;
        div = baseDiv / speedMultiplier;
    }

    void resetCollider()
    {
        //bc.center = new Vector3(0, 1, 0);
        //bc.size = new Vector3(0.9f, 1.8f, 0.9f);

        cc.center = new Vector3(0, 1, 0);
        cc.height = 1.5f;
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
                //if (Input.GetKeyDown(KeyCode.Z))
                if(Input.GetButtonDown("Jump"))
                {
                    if (!jumping && !wallRide && !sliding)
                    {
                        jumping = true;
                        timeJump = timeJUMP;

                        cc.center += transform.up * 1.5f;
                        cc.height = 0.75f;

                        //bc.center += transform.up * 1.5f;
                        //bc.size = new Vector3(0.9f, 0.9f, 0.9f);

                        anim.ResetTrigger("Jumping");
                        anim.SetTrigger("Jumping");
                    }
                }

                //if (Input.GetKeyDown(KeyCode.S))
                if(Input.GetButtonDown("Slide"))
                {
                    if (!sliding && !wallRide && !jumping)
                    {
                        sliding = true;
                        timeSlide = timeSLIDE;

                        cc.center -= transform.up * 0.5f;
                        cc.height = 0.75f;

                        //bc.center -= transform.up;
                        //bc.size = new Vector3(0.9f, 0.9f, 0.9f);


                        anim.ResetTrigger("Sliding");
                        anim.SetTrigger("Sliding");
                    }
                }

                //if (Input.GetKeyDown(KeyCode.Q))
                if(Input.GetButtonDown("Gauche"))
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

                                cc.center += transform.up * 0.5f;
                                cc.height = 0.75f;

                                //bc.center += transform.up * 0.5f;
                                //bc.size = new Vector3(0.9f, 0.9f, 0.9f);

                                anim.ResetTrigger("WallRunningL");
                                anim.SetTrigger("WallRunningL");
                            }
                        }
                    }
                }

                //if (Input.GetKeyDown(KeyCode.D))
                if(Input.GetButtonDown("Droite"))
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

                                cc.center += transform.up * 0.5f;
                                cc.height = 0.75f;

                                //bc.center += transform.up * 0.5f;
                                //bc.size = new Vector3(0.9f, 0.9f, 0.9f);

                                anim.ResetTrigger("WallRunningR");
                                anim.SetTrigger("WallRunningR");
                            }
                        }
                    }
                }
            }
            transform.position += Vector3.forward * speed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Instantiate(goToinstantiate, transform.position - new Vector3(0, 0.5f, 2), Quaternion.identity);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }    
    }

    public void dead()
    {
        alive = false;
        anim.SetTrigger("Death");
        transform.position = lastPos;
        Debug.Log("dead");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && deathOnHit)
        {
            Debug.Log(this.gameObject.tag);
            alive = false;
            anim.SetTrigger("Death");
            // FindObjectOfType<ScoreManager>().scoreIncreasing = false;
            transform.position = lastPos;
            Debug.Log("dead");
        }

        else if (other.gameObject.CompareTag("Obstacle") && !deathOnHit)
        {
            Debug.Log(this.gameObject.tag);
            Debug.Log("dead");
            comboManager.hitObstacles();
            other.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Red");
        }


        else if (other.gameObject.gameObject.CompareTag("Bonus"))
        {
            // ps1.Play();
            // ps2.Play();

            Destroy(other.gameObject);
            updateSpeedMultiplier(speedMultiplier + speedIncrement);
            if (speed >= 12f)
            {
                if(!psFoot1.isPlaying)
                {
                    psFoot1.Play();
                    psFoot2.Play();
                }
            }
            if (speed >= 14f)
            {
                SerializedObject so = new SerializedObject(psBack);
                if (!psBack.isPlaying)
                {
                    so.FindProperty("InitialModule.startColor.minColor").colorValue = new Color(0.2688679f, 0.6874771f, 1f, 1f);
                    so.FindProperty("InitialModule.startColor.maxColor").colorValue = new Color(0.2688679f, 0.6874771f, 1f, 1f);
                    so.ApplyModifiedProperties();
                    psBack.Play();
                }
                if(speed >= 16f)
                {
                    so.FindProperty("InitialModule.startColor.minColor").colorValue = new Color(0.2688679f, 0.6874771f, 1f, 1f);
                    so.FindProperty("InitialModule.startColor.maxColor").colorValue = new Color(1f, 0.3615f, 0f, 1f);
                    so.ApplyModifiedProperties();
                    // ps2.Play();
                    // ps2.loop = true;
                }
            }
            Debug.Log("Power up");
        }        else
        {
            comboManager.succees();
        }
    }
}