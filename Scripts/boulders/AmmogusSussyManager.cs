using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AmmogusSussyManager : MonoBehaviour
{
    Rigidbody rb;

    public GameObject Spawn;

    public Vector3 ForceFactor;
    public float ForceDeviation = 0.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        RespawnBoulder();
    }

    public void RespawnBoulder()
    {
        TryGetComponent<SphereCollider>(out var sphere);
        if (!sphere) sphere = gameObject.AddComponent<SphereCollider>();

        if (Spawn.TryGetComponent<BoxCollider>(out var box))
        {
            var minspawn = box.bounds.min + sphere.radius * transform.localScale;
            var maxspawn = box.bounds.max - sphere.radius * transform.localScale;

            transform.position = new Vector3(Mathf.Lerp(minspawn.x, maxspawn.x, Random.value), Mathf.Lerp(minspawn.y, maxspawn.y, Random.value), Mathf.Lerp(minspawn.z, maxspawn.z, Random.value));
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            var rand = Random.value * 2 - 1;
            var force = new Vector3(contact.normal.x * ForceFactor.x, contact.normal.y * ForceFactor.y, contact.normal.z * ForceFactor.z);
            rb.AddForce(Vector3.Slerp(force, force * rand, ForceDeviation));
        }
    }
}
