using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlagBehaviour : MonoBehaviour
{
    [SerializeField] private Animator flagAnimator;
    [SerializeField] private Tilemap tilemap;

    [Tooltip("Name of the jump animation in the Animator")]
    [SerializeField] private string jumpAnimationName = "FlagJump";

    [Header("Jump Arc")]
    [SerializeField] private float jumpHeight = 1f;

    private void Start()
    {
        Vector3 targetWorldPos = GetRightmostMiddleCellPosition();
        float clipLength = GetAnimationClipLength(jumpAnimationName);
        flagAnimator.SetTrigger("Jump");

        StartCoroutine(MoveFlag(targetWorldPos, clipLength));
    }

    private Vector3 GetRightmostMiddleCellPosition()
    {
        BoundsInt bounds = tilemap.cellBounds;
        int maxX = bounds.xMin;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    maxX = Mathf.Max(maxX, x);
                }
            }
        }

        List<int> yPositions = new List<int>();
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            if (tilemap.HasTile(new Vector3Int(maxX, y, 0)))
            {
                yPositions.Add(y);
            }
        }

        int middleY = yPositions[yPositions.Count / 2];

        Vector3Int cellPos = new Vector3Int(maxX, middleY, 0);
        Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);

        return worldPos;
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
