using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaunchKnife : MonoBehaviour
{
    [Header("Variables del Cuchillo")]
    public float staminaTotal=100.0f;
    public float staminaPlayer=100.0f;

    public GameObject Knife;
    public Transform spawnKnife;

    public float costLaunchKnife=60.0f;

    public float forceKnife=1000f;

    public bool canLaunchKnife = true;

    [Header("UI Stamina Knife")]
    [SerializeField] private Image staminaProgresivessUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    [Header("Regenerate Parametros")]
    [Range(0, 50)] [SerializeField] private float staminaRegenerate = 1f;

    private void Update()
    {
        staminaProgresivessUI.fillAmount = staminaPlayer / staminaTotal;
        if(staminaPlayer<staminaTotal)staminaPlayer += staminaRegenerate*Time.deltaTime;
    }

    private void SubstractStamina()
    {
        if (staminaPlayer >= costLaunchKnife) canLaunchKnife = true;
        else canLaunchKnife = false;
    }

    public void LauncherKnife()
    {
        SubstractStamina();
        if (canLaunchKnife)
        {
            GameObject newKinife;
            newKinife = Instantiate(Knife,spawnKnife.position,spawnKnife.rotation);
            newKinife.GetComponent<Rigidbody>().AddForce(spawnKnife.forward * forceKnife);
            staminaPlayer -= costLaunchKnife;
            Destroy(newKinife, 1);
        }
    }
}
