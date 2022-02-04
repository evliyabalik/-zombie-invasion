using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BombScripts : MonoBehaviour
{
    [SerializeField] float overlapRadius=5; //Etki yar� �ap�
    [SerializeField] float force=700; //F�rlatma etkisi
    [SerializeField] Collider[] colliders; //Collider Dizisi
    [SerializeField] float bombDamage; //Bomban�n zombi �zerindeki etki oran�

    public static Vector3 mousePos;

    public static bool isPlayingBomb = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }


  



    void LateUpdate()
    {

        if (Input.GetMouseButton(0) && GameControllerScript.isBombLeft>0 && isPlayingBomb) //Mouse sol tu� t�klan�rsa konum bilgilerine g�re bombay� ta��yor ve eylemi ger�ekle�tiriyor.
        { 

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isPlayingBomb = false;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePos.x, 0, mousePos.z);

                if (!transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().isPlaying)
                {
                    transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                    transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                    transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                }

                Boom();
            }

        }
       

    }


    void Boom()
    {// Bomba fonksiyonu
        if (transform.GetChild(0).gameObject.activeSelf) // Burada patlama particle'i a��k ise devreye giriyor
        {
            colliders = Physics.OverlapSphere(transform.position, overlapRadius,1<<6);
            
            foreach(Collider coll in colliders)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                
                //Rigidbody yok ise eklemek i�in sorgulan�yor
                if (rb==null)
                {
                    rb = coll.gameObject.AddComponent<Rigidbody>();
                    
                }

                
                //Bomba patlamas� ve etki eylemleri burada yap�l�yor
                if (rb!=null && rb.gameObject.tag!="Ground")
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    rb.constraints = RigidbodyConstraints.FreezePositionY;
                    rb.AddExplosionForce(force, transform.position, overlapRadius,0,ForceMode.Force);
                    rb.transform.gameObject.GetComponent<ZombieScript>().isDead = true;
                    rb.transform.gameObject.GetComponent<ZombieScript>().speed = 0;
                    if (rb.transform.gameObject.GetComponent<ZombieScript>().isDead)
                    {
                        rb.transform.gameObject.GetComponent<ZombieScript>().health -= bombDamage;
                        rb.transform.gameObject.GetComponent<ZombieScript>().isDead = false;
                    }

                    //Y�r�me h�z� ve animasyon ayarlan�yor
                    if (transform.GetChild(0).gameObject.activeSelf)
                    {
                        rb.transform.gameObject.GetComponent<ZombieScript>().speed = 0;
                        rb.transform.gameObject.GetComponent<Animator>().SetTrigger("isShot");
                    }
                    else
                    {
                        rb.transform.gameObject.GetComponent<ZombieScript>().speed = 1;
                    }
                }
            }

            RemoveArrayElement();

          
        }
    }

    void RemoveArrayElement() // Array i�ine al�nm�� Rigidbody elementlerini temizliyor
    {
        foreach (var item in colliders)
        {
            Destroy(item.GetComponent<Rigidbody>(), 1f);
        }
    }

   
}