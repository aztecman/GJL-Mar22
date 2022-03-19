using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    public enum AimDirection {
        Left,
        Forward,
        Right
    }
    public List<GameObject> bodySegments;
    [SerializeField] GameObject tail;
    [SerializeField] float moveIncrement;
    [SerializeField] GameObject bodySegmentPrefab;
    public AimDirection currentDirection;
    public bool headOnTile;
    public int fullness = 0;
    //NOTE: snek head transform.position is actually the position of the base of the skull ('neck-bone')
    void Start()
    {
        headOnTile = false;
        GameManager.onStep += MoveHead;
        currentDirection = AimDirection.Forward;
        //MoveHead();
    }

    public void MoveHead()
    {
        //Move Head forward
        Vector3 prevPos = transform.position;
        transform.position += transform.up * moveIncrement;

        //Move Body
        foreach (GameObject bodySeg in bodySegments)
        {
            prevPos = MoveBodySegment(bodySeg, prevPos);
        }
        if (fullness > 0)
        { //if food is in stomach, convert to a new body segment
            fullness -= 1;
            GameObject newBodySegment = Instantiate(bodySegmentPrefab, prevPos, Quaternion.identity);
            bodySegments.Add(newBodySegment);
        }
        else //only move the tail if not adding a body segment
        {
            //tail.transform.Rotate(90, 0, 0);
            MoveBodySegment(tail, prevPos);
            Vector3 lastBodySegmentPosition = bodySegments[bodySegments.Count - 1].transform.position;
            Vector3 alignDir = (lastBodySegmentPosition - tail.transform.position).normalized;
            tail.transform.rotation = Quaternion.LookRotation(Vector3.forward, alignDir);
        }

        headOnTile = !headOnTile;
        CheckMouthForConsumable();

        if (!headOnTile)
        {
            if (currentDirection == AimDirection.Left)
            {
                transform.Rotate(new Vector3(0, 0, 90));
                currentDirection = AimDirection.Forward;
            }
            else if (currentDirection == AimDirection.Right)
            {
                transform.Rotate(new Vector3(0, 0, -90));
                currentDirection = AimDirection.Forward;
            }
        }
    }

    void CheckMouthForConsumable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.up * moveIncrement, .2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Consumable"))
            {
           
                 FindObjectOfType<AudioManager>().Play("eating");
                

                Consumable consumableComponent = hitCollider.GetComponent<Consumable>();
                int foodValue = consumableComponent.GetEaten();

                fullness += foodValue;
                //hitCollider.SendMessage("GetEaten");
                Debug.Log("Yum! Value: " + foodValue);
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
        //if (Input.GetKeyDown(KeyCode.Space)){
        //    MoveHead();
        //}
        Debug.Log(Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Horizontal") < -0.8f)
            {
               if(!FindObjectOfType<AudioManager>().isPlaying("snakemove")){
                 FindObjectOfType<AudioManager>().Play("snakemove");
                }
            //   
                //MoveHead();
                currentDirection = AimDirection.Left;
                //transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (Input.GetAxis("Horizontal") > 0.8f)
            {
                //MoveHead();
                currentDirection = AimDirection.Right;

                if(!FindObjectOfType<AudioManager>().isPlaying("snakemove")){
                 FindObjectOfType<AudioManager>().Play("snakemove");
                }
                
                //transform.Rotate(new Vector3(0, 0, -90));
            }
    }
}
