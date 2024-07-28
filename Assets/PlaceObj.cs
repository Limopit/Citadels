using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceObj : MonoBehaviour
{
    public Material newMaterial;
    public Material oldMaterial;// Целевая область для перемещения
    public float speed = 1.0f;
    private bool isObjectSelected = false;
    private Vector3 targetPosition;

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
        // Перемещаем объект к целевой области
        while (transform.position != obj.transform.position)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, obj.transform.position, speed * Time.deltaTime);
        }
         // Сбрасываем выбор объекта

    }

    private GameObject findObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject clickedObject = null;

        // Выполнение Raycast и проверка, попал ли луч в коллайдер объекта
        if (Physics.Raycast(ray, out hit))
        {
            // Получение объекта, в который попал луч
            clickedObject = hit.collider.gameObject;
            if (clickedObject.CompareTag("Draggable")) return null;
            //Debug.Log("Clicked on: " + clickedObject.name + " is " + taken_slot._objects[clickedObject]);
            if (taken_slot._objects[clickedObject] == false) taken_slot._objects[clickedObject] = true;
            else if (taken_slot._objects[clickedObject] == true) return null;
            Debug.Log("Clicked on: " + clickedObject.name);

            // Дальнейшие действия с объектом
            // Например, можно выделить объект или применить к нему эффект
        }

        return clickedObject;
    }
}
