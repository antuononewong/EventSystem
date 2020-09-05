using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class ObjectEventManager : MonoBehaviour
{
    public static event Action<GameObject> OnObjectSelect;
    public static event Action<GameObject> OnObjectDeselect;
    public static event Action<string> OnAxisDrag;

    private GameObject currentObject;
    

    public void Update()
    {
        if (Input.GetButtonDown("LeftClick"))
        {
            CheckObjectCollision();
        }

        if (Input.GetButton("LeftClick"))
        {
            CheckAxisCollision();
        }
     
    }

    private void CheckObjectCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Object")))
        {
            GameObject newObject = hit.transform.gameObject;
            if (!String.Equals(newObject, currentObject))
            {
                if (currentObject != null)
                {
                    OnObjectDeselect?.Invoke(currentObject); // if another obj already selected, deselect it
                }

                OnObjectSelect?.Invoke(newObject);
                currentObject = newObject;
            }
            
        }
        else 
        {
            //Debug.Log("Didn't click UI");
            OnObjectDeselect?.Invoke(currentObject);
            currentObject = null;
            
        }

    }

    private void CheckAxisCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Axis")))
        {
            OnAxisDrag?.Invoke(hit.transform.name);
        }

    }

    
}
