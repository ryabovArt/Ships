using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    private int timeStart;
    [SerializeField] private Text timerText;
    private bool stopTime = true;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Запуск таймера
    /// </summary>
    public void StartTimer()
    {
        timerText.text = timeStart.ToString();
        StartCoroutine(ChangeTime());
    }

    IEnumerator ChangeTime()
    {
        while (stopTime)
        {
            timeStart++;
            timerText.text = timeStart.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Остановка таймера
    /// </summary>
    public void StopTimer()
    {
        stopTime = false;
        PlayerPrefs.SetInt("CurrentTimerValue", timeStart);
        timeStart = 0;
    }

    /// <summary>
    /// Текущее значение таймера
    /// </summary>
    public void GetCurrentTimerValue()
    {
        PlayerPrefs.GetInt("CurrentTimerValue").ToString();
    }


}
