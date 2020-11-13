using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public Text lasttimerText;
    public Text longesttimerText;
    public Text rankingText;
    float lasttimer;
    float longesttimer;
    int ranking;

    void Start()
    {
        lasttimer = PlayerPrefs.GetFloat("staytime");
        lasttimerText.text = lasttimer.ToString("F1")+"秒";
        ranking = PlayerPrefs.GetInt("playerranking");
        rankingText.text = ranking.ToString() + "位";

        if (PlayerPrefs.HasKey("LongestTimer") == true)
        {
            longesttimer = PlayerPrefs.GetFloat("LongestTimer");
            if (longesttimer < lasttimer)
            {
                longesttimer = lasttimer;
                PlayerPrefs.SetFloat("LongestTimer", longesttimer);
            }
        }
        else
        {
            longesttimer = lasttimer;
            PlayerPrefs.SetFloat("LongestTimer", longesttimer);
        }
        longesttimerText.text = longesttimer.ToString("F1")+"秒";
    }

    public void OnclicMoveStart()
    {
        SceneManager.LoadScene("StartScene");
    }

   
}
