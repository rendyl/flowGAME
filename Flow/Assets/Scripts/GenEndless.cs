using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GenEndless : MonoBehaviour
{
    public float TriggerDistance;

    public Vector3 startPoint;
    private Vector3 currentPoint;

    public GameObject joueur;
    private Vector3 posJoueur;

    int lastIndex = -1;

    public float easyToMedium = 20.0f;
    public float mediumToHard = 20.0f;
    public float hardToUltra = 20.0f;
    private float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = startPoint;
        targetTime = 0;
        mediumToHard += easyToMedium;
        hardToUltra += mediumToHard;
    }

    // Update is called once per frame
    void Update()
    {
        posJoueur = joueur.transform.position;
        targetTime += Time.deltaTime;

        Vector3 distance = currentPoint - joueur.transform.position;
        if (distance.magnitude < TriggerDistance)
        {
            //Debug.Log("CreateObs");
            createObs();
        }
    }

    private void createObs()
    {
        //generation de 2 obstacles : 1 vide et un obs + setup de currentPoint
        currentPoint += new Vector3(0, 0, 5f);
        //Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/Obs0"), currentPoint, Quaternion.identity),6);

        currentPoint += new Vector3(0, 0, 5f);
        int index = Random.Range(1, 23);
        // tirage du Obs aléatoirement
        int proba = Random.Range(1, 4);
        // tirage de la probabilite 1 chance sur 3

        int easy = Random.Range(1, 5);
        int medium = Random.Range(6, 10);
        int hard = Random.Range(11, 14);
        int ultra = Random.Range(15, 23);

        while ((lastIndex == 18 && ultra == 19) || (lastIndex == 19 && ultra == 18))
        {
            ultra = Random.Range(15, 23);
        }

        if (targetTime >= easyToMedium)
        {
            if (targetTime >= mediumToHard)
            {
                if (targetTime >= hardToUltra)
                {
                    if (proba == 1)
                    {
                        index = hard;
                    }
                    else
                    { 
                        index = ultra;
                    }
                }
                else
                {
                    if (proba == 1)
                    {
                        index = medium;
                    }
                    else
                    {
                        index = hard;
                    }
                }
            }
            else
            {
                if (proba == 1)
                {
                    index = easy;
                }
                else
                {
                    index = medium;
                }
            }
        }
        else
        {
            index = easy;
        }

        lastIndex = index;

        Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/Obs" + index), currentPoint, Quaternion.identity), 6);
    }
}