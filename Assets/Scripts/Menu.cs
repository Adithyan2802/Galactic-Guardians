using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class Menu : MonoBehaviour
{
    public GameObject loadScreen;
    public GameObject button;
    public GameObject exitBtn;
    public GameObject rocket;
    public GameObject rocket2;
    public Slider slider;
    public bool functionCalled = false;
    [SerializeField] Image circle;
    public void Start()
    {
        Time.timeScale = 1;
    }

    public void StartFill()
    {
        StartCoroutine(Fill());
    }

    IEnumerator Fill()
    {
        functionCalled = false;
        float timer = 0;
        Debug.Log("Fill function executing..");

        while (timer < 3)
        {
            if (functionCalled)
            {
                circle.fillAmount = 0;
                break;
            }


            timer += Time.deltaTime;

            //Lerp the fill amount from 0 to 1 over the duration
            circle.fillAmount = Mathf.Lerp(0, 1, timer / 3);
            // effect.rotation = Quaternion.Euler(new Vector3(0f, 0f, -progress * 360));
            yield return null;
        }

        if (timer > 3)
        {
            rocket.SetActive(false);
            rocket2.SetActive(false);
            button.SetActive(false);
            exitBtn.SetActive(false);

            loadScreen.SetActive(true);

            float loadtimer = 0;
            while (loadtimer < 9)
            {
                loadtimer += Time.deltaTime;
                yield return null;
            }
            if (loadtimer > 9)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(1);

                while (!operation.isDone)
                {
                    float progress = Mathf.Clamp01(operation.progress / .9f);
                    slider.value = progress;
                    yield return null;
                }
            }
        }

    }

    public void StopFill()
    {
        functionCalled = true;
        Debug.Log("Stop Fill function executing..");
        circle.fillAmount = 0;
    }
}
