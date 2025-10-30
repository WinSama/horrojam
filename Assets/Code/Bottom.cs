using UnityEngine;
using UnityEngine.SceneManagement;
public class Bottom : MonoBehaviour
{
    public void ClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
