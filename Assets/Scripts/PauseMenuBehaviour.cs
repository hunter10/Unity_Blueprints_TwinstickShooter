using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MainMenuBehaviour
{
    public GameObject optionMenu;

    public static bool isPaused;
    public GameObject pauseMenu;
    public void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);

        UpdateQualityLabel();
        UpdateVolumeLabel();
    }

    public void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            if (!optionMenu.activeInHierarchy)
            {
                isPaused = !isPaused;
                Time.timeScale = (isPaused) ? 0 : 1;
                pauseMenu.SetActive(isPaused);
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityLabel();
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityLabel();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        UpdateVolumeLabel();
    }

    private void UpdateQualityLabel()
    {
        int currentQuality = QualitySettings.GetQualityLevel();
        string qualityName = QualitySettings.names[currentQuality];

        optionMenu.transform.Find("Quality Level").GetComponent<UnityEngine.UI.Text>().text = "Quality Level - " + qualityName;
    }

    private void UpdateVolumeLabel()
    {
        optionMenu.transform.Find("Master Volume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " + (AudioListener.volume * 100).ToString("f2") + "%";
    }

    public void OpenOptions()
    {
        optionMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
