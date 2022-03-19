using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ActionStep();
    public static ActionStep onStep;

    [SerializeField] float secondsPerTick = 1f;
    void Start()
    {
        StartCoroutine(GameClock());
    }

    IEnumerator GameClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsPerTick);
            onStep?.Invoke();
        }

    }

}
