using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour
{
    [SerializeField] bool isSnek = false;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public int GetConsumableFromMouth(Vector3 mouthPosition, GameObject hungryObject)
    {
        Collider[] hitColliders = Physics.OverlapSphere(mouthPosition, .05f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Consumable"))
            {
                if (!ReferenceEquals(hitCollider.gameObject, hungryObject))
                {
                    Debug.Log(gameObject.name);
                    FindObjectOfType<AudioManager>().Play("eating");


                    Consumable consumableComponent = hitCollider.GetComponent<Consumable>();
                    int foodValue = consumableComponent.GetEaten();


                    //hitCollider.SendMessage("GetEaten");
                    Debug.Log("Yum! Value: " + foodValue);
                    //TODO: show the player message: YUM, or BLEH
                    return foodValue;
                }
            }
            if (isSnek)
            {
                if (hitCollider.CompareTag("Snek"))
                {
                    if (!ReferenceEquals(hitCollider.gameObject, hungryObject))
                    {
                        //Debug.Log(mouthPosition);
                        //Debug.Log(hitCollider.name);
                        //Debug.Log(hitCollider.transform.position);
                        gameManager.PlayerDeath();
                        return 0;
                    }
                }
                if (hitCollider.CompareTag("SnekTail"))
                {
                    Debug.Log("Connection!");
                }
            }
        }
        return 0;
    }
}
