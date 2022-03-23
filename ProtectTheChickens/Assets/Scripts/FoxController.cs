using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour, Consumable
{
    Wanderer wanderer;
    Eater eater;
    public int fullness = 0;
    [SerializeField] int nutritionValue;
    Vector3 hungerDirection;
    // Start is called before the first frame update
    void Start()
    {
        hungerDirection = Vector3.zero;
        wanderer = GetComponent<Wanderer>();
        eater = GetComponent<Eater>();
        GameManager.onStep += PerformStep;
        GameManager.lateStep += TryEating;
    }

    public void PerformStep()
    {
        bool foundFood = HasNearbyFood();
        if (foundFood)
        {
            transform.position += hungerDirection;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, hungerDirection);
        }
        else
        {
            wanderer.Wander();
        }
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

    bool HasNearbyFood() {
        Vector3[] moveDirections = new Vector3[]
        {
            Vector3.up, Vector3.right, Vector3.down, Vector3.left
        };
        foreach (Vector3 dir in moveDirections) {
            if (PosHasFoxFood(transform.position + dir))
            {
                hungerDirection = dir;
                return true;
            }
        }
        return false;
    }


    bool PosHasFoxFood(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, 0.2f);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(pos.x + ", " + pos.y + " contains: " + hitCollider.tag);
            if (hitCollider.CompareTag("Consumable"))
            {
                Debug.Log("fox hungry");
                return true;
            }
        }
        return false;
    }
}
