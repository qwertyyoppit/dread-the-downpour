using System.Collections;
using UnityEngine;

class BulletTrailVisuals : MonoBehaviour
{
    public float Speed;
    public GameObject HitEffect;
    public float LifeDuration;
    public float Width;
    public float Length;

    private Vector3 start;
    private Vector3 end;
    private Vector3 hit;
    private bool hasHit = false;
    private float lifetime;

    public void StartProjectile(Vector3 hitpoint)
    {
        hit = hitpoint;
        start = transform.position;
        end = transform.position;
        lifetime = 0;
        hasHit = false;
        UpdateVisuals();
    }

    private void Update()
    {
        if (Time.timeScale <= 0.0f) return;

        if (lifetime > LifeDuration)
        {
            Destroy(gameObject);
        }
        lifetime += Time.deltaTime;

        if (end == hit)
        {
            if (!hasHit) OnHit();
            start = Vector3.MoveTowards(start, end, Mathf.Max(Vector3.Distance(start, end) - Length, 0));
            //start = Vector3.MoveTowards(start, hit, Speed * Time.deltaTime);
        }
        else
        {
            end = Vector3.MoveTowards(end, hit, Speed * Time.deltaTime);
            start = Vector3.MoveTowards(start, end, Mathf.Max(Vector3.Distance(start, end) - Length, 0));
        }
        if (start == end)
        {
            Destroy(gameObject);
        }
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        var length = Vector3.Distance(start, end);
        transform.position = start + (end - start) * 0.5f;
        transform.localScale = new(Width * (1 - lifetime/LifeDuration), Width * (1 - lifetime/LifeDuration), length);
    }

    private IEnumerator Pause(float time)
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = .0f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1.0f;        
    }

    protected void OnHit()
    {
        if (HitEffect) Instantiate(HitEffect, hit, Quaternion.identity);
        hasHit = true;
    }
}
//we would need to do it differently if we wanna use it for actual projectiles