using UnityEngine;

[CreateAssetMenu(fileName = "TeleporterEventInfo", menuName = "Scriptable Objects/TeleporterEventInfo")]
public class TeleporterEventInfo : ScriptableObject
{
    [Tooltip("Radius of the teleporter field")]
    public float Radius = 25.0f;
    [Tooltip("Rate (in seconds) the event reaches completion of 100")]
    public float ProgressRate = 10.0f;
}
