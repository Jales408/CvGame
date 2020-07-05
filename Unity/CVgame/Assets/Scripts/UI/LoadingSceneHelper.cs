using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneHelper : MonoBehaviour
{
    public EasyTween loadingPanelTween;
    public float minimumTimeToLoad = 1.5f;

    private bool isLoadingScene;
    private AsyncOperation async;

    IEnumerator LoadSceneAsync(string sceneName) {
        DontDestroyOnLoad(transform.root.gameObject);
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(1.5f);
        async.allowSceneActivation = true;
        while (!async.isDone){
            yield return null;
        }
        loadingPanelTween.OpenCloseObjectAnimation();
        Time.timeScale = 1f;
        Destroy(loadingPanelTween.transform.root.gameObject,loadingPanelTween.GetAnimationDuration()*2.0f);
    }

     public void loadScene(string sceneName){
         if(!isLoadingScene){
             isLoadingScene = true;
             loadingPanelTween.OpenCloseObjectAnimation();
             StartCoroutine(LoadSceneAsync(sceneName));
         }
     }
}
