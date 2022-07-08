using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControler : MonoBehaviour
{
    /* LA ROTACION DEL CUCHILLO ES: X(0.29364), Y (0.05895), Z(0.030619)
     * LA ESCALA DEL CUCHILLO ES: X(0.29364), Y (0.05895), Z(0.030619)
     * LA ESCALA DEL MODELO DEL CUCHILLO (HIJO DEL PREFAB) ES: X(-561.219), Y (3152.968), Z(-5557.904)
     * Esto es necesario setearlo en el prefab del chucillo original ya que al rotarlo se deforma el modelo
     * Posteriormente hay que cambiar la escala de KNIFE (PADRE E HIJO) ya que estan desproporcionadas       
     * La hitbox las medidas son: Center   X (0.66) Y(0.03) Z(0)
     *                            Size     X (1.69) Y(1.54) Z(1)    
     */
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider knife;
    public float[] attackDetails = new float[2];
    public float damage;

    private void Awake()
    {
        transform.Rotate(new Vector3(0f, -90f, 0f));
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
