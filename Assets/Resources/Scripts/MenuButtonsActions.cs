using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsActions : MonoBehaviour
{
    [SerializeField] private Button level_1;
    [SerializeField] private Button level_2;
    private int completeLevel;

    void Start()
    {
        completeLevel = PlayerPrefs.GetInt("CompleteLevel");
        level_2.interactable = false;

        switch (completeLevel)
        {
            case 1:
                level_1.interactable = true;
                break;
            case 2:
                level_1.interactable = true;
                level_2.interactable = true;
                break;
        }
    }

    /// <summary>
    /// Загрузка сцены
    /// </summary>
    /// <param name="level">номер сцены</param>
    public void SceneLoader(int level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Кнопка для сброса пройденых уровней. Отключена на канвасе в меню
    /// </summary>
    public void Reset()
    {
        level_2.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
