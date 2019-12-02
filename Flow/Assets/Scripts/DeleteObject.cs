using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        Destroy(col.gameObject);   
    }
}
