using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LogicTests : MonoBehaviour
{
    bool[,] snakeCells; //cells occupied by snake
    bool[,] arrayToFlood;
    // Start is called before the first frame update
    void Start()
    {

        //size of snakeCells includes an empty buffer around the game board (14x14 is used to represent a 12x12 board)
        snakeCells = new bool[,] {
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, true, true, true, true, true, false, false, false, false, false},
            {false, false, false, false, true, false, false, false, true, false, false, false, false, false},
            {false, false, false, false, true, false, false, false, true, false, false, false, false, false},
            {false, false, false, false, true, true, true, true, true, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false}
        };

        //flood fill test: success!!
        int trueCount = 0;
        FloodArray(snakeCells);
        for (int i = 0; i < arrayToFlood.GetLength(0); i++) {
            for (int j = 0; j < arrayToFlood.GetLength(1); j++) {
                if (arrayToFlood[i, j]) trueCount += 1;
            }
        }
        Debug.Log("True Count: " + trueCount);
    }

    void FloodArray(bool[,] originalArray) {
        arrayToFlood = originalArray.Clone() as bool[,];

        FloodFill(new Vector2Int(0, 0));
    }

    void FloodFill(Vector2Int nodeIndex) {
        //Debug.Log("flooding index: " + nodeIndex);
        // [based on wiki article: Flood_Fill]

        if (nodeIndex.x < 0 || nodeIndex.x >= arrayToFlood.GetLength(0) ||
           nodeIndex.y < 0 || nodeIndex.y >= arrayToFlood.GetLength(1)) 
        {
            return; //if index out of bounds, return
        }

        if (arrayToFlood[nodeIndex.x, nodeIndex.y])
        {
            return; //if index is already filled, return
        }
        else {
            arrayToFlood[nodeIndex.x, nodeIndex.y] = true; // 'fill' unfilled index
        }

        FloodFill(nodeIndex + Vector2Int.down);
        FloodFill(nodeIndex + Vector2Int.up);
        FloodFill(nodeIndex + Vector2Int.left);
        FloodFill(nodeIndex + Vector2Int.right);
    }
}
