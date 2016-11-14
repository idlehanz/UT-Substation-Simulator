/*This script controls the rendering of the wires, between wire nodes,
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class WireNode : MonoBehaviour
{

    //a list to hold the nodes that this node will render to.
    public WireNode nextNode;



    public Material wireMaterial;

    //some values for the wire we will render, they will have default values but we can change them in the editor,
    public Color wireColor = Color.gray;
    public float wireWidth = .2f;






    protected LineRenderer lineRenderer;

    //right now the wireNodes are represented in the editor as white blocks,
    //this controls whether those blocks show up at run time.
    public static bool renderMesh = true;

    protected List<GameObject> joints = new List<GameObject>();


    public float wireDrag = .1f;
    public float wireMass = .5f;

    //num verticies per 5 units
    public float vertexDistance = 1;
    protected int numVerticies;

    //this is how far down the line will droop
    public float slack = 2;


    protected bool initialized = false;

    // Use this for initialization
    void Start()
    {
        //should we render the white blocks?
        if (!renderMesh)
            Destroy(gameObject.GetComponent<MeshRenderer>());

        //so long as we have a next node. create the wire, create the line renderer and update the line positions.
        if (nextNode != null)
        {
            setNewNextNode(nextNode);
        }

    }

    public void setNewNextNode(WireNode newNext)
    {
        if (!initialized)
        {
            initialized = true;
            nextNode = newNext;
            createWire();
            //create a line render object
            createLineRenderer();
            updateLinePositions();
        }
        else
        {
            Debug.Log("ignoring reassignment of wire node");
        }

    }

    //a function to create the wire,
    protected void createWire()
    {
        joints = new List<GameObject>();
        float distanceBetweenNodes = Vector3.Distance(transform.position, nextNode.transform.position);
        //numVerticies =(int)( (distanceBetweenNodes / vertexDistance) * vertexDensity);
        numVerticies = (int)(distanceBetweenNodes / vertexDistance);
        Vector3 positionStep = (nextNode.transform.position - transform.position) / numVerticies;
        /*First, create the gameObjects for the joints in the wire,
         we can't assign the spring joints yet because we need to reference the
         nodes before and after each node, and since each node will have 2
         seperate spring joints a search of the gameobject components will only return
         one of the instances, so we will add and then assign these joints after
         we create all of our objects*/
        joints.Add(gameObject);
        for (int i = 1; i < numVerticies; i++)
        {
            joints.Add(new GameObject());//create the object

            //add a rigid body to the new joint and set up some default values.
            Rigidbody rigid = joints[i].AddComponent<Rigidbody>();
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }
            rigid.mass = wireMass;
            rigid.drag = wireDrag;

            //add a collider so that the wire will react when affected by forces.
            SphereCollider col = joints[i].AddComponent<SphereCollider>();
            col.radius = wireWidth;


            //set up the joint position.
            Vector3 jointPosition = transform.position + (positionStep * (i));
            //we want the wire to curve downward, so I cooked up this formula to do just that.
            float y = jointPosition.y - (Mathf.Sin(Mathf.PI * (i - 1) / numVerticies) * slack);
            jointPosition.y = y;
            joints[i].transform.position = jointPosition;


            //last, parent this joint object with the wireNode object,
            joints[i].transform.parent = transform;

        }
        joints.Add(nextNode.gameObject);


        //now that the objects are created we will assign spring joints to the objects
        for (int i = 1; i < numVerticies; i++)
        {
            HingeJoint prevAnchor = joints[i].AddComponent<HingeJoint>();
            //HingeJoint
            prevAnchor.enableCollision = false;
            HingeJoint nextAnchor = joints[i].AddComponent<HingeJoint>();
            nextAnchor.enableCollision = false;
            
            //hook the ancors to the adjacent joints.
            nextAnchor.connectedBody = joints[i - 1].GetComponent<Rigidbody>();
            prevAnchor.connectedBody = joints[i + 1].GetComponent<Rigidbody>();



        }


    }

    //this function creates the line renderer object 
    protected void createLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        if (lineRenderer==null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = wireMaterial;
        lineRenderer.SetColors(wireColor, wireColor);
        lineRenderer.SetWidth(wireWidth, wireWidth);
        lineRenderer.SetVertexCount(joints.Count);
    }

    //this function will udpate the line positions.
    //this will be called once per update,
    public void updateLinePositions()
    {
        //make sure we have a next node.
        if (nextNode != null)
        {
            List<Vector3> points = new List<Vector3>();
            foreach (GameObject g in joints)
            {
                if (g != null)
                {
                    points.Add(g.transform.position);
                }
            }
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateLinePositions();
    }
}
