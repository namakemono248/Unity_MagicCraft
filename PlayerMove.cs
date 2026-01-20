using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    [Header("Move")]
    public float speed = 5f;
    public Vector2 lastDirection = Vector2.down;

    [Header("Clamp")]
    [SerializeField] float edgeMargin = 0.5f;

    bool skipClampOneFrame;   // ★ 追加

    StageBounds Bounds => StageManager.Current;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Bounds == null) return;

        HandleInput();
        ClampPosition();
    }

    void HandleInput()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            input.x =
                ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) ? -1 : 0) +
                ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) ?  1 : 0);

            input.y =
                ((Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) ? -1 : 0) +
                ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) ?  1 : 0);
        }

        if (input == Vector2.zero) return;

        lastDirection = input.normalized;

        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position += (Vector3)(input.normalized * speed * Time.deltaTime);
    }

    void ClampPosition()
    {
        // ★ TP直後は Clamp しない
        if (skipClampOneFrame)
        {
            skipClampOneFrame = false;
            return;
        }

        float x = Mathf.Clamp(
            transform.position.x,
            Bounds.MinX + edgeMargin,
            Bounds.MaxX - edgeMargin
        );

        float y = Mathf.Clamp(
            transform.position.y,
            Bounds.MinY + edgeMargin,
            Bounds.MaxY - edgeMargin
        );

        transform.position = new Vector3(x, y, transform.position.z);
    }

    // ★ ワープAPI（Clamp対策済み）
    public void WarpTo(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        skipClampOneFrame = true;
    }
}
