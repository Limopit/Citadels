using UnityEngine;
using System.Collections;

public class EarthSpinScript : MonoBehaviour {
    public float speed = 10f;

    void Update() {
        Vector3 worldAxis = transform.TransformDirection(Vector3.up);
                
                transform.Rotate(worldAxis, speed * Time.deltaTime, Space.World);
    }
}