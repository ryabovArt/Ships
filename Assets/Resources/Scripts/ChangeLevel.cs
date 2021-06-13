using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    public static ChangeLevel instance;

    private int sceneIndex;
    private int levelComplete;

    [SerializeField] private Button startButton;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("CompleteLevel");
        Timer.instance.StartTimer();
        startButton.interactable = true;
    }

    /// <summary>
    /// Cvtyf ehjdyz
    /// </summary>
    public void EndGame()
    {
        if (sceneIndex == 2)
        {
            PlayerPrefs.SetInt("CompleteLevel", sceneIndex);
            Invoke("LoadMainMenu", 1f);
        }
        else
        {
            if (levelComplete < sceneIndex)
            {
                PlayerPrefs.SetInt("CompleteLevel", sceneIndex);
            }
            Invoke("NextLevel", 1f);
        }
        Timer.instance.StopTimer();
    }

    /// <summary>
    /// Рестарт уровня
    /// </summary>
    public void RestartLevel()
    {
        StartCoroutine(WaitForTheCurrentSceneToLoad());
    }
    IEnumerator WaitForTheCurrentSceneToLoad()
    {
        Timer.instance.StopTimer();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Загрузка следующего уровня
    /// </summary>
    private void NextLevel()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    /// <summary>
    /// Загрузка меню
    /// </summary>
    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Отключение интерактивности кнопки
    /// </summary>
    public void InteractableButton()
    {
        startButton.interactable = false;
    }

    /// <summary>
    /// Считывание значения таймера
    /// </summary>
    public void WriteTimerValue()
    {
        PlayerPrefs.SetString("TimerValue", PlayerPrefs.GetInt("CurrentTimerValue").ToString());
    }
}
