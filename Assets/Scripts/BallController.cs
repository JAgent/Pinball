using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	// ボールが見える可能性のあるz軸の最大値
	private float visiblePosZ = -6.5f;

	// スコア
	public int Score = 0;
	public GameObject Ball;

	// スコアを表示するテキスト
	private GameObject scoreText;



	// ゲームオーバーを表示するテキスト
	private GameObject gameoverText;

	// Use this for initialization
	void Start () {
		// 真鍮のGameOverTextオブジェクトを取得
		this.gameoverText = GameObject.Find("GameOverText");
		// ScoreTextオブジェクトを取得
		scoreText = GameObject.Find("ScoreText");
		scoreText.GetComponent<Text> ().text = "スコア：" + Score ;
	}
	
	// Update is called once per frame
	void Update () {
		// ボールが画面外に出た場合
		if (this.transform.position.z < this.visiblePosZ) {
			// GameoverTextにゲームオーバーを表示する
			this.gameoverText.GetComponent<Text> ().text = "Game Over";
		}
	}

	//オブジェクトが衝突したとき	
	void OnCollisionEnter(Collision other) {
		// タグによって加算するスコアを設定する
		if (other.gameObject.tag == "SmallStarTag") {
			Score = Score + 10;
		} else if(other.gameObject.tag == "LargeStarTag"){
			Score = Score + 100;
		} else if (other.gameObject.tag == "SmallCloudTag" || other.gameObject.tag == "LargeCloudTag") {
			Score = Score + 50;
		}
		scoreText.GetComponent<Text> ().text = "スコア：" + Score;
	}
}
