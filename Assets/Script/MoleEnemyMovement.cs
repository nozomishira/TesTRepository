using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoleEnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private NavMeshAgent agent;
    Rigidbody rigidbody;
    Color Floorcolor;
    float speed = 0.2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Player.transform.position, speed);
        //agent.destination = Player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            // Debug.Log("地面接触" + collision.gameObject.name);
            //this.Floorcolor = collision.gameObject.GetComponent<Renderer>().material.color;
            for (int i = 0; i < collision.gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                collision.gameObject.GetComponent<Renderer>().materials[i].color = Color.black;
            }
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.black;
            collision.gameObject.GetComponent<FloorManager>().DeleteFloor();
        }

       

    }
    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            collision.gameObject.GetComponent<Renderer>().material.color = this.Floorcolor;
        }
    }*/
}
