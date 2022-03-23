using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour, Consumable
{
    [SerializeField] int nutritionValue = 1;
    [SerializeField] float eggHatchChance = 0.01f;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] Vector3 hatchPosition;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onStep += AttemptHatch;
        gameManager = FindObjectOfType<GameManager>();
    }

    void AttemptHatch() {
        if (Random.value < eggHatchChance &&
            !gameManager.gameHasEnded)
        {
            Hatch();
        }
        eggHatchChance += 0.001f;
    }
    void Hatch() {
        Instantiate(chickenPrefab, transform.position + hatchPosition, Quaternion.identity);
           FindObjectOfType<AudioManager>().Play("Hatched");
        Die();
    }

    public int GetEaten() {
        Die();
           FindObjectOfType<AudioManager>().Play("Wolf");
        return nutritionValue;
    }

    void Die() {
    
        Destroy(gameObject);
        GameManager.onStep -= AttemptHatch;
    }
}
