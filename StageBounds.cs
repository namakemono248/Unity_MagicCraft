using UnityEngine;

public class StageBounds : MonoBehaviour
{
    public float width = 50f;
    public float height = 50f;

    public float MinX => transform.position.x - width * 0.5f;
    public float MaxX => transform.position.x + width * 0.5f;
    public float MinY => transform.position.y - height * 0.5f;
    public float MaxY => transform.position.y + height * 0.5f;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
#endif
}
