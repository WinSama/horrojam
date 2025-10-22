using UnityEngine;

public class HoldKey : MonoBehaviour
{
    public GameObject Arm;

    void Start()
    {
        Arm.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
        {
            Arm.SetActive(true);
        }
        else Arm.SetActive(false);
        
    }
}
