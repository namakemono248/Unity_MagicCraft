using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    MagicData data;

    void Update()
    {
        if (data == null) return;

        // 弾を自身の向きに移動
        transform.position += transform.right * data.speed * Time.deltaTime;
    }

    public void SetData(MagicData d)
    {
        data = d;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * data.speed;

        Destroy(gameObject, data.duration);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Enemy タグのオブジェクトだけ処理
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Damage: " + data.damage);

            if (data.explosive)
            {
                Debug.Log("爆発！");
            }

            Destroy(gameObject); // 弾を消す
        }
    }
}
