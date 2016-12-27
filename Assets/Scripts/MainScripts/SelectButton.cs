using UnityEngine;
using System.Collections;

public class SelectButton : MonoBehaviour {

    //변경할 이미지를 등록
    public SpriteRenderer stageSprite;

    //이미지 목록
    public Sprite[] StageObj = new Sprite[8];

    //현재 이미지 번호
    private int CurrentNum;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        CurrentNum = 0;

        stageSprite.sprite = StageObj[CurrentNum];
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = StageObj[CurrentNum];
    }


    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CurrentNum < 7)
                spriteRenderer.sprite = StageObj[++CurrentNum];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (CurrentNum > 0)
                spriteRenderer.sprite = StageObj[--CurrentNum];
        }*/
    }
    public void PrevButton()
    {
        if (CurrentNum > 0)
            stageSprite.sprite = StageObj[--CurrentNum];
        //            spriteRenderer.sprite = StageObj[--CurrentNum];
    }
    public void NextButton()
    {
        if (CurrentNum < 7)
            stageSprite.sprite = StageObj[++CurrentNum];
        //            spriteRenderer.sprite = StageObj[++CurrentNum];
    }

}
