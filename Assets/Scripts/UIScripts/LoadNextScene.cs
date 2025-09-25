using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField] private float minLoadingTime = 1.5f;
    [SerializeField] private Animator transition;

    private static bool playedEndTransition = false;

    private void Start()
    {
        if (playedEndTransition)
        {
            transition.Play("TransitionEnd");
            playedEndTransition = false;
        }
        else
        {
            transition.Play("TransitionIdle");
        }
    }

    public void LoadNextLevel()
    {
        int levelIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
        StartCoroutine(LoadLevel(levelIndex));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(minLoadingTime);

        playedEndTransition = true;
        SceneManager.LoadScene(levelIndex);
    }
}
