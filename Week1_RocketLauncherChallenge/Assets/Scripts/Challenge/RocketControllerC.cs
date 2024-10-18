using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;
    
    private bool _isMoving;
    private Vector3 _inputDirection;
    [SerializeField]private float _movementDirection;
    
    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }
    
    private void FixedUpdate()
    {
        if (!_isMoving) return;
        
        if(!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;
        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 dir = value.ReadValue<Vector2>().normalized;
        Debug.Log(dir);
        _movementDirection = dir.x; 
        
        _isMoving = dir.sqrMagnitude > 0; 
    }


    // OnBoost 구현
    public void OnBoost(InputAction.CallbackContext value)
    {
        // 부스트 버튼을 눌렀을 때
        if (value.performed)
        {
            // 에너지를 사용할 수 있으면 부스트 적용
            if (_energySystem.UseEnergy(ENERGY_BURST))
            {
                _rocketMovement.ApplyBoost();
            }
        }
    }
}