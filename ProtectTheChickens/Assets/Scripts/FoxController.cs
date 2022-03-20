using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour, Consumable
{
    Wanderer wanderer;
    Eater eater;
    public int fullness = 0;
    [SerializeField] int nutritionValue;
    // Start is called before the first frame update
    void Start()
    {
        wanderer = GetComponent<Wanderer>();
        eater = GetComponent<Eater>();
        GameManager.onStep += PerformStep;
        GameManager.lateStep += TryEating;
    }

    public void PerformStep()
    {
        // TODO: try to find something to eat before wandering
        wanderer.Wander();
    }

    public void TryEating() {
        fullness += eater.GetConsumableFromMouth(transform.position, gameObject);
    }

    public int GetEaten() {
        GameManager.onStep -= PerformStep;
        GameManager.lateStep -= TryEating;
        Destroy(gameObject);
        return nutritionValue;
    }
}
