using UnityEngine;

/* Script that handles creation of X, Y, Z coordinate lines. Sets various
 * properties like dimensions, position, etc...
 */
public class ObjectAxisController : MonoBehaviour
{
    public GameObject parentObj;

    private LineRenderer lineRenderer;
    private BoxCollider boxCollider;

    private readonly float width = 0.3f;
    private string axis;

    // Initialization - called when script is loaded
    private void Awake()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
    }

    // Draw a line based on input and sets default properties
    public void DrawAxis(string name, float lineLength)
    {
        axis = name;
        gameObject.transform.name = name;
        gameObject.layer = 8;
        SetLineProperties();
        DrawLine(lineLength);
    }

    // Set dimensions and positions of the drawn line and collider
    private void DrawLine(float lineLength)
    {

        switch (axis)
        {
            case "X":
                lineRenderer.SetPosition(1, Vector3.right * lineLength);
                boxCollider.center = new Vector3(lineLength/2, 0, 0);
                boxCollider.size = new Vector3(lineLength, 1f, 1f);
                break;
            case "Y":
                lineRenderer.SetPosition(1, Vector3.up * lineLength);
                boxCollider.center = new Vector3(0, lineLength/2, 0);
                boxCollider.size = new Vector3(1f, lineLength, 1f);
                break;
            case "Z":
                lineRenderer.SetPosition(1, Vector3.forward * lineLength);
                boxCollider.center = new Vector3(0, 0, lineLength / 2);
                boxCollider.size = new Vector3(1f, 1f, lineLength);
                break;
        }
       
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        gameObject.transform.parent = parentObj.transform;

    }

    // Set other default properties of drawn line
    private void SetLineProperties() 
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));

        switch (axis)
        {
            case "X":
                lineRenderer.material.color = Color.red;
                break;
            case "Y":
                lineRenderer.material.color = Color.green;
                break;
            case "Z":
                lineRenderer.material.color = Color.blue;
                break;
        }
    }
    
}
