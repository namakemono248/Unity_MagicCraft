using UnityEngine;

[RequireComponent(typeof(StageBounds))]
[RequireComponent(typeof(BoxCollider2D))]
public class StageAutoDetector : MonoBehaviour
{
    StageBounds bounds;

    void Awake()
    {
        bounds = GetComponent<StageBounds>();

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(bounds.width, bounds.height);
        col.offset = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        StageManager.ForceSetStage(bounds);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (StageManager.Current != bounds)
            StageManager.ForceSetStage(bounds);
    }
}
