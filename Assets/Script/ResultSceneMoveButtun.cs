using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneMoveButtun : MonoBehaviour
{
    // Start is called before the first frame update

    public Button ResultSceneMoveButton;
    //public GameObject gameobjResultSceneMoveButton;
    public static int ResultSceneMoveButtonflag = 0;
   // private AudioSource audio = new AudioSource();

    void Start()
    {
        this.gameObject.SetActive(false);
        //audio = GetComponent<AudioSource>();
        //if (audio == null) audio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ResultSceneMoveButtonflag >=1)
        {
            //ResultSceneMoveButton.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
            //ResultSceneMoveButton.interactable = true;
        }
    }

    public void OnClick()
    {
        //audio.PlayOneShot(audio.clip);
        SceneManager.LoadScene("EndScene");
    }
}
