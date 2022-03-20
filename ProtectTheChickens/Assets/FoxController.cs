using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    Wanderer wanderer;
    Eater eater;
    public int fullness = 0;
    // Start is called before the first frame update
    void Start()
    {
        wanderer = GetComponent<Wanderer>();
        eater = GetComponent<Eater>();
        GameManager.onStep += PerformStep;
    }

    public void PerformStep()
    {
        // TODO: try to find something to eat before wandering
        wanderer.Wander();
        fullness += eater.GetConsumableFromMouth(transform.position, gameObject);
    }
}
