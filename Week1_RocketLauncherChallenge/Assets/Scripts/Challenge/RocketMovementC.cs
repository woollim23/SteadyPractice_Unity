using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 0.02f;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void ApplyMovement(float movementDirection)
    {
        Rotate(movementDirection);  // 먼저 회전
    }

    public void ApplyBoost()
    {
        _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
    }

    private void Rotate(float inputX)
    {
        // 기존 회전 값을 오일러 각도로 변환하여 Z축 회전 값을 추출
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float newZRotation;
        if (inputX > 0)
        {
            // 입력값을 곱한 후 Z축에 더해 새로운 회전 값 설정
            newZRotation = currentRotation.z + inputX * 0.02f * Mathf.Rad2Deg;
        }
        else
        {
            newZRotation = currentRotation.z + (-3.141593f) * 0.02f * Mathf.Rad2Deg;
            
        }
        Debug.Log(newZRotation);
        transform.rotation = Quaternion.Euler(0, 0, newZRotation);

        //_rb2d.AddForce(transform.up * SPEED, ForceMode2D.Force);
    }


}