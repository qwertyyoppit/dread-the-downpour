using System.Collections.Generic;
using UnityEngine;

public class EnhancedTeleporterObject : TeleporterEventObject
{
    public int requiredKills = 10;
    public float spawnRateIncrement = 1.0f;

    int wave;
    int killCount;

    public List<CharacterBody> spawnedBodies = new();
    private MeshRenderer fieldRenderer;
    private float originalHue;
    private float originalSat;
    private float originalValue;
    private Color visualColor;

    protected override void Awake()
    {
        spawnCooldown = spawnRate;
        fieldRenderer = TeleporterField.GetComponent<MeshRenderer>();
        visualColor = fieldRenderer.material.color;
        Color.RGBToHSV(visualColor, out originalHue, out originalSat, out originalValue);
        Debug.Log($"{originalHue}, {originalSat}, {originalValue}");
        Debug.Log(Color.HSVToRGB(originalHue, originalSat, originalValue));
        killCount = requiredKills;
    }

    protected override void Update()
    {
        if (TEActive)
        {
            if (spawnCooldown > 0)
                spawnCooldown -= Time.deltaTime;
            else
            {
                if (!enemy) return;
                spawnCooldown += spawnRate / (1 + spawnRateIncrement * wave);
                var pos = 0.5f * TEInfo.Radius * new Vector3(Mathf.Sqrt(2) / 2 * (Random.value * 2 - 1), 0, Mathf.Sqrt(2) / 2 * (Random.value * 2 - 1)) + transform.position;
                if (Physics.Raycast(pos, Vector3.down, out var hit))
                {
                    if (pos.y - hit.point.y <= 1.0) pos.y = hit.point.y;
                }
                var spawned = Instantiate(enemy, pos, Quaternion.identity);
                if (spawned.TryGetComponent<CharacterBody>(out var body))
                {
                    body.Died += IncrementOnDeath;
                    spawnedBodies.Add(body);
                }
            }
        }
    }

    protected override void LateUpdate()
    {
        float targetRadius = 0;
        if (TEActive)
        {
            TeleporterProgress += TEInfo.ProgressRate * Time.deltaTime;
            targetRadius = TEInfo.Radius;
        }
        visualRadius = Mathf.Lerp(visualRadius, targetRadius, 10.0f * Time.deltaTime);
        TeleporterField.transform.localScale = Vector3.one * visualRadius;
        var targetCol = Color.HSVToRGB(originalHue + 0.15f * Mathf.Pow(spawnRateIncrement, wave), originalSat + 0.1f * Mathf.Pow(spawnRateIncrement, wave), originalValue + 0.1f * Mathf.Pow(spawnRateIncrement, wave));
        targetCol.a = visualColor.a;
        visualColor = Color.Lerp(visualColor, targetCol, 2.5f * Time.deltaTime);
        fieldRenderer.material.color = visualColor;
    }

    private void OnDestroy()
    {
        foreach (var body in spawnedBodies) body.Died -= IncrementOnDeath;
    }

    private void IncrementOnDeath(CharacterBody body)
    {
        killCount--;
        if (killCount < 1)
        {
            killCount = Mathf.FloorToInt(requiredKills * Mathf.Pow(spawnRateIncrement, wave));
            wave++;
        }
    }
}
