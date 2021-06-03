using UnityEngine;
using UnityEngine.UI;

public class HomeHealth : MonoBehaviour
{
    public static float homeHealth=100;

    [Header("Sprite")]
    [SerializeField] Image houseBar;

    [Header("Text")]
    [SerializeField] Text LifeText;

    // Start is called before the first frame update

    void Update()
    {
        HouseBar();
    }

    void HouseBar()
    {
        houseBar.fillAmount = homeHealth / 100;

        if(homeHealth==100f)
            LifeText.text = "%" + homeHealth.ToString("f0");
        else
            LifeText.text = "%" + homeHealth.ToString("f2");
    }




}
