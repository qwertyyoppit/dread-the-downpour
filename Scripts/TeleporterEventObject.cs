using UnityEngine;

public class TeleporterEventObject : MonoBehaviour
{
    public TeleporterEventInfo TEInfo;
    public GameObject TeleporterField;
    public GameObject TeleporterObject;

    public float TeleporterProgress { get; protected set; }
    public bool TEActive { get; protected set; }

    protected float visualRadius;

    public GameObject enemy;
    public float spawnRate = 5.0f;
    protected float spawnCooldown;

    protected virtual void Awake()
    {
        spawnCooldown = spawnRate;
    }

    protected virtual void Update()
    {
        if (TEActive)
        {
            if (spawnCooldown > 0)
                spawnCooldown -= Time.deltaTime;
            else
            {
                if (!enemy) return;
                spawnCooldown += spawnRate;
                var pos = 0.5f * TEInfo.Radius * new Vector3(Mathf.Sqrt(2) / 2 * (Random.value * 2 - 1), 0, Mathf.Sqrt(2) / 2 * (Random.value * 2 - 1)) + transform.position;
                if (Physics.Raycast(pos, Vector3.down, out var hit))
                {
                    if (pos.y - hit.point.y <= 1.0) pos.y = hit.point.y;
                }
                var spawned = Instantiate(enemy, pos, Quaternion.identity);
            }
        }
    }

    protected virtual void LateUpdate()
    {
        if (TEActive)
        {
            TeleporterProgress += TEInfo.ProgressRate * Time.deltaTime;
            visualRadius = Mathf.Lerp(visualRadius, TEInfo.Radius, 10.0f * Time.deltaTime);
            TeleporterField.transform.localScale = Vector3.one * visualRadius;
        }
    }

    public virtual void SetEventActive(bool active = true)
    {
        if (active)
        {
            if (TEActive) return;
        }
        else
        {
            if (!TEActive) return;
        }
        TEActive = active;
        TeleporterField.SetActive(active);
        TeleporterProgress = 0.0f;
        visualRadius = 0.0f;
        TeleporterField.transform.localScale = Vector3.zero;
    }

    public bool InRadius(Vector3 position)
    {
        return Vector3.Distance(TeleporterObject.transform.position, position) <= GetRadius();
    }

    public float GetRadius()
    {
        return TEInfo.Radius;
    }
}
