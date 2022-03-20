using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void ActionStep();
    public static ActionStep onStep;

    [SerializeField] float secondsPerTick = 1f;
    [SerializeField] GameObject playerDeathPanel;
    public float minX, maxX, minY, maxY;

    Coroutine gameClock;

    void Start()
    {
        gameClock = StartCoroutine(GameClock());
    }

    IEnumerator GameClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsPerTick);
            onStep?.Invoke();
        }

    }
    public void PlayerDeath() {
        playerDeathPanel.SetActive(true);
        StopCoroutine(gameClock);
    }

    public void ResetGame() {
        onStep = null;
        SceneManager.LoadScene(0);
    }
}
