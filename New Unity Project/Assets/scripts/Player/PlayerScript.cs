using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using WeaponState;


public class PlayerScript : MonoBehaviour
{

    /*
    Bu kod dosyası hedef göstergesi (yani player)'nin kodlarını barındırır
    // parmak yönüne doğru hareket
    // parmak algılaması
    // ekran sınırları
    // Ateş etme komutu
    */

   


    [SerializeField]float speed; // hedef göstergersinin parmağı takip edeceği hız
    [SerializeField]Vector3 offsetFin; // bunları ekledim ama kullanmadım kullanmak istersen raycast çizgisinin bakış yönünü değiştiriyor
    [SerializeField]Vector3 offsetStart;  // bu da kullanmıyorum kullanmak istersen raycast çizgisinin doğduğu yeri kaydırabiliyorsun
    [SerializeField] GameObject Enemy; // zaten bu düşman

    float fireTime=2f; // ateş süresi (bunu eklememin sebebi silahı güçlendirdikçe ateş hızını değiştirecem)
    public static State state;

    //public 
    public static Vector3[] deadEnemyPos;

  

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("FireisStart");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0){ // parmak sayısı 0'ın üstündeyse
            Touch parmak=Input.GetTouch(0); // 1. parmağı algıla
            if (state == State.Weapon)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
                if (parmak.phase == TouchPhase.Moved)
                { // parmak hareket ediyorsa
                    Vector2 vec = Camera.main.ScreenToViewportPoint(parmak.deltaPosition); // kamera görüş alanı içersinden sürükleme payını hesapla

                    transform.Translate(vec * Time.deltaTime * speed); // hedef göstergesini hareket ettir
                                                                       //print(transform.position);

                    Boundary(); // ekran sınırlar / hareket edebileceği alan sınırı

                    RaycastFunc(); // raycast oluştur
                }

                print("Silah Aktif");
            }

            if (state == State.Bomb)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;

                print("Bomba Aktif");
            }
           
        }



        print(state);


    }



   


    void Boundary(){  // Sınır çizen metod
        if (transform.position.x > 4.45f){
            transform.position=new Vector3(4.45f,transform.position.y,transform.position.z);
        }

        if (transform.position.x < -4.45){
            transform.position=new Vector3(-4.45f,transform.position.y,transform.position.z);
        }

        if (transform.position.z > 4.69f){
            transform.position=new Vector3(transform.position.x,transform.position.y,4.69f);
        }

        if (transform.position.z < -11.3f){
            transform.position=new Vector3(transform.position.x,transform.position.y,-11.3f);
        }
    }


    void RaycastFunc(){
        RaycastHit hit;
        Debug.DrawRay(transform.position-offsetStart,transform.forward-offsetFin,Color.green);
        
        if(Physics.Raycast(transform.position-offsetStart,transform.forward-offsetFin, out hit,1000,1<<LayerMask.NameToLayer("Target"))){
            if(hit.collider.tag=="zombie"){
                GetComponent<Renderer>().material.SetColor("_BaseColor",Color.green); // hedef zombiye geldiğinde yeşil olsun
                Enemy = hit.collider.transform.gameObject;
               // fire=FireisStart();
                if (hit.collider.transform.gameObject.GetComponent<ZombieScript>().health > 0)
                { // zombi canı eğer 0'ın üstündeyse
                    hit.collider.transform.gameObject.GetComponent<ZombieScript>().isDead = true;// ölümü true olarak gönder
                    
                }

                else
                {
                    hit.collider.transform.gameObject.GetComponent<ZombieScript>().isDead = false;// aksi halde ölümü false yap
                    
                }



            }
            else{
                
                GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white); // zombiden ayrıldığında beyaz olsun
                Enemy = null;
                hit.collider.transform.gameObject.GetComponent<ZombieScript>().isDead = false;
                
            }

        }
        else
        {
            if (Enemy!=null)
            {
                Enemy.GetComponent<ZombieScript>().isDead = false;
            }
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white); // zombiden ayrıldığında beyaz olsun
            Enemy = null;
        }
    }




    IEnumerator FireisStart(){
        
        while (true)
        {
            yield return new WaitForSeconds(fireTime);
            if (Enemy != null)
            {
                if (Enemy.GetComponent<ZombieScript>().isDead)
                {
                    Enemy.GetComponent<ZombieScript>().health -= 100;
                }
            }
         
        }

       
    }



    
    


}

