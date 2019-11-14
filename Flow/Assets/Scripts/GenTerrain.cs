using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTerrain : MonoBehaviour
{
    [Tooltip("0 etant l obs vide")]
    public int nbObs;
    public float TriggerDistance;

    public Vector3 startPoint;
    private Vector3 currentPoint;

    public GameObject joueur;
    private Vector3 posJoueur;

    private List<GameObject> obstacles;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = Resources.Load<GameObject>("Prefabs/Obs1");
        //if (go == null)
        //{
        //    Debug.Log("Defaite");
        //}
        //else
        //{
        //    Debug.Log("Victoire");
        //    Instantiate(go, currentPoint, Quaternion.identity);
        //}
        //obstacles.Add(go);
        //obstacles.Add(go);
        //for (int i = 0; i <= nbObs; i++)
        //{
        //    string path = "Prefabs/Obs";
        //    Debug.Log(path);
        //    obstacles.Add(Resources.Load<GameObject>(path));
        //}
        currentPoint = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        posJoueur = joueur.transform.position;
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
        int index = Random.Range(1, nbObs+1);
        Instantiate(Resources.Load<GameObject>("Prefabs/Obs"+index), currentPoint, Quaternion.identity);
    }
}
