using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private LoadNextScene loadNextScene;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (loadNextScene != null)
        {
            loadNextScene.LoadNextLevel();
        }
    }
}
