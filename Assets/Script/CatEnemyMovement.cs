using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnemyMovement : MonoBehaviour
{

    Color Floorcolor;
    Rigidbody rigidbody;
    RaycastHit hit;
    float speed = 4.0f;
    private Vector3 CatEnemyPossition; 

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        CatEnemyPossition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CatDirectMove();
        /*if (Input.GetKeyDown("f"))
        {

       StartCoroutine("RotateAxisY");
     
        }*/
    }
    
    private void CatDirectMove()
    {
       
        var scale = transform.lossyScale.x * 1.0f;
        //var isHit = Physics.BoxCast(transform.position, Vector3.one * scale, transform.forward, out hit, transform.rotation);
        var isHit = Physics.Raycast(this.transform.position+transform.forward*3.0f,transform.up*-0.5f, out hit,1.0f);

        if (isHit)
        {
             this.transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            StartCoroutine("RotateAxisY");
            this.transform.position += transform.forward * speed * Time.deltaTime;

        }
    }

    IEnumerator RotateAxisY()
    {
        this.transform.position += transform.forward * speed * Time.deltaTime;
        
        var currentRotation = this.gameObject.transform.localRotation; // localEulerAnglesの代わりにlocalRotationを取得
       //var newRotation = currentRotation * Quaternion.AngleAxis(90, Vector3.right); // currentRotationを(1, 0, 0)軸周り90°回転したものをnewRotationとする
       var newRotation = currentRotation * Quaternion.AngleAxis(Random.Range(130.0f,260.0f), Vector3.up);
       //var newRotation = currentRotation * Quaternion.AngleAxis(180, Vector3.up);


        //  Debug.Log(currentRotation.eulerAngles);
        //Debug.Log(newRotation.eulerAngles);

        for (float t = 0; t < 0.005f; t += Time.deltaTime)
        {
            Quaternion rotation = Quaternion.Slerp(currentRotation, newRotation, t * 2); // 中間の回転を求めるのにSlerpを使いましたが、より高速なLerpを使ってもほとんど違和感はないかと思います

            //rotatingAxisは回転させるオブジェクト
            this.gameObject.transform.localRotation = rotation;

            yield return null;
        }

        //ちょうど90度刻みになるように調整
        this.gameObject.transform.localRotation = newRotation;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            //Debug.Log("地面接触" + collision.gameObject.name);
            //this.Floorcolor = collision.gameObject.GetComponent<Renderer>().material.color;
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            for (int i = 0; i < collision.gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                collision.gameObject.GetComponent<Renderer>().materials[i].color = Color.black;
            }
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
