using UnityEngine;

public class SequentRandomEvent : MonoBehaviour
{
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //startEventOne
        {
            Debug.Log("Event One starts");
            EventOne.Instance.GuardEvent();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Event Two starts");
            EventTwo.Instance.StartEventTwo();
        }
    }
}
