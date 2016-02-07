using UnityEngine;
using System.Collections;

public class SNPiece : MonoBehaviour {

	public int index;
	private Snake snake;
	public Vector3 direction;

	public SNPiece ()
	{
		snake = Snake.getInstance ();
		this.index = snake.getMyCellIndex ();
	//	Debug.Log ("this.index:"+this.index);
	}
	// Use this for initialization
	void Start () {


		this.direction = snake.direction;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		 
		if (snake.gameStatus != Snake.STATUS_RUNNING)
			return;
			
		this.direction = snake.direction;
		//Debug.Log ("this.direction:"+this.direction.y);		
		if(this.direction.x==0)
			this.transform.position = transform.position + (this.direction *snake.cellHeight/10* snake.speed);
		else this.transform.position = transform.position + (this.direction *snake.cellWidth/10* snake.speed);
		
	}
}
