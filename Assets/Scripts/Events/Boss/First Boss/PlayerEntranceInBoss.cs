using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntranceInBoss : MonoBehaviour
{
    public BoxCollider entranceCollider;

    public GameObject boss;
    
    public BossController bossController;

    public PlayerController player;

    private Vector3 bossPosition;

    private void Awake()
    {
        entranceCollider = GetComponent<BoxCollider>();
        bossController = FindObjectOfType<BossController>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActiveEntrance();
        }
    }


    private void ActiveEntrance()
    {
        if (!player.isFacingRight)
        {
            player.Flip();
        }

        player.CanDoAnyMovement(false);

        bossPosition.Set(player.transform.position.x + 20.0f, bossController.transform.position.y, 0.0f);

        bossController.transform.position = bossPosition;
        bossController.Entrance();


        this.gameObject.SetActive(false);
    }

}
