using UnityEngine;
using UnityEngine.UI;
public class EyesClosing : MonoBehaviour
{
    [Header("Eyes")]
    public GameObject EyesOff;

    public static EyesClosing Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        EyesOff.SetActive(false);
        
    }


    void Update()
    {
        CloseYourEyes();
    }

    public void CloseYourEyes()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // คลิกซ้าย
        {

            EyesOff.SetActive(true);

        }
        else
        {
            EyesOff.SetActive(false);
        }
    }


}
