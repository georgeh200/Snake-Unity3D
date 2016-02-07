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
		if (cell.direction != Vector3.zero) {
			this.direction = cell.direction;
			if (snake.lastPieceInSnake (this.gameObject)) {
				cell.direction = Vector3.zero;
			}
		}
	//	Debug.Log ("updateCellupdateCell");
	//	Debug.Log ("row:"+cell.row);
	//	Debug.Log ("column:"+cell.column);
		//Debug.Break ();
		this.transform.position = cell.getCenter ();
	}

	public SNCell getNextCell()
	{
		int rr = this.row;
		int cc = this.column;

		if (this.direction == Vector3.up) {
			rr++;
		} else if (this.direction == Vector3.down) {
			rr--;
		} else if (this.direction == Vector3.left) {
			cc--;
		} else if (this.direction == Vector3.right) {
			cc++;
		}

		return snake.getCell (rr,cc);

	}
}
