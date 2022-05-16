using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioClip menuSelect;
    [SerializeField] AudioClip levelComplete;
    [SerializeField] AudioClip reloadLevel;

    public float transitionTime = 1f;
    private static bool reloadAnim = false;
    private AudioSource audioSource;

    private void Awake()
    {
        animator.SetBool("Reload", reloadAnim);
        animator.SetTrigger("End");
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadNextScene()
    {
        audioSource.PlayOneShot(menuSelect);
        reloadAnim = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentSceneIndex + 1));
    }

    public void LoadNextLevel()
    {
        audioSource.PlayOneShot(levelComplete);
        reloadAnim = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentSceneIndex + 1));
    }

    public void LoadMainMenu()
    {
        audioSource.PlayOneShot(menuSelect);
        StartCoroutine(LoadLevel(0));
    }

    public void ReloadLevel()
    {
        reloadAnim = true;
        audioSource.PlayOneShot(reloadLevel);
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
