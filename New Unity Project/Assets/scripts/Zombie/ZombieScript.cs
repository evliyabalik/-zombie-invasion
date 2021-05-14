using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    /*
    Bu kod dosyasında zombi ile ilgili kısımlar yer almaktadır
    1- zombi yapay zekası
    a- hedef görme, hedefe gitme
    b- hedefe gelince saldırı yapma
    2- zombi canı ve diğer şeyler
    */

    public float health=100f; // zombi canı
    public float speed;// zombi animasyon hızı
    public bool isDead=false; //ölüp ölmediği ile ilgili değişken
    public bool isStopped=false; //zombinin durup durmadığını kontrol eder


    //Private
    NavMeshAgent nav;
    Animator anim;
    int targetNum;
    [SerializeField]Transform home;// hedef objesi


    //SerializeField
    [SerializeField]Transform house;//ev objesi


    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        nav=GetComponent<NavMeshAgent>();
        home=GameObject.Find("House").transform;


        //Coroutine
        StartCoroutine(isDeath()); // zombi öldüğünde
        StartCoroutine(SelectTarget()); //rasgele evin çevresinden hedef seçme
        StartCoroutine(SpeedOff()); // karakter durduğunda
    }

    // Update is called once per frame
    void Update()
    {
        SpeedWalk(); // yürüme hızı

        if(Vector3.Distance(transform.position,house.position)< 0.47f){ //karakterin hedefe olan mesafesi ölçüülüyor
            speed=0f;
            FaceTarget();
            isStopped=true;
        }
        else{
            speed=1f;
            isStopped=false;
        }


    }

    void Death(){ // Ölüm kodları
        if(health<=0){
            anim.SetTrigger("Dead");
            this.gameObject.tag="Untagged";
            Destroy(this.gameObject, 3);

        }
    }

    void SpeedWalk(){ // zombi yürüme animasyonu hızı belirleniyor
        anim.SetFloat("MoveSpeed",speed);
        if(speed>0f){ // hız 0'dan yüksekse hedefe doğru git
            nav.SetDestination(house.position);
        }
        else{

        }


    }

    void FaceTarget(){
        Vector3 direction=(home.position-transform.position).normalized;
        Quaternion lookRotation=Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation=Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
    }





    IEnumerator isDeath(){ // karakterin ölümü gerçekleştiriliyor
        yield return new WaitForEndOfFrame();
        Death();
        yield return isDeath();
    }

    IEnumerator SelectTarget(){ //rastgele hedef seçimi yap
        yield return new WaitForEndOfFrame(); // her kareden sonra çalıştır

        for (int i=0;i<GameControllerScript.targetTransform.Count;i++){ // targetTransform listesi uzunluğunda döngü oluştur
            if(GameControllerScript.targetTransform[i]!=null && i<=GameControllerScript.targetTransform.Count){ // eğer gelen sayı listede null değilse ve eğer liste uzunluğunu aşmıyorsa ekle
                house = GameControllerScript.targetTransform[i]; // gelen sayıyı ekle
                GameControllerScript.targetTransform.Remove(GameControllerScript.targetTransform[i]); // eklenen hedefi listeden sil
                break; // kodu kes
            }
            else{
                yield return SelectTarget();//değilse eğer bu kodu tekrarla
            }
        }
    }

    IEnumerator SpeedOff(){ // Karakter durduğunda

        yield return new WaitForSeconds(1.0f);
        if(isStopped){ // eğer değişken true ise
             // animasyon hızını kes
            //transform.LookAt(home.transform.position); // Eve doğru bak

            anim.SetTrigger("Attack"); // ve saldır
        }
        else{

        }

        yield return SpeedOff(); // kodu tekrarla


    }


    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag=="home"){
            HomeHealth.homeHealth-=Random.Range(0.1f,0.5f);
            print(HomeHealth.homeHealth);
        }
    }



}
