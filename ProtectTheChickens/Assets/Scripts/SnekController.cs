using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    public GameObject[] bodySegments;
    public bool headOnTile;
    [SerializeField] GameObject tail;
    [SerializeField] float moveIncrement;
    //NOTE: snek head transform.position is actually the position of the base of the skull ('neck-bone')
    void Start()
    {
        headOnTile = false;
        //MoveHead();
    }

    void MoveHead() {
        //move head forward
        Vector3 prevPos = transform.position;
        transform.position += transform.up * moveIncrement;

        //Move Body
        foreach (GameObject bodySeg in bodySegments) {
            prevPos = MoveBodySegment(bodySeg, prevPos);
        }
        headOnTile = !headOnTile;
        CheckMouthForConsumable();
    }

    void CheckMouthForConsumable() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.up*moveIncrement, .2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Consumable"))
            {
                hitCollider.SendMessage("GetEaten");
                Debug.Log("Yum!");
                //TODO: show the player message: YUM, or BLEH
            }
        }
    }

    Vector3 MoveBodySegment(GameObject segment, Vector3 destination)
    {
        Vector3 prevPos = segment.transform.position;
        segment.transform.position = destination;
        return prevPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            MoveHead();
        }
        if (headOnTile) {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveHead();
                transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                MoveHead();
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
}
