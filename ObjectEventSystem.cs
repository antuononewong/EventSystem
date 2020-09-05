using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        original = objRenderer.material;
        selected = Resources.Load("SelectedMaterial", typeof(Material)) as Material;

        ObjectEventManager.OnObjectSelect += OnObjectSelected;
        ObjectEventManager.OnObjectDeselect += OnObjectDeselected;
        ObjectEventManager.OnAxisDrag += OnAxisDragged;
    }

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

    private void ClearAxes()
    {
        Destroy(xAxis);
        Destroy(yAxis);
        Destroy(zAxis);
    }

    private void OnObjectSelected(GameObject obj)
    {
        if (String.Equals(obj.transform.name, transform.name))
        {
            objRenderer.material = selected;
            DrawAxes();
        }
    }

    private void OnObjectDeselected(GameObject obj)
    {
        if (String.Equals(obj.transform.name, transform.name))
        {
            objRenderer.material = original;
            ClearAxes();
        }
    }

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
