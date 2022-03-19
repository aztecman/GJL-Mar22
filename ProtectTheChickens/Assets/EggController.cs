using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour, Consumable
{
    int nutritionValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Hatch() {

    }

    public int GetEaten() {
        Destroy(gameObject);
        return nutritionValue;
    }
}
