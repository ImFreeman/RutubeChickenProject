using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestTwo : MonoBehaviour
{
    [SerializeField] private Vector3 value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(value.x, value.y, value.z, 1f);
    }
}
