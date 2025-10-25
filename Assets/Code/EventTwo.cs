using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;
public class EventTwo : MonoBehaviour
{
    public static EventTwo Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Light process")]
    [SerializeField] public GameObject[] Lightbulb;
  

    [Header("Sound")]
    [SerializeField] public AudioSource AudioS;
    [SerializeField] public AudioClip lightsound;

    [Header("Blink Settings")]
    [SerializeField] private int blinkCount = 5;         // จำนวนครั้งที่กระพริบ
    [SerializeField] private float minShort = 0.1f;
    [SerializeField] private float maxShort = 1.1f;

    [SerializeField] private float minInterval = 0.2f;   // เวลาต่ำสุดระหว่างกระพริบ
    [SerializeField] private float maxInterval = 0.9f;     // เวลาสูงสุดระหว่างกระพริบ


    private void Start()
    {


    }


    public void StartEventTwo()
    {
        foreach (GameObject bulb in Lightbulb) //Blink all bulb but  Random make this not the same times
        {

            StartCoroutine(blinkSiglebulb(bulb));

        }
    }
    
    private IEnumerator blinkSiglebulb(GameObject bulb)
    {
        for (int i = 0; i < blinkCount; i++) //Do this 5 times  
        {
            bulb.SetActive(!bulb.activeSelf);
            if (lightsound != null && AudioS != null)
            {
                AudioS.PlayOneShot(lightsound);
            }

            float bulbrandom = Random.Range(minInterval, maxInterval); //Random blinktime to no sync
            float Soundrandom = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(bulbrandom);
            yield return new WaitForSeconds(Soundrandom);

        }



        bulb.SetActive(true);
    }
    
    

}
