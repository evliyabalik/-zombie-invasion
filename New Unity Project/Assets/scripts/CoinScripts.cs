using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScripts : MonoBehaviour
{
    [SerializeField] float rotateSpeed=30f;

    [SerializeField] GameObject rootParent;

    void Start()
    {
        rootParent = transform.parent.parent.gameObject;
        rootParent.transform.Rotate(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(1 * Time.deltaTime * rotateSpeed, 0, 0);
        
    }
}
