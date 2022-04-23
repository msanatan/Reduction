using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator animator;

    public float transitionTime = 1f;
    private static bool reloadAnim = false;

    private void Awake()
    {
        animator.SetBool("Reload", reloadAnim);
        animator.SetTrigger("End");
    }

    public void LoadNextScene()
    {
        reloadAnim = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentSceneIndex + 1));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void ReloadLevel()
    {
        reloadAnim = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentSceneIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetBool("Reload", reloadAnim);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
    }
}
