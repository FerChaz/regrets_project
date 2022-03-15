using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using scripts.respawn.respawnController;
using scripts.limbo.limboController;

public class DeathRespawnAndRecover : MonoBehaviour
{
    public string sceneToRespawn;
    public RespawnInfo respawnInfo;
    public RecoverSoulsInfo soulRecoveryData;
    public AdditiveScenesInfo additiveSceneInfo;
    public RecoverSoulsController recover;

    [Header("Controllers")]
    public SoulController soulsController;
    public LimboController limboController;
    public RespawnController respawnController;
    public PlayerJump playerJump;

    private int _totalSoulsToRecover;

    public bool isFirstDead;
    public Vector3 deathPosition;
    public float initialForceJump;

    public LimboInfo limboInfo;

    private void Awake()
    {
        soulsController = GetComponentInChildren<SoulController>();
        limboController = FindObjectOfType<LimboController>();
        respawnController = FindObjectOfType<RespawnController>();
        recover = FindObjectOfType<RecoverSoulsController>();
        playerJump = GetComponent<PlayerJump>();
    }

    private void Start()
    {
        isFirstDead = true;
        initialForceJump = playerJump.jumpVelocity;
    }

    public void Death(Vector3 playerLastPositionInGround)
    {
        playerJump.jumpVelocity = initialForceJump;

        if (isFirstDead)
        {
            deathPosition = playerLastPositionInGround;
            isFirstDead = false;
            limboInfo.deathScene = additiveSceneInfo.actualScene;
            limboInfo.isPlayerInLimbo = true;
            limboController.ChargeLimboScene(deathPosition);
            // Falta desactivar los objetos en escena
        }
        else
        {
            if (!limboInfo.isPlayerInLimbo){

                deathPosition = playerLastPositionInGround;
            }

            isFirstDead = true;
            Respawn();
        }
    }


    public void Respawn()
    {
        AssignRecoverSoulData();
        respawnController.Respawn();

        if (limboInfo.isPlayerInLimbo)
        {
            limboInfo.isPlayerInLimbo = false;
            limboController.UnloadLimboScene();
        }
        
    }

    private void AssignRecoverSoulData()
    {
        soulRecoveryData.needRecover = true;

        _totalSoulsToRecover = soulsController.TotalSouls();

        soulRecoveryData.deathPosition = deathPosition;
        soulRecoveryData.deathPosition.y += 2.5f;

        soulRecoveryData.totalSouls = _totalSoulsToRecover;

        soulsController.DiscountSouls(_totalSoulsToRecover);

        recover.Activate();
    }


}
