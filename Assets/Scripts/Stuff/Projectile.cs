using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Range(0, 5)] public float lifespan = 3;
    [Range(1, 100)] public float speed = 10.0f;
    [Range(-100, 100)] public float angle = 0.0f;
    public bool destroyOnHit = false;
    public GameObject destroyEffect;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void OnDestroy()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }

    public void Fire(Vector3 forward)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        rigidbody.AddForce(rotation * forward * speed, ForceMode.VelocityChange);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
