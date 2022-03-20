using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour, Consumable
{
    [SerializeField] GameObject eggPrefab;
    [SerializeField] Vector3 layingPosition;
    [SerializeField] int nutritionValue;
    [SerializeField] float eggLayChance = 0.05f;
    [SerializeField] float growChance = 0;
    [SerializeField] float actChange = 0.4f;
    [SerializeField] GameObject largeModel, smallModel;
    public bool isGrown = false;
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
        if (Random.value < actChange)
        {
            wanderer.Wander();
            if (Random.value < eggLayChance && isGrown)
            {
                LayEgg();
            }
            eggLayChance += 0.001f;
            if (!isGrown)
            {
                growChance += 0.001f;
                if (Random.value < growChance)
                {
                    largeModel.SetActive(true);
                    smallModel.SetActive(false);
                    FindObjectOfType<AudioManager>().Play("Chicken");
                    isGrown = true;
                }
            }
        }
    }

    void LayEgg() {
        Instantiate(eggPrefab, transform.position + layingPosition, Quaternion.identity);
        eggLayChance = 0;
          FindObjectOfType<AudioManager>().Play("Lay");
    }

    public int GetEaten() {
        Destroy(gameObject);
        GameManager.onStep -= PerformStep;
        return nutritionValue;
    }
}
