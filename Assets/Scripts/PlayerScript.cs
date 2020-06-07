using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check for click
        if (Input.GetMouseButtonUp(0))
        {
            // cast ray to see what was clicked (within a range)
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                Transform hitTransform = hit.transform;

                // if a tile was hit
                if (hitTransform.ToString().Contains("Terrain"))
                {
                    Stack<Tilemap.Vertex> vertexPath = GameObject.Find("Ground").GetComponent<Tilemap>().findPath(this.transform.position, hit.point);
                    this.GetComponent<Movement>().setVertexPath(vertexPath);
                }
            }
        }
    }
}
