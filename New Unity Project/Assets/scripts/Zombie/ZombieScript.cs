using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{

    public float health=100f;
    public float speed;
    public bool isDead=false;

    //Private
    NavMeshAgent nav;
    Animator anim;

    //SerializeField
    [SerializeField]Transform house;


    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        nav=GetComponent<NavMeshAgent>();


        //Coroutine
        StartCoroutine(isDeath());
    }

    // Update is called once per frame
    void Update()
    {
        SpeedWalk();

    }

    void Death(){
        if(health<=0){
            anim.SetTrigger("Dead");
            this.gameObject.tag="Untagged";
            Destroy(this.gameObject, 3);

        }
    }

    void SpeedWalk(){
        anim.SetFloat("MoveSpeed",speed);
        if(speed>0f){
            nav.destination=house.position;
        }


    }



    IEnumerator isDeath(){
        yield return new WaitForEndOfFrame();
        Death();
        yield return isDeath();
    }
}
