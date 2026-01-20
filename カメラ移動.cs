using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    Camera cam;

    StageBounds Bounds => StageManager.Current;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (target == null || Bounds == null) return;

        float halfH = cam.orthographicSize;
        float halfW = cam.orthographicSize * cam.aspect;

        float x = Mathf.Clamp(
            target.position.x,
            Bounds.MinX + halfW,
            Bounds.MaxX - halfW
        );

        float y = Mathf.Clamp(
            target.position.y,
            Bounds.MinY + halfH,
            Bounds.MaxY - halfH
        );

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
