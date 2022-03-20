using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour
{
    public int GetConsumableFromMouth(Vector3 mouthPosition, GameObject hungryObject)
    {
        Collider[] hitColliders = Physics.OverlapSphere(mouthPosition, .2f);
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

                    return foodValue;
                    //hitCollider.SendMessage("GetEaten");
                    Debug.Log("Yum! Value: " + foodValue);
                    //TODO: show the player message: YUM, or BLEH
                }
            }
        }
        return 0;
    }
}
