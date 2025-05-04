using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public Slider loadingBar;
    public Image fadeOverlay;

    public float fakeLoadDuration = 3f;
    public float fadeDuration = 0.5f;

    private string targetScene;

    public void SetTargetScene(string sceneName)
    {
        targetScene = sceneName;
        StartCoroutine(FakeLoadingThenFadeThenLoad());
    }

    IEnumerator FakeLoadingThenFadeThenLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        float elapsed = 0f;
        while (elapsed < fakeLoadDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / fakeLoadDuration);
            loadingBar.value = progress;
            yield return null;
        }

        loadingBar.value = 1f;


        operation.allowSceneActivation = true;
    }
}



