using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatController : MonoBehaviour
{
    [SerializeField] private BossFSM bossFSM;
    [SerializeField] private EnemyLifeController bossLife;
    [SerializeField] private LifeController _playerLife;
    [SerializeField] private SoulController _playerSouls;
    [SerializeField] private BossController _bossController;

    public int damage;
    public int soulsToDrop;

    private void Awake()
    {
        _playerLife = FindObjectOfType<LifeController>();
        _playerSouls = FindObjectOfType<SoulController>();
        bossLife = GetComponent<EnemyLifeController>();
        bossFSM = GetComponent<BossFSM>();
        _bossController = GetComponent<BossController>();
    }

    //-- DO DAMAGE -----------------------------------------------------------------------------------------------------------------

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager") && !_bossController.isFallen)
        {
            Vector3 hitDirection = transform.position;

            _playerLife.RecieveDamage(damage, hitDirection, true);
        }
    }

    //-- GET DAMAGE ----------------------------------------------------------------------------------------------------------------

    public void GetDamage(float[] damage)
    {
        if (!_bossController.isRolling && !_bossController.isDead)
        {
            bossLife.RecieveDamage(damage[0]);

            if (bossLife.currentLife <= 0)
            {
                _playerSouls.AddSouls(soulsToDrop);
                bossFSM.Death();
            }
        }
    }


}
