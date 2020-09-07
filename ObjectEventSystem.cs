using System;
using UnityEngine;

/* Main event handler of created objects in game world. Reacts based on user
 * input for different scenarios. Handles creation and adjustments of object
 * itself and axes it creates.
 */
public class ObjectEventSystem : MonoBehaviour
{
    private Renderer objRenderer;
    private Material original;
    private Material selected;

    private GameObject xAxis;
    private GameObject yAxis;
    private GameObject zAxis;

    private float lineLength = 5f;
    private float dragSpeed = 40f;

    // Initializaation - called when script is loaded
    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        original = objRenderer.material;
        selected = Resources.Load("SelectedMaterial", typeof(Material)) as Material;

        // Events script is assigned to
        ObjectEventManager.OnObjectSelect += OnObjectSelected;
        ObjectEventManager.OnObjectDeselect += OnObjectDeselected;
        ObjectEventManager.OnAxisDrag += OnAxisDragged;
    }

    // Creates X, Y, Z coordinate arrows at the center of object that has been clicked on
    private void DrawAxes()
    {
        Vector3 start = objRenderer.bounds.center;
        if (xAxis == null)
        {
            xAxis = new GameObject();
            xAxis.transform.position = start;
            xAxis.AddComponent<ObjectAxisController>();
            ObjectAxisController controller = xAxis.GetComponent<ObjectAxisController>();
            controller.parentObj = this.gameObject;
            controller.DrawAxis("X", lineLength);
        }

        if (yAxis == null)
        {
            yAxis = new GameObject();
            yAxis.transform.position = start;
            yAxis.AddComponent<ObjectAxisController>();
            ObjectAxisController controller = yAxis.GetComponent<ObjectAxisController>();
            controller.parentObj = this.gameObject;
            controller.DrawAxis("Y", lineLength);
        }

        if (zAxis == null)
        {
            zAxis = new GameObject();
            zAxis.transform.position = start;
            zAxis.AddComponent<ObjectAxisController>();
            ObjectAxisController controller = zAxis.GetComponent<ObjectAxisController>();
            controller.parentObj = this.gameObject;
            controller.DrawAxis("Z", lineLength);
        }

    }

    // Removes X, Y, Z coordinate arrows from scene
    private void ClearAxes()
    {
        Destroy(xAxis);
        Destroy(yAxis);
        Destroy(zAxis);
    }

    // Adjusts selected item texture and adds X, Y, Z coordinate arrows to object
    private void OnObjectSelected(GameObject obj)
    {
        if (String.Equals(obj.transform.name, transform.name))
        {
            objRenderer.material = selected;
            DrawAxes();
        }
    }

    // Adjusts selected item texture to its former and removes X, Y, Z coordinate arrows from object
    private void OnObjectDeselected(GameObject obj)
    {
        if (String.Equals(obj.transform.name, transform.name))
        {
            objRenderer.material = original;
            ClearAxes();
        }
    }

    // Parse user mouse input and moves selected object based on which axis is being interacted with
    private void OnAxisDragged(string name)
    {
        float horizontal;
        float vertical;

        switch (name)
        {
            case "X":
                horizontal = Input.GetAxis("Mouse X");

                if (horizontal > 0)
                {
                    gameObject.transform.Translate(Vector3.right * dragSpeed * Time.deltaTime);
                }

                else if (horizontal < 0)
                {
                    gameObject.transform.Translate(Vector3.right * -dragSpeed * Time.deltaTime);
                }
                break;
            case "Y":
                vertical = Input.GetAxis("Mouse Y");

                if (vertical > 0)
                {
                    gameObject.transform.Translate(Vector3.up * dragSpeed * Time.deltaTime);

                }
                else if (vertical < 0)
                {
                    gameObject.transform.Translate(Vector3.up * -dragSpeed * Time.deltaTime);
                }
                break;
            case "Z":
                horizontal = Input.GetAxis("Mouse X");
                vertical = Input.GetAxis("Mouse Y");

                if (horizontal > 0)
                {
                    gameObject.transform.Translate(Vector3.forward * -dragSpeed * Time.deltaTime);

                }
                else if (horizontal < 0)
                {
                    gameObject.transform.Translate(Vector3.forward * dragSpeed * Time.deltaTime);
                }

                if (vertical > 0)
                {
                    gameObject.transform.Translate(Vector3.forward * dragSpeed * Time.deltaTime);
                }

                else if (vertical < 0)
                {
                    gameObject.transform.Translate(Vector3.forward * -dragSpeed * Time.deltaTime);
                }
                break;
        }
    }
}
