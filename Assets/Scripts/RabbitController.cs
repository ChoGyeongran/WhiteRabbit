using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour {

    Rigidbody2D rb2d;
    Animator animator;
    int jumpNum = 0;
    //bool isJump = false;
    bool isDead;

    public float jumpVelocity;
    public GameObject sprite;


    public bool IsDead()
    {
        return isDead;
    } 

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        //죽으면 점프하지 못한다
        if (isDead) return;
        //클릭했을때 점프, 2단 점프까지 가능
        //Input.GetKeyDown(KeyCode.Space)
        if (Input.GetButtonDown("Fire1") && jumpNum<2 ) //!isJump)
        {
            Flap();
        }
        //점프 시 애니메이션 변경
        animator.SetBool("jump", jumpNum != 0);// isJump);
    }
    
    /* 죽었을때 사망 애니메이션으로 변경 */

    public void Flap()
    {
        if (rb2d.isKinematic) return;
        
        //isJump = true;
        if(jumpNum <=2) jumpNum++;
        rb2d.velocity = new Vector2(0.0f, jumpVelocity);
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
}
