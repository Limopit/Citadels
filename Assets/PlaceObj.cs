using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceObj : MonoBehaviour
{
    public Material newMaterial;
    public Material oldMaterial;// Целевая область для перемещения
    public float speed = 1.0f;
    private float delay = 2f;
    private bool isObjectSelected = false;
    private Vector3 targetPosition;
    private GameObject desk = null;

    private void Start()
    {
        desk = GameObject.Find("Desk");
        
    }

    void Update()
    {
        // Проверяем, был ли клик по объекту
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    // Объект был выбран
                    isObjectSelected = true;
                    gameObject.GetComponent<Renderer>().material = newMaterial;
                }
                else if (isObjectSelected && Input.GetMouseButtonDown(0))
                {
                    
                    GameObject obj = findObj();
                    
                    if (obj != null && obj.CompareTag("Destinations"))
                    {
                        targetPosition = new Vector3(obj.transform.position.x, obj.transform.position.z,
                            transform.position.z);
                        //Debug.Log("tyu");
                        MoveToTargetArea(obj);
                    }
                    else if (obj != null && obj.CompareTag("Draggable"))
                    {
                        
                        slotSwap(gameObject, obj);
                        
                    }
                    isObjectSelected = false;
                    gameObject.GetComponent<Renderer>().material = oldMaterial;
                    // Если объект был выбран и клик произошел по целевой области
                    //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
                }
            }
        }
    }

    void MoveToTargetArea(GameObject obj)
    {
            Vector3 targetPos = obj.transform.position;
            //transform.rotation = obj.transform.rotation;
        // Перемещаем объект к целевой области
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
         // Сбрасываем выбор объекта

    }

    private GameObject findObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        GameObject clickedObject = null;
        GameObject slotObject = null;

        // Выполнение Raycast и проверка, попал ли луч в коллайдер объекта
        if (Physics.Raycast(ray))
        {
            hits = Physics.RaycastAll(ray);
            // Получение объекта, в который попал луч
            clickedObject = hits[0].collider.gameObject;
            if (!clickedObject.CompareTag("Destinations")) return null;
            slotObject = hits[1].collider.gameObject; 
            //Debug.Log(clickedObject + " " + slotObject + " ");
            if (taken_slot._objects.ContainsKey(clickedObject) && slotObject == desk)
            {
                if (checkDesk())
                {
                    return clickedObject;
                }
                if (taken_slot._objects[clickedObject] == false) taken_slot._objects[clickedObject] = true;
                else if (taken_slot._objects[clickedObject] == true) return null;
            }
            else 
            {
                if (checkDesk()) {
                    return slotObject;
                }
            }
        }

        return clickedObject;
    }

    private void slotSwap(GameObject original, GameObject replaceable)
    {
        Vector3 tempPosition = original.transform.position;
        Transform tempParent = original.transform.parent;

        // Перемещаем obj1 на место obj2
        original.transform.position = replaceable.transform.position;
        original.transform.parent = replaceable.transform.parent;

        // Перемещаем obj2 на место obj1
        replaceable.transform.position = tempPosition;
        replaceable.transform.parent = tempParent;
        Debug.Log(original.name + " " + replaceable.name);
        
        //MoveToTargetArea(original);
    }

    private bool checkDesk()
    {
        Bounds descBounds = desk.GetComponent<Renderer>().bounds;

        return (transform.position.x >= descBounds.min.x && transform.position.x <= descBounds.max.x &&
            transform.position.z >= descBounds.min.z && transform.position.z <= descBounds.max.z) ;
    }
}
