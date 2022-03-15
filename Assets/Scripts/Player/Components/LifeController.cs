using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Life")]
    public int maxLife;
    public IntValue currentLife;

    [Header("Invulnerability Variables")]
    [SerializeField] private float invincibilityDeltaTime;
    public float invulnerabilityTime;
    public float invulnerabilityTimeSpikes;
    private bool invulnerability;

    [Header("Components")]
    [SerializeField] private GameObject model;
    private BoxCollider colliderLifeManager;

    [Header("SignalSender")]
    public SignalSender playerHealthSignal;

    [Header("Player Scripts")]
    public PlayerController playerController;
    public PlayerKnockback knockbackController;

    [Header("Fade")]
    public GameObject deathFade;

    [Header("Recover")]
    public RecoverSoulsInfo soulsToRecover;
    public GameObject modelToShow;

    private Vector3 _scale;

    [Header("LowLifeCanvas")]
    public GameObject lowLifeCanvas;

    [Header("Script Sonido Da�o y muerte ")]
    public DamagePlayerAudio damagePlayerAudio;

    public GameObject lifeContainer;
    public IntValue heartContainers;


    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        colliderLifeManager = GetComponent<BoxCollider>();
        playerController = GetComponentInParent<PlayerController>();
        knockbackController = GetComponentInParent<PlayerKnockback>();
        damagePlayerAudio = GetComponent<DamagePlayerAudio>();
    }

    private void Start()
    {
        currentLife.initialValue = maxLife;
        heartContainers.initialValue = 4;
        _scale = model.transform.localScale;
    }


    //-- MODIFIERS -----------------------------------------------------------------------------------------------------------------

    public void RestoreLife(int life)
    {
        currentLife.initialValue += life;

        if (currentLife.initialValue > maxLife)
        {
            currentLife.initialValue = maxLife;
        }

        playerHealthSignal.Raise(); // CHANGE UI
        lowLifeCanvas.SetActive(false);

    }

    public void RestoreMaxLife()
    {
        currentLife.initialValue = maxLife;
        playerHealthSignal.Raise(); // CHANGE UI

        lowLifeCanvas.SetActive(false);
    }

    public void AddMaxLife(int life)
    {
        maxLife += life;                        // FALTA CAMBIAR LA UI
        currentLife.initialValue = maxLife;
        heartContainers.initialValue++;
        lifeContainer.SetActive(true);

        lowLifeCanvas.SetActive(false);
    }

    public void RecieveDamage(int damage, Vector3 direction, bool isEnemy)
    {
        if (!isEnemy)                                               // Spikes
        {
            
            currentLife.initialValue -= damage;
            playerHealthSignal.Raise();

            if (currentLife.initialValue > 0)
            {
                damagePlayerAudio.AudioDa�oPlayer();       //   Reproduce el sonido de da�o
                knockbackController.KnockBackGetFromSpikes(direction);

                if (currentLife.initialValue == 1)
                {
                    lowLifeCanvas.SetActive(true);
                }

                StartCoroutine(Invulnerability(invulnerabilityTime));
            }
            else 
            {
                Death();
                StartCoroutine(Invulnerability(invulnerabilityTime + 2f));
            }
        } 
        else if (!invulnerability)
        {
            currentLife.initialValue -= damage;

            if (currentLife.initialValue > 0)
            {
                damagePlayerAudio.AudioDa�oPlayer();       //   Reproduce el sonido de da�o
                playerHealthSignal.Raise(); // CHANGE UI
                knockbackController.KnockBackGetFromEnemy(direction);

                if (currentLife.initialValue == 1)
                {
                    lowLifeCanvas.SetActive(true);
                }

                StartCoroutine(Invulnerability(invulnerabilityTime));
            }
            else
            {
                Death();
                StartCoroutine(Invulnerability(invulnerabilityTime + 2f));
            }
        }
    }

    private void Death()
    {
        damagePlayerAudio.AudioMuertePlayer();//Reproduce Audio Muerte
        knockbackController.DeathKnockBack();
        playerController.Death();
        currentLife.initialValue = maxLife;
        playerHealthSignal.Raise(); // CHANGE UI
        lowLifeCanvas.SetActive(false);
    }


    //-- CO-ROUTINE INVULNERABILITY --------------------------------------------------------

    IEnumerator Invulnerability(float invulTime)
    {
        invulnerability = true;
        colliderLifeManager.enabled = false;

        for (float i = 0; i < invulTime; i += invincibilityDeltaTime)
        {
            if (model.transform.localScale == _scale)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(_scale);
            }
            
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        ScaleModelTo(_scale);
        invulnerability = false;
        colliderLifeManager.enabled = true;
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }
}
