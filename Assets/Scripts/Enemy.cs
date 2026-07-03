using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject effect;
    private Vector3 direction;
    private Vector3 movement;
    public float damage;
    public float health;
    [SerializeField] private float moveSpeed = 2f;
    public float knockbackForce = 15f;
    public int kills;

    void Start()
    {
        
    }
    void Update()
    {
        if (playercontroller.instance.gameObject.activeSelf)
        {
            if (playercontroller.instance.transform.position.x > transform.position.x)
            {
                SpriteRenderer.flipX = true;
            }
            else
            {
                SpriteRenderer.flipX = false;
            }
            direction = (playercontroller.instance.transform.position - transform.position).normalized;
            movement = new Vector3(direction.x, direction.y).normalized;
            rb.velocity = movement * moveSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playercontroller.instance.Damage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Debug.Log("Weapon Hit Enemy (trigger)");

            Vector2 knockDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
    public void takedamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.juststart = false;
            Destroy(gameObject);
            Instantiate(effect, transform.position, transform.rotation);
            playercontroller.instance.enemycount(kills);
        }
    }
public void AreaKnockBack(Vector2 direction, float force)
{
    rb.velocity = Vector2.zero;
    rb.AddForce(direction * force, ForceMode2D.Impulse);
}
}
