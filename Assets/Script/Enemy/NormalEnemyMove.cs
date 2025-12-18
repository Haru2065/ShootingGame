using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemyMove : MonoBehaviour
{
    private enum State
    {
        Enter, //N“ü
        Stop, //’â~
        Exit, //‘Şê
    }

    [Header("“G‚ÌˆÚ“®‘¬“x")]
    [SerializeField, Header("“G‚ÌN“ü‘¬“x")]
    private float enterSpped;

    [SerializeField,Header("“G‚Ì‘Şê‘¬“x")]
    private float exitSpped;

    [SerializeField, Header("“G‚Ì’ÇÕ‘¬“x")]
    private float followSpeed;

    [Header("’â~ŠÔ")]
    [SerializeField]
    private float stopTime;

    private State currentState = State.Enter;

    private Transform player;

    [SerializeField]
    private float stopTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        stopTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Enter:
                EnterMove();
                break;
            
            case State.Stop:
                StopMove();
                break;

            case State.Exit:
                ExitMove();
                break;
            default:
                break;
        }
    }

    private void EnterMove()
    {
        FollowPlayerX();

        transform.Translate(Vector2.down * enterSpped * Time.deltaTime);

        //‰æ–Ê“à‚ÌŠ’èˆÊ’u‚É—ˆ‚½‚ç’â~
        if(transform.position.y <= 3.0f)
        {
            currentState = State.Stop;
            stopTimer = stopTime;
        }
    }

    private void StopMove()
    {
        stopTimer -= Time.deltaTime;

        if(stopTimer <= 0f)
        {
            currentState = State.Exit;
        }
    }

    private void ExitMove()
    {
        transform.Translate(Vector2.down * exitSpped * Time.deltaTime);

        if(transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    private void FollowPlayerX()
    {
        if (player == null) return;

        float targetX = player.position.x;
        float newX = Mathf.Lerp(
            transform.position.x,
            targetX,
            followSpeed * Time.deltaTime
        );

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
