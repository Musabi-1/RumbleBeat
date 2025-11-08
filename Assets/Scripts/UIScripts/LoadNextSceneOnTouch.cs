using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadNextSceneOnTouch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private LoadNextScene loadNextScene;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked!");
        if (loadNextScene != null)
        {
            loadNextScene.LoadNextLevel();
        }
    }
}
