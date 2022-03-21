using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LogicTests : MonoBehaviour
{
    bool[,] snekCells; //cells occupied by snake
    bool[,] floodArr;
    // Start is called before the first frame update
    void Start()
    {

        //size of snakeCells includes an empty buffer around the game board (14x14 is used to represent a 12x12 board)
        snekCells = new bool[12, 12];
        for (int i = 0; i < snekCells.GetLength(0); i++)
        {
            for (int j = 0; j < snekCells.GetLength(1); j++)
            {
                snekCells[i, j] = false;
            }
        }
        //{
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, true, true, true, true, true, false, false, false, false, false},
        //    {false, false, false, false, true, false, false, false, true, false, false, false, false, false},
        //    {false, false, false, false, true, false, false, false, true, false, false, false, false, false},
        //    {false, false, false, false, true, true, true, true, true, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false, false, false, false, false, false}
        //};

        //flood fill test: success!!
        int trueCount = 0;
        FloodArray(snekCells);
        for (int i = 0; i < floodArr.GetLength(0); i++)
        {
            for (int j = 0; j < floodArr.GetLength(1); j++)
            {
                if (floodArr[i, j]) trueCount += 1;
            }
        }
        Debug.Log("True Count: " + trueCount);
    }

    void FloodArray(bool[,] originalArray)
    {
        floodArr = originalArray.Clone() as bool[,];

        FloodFill(new Vector2Int(0, 0));
    }

    void FloodFill(Vector2Int nodeIndex)
    {
        //Debug.Log("flooding index: " + nodeIndex);
        // [based on wiki article: Flood_Fill]

        if (nodeIndex.x < 0 || nodeIndex.x >= floodArr.GetLength(0) ||
           nodeIndex.y < 0 || nodeIndex.y >= floodArr.GetLength(1))
        {
            return; //if index out of bounds, return
        }

        if (floodArr[nodeIndex.x, nodeIndex.y])
        {
            return; //if index is already filled, return
        }
        else
        {
            floodArr[nodeIndex.x, nodeIndex.y] = true; // 'fill' unfilled index
        }

        FloodFill(nodeIndex + Vector2Int.down);
        FloodFill(nodeIndex + Vector2Int.up);
        FloodFill(nodeIndex + Vector2Int.left);
        FloodFill(nodeIndex + Vector2Int.right);
    }

    public bool[,] GetLoopedCells()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                snekCells[i, j] = CellHasSnek(i, j);
            }
        }

        FloodArray(snekCells);

        bool[,] finalArr = new bool[12, 12];
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                finalArr[i, j] = !floodArr[i, j];
            }
        }
        return finalArr;
    }

    bool CellHasSnek(int col, int row)
    {
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(col - 6.5f, 6.5f - row), 0.2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag.Contains("Snek"))
            {
                return true;
            }
        }
        return false;
    }
}
