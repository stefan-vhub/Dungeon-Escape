using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    // Destroy this after 5 seconds

    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    private void Update()
    {
        // Move right at 3 meters per second
        transform.Translate(Vector3.right * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect player and deal damage (IDamageable Interface)
        if (other.tag == "player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();

            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }

        }
    }

}
