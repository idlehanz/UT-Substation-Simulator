/*This script controls the rendering of the wires, between wire nodes,
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class WireNode : MonoBehaviour {

    public List<WireNode> nodes;
    //we will treat the wirenodes as a single linked list,
    //each node will be responsable for rendering the wire to the node in front of it,
    //in order to do that we need a reference to the next node, this reference will be set in the editor.
    public WireNode nextNode;
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
        //do we have a next node? if so create a line renderer
        if (nextNode != null)
        {            
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.material = wireMaterial;
            lineRenderer.SetColors(wireColor, wireColor);
            lineRenderer.SetWidth(wireWidth, wireWidth);
            lineRenderer.SetVertexCount(2);


            
            //now render the wire.
            //NOTE: right now I'm only setting 2 verticies for the line, the start and end position,
            //it makes for a very rigid looking wire, 
            Vector3[] points = new Vector3[numVertexes];
            points[0] = transform.position;
            points[numVertexes - 1] = nextNode.transform.position;
            for (int i = 1; i < numVertexes - 1; i++)
            {
                //do math for other verticies.
            }
            lineRenderer.SetPositions(points);


        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
