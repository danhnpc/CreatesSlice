using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject autdioScreen;
    public GameObject pauseScreen;
    public AudioSource audio;
    public Slider slider;

    private float spawnRate = 1f;
    private int score;
    public bool isGameActive;
    public int live;
    public bool isPause;

    void Start()
    {
        live = 3;
        liveText.text = "Live: " + live;
        slider.value = audio.volume;
    }

    private void Update()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                    Resume();
                else
                    Pause();
            }
        }
            
    }

    private void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        pauseScreen.SetActive(true);
    }
    private void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        pauseScreen.SetActive(false);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        autdioScreen.gameObject.SetActive(false);
    }
    public void UpdateLive(int value)
    {
        live += value;
        liveText.text = "Live: " + live;
    }
    public void OnVolumeChange()
    {
        audio.volume = slider.value;
    }
}
