using UnityEngine.SceneManagement;
using UnityEngine;

public class BackScene : MonoBehaviour
{
    public void BackMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
