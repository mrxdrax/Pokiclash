using Unity.VisualScripting;
using UnityEngine;

public class Grande : MonoBehaviour
{
    public static Grande instance;

    public float spawnTimer = 5f;
    public float cooldown = 5f;

    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        instance = this;
    }

    public void DropBombs()
    {
        spawnTimer = cooldown;
        Instantiate(prefab, transform.position, transform.rotation);
    }
}