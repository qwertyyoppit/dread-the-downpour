using UnityEngine;

public class BoulderReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<AmmogusSussyManager>(out var boulder);
        if (!boulder) return;
        boulder.RespawnBoulder();
    }
}
