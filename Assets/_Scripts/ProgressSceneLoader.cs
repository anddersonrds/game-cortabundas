using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProgressSceneLoader : MonoBehaviour
{
    [SerializeField]
    private Text progressText;
    [SerializeField]
    private Slider slider;

    private AsyncOperation operation;       

    public void LoadScene(string sceneName)
    {
        slider.gameObject.SetActive(true);
        UpdateProgressUI(0);
        StartCoroutine(BeginLoad(sceneName));
    }

    private IEnumerator BeginLoad(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }
        UpdateProgressUI(operation.progress);
        operation = null;
    }

    private void UpdateProgressUI(float progress)
    {
        slider.value = progress;
        progressText.text = (int)(progress * 100) + "%";
    }
    
}
