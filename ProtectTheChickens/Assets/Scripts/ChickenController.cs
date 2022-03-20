using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour, Consumable
{
    [SerializeField] GameObject eggPrefab;
    [SerializeField] Vector3 layingPosition;
    [SerializeField] int nutritionValue;
    [SerializeField] float eggLayChance = 0.05f;
    GameManager gameManager;
    Wanderer wanderer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        wanderer = GetComponent<Wanderer>();
        GameManager.onStep += PerformStep;
    }

    public void PerformStep() {
        // TODO: with a small chance lay an egg.
        // wander
        wanderer.Wander();
        if (Random.value < eggLayChance) {
            LayEgg();
        }
        eggLayChance += 0.001f;
    }

    void LayEgg() {
        Instantiate(eggPrefab, transform.position + layingPosition, Quaternion.identity);
        eggLayChance = 0;
    }

    public int GetEaten() {
        Destroy(gameObject);
        GameManager.onStep -= PerformStep;
        return nutritionValue;
    }
}
