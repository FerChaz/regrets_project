using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedBoss : MonoBehaviour
{
    public PlayerController player;
    public GameObject boss;
    private BossController bossController;
    public Vector3 positionCanvas;
    public bool isInWorld;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        bossController = boss.GetComponent<BossController>();
    }

    private void OnEnable()
    {
        if (isInWorld)
        {
            if (bossController.facingDirection > 0)
            {
                positionCanvas.Set(boss.transform.position.x + 2, boss.transform.position.y, 0.0f);
            }
            else
            {
                positionCanvas.Set(boss.transform.position.x - 4, boss.transform.position.y, 0.0f);
            }

            this.transform.position = positionCanvas;
        }
        
        player.CanDoAnyMovement(false);
    }

    private void OnDisable()
    {
        player.CanDoAnyMovement(true);
    }
}
