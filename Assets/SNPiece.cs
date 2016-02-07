using UnityEngine;
using System.Collections;

public class SNPiece : MonoBehaviour {

	public int index;
	private Snake snake;
	public Vector3 direction;

	public int row;
	public int column;	

	public SNPiece ()
	{
		snake = Snake.getInstance (	);
	//	this.index = snake.getMyCellIndex ();
	//	Debug.Log ("this.index:"+this.index);
	}
	// Use this for initialization
	void Start () {


		this.direction = snake.direction;

	}
	
	// Update is called once per frame
//	void FixedUpdate () {
		 
//		if (snake.gameStatus != Snake.STATUS_RUNNING)
//			return;
				
	//	SNCell cell = snake.getMyCell (this.gameObject);
	//	Debug.Log ("1:"+this.gameObject.transform.position.x);
	//	Debug.Log ("2:"+this.transform.position.x);
	//	if (cell.direction != Vector3.zero && Vector3.Distance (this.transform.position, cell.getCenter ()) < 0.05f)
	//		this.direction = cell.direction;
		//Debug.Log ("this.direction:"+this.direction.y);		

	
		
//	}

	public void move()
	{
		this.transform.position = transform.position + (this.direction *snake.cellWidth* snake.speed);
	}

	public void updateCell()
	{
		if (this.direction == Vector3.up) {
			this.row++;
		} else if (this.direction == Vector3.down) {
			this.row--;
		} else if (this.direction == Vector3.left) {
			this.column--;
		} else if (this.direction == Vector3.right) {
			this.column++;
		}

		SNCell cell = snake.getCell (this.row, this.column);
		this.transform.position = cell.getCenter ();
	}
}
