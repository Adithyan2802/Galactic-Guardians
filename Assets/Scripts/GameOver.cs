using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public TMP_Text points;

    [SerializeField] Image restartCircle;
    [SerializeField] Image menuCircle;
    public bool functionCalled = false;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        points.text = score.ToString();
    }

    public void StartRFill()
    {
        StartCoroutine(RestartFill());
    }

    public void StartMFill()
    {
        StartCoroutine(MenuFill());
    }


    public void StopFill()
    {
        functionCalled = true;
        Debug.Log("Stop Fill function executing..");
        restartCircle.fillAmount = 0;
        menuCircle.fillAmount = 0;
    }

    IEnumerator RestartFill()
    {
        functionCalled = false;
        float timer = 0;
        Debug.Log("Fill function executing..");

        while (timer < 3)
        {
            if (functionCalled)
            {
                restartCircle.fillAmount = 0;
                break;
            }


            timer += Time.deltaTime;

            //Lerp the fill amount from 0 to 1 over the duration
            restartCircle.fillAmount = Mathf.Lerp(0, 1, timer / 3);
            yield return null;
        }

        if (timer > 3)
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator MenuFill()
    {
        functionCalled = false;
        float timer = 0;
        Debug.Log("Fill function executing..");

        while (timer < 3)
        {
            if (functionCalled)
            {
                menuCircle.fillAmount = 0;
                break;
            }


            timer += Time.deltaTime;

            //Lerp the fill amount from 0 to 1 over the duration
            menuCircle.fillAmount = Mathf.Lerp(0, 1, timer / 3);
            yield return null;
        }

        if (timer > 3)
        {
            SceneManager.LoadScene(0);
        }
    }
}
