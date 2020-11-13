using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameManager gameManager;
    [SerializeField] bool isEnable = false;
    [SerializeField] float timer = 2.5f;//床が消される秒数
    Rigidbody rigidbody;
    RaycastHit hit;
    public float speed = 0.5f;
    public float sphereRadius;
    Vector3 velocity;
    float jumpPower = 2.5f;
    [SerializeField] private float m_speedScale = 5.0f;
    [SerializeField] private float applySpeed = 0.2f;       // 回転の適用速度
    Vector3 playerPos;
    Color Floorcolor;
    Animator animator;
    private GameObject PlayerCamera;
    [SerializeField] GameObject ResultSceneMoveButton;
    public float StayTime = 0.0f;
    //public AudioClip stepsound;
    private AudioSource audiosource = new AudioSource();



    void Start()
    {
       
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayerCamera = GameObject.Find("PlayerFollowingCamera");
        audiosource = GetComponent<AudioSource>();
        if (audiosource == null) audiosource = gameObject.AddComponent<AudioSource>();

    }

    void Update()
    {
     
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);
        //アニメーションをつけるコードを書く
        animator.SetFloat("Animatorspeed", Mathf.Abs(velocity.x));
        animator.SetFloat("Animatorspeed", Mathf.Abs(velocity.y));
        // スティックから入力を取得
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector3 addPos = new Vector3(horizontal,0, vertical) * m_speedScale;
      // transform.position += addPos * Time.deltaTime;
       transform.position += Time.deltaTime * m_speedScale * (PlayerCamera.transform.forward * vertical + PlayerCamera.transform.right * horizontal);
        //スティックからでもアニメーションが付くようにする
        animator.SetFloat("Animatorspeed", Mathf.Abs(vertical));
        animator.SetFloat("Animatorspeed", Mathf.Abs(horizontal));
        /*
         以下移動時にPlayerの向きを変えるコードを書く
       */
       // Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Vector3 target_stickdir = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));//stickから向きを変える
        Vector3 target_stickdir = PlayerCamera.transform.forward * vertical + PlayerCamera.transform.right * horizontal;
        //Run
       /* if (target_dir.magnitude > 0.1)
        {
            //体の向きを変更
            //transform.rotation = Quaternion.LookRotation(target_dir);
            this.transform
            //前方へ移動
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
        }*/

        if (target_stickdir.magnitude > 0.1)
        {
            //体の向きを変更
            transform.rotation = Quaternion.LookRotation(target_stickdir);
            //前方へ移動
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }



        if (Input.GetKeyDown("space")&&isGround())
        {
            Jump();
        }else if(CrossPlatformInputManager.GetButton("Jump")&&isGround())
             {
            //GetComponent<Renderer>().material.color = Color.red;
            Jump();
        }
        else
        {
            //GetComponent<Renderer>().material.color = Color.white;
        }
        StayTime += Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            //Debug.Log("地面接触" + collision.gameObject.name);
            // this.Floorcolor = collision.gameObject.GetComponent<Renderer>().material.color;
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            audiosource.PlayOneShot(audiosource.clip);
            for (int i = 0; i < collision.gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                collision.gameObject.GetComponent<Renderer>().materials[i].color = Color.black;
            }
            collision.gameObject.GetComponent<FloorManager>().DeleteFloor();
        }

     
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destination")
        {
            PlayerPrefs.SetFloat("staytime", StayTime);
            PlayerPrefs.SetInt("playerranking", RankingManager.PlayerRanking);
            ResultSceneMoveButton.SetActive(true);
        }
    }
    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            //collision.gameObject.GetComponent<Renderer>().material.color = this.Floorcolor;
            for (int i = 0; i < collision.gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                collision.gameObject.GetComponent<Renderer>().materials[i].color = this.Floorcolor;
            }
        }
    }
    */
    bool isGround()//地面についている状態かどうかを判定する関数
    {
        /*if (isEnable == false)
            return true;*/

        var scale = transform.lossyScale.x * 1.0f;

        //var isHit = Physics.BoxCast(transform.position, Vector3.one * scale, transform.forward, out hit, transform.rotation);
        //var isHit = Physics.BoxCast(transform.position, Vector3.one * scale, transform.up*-0.5f, out hit, transform.rotation);
       var isHit = Physics.Raycast(this.transform.position, transform.up * -1.0f, out hit, 1.0f);


        if (isHit)
        {
            //Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            //Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, Vector3.one * scale * 2);
           // Debug.Log("着地");
            return true;
        }
        else
        {
            //Gizmos.DrawRay(transform.position, transform.forward * 100);
            return false;
        }
    }

    void Jump()//jumpを行う関数
    {
        //Debug.Log("jmp");
        rigidbody.AddForce(Vector3.up * 100 * jumpPower);
    }
}
