using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeaponState;

public class GameControllerScript : MonoBehaviour
{

    /*
    bu kod dosyasında oyun kontrolleri, algoritması yazılacak
    */

    //SerializeField


    //Public
    public static List<Transform> targetTransform;
    public static int score;




 
    void Start()
    {
        SelectTarget();
       // CoinSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        print(score);
    }

    void SelectTarget()
    {
        targetTransform = new List<Transform>(); // hedef listesini oluştur
        foreach (var i in GameObject.FindGameObjectsWithTag("target"))
        { // tagı target olan bütün objeleri bul
            targetTransform.Add(i.transform); //bulunanların transform bilgilerini listeye ekle
        }
    }


    //Plublic Method UI


    public void Bomb()
    {
        PlayerScript.state = State.Bomb;
        
    }

    public void Weapon()
    {
        PlayerScript.state = State.Weapon;
    }

  
  

    
   
}
