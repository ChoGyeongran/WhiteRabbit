
using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour {

    Rigidbody2D rb2d;
    Animator animator;
    int jumpNum = 0;
    //bool isJump = false;
    bool isDead;

    //public float jumpVelocity;
    public GameObject sprite;


    //
    float y = 0.0f;
    float gravity = 0.0f;     // 중력느낌용
    int direction = 0;       // 0:정지상태, 1:점프중, 2:다운중
    // 설정값
    //테스트를 위해 public
    //기본 private const
    const float jump_speed = 0.25f;  // 점프속도 점프높이
    const float jump_accell = 0.018f; // 점프가속
    const float y_base = -3.45f;      // 캐릭터가 서있는 기준점
    //


    public bool IsDead()
    {
        return isDead;
    } 

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();

        //
        y = y_base;
        //
    }

    void Update()
    {
        //죽으면 점프하지 못한다
        if (isDead) return;


        JumpProcess();

        //클릭했을때 점프, 2단 점프까지 가능
        //Input.GetKeyDown(KeyCode.Space)
        if (Input.GetButtonDown("Fire1") && jumpNum<2 ) //!isJump)
        {
            DoJump();
        }

        Vector3 pos = gameObject.transform.position;
        pos.y = y;
        gameObject.transform.position = pos;

        //점프 시 애니메이션 변경
        animator.SetBool("jump", jumpNum != 0);// isJump);
    }
 

    public void DoJump()
    {
        if (rb2d.isKinematic) return;

        direction = 1;
        gravity = jump_speed;

        
        //isJump = true;
        if(jumpNum <=2) jumpNum++;
        /*
        rb2d.velocity = new Vector2(0.0f, jumpVelocity);
        */
    }

    void OnCollisionEnter2D(Collision2D collision) //트리거가 아닌 콜라이더의 충돌 판정
    {
        //if (!isJump) return;
        //isJump = false;
        if (isDead) return;
        if (collision.gameObject.tag != "Ground") {
            animator.SetBool("jump", !isDead);
            isDead = true;
        }

        if (jumpNum == 0) return;
        jumpNum = 0;
    }

    public void SetSteerAcitve(bool active)
    {
        rb2d.isKinematic = !active;
    }




    void JumpProcess()
    {
        switch (direction)
        {
            case 0: // 2단 점프시 처리
                {
                    if (y > y_base)
                    {
                        if (y >= jump_accell)
                        {
                            y -= jump_accell;
                            y -= gravity;
                        }
                        else
                        {
                            y = y_base;
                        }
                    }
                    break;
                }
            case 1: // up
                {
                    y += gravity;
                    if (gravity <= 0.0f)
                    {
                        direction = 2;
                    }
                    else
                    {
                        gravity -= jump_accell;
                    }
                    break;
                }

            case 2: // down
                {
                    y -= gravity;
                    if (y > y_base)
                    {
                        gravity += jump_accell;
                    }
                    else
                    {
                        direction = 0;
                        y = y_base;
                    }
                    break;
                }
        }

    }


}


