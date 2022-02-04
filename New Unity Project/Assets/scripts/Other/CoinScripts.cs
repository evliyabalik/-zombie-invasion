using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScripts : MonoBehaviour
{
    /*
     Bu script dosyasýnda coinlerle ilgili temel olaylar yer alýyor
     */


    [SerializeField] float rotateSpeed=30f;

    [SerializeField] GameObject rootParent;


    //private


    void Start()
    {
        rootParent = transform.parent.parent.gameObject;
        rootParent.transform.Rotate(0, Random.Range(0, 360), 0);
        
        
        //Coroutine
        StartCoroutine(CoinEffect());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(1 * Time.deltaTime * rotateSpeed, 0, 0);
        
    }


    IEnumerator CoinEffect()
    {
        yield return new WaitForSeconds(1.0f);
        transform.parent.GetChild(1).gameObject.SetActive(true);
        
        yield return new WaitForEndOfFrame();
        GetComponent<MeshRenderer>().enabled = false;
        
    }
}
