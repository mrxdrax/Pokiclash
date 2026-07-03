using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public float spawnTimer=5f;
    public float cooldown = 5f;
    public float duration = 3f;
    public float damage = 1f;
    // scale applied to spawned area prefab
    public float areaScale = 1f;
    public static AreaWeapon instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
{
    StartCoroutine(AreaLoop());
}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
{
    IncreaseAreaSize(0.2f);
    Debug.Log("Area Scale : " + areaScale);
}

        
    }
    IEnumerator AreaLoop()
{
    GameObject obj =
        Instantiate(prefab, transform.position,
        Quaternion.identity,
        transform);

    areaprefab area = obj.GetComponent<areaprefab>();

    obj.transform.localScale = Vector3.zero;

    while (true)
    {
        area.activeScale = Vector3.one * areaScale;

        area.ShowArea();

        yield return new WaitForSeconds(3f);

        area.HideArea();

        yield return new WaitForSeconds(2f);
    }
}

    // Increase the scale
    public void IncreaseAreaSize(float amount)
{
    areaScale += amount;

    if(areaScale < 0.1f)
        areaScale = 0.1f;
}
}
