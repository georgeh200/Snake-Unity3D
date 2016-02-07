using UnityEngine;
using System.Collections;

public class SNFood : MonoBehaviour {

	private Snake snake;
	private static SNFood instance;

	public static SNFood getInstance()
	{
		return instance;
	}

	private SNCell myCell;

	public SNFood()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		snake = Snake.getInstance ();
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void eaten()
	{
		this.enabled = false;
		this.myCell.hasFood = false;
		this.myCell = null;
	}
	public void reset()
	{
		this.enabled = false;
		this.myCell = null;
	}
	public void generate()
	{
		if (this.enabled)
			return;
		
		for (int j = 0; j < 5; j++) {
			int r = (int)Mathf.Floor (Random.Range (1, snake.rows - 1));
			int c = (int)Mathf.Floor (Random.Range (1, snake.colums - 1));

			SNCell cell = snake.getCell (r, c);
			if (cell.runningPiece == null) {
				cell.hasFood = true;
				this.myCell = cell;
				this.transform.position = cell.getCenter ();
				this.enabled = true;
				return;
			}
		}



	}
}
