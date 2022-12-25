using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform camertata;
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        camertata = GameObject.FindWithTag("TotorCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(camertata);
    }
}
