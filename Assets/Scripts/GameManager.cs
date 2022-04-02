using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoretext;
    [SerializeField] private AudioClip[] audios;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject bestScoreUI;
    [SerializeField] private Player player;
    private Pipe[] pipes;
    private AudioSource audioSource;
    private int score;
    private int bestScore = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameOverUI.SetActive(false);
        bestScoreUI.SetActive(false);
        Pause();
    }

    public void Play()
    {
        playButton.SetActive(false);
        gameOverUI.SetActive(false);
        bestScoreUI.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1.0f;

        score = 0;
        scoreText.text = score.ToString();

        pipes = FindObjectsOfType<Pipe>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        bestScoretext.text = bestScore.ToString();
        audioSource.clip = audios[1];
        audioSource.PlayOneShot(audioSource.clip);

        playButton.SetActive(true);
        gameOverUI.SetActive(true);
        
        Pause();

        if(score > bestScore)
        {
            bestScore = score;
            bestScoretext.text = bestScore.ToString();
        }
        bestScoreUI.SetActive(true);
        //  Invoke(nameof(Pause), 1f);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        audioSource.clip = audios[0];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
