using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSistem : MonoBehaviour
{
    [Header("Regenerate Parametros")]
    [Range(0, 50)] [SerializeField] private float staminaRegenerate; 

    //Recominedo crear una variable Scriptiable que varie la regeneracion para alguna mejora de Personaje

    [Header("UI Stamina Elements")]
    [SerializeField] private Image staminaProgresivessUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    public ScriptStamine scriptStamine;
    [SerializeField]public float playerActualStamine;
 

    private void Start()
    {
        playerActualStamine = scriptStamine.stamineMaxPlayer;
    }
    public void Update()
    {
        RegenerateStamina();
    }
    void RegenerateStamina()// Actualisa la UI y "regenrea" la estamina 
    {
        staminaProgresivessUI.fillAmount = playerActualStamine / scriptStamine.stamineMaxPlayer;
        if (playerActualStamine < scriptStamine.stamineMaxPlayer) playerActualStamine += staminaRegenerate * Time.deltaTime;
    }
    public void SubstractStamina(float costStamine) 
    {
        if (playerActualStamine >= costStamine) 
        { 
            playerActualStamine -= costStamine;
        }
    }
}
