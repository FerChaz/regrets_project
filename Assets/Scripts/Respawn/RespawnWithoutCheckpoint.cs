using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnWithoutCheckpoint : MonoBehaviour
{
    private LifeController _lifeController;
    public RespawnInfo respawnInfo;

    public Vector3 respawnPosition;
    public StringValue sceneToRespawn;

    public List<StringValue> additiveScenesToCharge;

    private void Awake()
    {
        _lifeController = FindObjectOfType<LifeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (respawnInfo.isRespawning)
            {
                respawnInfo.isRespawning = false;
                _lifeController.RestoreMaxLife();

                respawnInfo.respawnPosition = respawnPosition;
                respawnInfo.sceneToRespawn = sceneToRespawn;
                respawnInfo.additiveScenesToCharge = additiveScenesToCharge;
            }
        }
        
    }

}
