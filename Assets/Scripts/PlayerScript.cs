using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Stack<Tilemap.Vertex> vertexPath;
    Vector3? destinationPosition;
    float playerSpeed;
    Animation walkAnimation;
    public Vector3 tileLocation;

    void Start()
    {
        this.destinationPosition = null;
        this.playerSpeed = 2f;
        this.walkAnimation = this.GetComponent<Animation>();
        this.vertexPath = new Stack<Tilemap.Vertex>();
        this.tileLocation = new Vector3(Mathf.Floor(this.transform.position.x), Mathf.Floor(this.transform.position.y), Mathf.Floor(this.transform.position.z));

    }

    // used to compare positioning here (due to float comparison not being exact)
    bool isEqual(float a, float b)
    {
        if (a >= b - .02f && a <= b + .02f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // this will set the path for the player to follow
    public void setVertexPath(Stack<Tilemap.Vertex> _vertexPath)
    {
        this.vertexPath = _vertexPath;
        if (this.vertexPath.Count == 0)
        {
            return;
        }
        Tilemap.Vertex nextVertex = this.vertexPath.Pop();
        this.setDestination(new Vector3(nextVertex.x + .5f, 0, nextVertex.z + .5f));
    }

    // this will be called for each vertex in the path to follow, one after another
    public void setDestination(Vector3? newPosition)
    {
        this.destinationPosition = newPosition;
    }

    void Update()
    {
        // if there is a tile to move to
        if (this.destinationPosition.HasValue)
        {
            // animate walk
            if (!this.walkAnimation.isPlaying)
            {
                this.walkAnimation.Play();
            }

            // adjust change in position for this frame
            Vector3 deltaPosition = new Vector3(0f, 0f, 0f);
            if (!this.isEqual(this.destinationPosition.Value.x, this.transform.position.x))
            {
                deltaPosition.x = ((this.destinationPosition.Value.x - this.transform.position.x) / Mathf.Abs(this.destinationPosition.Value.x - this.transform.position.x)) * this.playerSpeed;
            }
            else
            {
                deltaPosition.x = 0f;
            }
            if (!this.isEqual(this.destinationPosition.Value.z, this.transform.position.z))
            {
                deltaPosition.z = ((this.destinationPosition.Value.z - this.transform.position.z) / Mathf.Abs(this.destinationPosition.Value.z - this.transform.position.z)) * this.playerSpeed;
            }
            else
            {
                deltaPosition.z = 0f;
            }

            // and actually move player and rotate him towards destination
            this.transform.Translate(deltaPosition * Time.deltaTime, Space.World);
            this.transform.LookAt(this.transform.position + deltaPosition);
            
            // if player has reached next tile
            if (this.isEqual(this.destinationPosition.Value.x, this.transform.position.x) && this.isEqual(this.destinationPosition.Value.z, this.transform.position.z))
            {
                this.tileLocation = new Vector3(Mathf.Floor(this.transform.position.x), Mathf.Floor(this.transform.position.y), Mathf.Floor(this.transform.position.z));
                // check if he's reached destination
                if (this.vertexPath.Count == 0)
                {
                    if (this.walkAnimation.isPlaying)
                    {
                        this.walkAnimation.Rewind();
                        this.walkAnimation.Stop();
                    }
                    Debug.Log("Finishing walk");
                    this.destinationPosition = null;
                }
                // if not, queue next tile to walk to
                else
                {
                    Tilemap.Vertex nextVertex = this.vertexPath.Pop();
                    this.setDestination(new Vector3(nextVertex.x + .5f, 0, nextVertex.z + .5f));
                }
            }
        }
    }


}
