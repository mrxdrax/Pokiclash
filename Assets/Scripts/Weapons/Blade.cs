using UnityEngine;

public class Blade : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Enemy"))
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takedamage(AreaWeapon.instance.damage);
            }
        }
    }
}
