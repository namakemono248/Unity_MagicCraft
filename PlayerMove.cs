using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;
    public Vector2 lastDirection { get; private set; } = Vector2.down;

    [Header("Clamp")]
    [SerializeField] float edgeMargin = 0.5f;

    bool clampEnabled = false;

    StageBounds Bounds => StageManager.Current;

    void OnEnable()
    {
        StageManager.OnStageChanged += OnStageChanged;
    }

    void OnDisable()
    {
        StageManager.OnStageChanged -= OnStageChanged;
    }

    void Update()
    {
        HandleInput();
    }

    void LateUpdate()
    {
        if (!clampEnabled || Bounds == null) return;

        ClampNow(Bounds);
    }

    void HandleInput()
    {
        if (Keyboard.current == null) return;

        Vector2 input = new Vector2(
            ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) ? 1 : 0) -
            ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) ? 1 : 0),

            ((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) ? 1 : 0) -
            ((Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) ? 1 : 0)
        );

        if (input == Vector2.zero) return;

        lastDirection = input.normalized;
        transform.position += (Vector3)(lastDirection * speed * Time.deltaTime);
    }

    void OnStageChanged(StageBounds stage)
    {
        clampEnabled = true;

        // ★ ステージ切替直後に即 Clamp
        ClampNow(stage);
    }

    void ClampNow(StageBounds stage)
    {
        float x = Mathf.Clamp(
            transform.position.x,
            stage.MinX + edgeMargin,
            stage.MaxX - edgeMargin
        );

        float y = Mathf.Clamp(
            transform.position.y,
            stage.MinY + edgeMargin,
            stage.MaxY - edgeMargin
        );

        transform.position = new Vector3(x, y, transform.position.z);
    }

    // ★ Warp 後も即 Clamp + 有効化
    public void WarpTo(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        if (StageManager.Current != null)
        {
            ClampNow(StageManager.Current);  // TP 直後の位置を Clamp
            clampEnabled = true;             // すぐ制限有効化
        }
    }
}
