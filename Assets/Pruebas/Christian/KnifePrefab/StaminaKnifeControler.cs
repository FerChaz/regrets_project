using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaKnifeControler : MonoBehaviour
{
    [Header("Parametros Main Stamina")]
    public float kaoriStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float launchCost = 5.0F;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreLaunchKnife = false;

    [Header("Regenerate Parametros")]
    [Range(0, 50)][SerializeField] private float staminaDraind = 0.5f;
    [Range(0, 50)] [SerializeField] private float staminaRegenerate = 0.5f;

    [Header("UI Stamina Elements")]
    [SerializeField] private Image staminaProgresivessUI=null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    private void Update()
    {
        if (!weAreLaunchKnife)
        {
            if (kaoriStamina <= maxStamina - 0.01)
            {
                kaoriStamina += staminaRegenerate*Time.deltaTime;
                if (kaoriStamina >= maxStamina) 
                { 
            
                hasRegenerated = true;
            
                }

            }
            
        }
    }
    void UpdateStamina(int vaule) 
    {
        staminaProgresivessUI.fillAmount = kaoriStamina / maxStamina;
        if (vaule == 0)
        {
            sliderCanvasGroup.alpha = 0;

        }
        else
        {
            sliderCanvasGroup.alpha = 1;

        }
    }
}
