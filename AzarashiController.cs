using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
   
    Rigidbody2D rd2d;
    Animator animator;
    float angle;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;

    void Awake()
    {
	rd2d = GetComponent<Rigidbody2D>();
	animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        //最高高度に達していない場合に限りタップ入力を受け付ける
	if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
	{
	    Flap();
	}

	//角度を反映
	ApplyAngle();

	// angelが水平以上だったら、アニメーターのflapフラグをtureにする
	animator.SetBool("flap", angle >= 0.0f);
    }
    
    public void Flap()
    {
	// Velocityを直接書き換えて上方向に加速
	rb2d.velocity = new Vector2(0.0f, flapVelocity);
    } 

    void ApplyAngle()
    {
	// 現在の速度、相対速度から進んでいる角度を求める
	float targetAngle = Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
	
	// 回転アニメをスムージング
	angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

	// Rotationの反映
	sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }
}
