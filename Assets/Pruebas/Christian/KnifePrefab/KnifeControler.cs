using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider knife;
    public float[] attackDetails = new float[2];
    public float damage;

    private void Awake()
    {
        attackDetails[0] = damage;
        rb = GetComponent<Rigidbody>();
        knife = GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.transform.SendMessage("GetDamage", attackDetails);
        }
        rb.isKinematic = true;
        knife.isTrigger = true;
    }
}
