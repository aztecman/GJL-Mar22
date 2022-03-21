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
    LogicTests logicTests;

    void Start()
    {
        gameClock = StartCoroutine(GameClock());
        uiManager = FindObjectOfType<UIManager>();
        logicTests = FindObjectOfType<LogicTests>();
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

    public void PlayerDeath()
    {
        FindObjectOfType<AudioManager>().Play("Death");
        uiManager.ShowDeathPanel();
        StopCoroutine(gameClock);
    }

    public void ResetGame()
    {
        onStep = null;
        lateStep = null;
        SceneManager.LoadScene(0);
    }

    public void EatEgg()
    {
        eggsEaten += 1;
        uiManager.UpdateMission(1, "- Eat " + GetRemainingEggs() + " Eggs");
        if (GetRemainingEggs() == 0)
        {
            uiManager.UpdateMission(1, "- Loop Around Chicken");
            uiManager.UpdateMission(2, "Snek Satisfied");
        }
    }

    public void EndGame()
    {
        Debug.Log("2/");
        int chickenCount = 0;
        bool[,] loopedCells = logicTests.GetLoopedCells();
        for (int i = 0; i < loopedCells.GetLength(0); i++)
        {
            for (int j = 0; j < loopedCells.GetLength(0); j++)
            {
                if (loopedCells[i, j])
                {
                    if (CellHasChicken(i, j)) chickenCount += 1;
                }
            }
        }
        Debug.Log("Chicken Count: " + chickenCount);
        if (chickenCount > 0)
        {
            uiManager.UpdateMission(2, "You Win!");
        }
        else
        {
            uiManager.UpdateMission(2, "No chickens\nwere harmed\nin the making\nof this game");
        }
    }

    bool CellHasChicken(int col, int row)
    {
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(col - 6.5f, 6.5f - row, -.75f), 0.2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag.Contains("Consumable"))
            {
                if (hitCollider.gameObject.GetComponent<ChickenController>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetRemainingEggs()
    {
        return Mathf.Max(0, eggsToObtain[currentStage] - eggsEaten); //get remaining eggs, clipped at zero
    }
}