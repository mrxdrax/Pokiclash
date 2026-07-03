using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;


public class playercontroller : MonoBehaviour
{
    public static playercontroller instance;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    public bool isimmune;
    [SerializeField] private float immunityduration;
    [SerializeField] private float immunityTimer;
    float inputX;
    float inputY;
    private Vector3 movement;
    public float maxhealth;
    public float playerhealth;
    public int killcount = 0;
    
    private void Awake()
    {
            instance = this;
    }
    void Start()
    {
        playerhealth = maxhealth;
        UIcontroller.instance.UpdateHealth();
    }
    void Update()
    {
        inputX = SimpleInput.GetAxis("Horizontal");
        inputY = SimpleInput.GetAxis("Vertical");
        movement = new Vector3(inputX, inputY).normalized;
        animator.SetFloat("moveX", inputX);
        animator.SetFloat("moveY", inputY);
        if (movement==Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }
        if(immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }
        else
        {
            isimmune = false;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
    public void Damage(float damage)
    {
        if(isimmune == false)
        {
            isimmune = true;
            immunityduration =immunityTimer;
            playerhealth -= damage;
            UIcontroller.instance.UpdateHealth();
            if (playerhealth <= 0)
            {
                GameManager.Instance.gameover();
                gameObject.SetActive(false);
            }
        }
        
    }
    public void enemycount(int kills)
    {
        killcount = killcount + kills;
    }
}
