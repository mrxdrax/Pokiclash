using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class screenClear : MonoBehaviour
{
    public float damageAmount = 9999f;
    public float clearRadius = 5f;
    void Start()
    {

    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("Something in trigger: " + collider.name);
        if (Input.GetKey(KeyCode.V) && collider.CompareTag("Enemy"))
        {
            Debug.Log("screenclear");
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.takedamage(damageAmount);
        }
    }

    // Programmatic clear that damages all enemies within clearRadius
    public void ClearNow()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, clearRadius);
        foreach (var h in hits)
        {
            if (h.CompareTag("Enemy"))
            {
                Enemy enemy = h.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takedamage(damageAmount);
                }
            }
        }
    }
}

