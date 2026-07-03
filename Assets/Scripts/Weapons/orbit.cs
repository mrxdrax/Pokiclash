using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{[Header("Blade Settings")]
    public GameObject bladePrefab;
    public float orbitRadius = 1.5f;
    public float rotationSpeed = 180f;
    [Header("Blade Show / Hide")]
[SerializeField] private float showTime = 3f;
[SerializeField] private float hideTime = 2f;
[SerializeField] private float scaleSpeed = 8f;
[SerializeField] private Vector3 bladeScale = new Vector3(0.5f,0.5f,0.5f);

private bool bladesVisible = true;

    private List<GameObject> blades = new List<GameObject>();
IEnumerator Start()
{
    yield return new WaitForSeconds(Random.Range(0f,4f));

    StartCoroutine(BladeLoop());
}

    void Update()
    {
        RotateOrbit();
        UpdateBladePositions();

        // 🔥 B PRESS PE BLADE ADD
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddBlade();
        }
    }

    void RotateOrbit()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void UpdateBladePositions()
    {
        int bladeCount = blades.Count;
        if (bladeCount == 0) return;

        float angleStep = 360f / bladeCount;

        for (int i = 0; i < bladeCount; i++)
        {
            float angle = angleStep * i;
            Vector3 pos = GetPositionFromAngle(angle);
            blades[i].transform.localPosition = pos;

        Vector3 targetScale = bladesVisible ? bladeScale : Vector3.zero;

        blades[i].transform.localScale = Vector3.Lerp(
            blades[i].transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
);
        }
    }
    IEnumerator BladeLoop()
{
    while (true)
    {
        bladesVisible = true;
        yield return new WaitForSeconds(showTime);

        bladesVisible = false;
        yield return new WaitForSeconds(hideTime);
    }
}

    Vector3 GetPositionFromAngle(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * orbitRadius;
        float y = Mathf.Sin(rad) * orbitRadius;
        return new Vector3(x, y, 0);
    }

    public void AddBlade()
    {
        GameObject newBlade = Instantiate(bladePrefab, transform);
        blades.Add(newBlade);
    }
}
