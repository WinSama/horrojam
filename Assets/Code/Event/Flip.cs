using UnityEngine;

public class Flip : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
