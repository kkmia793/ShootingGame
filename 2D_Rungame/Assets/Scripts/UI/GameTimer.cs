using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float Timer = 180;
    public TextMeshProUGUI TimerText;
    public GameObject ClearWindow;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        TimerText.text = ((int)Timer).ToString();


        if (Timer <= 0)
        {
            ClearWindow.SetActive(true);
            Time.timeScale = 0;
            enabled = false;
        }



    }
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
