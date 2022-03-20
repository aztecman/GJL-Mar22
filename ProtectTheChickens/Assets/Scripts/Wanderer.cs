using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPredator;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Wander()
    {
        Vector3[] moveDirections = new Vector3[]
        {
            Vector3.up, Vector3.right, Vector3.down, Vector3.left
        };
        bool[] validDirections = new bool[]
        {
            true, true, true, true
        };
        //N E S W
        if (transform.position.y + 1 > gameManager.maxY ||
            HasObstacle(transform.position + moveDirections[0])) //||
            //HasSnekHead(transform.position + moveDirections[0]))
        {
            validDirections[0] = false;
        }
        if (transform.position.x + 1 > gameManager.maxX ||
            HasObstacle(transform.position + moveDirections[1])) //||
            //HasSnekHead(transform.position + moveDirections[1]))
        {
            validDirections[1] = false;
        }
        if (transform.position.y - 1 < gameManager.minY ||
            HasObstacle(transform.position + moveDirections[2])) //||
            //HasSnekHead(transform.position + moveDirections[2]))
        {
            validDirections[2] = false;
        }
        if (transform.position.x - 1 < gameManager.minX ||
            HasObstacle(transform.position + moveDirections[3])) //||
            //HasSnekHead(transform.position + moveDirections[3]))
        {
            validDirections[3] = false;
        }
        int numValid = 0;
        foreach (bool validDir in validDirections)
        {
            if (validDir) numValid += 1;
        }
        //TODO: avoid predators and don't cross snake's body

        if (numValid > 0)
        {

            int directionChoice = Random.Range(0, numValid);
            for (int i = 0; i < 4; i++)
            {
                if (!validDirections[i]) continue;
                if (directionChoice == 0)
                {
                    transform.position += moveDirections[i];
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, moveDirections[i]);
                    break;
                }
                else
                {
                    directionChoice -= 1;
                }
            }
        }

    }
    bool HasObstacle(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, .6f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag.Contains("Snek"))
            {
                return true;
            }
            if (!isPredator && 
                hitCollider.CompareTag("Consumable"))
            {
                return true;
            }
        }
        return false;
    }

    bool HasSnekHead(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("SnekHead"))
            {
                return true;
            }
        }
        return false;
    }
}
