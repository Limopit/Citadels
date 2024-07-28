using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour
{
    private GameObject selectedObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObj == null)
            {
                RaycastHit hit = castRay();

                if (hit.collider != null)
                {
                    if(!hit.collider.CompareTag("Draggable")) return;

                    selectedObj = hit.collider.gameObject;
                }
                
                
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
                selectedObj.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);

                selectedObj = null;
            }
        }

        if (selectedObj != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
            selectedObj.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);
        }
        
    }

    private RaycastHit castRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
