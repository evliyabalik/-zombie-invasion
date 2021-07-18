using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardCanvas : MonoBehaviour
{

    [SerializeField] Camera my_Camera;

    private void Start()
    {
        my_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    void Update()
    {
        transform.LookAt(transform.position + my_Camera.transform.rotation * Vector3.forward, my_Camera.transform.rotation * Vector3.up);
    }
}
