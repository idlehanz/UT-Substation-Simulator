/*This script controls the rendering of the wires, between wire nodes,
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class WireNode : MonoBehaviour {

    //a list to hold the nodes that this node will render to.
    public List<WireNode> nodes;
    


    public Material wireMaterial;

    //some values for the wire we will render, they will have default values but we can change them in the editor,
    public Color wireColor = Color.gray;
    public float wireWidth = .2f;

    //later, if we want to add sagging we will recalculate, but for now we just have 2 points, the start and end point.
    protected int numVertexes = 2;

    

    
    protected LineRenderer lineRenderer;

    //right now the wireNodes are represented in the editor as white blocks,
    //this controls whether those blocks show up at run time.
    public static bool renderMesh = true;


    // Use this for initialization
    void Start () {
        //should we render the white blocks?
        if (!renderMesh)            
            Destroy(gameObject.GetComponent<MeshRenderer>());
        

        numVertexes = 0;//keep track of how many verts we are adding
        List<Vector3> points = new List<Vector3>();//a list to hold the points
        foreach (WireNode n in nodes)//loop through the list.
        {
            //make sure this element isn't null to avoid errors
            if (n != null)
            {
                //add the start point, this wire nodes position
                points.Add(transform.position);
                numVertexes++;
                for (int i = 0; i < 1; i++)
                {
                    //do math for other verticies
                }
                //add the final position.
                points.Add(n.transform.position);
                numVertexes++;
            }
        }



        //create a line render object
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = wireMaterial;
        lineRenderer.SetColors(wireColor, wireColor);
        lineRenderer.SetWidth(wireWidth, wireWidth);
        lineRenderer.SetVertexCount(numVertexes);
        lineRenderer.SetPositions(points.ToArray());
       
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
