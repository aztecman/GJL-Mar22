using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] GameObject eggPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onStep += PerformStep;
    }

    public void PerformStep() {

    }

    void LayEgg() {

    }

    void Wander() {

    }
}
