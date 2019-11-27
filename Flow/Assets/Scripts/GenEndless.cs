using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GenEndless : MonoBehaviour
{
    [Tooltip("0 etant l obs vide")]
    public int nbObs;
    public float TriggerDistance;

    public Vector3 startPoint;
    private Vector3 currentPoint;

    public GameObject joueur;
    private Vector3 posJoueur;

    private List<GameObject> obstacles;

    public float easyToMedium = 200.0f;
    public float mediumToHard = 400.0f;
    public float hardToUltra = 600.0f;
    private float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = startPoint;
        targetTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        posJoueur = joueur.transform.position;
        targetTime += Time.deltaTime;

        Vector3 distance = currentPoint - joueur.transform.position;
        if (distance.magnitude < TriggerDistance)
        {
            Debug.Log("CreateObs");
            createObs();
        }
    }

    private void createObs()
    {
        //generation de 2 obstacles : 1 vide et un obs + setup de currentPoint
        currentPoint += new Vector3(0, 0, 5f);
        Instantiate(Resources.Load<GameObject>("Prefabs/Obs0"), currentPoint, Quaternion.identity);

        currentPoint += new Vector3(0, 0, 5f);
        int index = Random.Range(1, 23);

        int easy = Random.Range(1, 5);
        int medium = Random.Range(6, 10);
        int hard = Random.Range(11, 14);
        int ultra = Random.Range(15, 23);


        if (targetTime >= easyToMedium)
        {
            if (targetTime >= mediumToHard)
            {
                if (targetTime >= hardToUltra)
                {
                    index = ultra;
                }

                else
                {
                    index = hard;

                }
            }

            else
            {
                index = medium;
            }
        }

        else
        {
            index = easy;
        }

        Instantiate(Resources.Load<GameObject>("Prefabs/Obs" + index), currentPoint, Quaternion.identity);
    }
}