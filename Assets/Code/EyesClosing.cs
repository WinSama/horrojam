using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
        StartCoroutine(EyesCloseCoolDown(5f));
    }

    

    IEnumerator EyesCloseCoolDown (float sec)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) // คลิกซ้าย
        {

            EyesOff.SetActive(true);
            yield return new WaitForSeconds(sec);
            EyesOff.SetActive(false);

        }
        
    }
}
