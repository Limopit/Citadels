using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taken_slot : MonoBehaviour
{
    

    public static Dictionary<GameObject, bool> _objects;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go1 = GameObject.Find("Canvas/Ui_card_slot/Card_slot");
        GameObject go2 = GameObject.Find("Canvas/Ui_card_slot (1)/Card_slot");
        GameObject go3 = GameObject.Find("Canvas/Ui_card_slot (2)/Card_slot");
        GameObject go4 = GameObject.Find("Canvas/Ui_card_slot (3)/Card_slot");
        GameObject go5 = GameObject.Find("Canvas/Ui_card_slot (4)/Card_slot");
        GameObject go6 = GameObject.Find("Canvas/Ui_card_slot (5)/Card_slot");
        GameObject go7 = GameObject.Find("Canvas/Ui_card_slot (6)/Card_slot");
        _objects = new Dictionary<GameObject, bool>();
        _objects.Add(go1, false);
        _objects.Add(go2, false);
        _objects.Add(go3, false);
        _objects.Add(go4, false);
        _objects.Add(go5, false);
        _objects.Add(go6, false);
        _objects.Add(go7, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
