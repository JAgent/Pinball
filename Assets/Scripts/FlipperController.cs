﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlipperController : MonoBehaviour {
	// HingeJointコンポーネントを入れる

	// HigeJointコンポーネントを入れる
	private HingeJoint myHingeJoint;

	// 初期の傾き
	private float defaultAngle = 20;
	// 弾いた時の傾き
	private float flickAngle = -20;
	public BallController ballcontroller;
	public GameObject Ball;

	// 左右の指識別
	private int L_finger;
	private int R_finger;

	private int GO_flag;

	// Use this for initialization
	void Start () {
		// HingeJointコンポーネント取得
		this.myHingeJoint = GetComponent<HingeJoint>();
		// ballcontrollerコンポーネント取得
		ballcontroller = Ball.GetComponent<BallController>();

		// フリッパーの傾きを設定
		SetAngle(this.defaultAngle);

		// 指識別子の初期化
		L_finger = 0;
		R_finger = 0;

	}
	
	// Update is called once per frame
	void Update () {

		GO_flag = ballcontroller.GO_flag;

		// 左矢印方向キーを押したとき左手フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFlipperTag") {
			SetAngle (this.flickAngle);
		}
		// 右矢印方向キーを押したとき右手フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFlipperTag") {
			SetAngle (this.flickAngle);
		}

		// 矢印方向キーを離した時フリッパーを戻す
		if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFlipperTag") {
			SetAngle (this.defaultAngle);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFlipperTag") {
			SetAngle (this.defaultAngle);
		}

//		Touch[] myTouches = Input.touches;
		for(int i = 0; i < Input.touchCount; i++)
		{
			Touch t = Input.GetTouch(i);
//			var id = Input.touches[i].fingerId;
			var pos = Input.touches[i].position;
			if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) {
				// 触れている
				if (t.phase == TouchPhase.Began) {
					// 左画面をタッチしたとき左手フリッパーを動かす
					if (pos.x < Screen.width / 2 && tag == "LeftFlipperTag") {
						// 画面中央より左がタッチされたときの処理
						SetAngle (this.flickAngle);
						L_finger = t.fingerId;
					}
					// 右画面をタッチしたとき右手フリッパーを動かす
					if (pos.x > Screen.width / 2 && tag == "RightFlipperTag") {
						SetAngle (this.flickAngle);
						R_finger = t.fingerId;
					}
				}
				if (GO_flag == 1) {
					SceneManager.LoadScene ("GameScene");
				}

			}
			else {
				// それ以外の場合は触れていない
				if (t.fingerId == L_finger && tag == "LeftFlipperTag") {
					SetAngle (this.defaultAngle);
				}
				if (t.fingerId == R_finger && tag == "RightFlipperTag") {
					SetAngle (this.defaultAngle);
				}
			}
		}
		// 矢印方向キーを離した時フリッパーを戻す
		if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFlipperTag") {
			SetAngle (this.defaultAngle);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFlipperTag") {
			SetAngle (this.defaultAngle);
		}

	}

	// フリッパーの傾きを設定
	public void SetAngle (float angle){
		JointSpring jointSpr = this.myHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoint.spring = jointSpr;
	}
}