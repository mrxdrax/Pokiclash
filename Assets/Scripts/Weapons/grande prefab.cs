using System.Collections;
using UnityEngine;

public class grandeprefab : MonoBehaviour
{
    [SerializeField] private GameObject effect;

   private Vector3 startPos;
private Vector3 targetPos;

public Transform target;
[SerializeField] private float flyTime = 0.5f;
[SerializeField]
private float arcHeight = 2f;

void Start()
{
    startPos = transform.position;

    if (target != null)
        targetPos = target.position;
    else
        targetPos = transform.position;

    StartCoroutine(FlyBomb());
}
   IEnumerator FlyBomb()
{
    float t = 0f;

    while (t < 1f)
    {
        t += Time.deltaTime / flyTime;

        Vector3 pos = Vector3.Lerp(startPos, targetPos, t);

        pos.y += Mathf.Sin(t * Mathf.PI) * arcHeight;

        transform.position = pos;

        transform.Rotate(0, 0, 720 * Time.deltaTime);

        yield return null;
    }

    Explode();
}

    void Explode()
    {
        Instantiate(effect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}