using UnityEngine;
using System.Collections;

public class SNPiece : MonoBehaviour {

	public int index;
	private Snake snake;
	public Vector3 direction=Vector3.zero;

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




	}
	


	public void move()
	{
		if(this.direction==Vector3.zero)
		this.direction = snake.direction;

		this.transform.position = this.transform.position + (this.direction *snake.cellWidth* snake.speed);
	}


	public void reverseMove()
	{	
			
		this.transform.position = this.transform.position - (this.direction *snake.cellWidth* snake.speed);
	}

	public void updateCell()
	{
		SNCell cell = snake.getCell (this.row, this.column);
		cell.runningPiece = null;
		cell.exitPiece = this.gameObject;

		if (this.direction == Vector3.up) {
			this.row++;
		} else if (this.direction == Vector3.down) {
			this.row--;
		} else if (this.direction == Vector3.left) {
			this.column--;
		} else if (this.direction == Vector3.right) {
			this.column++;
		}

		 cell = snake.getCell (this.row, this.column);
		if (cell.runningPiece != null) {
			snake.snakeCollideitSelf ();
		}
		cell.runningPiece = this.gameObject;
		if (cell.hasFood) {
			snake.eatFood ();
			cell.hasFood = false;
		}

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

	public void attachPiece(GameObject piece)
	{
		int rr = this.row;
		int cc = this.column;

		Vector3 pp = this.transform.position;
		Vector3 pos = Vector3.zero;
		if (this.direction == Vector3.up) {
			rr-=1;
			pos = new Vector3 (pp.x, pp.y-this.transform.localScale.y, 0);
		} else if (this.direction == Vector3.down) {
			rr+=1;
			pos = new Vector3 (pp.x, pp.y+this.transform.localScale.y, 0);
		} else if (this.direction == Vector3.left) {
			cc+=1;
			pos = new Vector3 (pp.x+this.transform.localScale.x, pp.y, 0);
		} else if (this.direction == Vector3.right) {
			cc-=1;
			pos = new Vector3 (pp.x-this.transform.localScale.x, pp.y, 0);
		}

		piece.transform.position = pos;
		SNCell cell = snake.getCell (rr, cc);

		piece.GetComponent<SNPiece> ().direction = this.direction;
		piece.GetComponent<SNPiece> ().row = rr;
		piece.GetComponent<SNPiece> ().column = cc;
		cell.runningPiece=piece;



	}
}
