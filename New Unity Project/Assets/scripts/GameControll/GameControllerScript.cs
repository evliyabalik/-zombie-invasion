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
    [SerializeField] GameObject weapon, bomb;

    [SerializeField] Button bombButton;

    //Public
    public static List<Transform> targetTransform;
    public static int score;

    public static int isBombLeft =5;


    public static State state;





    void Start()
    {
        SelectTarget();
        bomb.SetActive(false);
        StartCoroutine(isBombActive());


        // CoinSpawner();
    }

    // Update is called once per frame
    void Update()
    {


        if (state == State.Weapon)
        {
            bomb.SetActive(false);
            weapon.SetActive(true);
        }


        if (state == State.Bomb)
        {
            bomb.SetActive(true);
            weapon.SetActive(false);
        }



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

        StartCoroutine(BombActive());
    }

    public void Weapon()
    {
        StartCoroutine(TurnOffBomb());
    }


    IEnumerator BombActive()
    {
        yield return new WaitForEndOfFrame();
        state = State.Bomb;
    }

    IEnumerator TurnOffBomb()
    {
        yield return new WaitForEndOfFrame();
        state = State.Weapon;
        BombScripts.mousePos = Vector3.zero;
    }

    IEnumerator isBombActive()
    {
       
        yield return new WaitForSeconds(1.0f);

        if (!BombScripts.isPlayingBomb)
        {
            BombScripts.isPlayingBomb = true;
            bombButton.interactable = false;

            if (isBombLeft > 0)
            {
                isBombLeft--;
            }
            else
            {
                isBombLeft = 0;
            }
        }
        else
        {
            bombButton.interactable = true;
        }

        yield return isBombActive();
    }





}
