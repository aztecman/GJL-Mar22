using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    public GameObject[] bodySegments;
    [SerializeField] GameObject tail;
    [SerializeField] float moveIncrement;
    // Start is called before the first frame update
    void Start()
    {
        //MoveHead();
    }

    void MoveHead() {
        Vector3 prevPos = transform.position;
        transform.position += transform.up * moveIncrement;

        //Move Body
        foreach (GameObject bodySeg in bodySegments) {
            prevPos = MoveBodySegment(bodySeg, prevPos);
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
        if (Input.GetMouseButtonDown(0)) {
            MoveHead();
        }
    }
}
