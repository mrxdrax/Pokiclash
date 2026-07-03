using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private float damage = 20f ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Enemy"))
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            enemy.takedamage(damage);
            Vector2 dir = (enemy.transform.position - transform.position).normalized;
            enemy.AreaKnockBack(dir, 10f);
        }
    }
}
