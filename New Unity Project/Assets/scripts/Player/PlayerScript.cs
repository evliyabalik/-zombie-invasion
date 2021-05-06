using System.Collections;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    [SerializeField]float speed;
    [SerializeField]Vector3 offsetFin;
    [SerializeField]Vector3 offsetStart;
    [SerializeField] GameObject Enemy;

    float fireTime=2f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireisStart());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0){
            Touch parmak=Input.GetTouch(0);

            if(parmak.phase==TouchPhase.Moved){
                Vector2 vec=Camera.main.ScreenToViewportPoint(parmak.deltaPosition);

                transform.Translate(vec*Time.deltaTime*speed);
                //print(transform.position);

                Boundary();

                RaycastFunc();
            }
        }




    }



    void Boundary(){
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

        if(Physics.Raycast(transform.position-offsetStart,transform.forward-offsetFin, out hit,1000)){
            if(hit.collider.tag=="zombie"){
                GetComponent<Renderer>().material.SetColor("_BaseColor",Color.green);

                if(Enemy.GetComponent<ZombieScript>().health>0)
                    Enemy.GetComponent<ZombieScript>().isDead=true;

                else
                    Enemy.GetComponent<ZombieScript>().isDead=false;


            }
            else{
                GetComponent<Renderer>().material.SetColor("_BaseColor",Color.white);


            }

        }
    }




    IEnumerator FireisStart(){
        yield return new WaitForSeconds(2f);
        if(Enemy.GetComponent<ZombieScript>().isDead && Enemy!=null){
            Enemy.GetComponent<ZombieScript>().health-=100f;
        }
        

        yield return FireisStart();
    }



}

