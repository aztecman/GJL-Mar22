using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    public GameObject[] bodySegments;
    public bool headOnTile;
    [SerializeField] GameObject tail;
    [SerializeField] float moveIncrement;
    // Start is called before the first frame update
    void Start()
    {
        headOnTile = false;
        //MoveHead();
    }

    void MoveHead(Vector3 moveDir) {
        Vector3 prevPos = transform.position;
        transform.position += moveDir * moveIncrement;

        //Move Body
        foreach (GameObject bodySeg in bodySegments) {
            prevPos = MoveBodySegment(bodySeg, prevPos);
        }
        headOnTile = !headOnTile;
    }

    Vector3 MoveBodySegment(GameObject segment, Vector3 destination)
    {
        Vector3 prevPos = segment.transform.position;
        segment.transform.position = destination;
        return prevPos;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            MoveHead(transform.up);
        }
        if (headOnTile) {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveHead(transform.up);
                transform.Rotate(new Vector3(0, 0, 90));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                MoveHead(transform.up);
                transform.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
}
