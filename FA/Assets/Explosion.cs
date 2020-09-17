using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius =50f;
    public float power = 100f;
    public Vector3 explosionPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Boom()
    {
        explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in colliders)
        {
            Debug.Log(hit);
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Debug.Log("force");
                if(gameObject.tag == "PS2")
                {
                    rb.AddForce((rb.gameObject.transform.position - explosionPos).normalized * power * (1 - ((rb.gameObject.transform.position - explosionPos).magnitude / radius)), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce((rb.gameObject.transform.position - explosionPos).normalized * power * 7 * (1 - ((rb.gameObject.transform.position - explosionPos).magnitude / (radius * 10))), ForceMode2D.Impulse);
                }
            }

        }
    }
    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * power * wearoff);
    }
}
