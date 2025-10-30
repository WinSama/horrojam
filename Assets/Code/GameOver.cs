using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
public class GameOver : MonoBehaviour
{

    public static GameOver instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("GameObject")]
    public GameObject GhoseHand;
    public GameObject GhostPrefab;
    public Transform GhostSpawn;
    public Transform targetPos;
    public GameObject Died;
    public float movespeed = 2;
   
    [Header("Sound")]
    public AudioSource GhostAudioSource;
    public AudioClip GhostAudioClip;

    public void StartHorror()
    {
        StartCoroutine(FailScence());
    }


    private IEnumerator FailScence()
    {
        yield return new WaitForSeconds(2f);
        GhoseHand.SetActive(true);

        yield return new WaitForSeconds(5f);

        if (GhostPrefab != null)
        {
            GameObject G = Instantiate(GhostPrefab, GhostSpawn.position, GhostSpawn.rotation);

            // ✅ เคลื่อนที่ไปยัง targetPos อย่างนุ่มนวล
            while (Vector3.Distance(G.transform.position, targetPos.position) > 0.1f)
            {
                G.transform.position = Vector3.MoveTowards(
                    G.transform.position,
                    targetPos.position,
                    movespeed * Time.deltaTime
                );
                yield return null;
            }

            GhostAudioSource.PlayOneShot(GhostAudioClip);

            FailGame();


        }


        
    }

    public void FailGame()
    {
        Died.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}