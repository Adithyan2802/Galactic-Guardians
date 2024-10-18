using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    public TMP_Text levelText;
    public TMP_Text levelDescription;
    public TMP_Text levelTitle;

    public GameObject life1, life2, life3;
    public GameObject ExplodeFX;

    private int score;
    private Blade blade;
    private Spawner spawner;

    private int lifeCount = 3;
    private int highScore = 0;
    private string highScoreKey = "HighScore";

    public GameOver gameOver;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    public void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highscoreText.text = highScore.ToString();
        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Fruit f in fruits)
        {
            Destroy(f.gameObject);
        }

        foreach (Bomb b in bombs)
        {
            Destroy(b.gameObject);
        }
    }

    public void ChangeLevel(int levelCounter)
    {
        if (levelCounter == 2)
        {
            score += 5;

            Debug.Log("level 1 completed");

            blade.enabled = false;
            spawner.enabled = false;

            levelText.text = "Level 2";
            levelTitle.text = "Level 2";
            levelDescription.text = "Destroy 15 Astroids to proceed..";

            spawner.bombChance = 0.35f;
            spawner.minSpawnDelay = 1f;
            spawner.maxSpawnDelay = 1.8f;
            spawner.minForce = 8f;
            spawner.maxForce = 10f;

            ClearScene();
            spawner.enabled = true;
            blade.enabled = true;
        }
        else if (levelCounter == 3)
        {
            score += 10;

            Debug.Log("Level 2 Completed");

            blade.enabled = false;
            spawner.enabled = false;

            levelText.text = "Level 3";
            levelTitle.text = "Level 3";
            levelDescription.text = "Destroy Astroids without hitting any planet.. ";

            spawner.bombChance = 0.56f;
            spawner.minForce = 12f;
            spawner.maxForce = 14f;
            spawner.minSpawnDelay = 0.5f;
            spawner.maxSpawnDelay = 1f;

            ClearScene();
            spawner.enabled = true;
            blade.enabled = true;
        }
        StartCoroutine(ReloadScore());
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            highscoreText.text = highScore.ToString();

            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }

        if (score == 10)
        {
            ChangeLevel(2);
        }

        if (score == 30)
        {
            ChangeLevel(3);
        }
    }

    public void Explode(Collider col)
    {
        Vector3 position = new Vector3(col.transform.position.x, col.transform.position.y, -7.8f);
        GameObject blast = Instantiate(ExplodeFX, position, Quaternion.identity);
        blast.GetComponent<ParticleSystem>().Play();

        Destroy(blast, 1f);

        lifeCount--;

        if (lifeCount == 2)
        {
            life1.GetComponent<Renderer>().material.color = Color.black;
            Debug.Log("life gone first time..");
        }
        else if (lifeCount == 1)
        {
            life2.GetComponent<Renderer>().material.color = Color.black;
            Debug.Log("life gone second time..");
        }
        else if (lifeCount <= 0)
        {
            Debug.Log("life gone third time..");
            life3.GetComponent<Renderer>().material.color = Color.black;
            blade.enabled = false;
            spawner.enabled = false;

            ExplodeFade();
        }
    }

    private void ExplodeFade()
    {
        gameOver.Setup(score);
    }

    private IEnumerator ReloadScore()
    {
        yield return new WaitForSeconds(4f);
        scoreText.text = score.ToString();
    }
}

