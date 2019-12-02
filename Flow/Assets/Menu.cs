using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public float timeCounter;
    public GameObject hips;
    private Quaternion baseTransform;

    public GameObject textPlayBlue;
    public GameObject textPlayOrange;
    public GameObject textQuitBlue;
    public GameObject textQuitOrange;

    private  float transformRotate;

    public float speedRotate;

    [Header("Hub")]
    public GameObject mainCanvas;
    public GameObject hubCanvas;
    public GameObject texts;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0.0f;
        Cursor.visible = false;
        hubCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transformRotate = transform.rotation.eulerAngles.y;

        timeCounter += Time.deltaTime;

        if(timeCounter > 7.8f)
        {   
            if (Input.GetAxis("Mouse X") != 0)
            {
                transform.RotateAround(hips.transform.position, Vector3.up, speedRotate*Input.GetAxis("Mouse X"));
            }
        }

        if (transformRotate > 84f && transformRotate < 245f && timeCounter > 7.8f)
        {
            textPlayBlue.SetActive(false);
            textPlayOrange.SetActive(true);

            textQuitBlue.SetActive(true);
            textQuitOrange.SetActive(false);

            if(Input.GetMouseButtonDown(0))
            {
                Application.Quit();
            }
        }
        else if (timeCounter > 7.8f)
        {
            textQuitBlue.SetActive(false);
            textQuitOrange.SetActive(true);

            textPlayBlue.SetActive(true);
            textPlayOrange.SetActive(false);

            if (Input.GetMouseButtonDown(0) && !hubCanvas.activeSelf)
            { 
                Debug.Log("load scene");
                //SceneManager.LoadSceneAsync("YouSayRun");
                showHub();
            }
        }
    }

    public void showHub()
    {
        Cursor.visible = true;
        texts.SetActive(false);
        mainCanvas.SetActive(false);
        hubCanvas.SetActive(true);
    }

    public void OnEndless()
    {
        Debug.Log("endless");
        SceneManager.LoadSceneAsync("Endless");
    }

    public void OnRythm()
    {
        Debug.Log("yousayrun");
        SceneManager.LoadSceneAsync("YouSayRun");
    }
}
