using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomscr : MonoBehaviour
{

    public GameObject qweqwe;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(qweqwe, transform.position + new Vector3(0, 0, 1), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
