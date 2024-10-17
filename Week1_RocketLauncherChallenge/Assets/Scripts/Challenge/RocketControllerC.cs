using UnityEngine;
using UnityEngine.InputSystem;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;
    
    private bool _isMoving;
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
        
        _movementDirection = Mathf.Atan2(dir.y, dir.x);
        Debug.Log(_movementDirection);
        // 방향값이 0이 아니면 움직이는 상태로 전환
        _isMoving = _movementDirection != 0;
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