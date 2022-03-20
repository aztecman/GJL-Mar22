using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour, Consumable
{
    [SerializeField] int nutritionValue = 1;
    [SerializeField] float eggHatchChance = 0.01f;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] Vector3 hatchPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onStep += AttemptHatch;
    }

    void AttemptHatch() {
        if (Random.value < eggHatchChance) {
            Hatch();
        }
        eggHatchChance += 0.001f;
    }
    void Hatch() {
        Instantiate(chickenPrefab, transform.position + hatchPosition, Quaternion.identity);
        Die();
    }

    public int GetEaten() {
        Die();
        return nutritionValue;
    }

    void Die() {
        Destroy(gameObject);
        GameManager.onStep -= AttemptHatch;
    }
}
