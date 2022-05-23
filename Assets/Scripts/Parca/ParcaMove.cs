using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcaMove : MonoBehaviour
{
    public float velocidadVertical;

    private Vector3 movement;

    private void Update()
    {
        if (gameObject.transform.localPosition.y <= -19.5f)
        {
            movement.Set(0, velocidadVertical, 0);
        }
        else if (gameObject.transform.localPosition.y >= -20.5f)
        {
            movement.Set(0, -velocidadVertical, 0);
        }

        transform.Translate(movement * Time.deltaTime);
    }
}
