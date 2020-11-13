using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckEnemyScript : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 destination;
    private CharacterController enemyController;
    private Animator animator;
    RaycastHit hit;

    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 40.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;

    
    void Start()
    {
        startPosition = transform.position;
        SetDestination(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward * walkSpeed * Time.deltaTime;

        if (!arrived)
        {
            var scale = transform.lossyScale.x * 1.0f;
            var isHit = Physics.Raycast(this.transform.position + transform.forward * 3.0f, transform.up * -0.5f, out hit, 1.0f);

            if (isHit)
            {
                velocity = Vector3.zero;
                //animator.SetFloat("Speed", 2.0f);
                direction = (destination - transform.position).normalized;
                transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                velocity = direction * walkSpeed;
               // Debug.Log(destination);
            }else
            {
                StartCoroutine("RotateAxisY");
            }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            this.transform.position += transform.forward * walkSpeed * Time.deltaTime;

            //enemyController.Move(velocity * Time.deltaTime);

            //　目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, destination) < 0.5f)
            {
                arrived = true;
                //animator.SetFloat("Speed", 0.0f);
            }
            //　到着していたら
        }
        else
        {
            elapsedTime += Time.deltaTime;

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                //Debug.Log(elapsedTime);
                CreateRandomPosition();
                destination = GetDestination();
                arrived = false;
                elapsedTime = 0f;
            }
            
        }
    }

    public void CreateRandomPosition()
    {
        //　ランダムなVector2の値を得る
        var randDestination = Random.insideUnitCircle * 8;
        //　現在地にランダムな位置を足して目的地とする
        SetDestination(startPosition + new Vector3(randDestination.x, 0, randDestination.y));
    }


    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    public Vector3 GetDestination()
    {
        return destination;
    }

    IEnumerator RotateAxisY()
    {
        this.transform.position += transform.forward * walkSpeed * Time.deltaTime;

        var currentRotation = this.gameObject.transform.localRotation; // localEulerAnglesの代わりにlocalRotationを取得
                                                                       //var newRotation = currentRotation * Quaternion.AngleAxis(90, Vector3.right); // currentRotationを(1, 0, 0)軸周り90°回転したものをnewRotationとする
        var newRotation = currentRotation * Quaternion.AngleAxis(Random.Range(130.0f, 260.0f), Vector3.up);
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
}




        
      