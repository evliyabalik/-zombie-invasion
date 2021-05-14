using UnityEngine;

public class HomeHealth : MonoBehaviour
{
    public static float homeHealth=100;
    // Start is called before the first frame update


    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag=="zombie"){
            homeHealth-=Random.Range(0.1f,0.5f);

        }
    }



}
