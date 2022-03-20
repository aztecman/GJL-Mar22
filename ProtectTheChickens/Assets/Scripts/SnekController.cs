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
    GameManager gameManager;
    Eater eater;
    //NOTE: snek head transform.position is actually the position of the base of the skull ('neck-bone')
    void Start()
    {
        headOnTile = false;
        gameManager = FindObjectOfType<GameManager>();
        eater = GetComponent<Eater>();
        GameManager.onStep += MoveHead;
        GameManager.lateStep += TryEating;
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

        if (transform.position.x > gameManager.maxX ||
            transform.position.y > gameManager.maxY ||
            transform.position.x < gameManager.minX ||
            transform.position.y < gameManager.minY
            ) //if out of bounds
        {
            gameManager.PlayerDeath();
            return;
        }

        headOnTile = !headOnTile;

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

    public void TryEating() {
        fullness += eater.GetConsumableFromMouth(transform.position + transform.up * moveIncrement, gameObject);
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
        //Debug.Log(Input.GetAxis("Horizontal"));
        //if (Input.GetAxis("Horizontal") < -0.8f)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!FindObjectOfType<AudioManager>().isPlaying("snakemove"))
            {
                FindObjectOfType<AudioManager>().Play("snakemove");
            }

            currentDirection = AimDirection.Left;

        }
        //else if (Input.GetAxis("Horizontal") > 0.8f)
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!FindObjectOfType<AudioManager>().isPlaying("snakemove"))
            {
                FindObjectOfType<AudioManager>().Play("snakemove");
            }
            
            currentDirection = AimDirection.Right;

        }
    }
}
