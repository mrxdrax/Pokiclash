using UnityEngine;

public class areaprefab : MonoBehaviour
{
    public Vector3 activeScale = Vector3.one;
    public Vector3 hiddenScale = Vector3.zero;

    public float expandSpeed = 5f;

    private Vector3 targetScale;

    private void Start()
    {
        targetScale = activeScale;
    }

    private void Update()
    {
        transform.localScale =
            Vector3.Lerp(
                transform.localScale,
                targetScale,
                Time.deltaTime * expandSpeed
            );
    }

    public void ShowArea()
    {
        targetScale = activeScale;
    }

    public void HideArea()
    {
        targetScale = hiddenScale;
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Enemy"))
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.takedamage(AreaWeapon.instance.damage);

                Vector2 dir =
                    (enemy.transform.position - transform.position).normalized;

                enemy.AreaKnockBack(dir, 4f);
            }
        }
    }
}