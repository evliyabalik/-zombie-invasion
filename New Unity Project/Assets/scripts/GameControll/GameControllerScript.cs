using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{

    /*
    bu kod dosyasında oyun kontrolleri, algoritması yazılacak
    */

    public static List<Transform> targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform=new List<Transform>(); // hedef listesini oluştur
        foreach (var i in GameObject.FindGameObjectsWithTag("target")){ // tagı target olan bütün objeleri bul
            targetTransform.Add(i.transform); //bulunanların transform bilgilerini listeye ekle
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
