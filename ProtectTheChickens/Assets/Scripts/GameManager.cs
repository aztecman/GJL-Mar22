using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void ActionStep();
    public static ActionStep onStep, lateStep;

    [SerializeField] float secondsPerTick = 1f;
    public int[] eggsToObtain;
    public int eggsEaten = 0;
    public int currentStage = 0;
    public float minX, maxX, minY, maxY;
    Coroutine gameClock;
    UIManager uiManager;

    void Start()
    {
        gameClock = StartCoroutine(GameClock());
        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateMission(1, "- Eat " + GetRemainingEggs() + " Eggs");
    }

    IEnumerator GameClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsPerTick);
            onStep?.Invoke();
            yield return new WaitForFixedUpdate();
            lateStep?.Invoke();
        }

    }

    public void PlayerDeath() {
        FindObjectOfType<AudioManager>().Play("Death");
        uiManager.ShowDeathPanel();
        StopCoroutine(gameClock);
    }

    public void ResetGame() {
        onStep = null;
        lateStep = null;
        SceneManager.LoadScene(0);
    }

    public void EatEgg() {
        eggsEaten += 1;
        uiManager.UpdateMission(1, "- Eat " + GetRemainingEggs() + " Eggs");
        if (GetRemainingEggs() == 0) {
            uiManager.UpdateMission(1, "- Loop Around Chicken");
            uiManager.UpdateMission(2, "Snek Satisfied");
        }
    }

    public int GetRemainingEggs() {
        return Mathf.Max(0, eggsToObtain[currentStage] - eggsEaten); //get remaining eggs, clipped at zero
    }
}
