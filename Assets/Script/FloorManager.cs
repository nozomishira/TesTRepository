using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 5.0f;
    /*void Start()
    {
        
    }*/

    public void DeleteFloor()
    {
        Destroy(this.gameObject,timer);
    }
}


