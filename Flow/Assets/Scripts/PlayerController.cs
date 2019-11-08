using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    private Animator anim;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        

        bc = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animator>();
        AnimationClip animJump = GetAnimationClip(anim, "Jumping");

        timeWallRide = 0f;
        wallRide = false;

        timeJump = 0f;
        jumping = false;

        timeSlide = 0f;
        sliding = false;
    }

    static public AnimationClip GetAnimationClip(Animator anim, string name)
    {
        if (anim) return null; // no animator

        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null; // no clip by that name
    }


    void resetCollider()
    {
        bc.center = new Vector3(0, 1, 0);
        bc.size = new Vector3(0.9f, 1.8f, 0.9f);
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
            if(jumping)
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!jumping && !wallRide)
            {
                jumping = true;
                timeJump = timeJUMP;
                bc.center += transform.up * 1.5f;
                bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                anim.SetTrigger("Jumping");
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!sliding && !wallRide)
            {
                sliding = true;
                timeSlide = timeSLIDE;
                bc.center -= transform.up;
                bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                anim.SetTrigger("Sliding");
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
                    bc.center += transform.up * 0.5f;
                    bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                    anim.SetTrigger("WallRunningL");
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
                    wallRide = true;
                    timeWallRide = timeWALLRIDE;
                    bc.center += transform.up * 0.5f;
                    bc.size = new Vector3(0.9f, 0.9f, 0.9f);
                    anim.SetTrigger("WallRunningR");
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
