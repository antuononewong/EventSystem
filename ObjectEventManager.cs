using System;
using UnityEngine;

/* Event definitions for different user inputs. Handles pinging
 * main event handler for said inputs.
 */
public class ObjectEventManager : MonoBehaviour
{
    // Events
    public static event Action<GameObject> OnObjectSelect;
    public static event Action<GameObject> OnObjectDeselect;
    public static event Action<string> OnAxisDrag;

    private GameObject currentObject;
    
    // Update is called every frame
    private void Update()
    {
        if (Input.GetButtonDown("LeftClick")) // Single click
        {
            CheckObjectCollision();
        }

        if (Input.GetButton("LeftClick")) // Mouse button held down
        {
            CheckAxisCollision();
        }
     
    }

    // Draws an invisible line from mouse cursor along Z-axis for specified distance. Depending on if
    // mouse cursor is over an object, different events are pinged and appropriate event handlers will
    // be called.
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
                    OnObjectDeselect?.Invoke(currentObject); // If another obj already selected, deselect it
                }

                OnObjectSelect?.Invoke(newObject);
                currentObject = newObject;
            }
            
        }
        else 
        {
            OnObjectDeselect?.Invoke(currentObject);
            currentObject = null;
            
        }

    }

    // Draws an invisible line from mouse cursor along Z-axis for specified distance. If line "hits" an axis,
    // event will be pinged and Object.EventSystem.OnAxisDragged() will be called.
    private void CheckAxisCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Axis")))
        {
            OnAxisDrag?.Invoke(hit.transform.name);
        }

    }

    
}
