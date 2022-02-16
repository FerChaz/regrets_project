using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntranceInBoss : MonoBehaviour
{
    public BoxCollider entranceCollider;

    public GameObject mainCamera;
    public GameObject bossCamera;
    public GameObject boss;
    
    public BossController bossController;

    public PlayerJump playerJump;

    private void Awake()
    {
        entranceCollider = GetComponent<BoxCollider>();
        mainCamera = FindObjectOfType<MainCamera>().gameObject;
        bossController = FindObjectOfType<BossController>();
        playerJump = FindObjectOfType<PlayerJump>();
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
        bossCamera.SetActive(true);
        mainCamera.SetActive(false);

        playerJump.jumpVelocity = 40.0f;

        bossController.Entrance();

        this.gameObject.SetActive(false);
    }

}
