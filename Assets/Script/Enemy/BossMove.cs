using UnityEngine;

public class BossMove : MonoBehaviour
{
    private enum MoveState
    {
        Enter,
        Phase1,
        Phase2,
        Phase3,
    }

    [Header("移動設定")]
    [SerializeField, Header("侵入速度")]
    private float enterSpeed;

    [SerializeField, Header("")]
    private float horizontalSpeed;

    [Header("位置設定")]
    [SerializeField]
    private float enterTargetY;

    [SerializeField]
    private float leftLimitX;

    [SerializeField]
    private float rightLimitX;

    [Header("フェーズ時間")]
    [SerializeField]
    private float phase1Duration;

    [SerializeField]
    private float phase2Duration;

    [SerializeField]
    private float phase3Duration;

    private MoveState currentState = MoveState.Enter;

    private float stateTimer;

    private int moveDir = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateTimer = phase1Duration;
    }

    // Update is called once per frame
    void Update()
    {
        //自身が破壊されたら即座に終了
        if (this == null) return;

        switch(currentState)
        {
            case MoveState.Enter:
                EnterMove();
                break;

            case MoveState.Phase1:
                PhaseStop(phase1Duration, MoveState.Phase2);
                break;

            case MoveState.Phase2:
                PhaseHorizontalMove(phase2Duration, MoveState.Phase3);
                break;

            case MoveState.Phase3:

                //最終停止
                break;
        }
    }

    private void EnterMove()
    {
        transform.position += Vector3.down * enterSpeed * Time.deltaTime;

        if(transform.position.y <= enterTargetY)
        {
            transform.position = new Vector3(
                transform.position.x,
                enterTargetY,
                transform.position.z
            );

            currentState = MoveState.Phase1;
            stateTimer = phase1Duration;
        }
    }

    private void PhaseStop(float duration, MoveState nextState)
    {
        stateTimer -= Time.deltaTime;
        if(stateTimer <= 0f)
        {
            currentState = nextState;
            stateTimer = duration;
        }
    }

    private void PhaseHorizontalMove(float duration, MoveState nextState)
    {
        transform.position += Vector3.right * horizontalSpeed * moveDir * Time.deltaTime;

        if (transform.position.x <= leftLimitX || transform.position.x >= rightLimitX)
        {
            moveDir *= -1;
        }

        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            currentState = nextState;
            stateTimer = duration;
        }
    }
}
