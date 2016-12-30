using UnityEngine;
using System.Collections;

public class ScrollObject : MonoBehaviour {

    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;

    void Update()
    {
        //일정시간마다 오브젝트를 왼쪽으로 이동
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        if (transform.position.x <= endPosition) ScrollEnd();
    }

	void ScrollEnd ()
    {
        //시작지점으로 다시 이동
        transform.Translate(-1 * (endPosition - startPosition), 0, 0);

        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }

    
}
