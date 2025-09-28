using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlagBehaviour : MonoBehaviour
{
    [SerializeField] private Animator flagAnimator;
    [SerializeField] private TilemapLogic tilemapLogic;

    [Tooltip("Name of the jump animation in the Animator")]
    [SerializeField] private string jumpAnimationName = "FlagJump";

    [Header("Jump Arc")]
    [SerializeField] private float jumpHeight = 1f;

    private void Start()
    {
        Vector3 targetWorldPos = tilemapLogic.GetRightmostMiddleCellPosition();
        float clipLength = GetAnimationClipLength(jumpAnimationName);
        flagAnimator.SetTrigger("Jump");

        StartCoroutine(MoveFlag(targetWorldPos, clipLength));
    }

    private float GetAnimationClipLength(string clipName) {
        foreach (var clip in flagAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }
        Debug.LogWarning($"Animation clip '{clipName}' not found!");
        return 1f;
    }

    private IEnumerator MoveFlag(Vector3 targetPos, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            Vector3 pos = Vector3.Lerp(startPos, targetPos, t);

            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;
            pos.y += height;

            transform.position = pos;
            yield return null;
        }

        transform.position = targetPos;
    }
}
