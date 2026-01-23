using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;

    Camera cam;
    bool followEnabled = false;

    StageBounds Bounds => StageManager.Current;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        StageManager.OnStageChanged += OnStageChanged;
    }

    void OnDisable()
    {
        StageManager.OnStageChanged -= OnStageChanged;
    }

    void LateUpdate()
    {
        if (!followEnabled || target == null || Bounds == null) return;
        Snap(Bounds);
    }

    void OnStageChanged(StageBounds stage)
    {
        followEnabled = true;

        if (target != null)
            Snap(stage);
    }

    void Snap(StageBounds stage)
    {
        float halfH = cam.orthographicSize;
        float halfW = cam.orthographicSize * cam.aspect;

        float x = Mathf.Clamp(
            target.position.x,
            stage.MinX + halfW,
            stage.MaxX - halfW
        );

        float y = Mathf.Clamp(
            target.position.y,
            stage.MinY + halfH,
            stage.MaxY - halfH
        );

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
