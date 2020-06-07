using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementScript : MonoBehaviour
{
    public int xWalkRange;
    public int zWalkRange;
    private Vector3 initialPosition;
    public int walkFrequency;
    private float timer;
    private float timeToWalk;

    // Start is called before the first frame update
    void Start()
    {
        this.timer = 0;
        this.timeToWalk = (float)Random.Range(8, 15);
        this.initialPosition = this.transform.position;
        Debug.Log(this.initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= this.timeToWalk)
        {
            this.timer = 0;
            this.timeToWalk = (float)Random.Range(5, 10);
            Debug.Log("Hey, I should move now");
            int lowerXLimit = (int)initialPosition.x - xWalkRange;
            int upperXLimit = (int)initialPosition.x + xWalkRange;
            int lowerZLimit = (int)initialPosition.z - zWalkRange;
            int upperZLimit = (int)initialPosition.z + zWalkRange;
            Vector3 newDestination = new Vector3((float)Random.Range(lowerXLimit, upperXLimit), 0f, (float)Random.Range(lowerZLimit, upperZLimit));
            Debug.Log(newDestination);
            Stack<Tilemap.Vertex> vertexPath = GameObject.Find("Ground").GetComponent<Tilemap>().findPath(this.transform.position, newDestination);
            GetComponent<Movement>().setVertexPath(vertexPath);
        }
    }
}
