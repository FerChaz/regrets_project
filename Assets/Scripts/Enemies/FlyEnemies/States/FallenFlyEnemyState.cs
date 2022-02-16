using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;

    private float _timeToRecover;

    private const string MAINCOLOR = "_MainColor";

    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.material.SetColor(MAINCOLOR, Color.black);
            _enemyController.StartFallenEfect();

            _enemyController.isAnyStateRunning = true;
            _enemyController.isFall = true;

            _timeToRecover = _enemyController.timeToRecover;

            _movement.Set(0.0f, 0.0f, 0.0f);
            _enemyController.rigidBody.velocity = _movement;

            _enemyController.canvas.SetActive(true);
            // ACTIVAR ANIMACION O EFECTO DE PARTICULAS
        }
    }

    public override void UpdateState(float delta)
    {
        if (_timeToRecover <= 0)
        {
            //_enemyController.gameObject.GetComponent<FlyEnemyCombatController>().RestoreColor();
            _enemyController.alreadyFall = true;
            _enemyController.isAnyStateRunning = false;
        }
        else
        {
            _timeToRecover -= delta;
            _enemyController.CanvasTimeController(_timeToRecover);
        }

        if (!_enemyController.groundDetected)
        {
            _enemyController.rigidBody.AddForce(3f * Physics.gravity); // Falling force = 3f, mismo que en el player
        }
        else
        {
            _movement.Set(0.0f, 0.0f, 0.0f);
            _enemyController.rigidBody.velocity = _movement;
        }
    }

    public override void ExitState()
    {
        _enemyController.material.SetColor(MAINCOLOR, _enemyController.initColorMaterial);
        _enemyController.StopFallenEffect();

        _enemyController.isFall = false;
        _enemyController.canvas.SetActive(false);
    }
}
