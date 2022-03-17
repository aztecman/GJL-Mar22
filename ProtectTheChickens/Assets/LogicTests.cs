using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTests : MonoBehaviour
{
    bool[,] snakeCells;
    // Start is called before the first frame update
    void Start()
    {

        //size of snakeCells includes an empty buffer around the game board (14x14 is used to represent a 12x12 board)
        snakeCells = new bool[,] {
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false}
        };

        //add a snake to the boolean array
        //TODO: add method based on floodfill algorithm
        //TODO: test floodfill algorithm
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetFloodedArray(bool[,] arrayToFlood) {
        /* [from wiki article: Flood_Fill]
         Flood-fill (node):
 1. If node is not Inside return.
 2. Set the node
 3. Perform Flood-fill one step to the south of node.
 4. Perform Flood-fill one step to the north of node
 5. Perform Flood-fill one step to the west of node
 6. Perform Flood-fill one step to the east of node
 7. Return.
         */
    }
}
