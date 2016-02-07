﻿using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {


	public const int STATUS_RUNNING = 1;
	public const int STATUS_GAME_OVER = 2;

	public GameObject prfFood;
	public GameObject prfPiece;



	public int gameStatus;

	private GameObject head;
	private ArrayList listCells;
	private SNCell [,] arrCells;
	private ArrayList listPieces;

	private int	colums;	
	private int rows;
	private static Snake instance;


	public Vector3 direction=Vector3.down;
	public float speed=0.1f;
	private float gameTime=0;
	private float snakeScreenWidth;
	private float snakeScreenHeight;
	private float gameWidth;
	private float gameHeight;
	private float startX;
	private float startY;


	public float cellWidth=0;
	public float cellHeight=0;
	private Rect rect1;

	private float dist;


	public Snake()
	{
		
		listCells = new ArrayList ();
		listPieces = new ArrayList ();
		instance = this;
	}

	public static Snake getInstance()
	{
		return instance;
	}

	// Use this for initialization
	void Start () {
		this.direction = Vector3.down;

		//instantiate some pieces

		head=(GameObject)Instantiate(this.prfPiece, new Vector3 (0, 0, 0), Quaternion.identity);

		this.cellWidth = head.transform.localScale.x;
		this.cellHeight = head.transform.localScale.y;

		listPieces.Add (head);




		setupGame ();
		this.gameStatus = Snake.STATUS_RUNNING;
		direction = Vector3.down;

	}


	public int getMyCellIndex()
	{
		return listPieces.Count;
	}
	public bool isLastPiece(SNPiece piece)
	{
		return (piece.index == (listPieces.Count - 1));
	}

	public SNCell getCell(int r, int c)
	{
		return arrCells[r,c];
	}

	public SNCell getMyCell(GameObject piece)
	{
		SNCell cell = null;
		for (int j = 0; j < listCells.Count; j++) {
			cell = (SNCell)listCells [j];

			if (direction.y > 0) {
				Debug.Log ("direction.y > 0");
				if((piece.transform.position.y+piece.transform.localScale.y/2)<(cell.y+cell.height)&&
				(piece.transform.position.y+piece.transform.localScale.y/2)>cell.y
					&&Mathf.Abs( cell.x-piece.transform.position.x-piece.transform.localScale.x/2)<0.05f)
				{
				return cell;
				}

			}
			else if (direction.y < 0) {
				Debug.Log ("direction.y < 0)");
				if((piece.transform.position.y-piece.transform.localScale.y/2)>cell.y&&
					(piece.transform.position.y-piece.transform.localScale.y/2)<(cell.y+cell.height)
					&&Mathf.Abs( cell.x-piece.transform.position.x-piece.transform.localScale.x/2)<0.05f)
				{
					return cell;
				}

			
				
			}
			else if (direction.x > 0) {
				Debug.Log ("direction.x > 0");
				if((piece.transform.position.x+piece.transform.localScale.x/2)>cell.x&&
					(piece.transform.position.x+piece.transform.localScale.x/2)<(cell.x+cell.width)
					&&Mathf.Abs(cell.y-piece.transform.position.y-piece.transform.localScale.y/2)<0.05f)
				{
					return cell;
				}

				
			}
			else if (direction.x < 0) {
				Debug.Log ("(direction.x < 0");
				if((piece.transform.position.x-piece.transform.localScale.x/2)>cell.x&&
					(piece.transform.position.x-piece.transform.localScale.x/2)<(cell.x+cell.width)
					&&Mathf.Abs(cell.y-piece.transform.position.y-piece.transform.localScale.y/2)<0.05f)
				{
					return cell;
				}
				
			}
		}

		return null;
	}

	private void setupGame()
	{
		//get width and heigh of screen in
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0,0,6));
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0,6));
		snakeScreenWidth = Vector3.Distance(p1, p2);	

		p2 = Camera.main.ViewportToWorldPoint(new Vector3(0,1,6));

		snakeScreenHeight = Vector3.Distance(p1, p2) ;	

			colums =  (int)Mathf.Floor( (snakeScreenWidth-0.5f) / cellWidth);
			rows= (int)Mathf.Floor((snakeScreenHeight-0.5f) / cellHeight);

		gameWidth = colums * cellWidth;
		gameHeight= rows * cellHeight;
		startX =  snakeScreenWidth/2-(snakeScreenWidth - gameWidth) / 2;
		startY = snakeScreenHeight/2-(snakeScreenHeight - gameHeight) / 2;

		startX *= -1;
		startY *= -1;

		Debug.Log ("snakeScreenWidth:"+snakeScreenWidth);
		Debug.Log ("snakeScreenHeight:"+snakeScreenHeight);


		Debug.Log ("gameWidth:"+gameWidth);
		Debug.Log ("gameHeight:"+gameHeight);

		Debug.Log ("startXstartX:"+startX);
		Debug.Log ("startXstartyy:"+startY);

		arrCells = new SNCell[rows,colums];

		SNCell cell = null;

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < colums; j++) {

				cell = new SNCell ();
				cell.x = startX + j * cellWidth;
				cell.y = startY + i * cellHeight;
				cell.width = cellWidth;
				cell.height = cellHeight;
				cell.row = i;
				cell.column = j;
				arrCells [i,j] = cell;
			}
		}



		for (int j = 0; j < colums; j++)
			for (int i = 0; i < rows; i++) {
				cell = new SNCell ();
				cell.x = startX + j * cellWidth;
				cell.y = startY + i * cellHeight;
				cell.width = cellWidth;
				cell.height = cellHeight;
				listCells.Add (cell);
			}
	


		drawGrid ();

	
		addInitialPieces ();


		for (int j = 0; j < listCells.Count; j++) {

		//	Debug.Log (((SNCell)listCells [j]).x);
		}


	}

	private void addInitialPieces()
	{
		
		int rr = rows / 2;
		int cc = colums / 2;
		head.transform.position = new Vector3 (startX+ cc * cellWidth+cellWidth/2,startY+ rr * cellHeight+cellHeight/2, 0);
		SNPiece pp = head.GetComponent<SNPiece> ();
		pp.row = rr;
		pp.column = cc;

	//	Debug.Log ("head.transform.position:" + head.transform.position.x);	

		GameObject piece = null;
		Vector3 p = Vector3.zero;
		for(int j=rr+1;j<rr+2;j++)
		{
			p=new Vector3 (startX+ cc * cellWidth+cellWidth/2,startY+ j * cellHeight+cellHeight/2, 0);
			piece=(GameObject)Instantiate(this.prfPiece, p, Quaternion.identity);
			pp = piece.GetComponent<SNPiece> ();
			pp.row = rr;
			pp.column = cc;
			listPieces.Add (piece);
		}
	}


	private void drawGrid()
	{
		LineRenderer line = getEdgeLine ();

		line.SetPosition(0,new Vector3(startX,startY,0));
		line.SetPosition(1,new Vector3(startX+gameWidth,startY,0));

		line = getEdgeLine ();
		line.SetPosition(0,new Vector3(startX+gameWidth,startY,0));
		line.SetPosition(1,new Vector3(startX+gameWidth,startY+gameHeight,0));


		line = getEdgeLine ();
		line.SetPosition(0,new Vector3(startX+gameWidth,startY+gameHeight,0));
		line.SetPosition(1,new Vector3(startX,startY+gameHeight,0));


		line = getEdgeLine ();

		line.SetPosition(0,new Vector3(startX,startY+gameHeight,0));
		line.SetPosition(1,new Vector3(startX,startY,0));





	//	for (int j = 0; j < colums; j++)
	//		for (int i = 0; i < rows; i++) {


		for (int j = 1; j < colums; j++) {
			line = getGridLine ();
			line.SetPosition (0, new Vector3 (startX + j * cellWidth, startY, 0));
			line.SetPosition (1, new Vector3 (startX + j * cellWidth, startY+gameHeight, 0));
		}

		for (int j = 1; j < rows; j++) {
			line = getGridLine ();
			line.SetPosition (0, new Vector3 (startX , startY+j*cellHeight, 0));
			line.SetPosition (1, new Vector3 (startX + gameWidth, startY+j*cellHeight, 0));
		}



	}

	private LineRenderer getEdgeLine()
	{
		GameObject gameObject = new GameObject ("");
		gameObject.AddComponent<LineRenderer> ();
		LineRenderer line = gameObject.GetComponent<LineRenderer> ();
		line.SetWidth (0.05f, 0.05f);
		line.SetVertexCount (2);
		return line;
	}

	private LineRenderer getGridLine()
	{
		GameObject gameObject = new GameObject ("");
		gameObject.AddComponent<LineRenderer> ();
		LineRenderer line = gameObject.GetComponent<LineRenderer> ();
		line.SetWidth (0.01f, 0.01f);
		line.SetVertexCount (2);
		return line;
	}



	void FixedUpdate () {

		GameObject piece = null;

		for (int j = 0; j < listPieces.Count; j++) {
			piece = (GameObject)listPieces [j];
			piece.GetComponent<SNPiece> ().move ();
		}

		this.dist += this.cellWidth * this.speed;

		if (this.dist >= this.cellWidth) {
			
			for (int j = 0; j < listPieces.Count; j++) {
				piece = (GameObject)listPieces [j];
				piece.GetComponent<SNPiece> ().updateCell ();
			}
		}

	}
	// Update is called once per frame
	void Update () {

		if (this.gameStatus != Snake.STATUS_RUNNING)
			return;
		if (this.boundariesCollide (head)) {
			this.gameStatus = Snake.STATUS_GAME_OVER;
			return ;
		}
		this.handleInput ();
		gameTime += Time.deltaTime;


	

	}

	private void handleInput()
	{
		SNCell cell = getCell (head.GetComponent<SNPiece> ().row, head.GetComponent<SNPiece> ().column);

		
		if (Input.GetKey ("up")) {
			cell.direction = Vector3.up;

		} else if (Input.GetKey ("down")) {
			cell.direction = Vector3.down;
			
		} else if (Input.GetKey ("left")) {
			cell.direction = Vector3.left;
			
		} else if (Input.GetKey ("right")) {

			cell.direction = Vector3.right;

		}
		
	}



	bool boundariesCollide(GameObject gameObject)
	{
		//startX startY 
		Vector3 p=gameObject.transform.position;

		float w = gameObject.transform.localScale.x;
		float h = gameObject.transform.localScale.y;

		float x = gameObject.transform.position.x;
		float y = gameObject.transform.position.y;

		if (x - w / 2 < startX)
			return true;
		if ((x + w / 2) > (startX + gameWidth))
			return true;


		if (y - h / 2 < startY)
			return true;
		if ((y + h / 2) > (startY + gameHeight))
			return true;


		return false;


	/*	Renderer renderer= gameObject.GetComponent<Renderer> ();
		p += new Vector3 (renderer.bounds.size.x / 2, renderer.bounds.size.y / 2, renderer.bounds.size.z / 2);

		Vector3 viewPos = Camera.main.WorldToViewportPoint(p);
		viewPos.x = Mathf.Clamp01(viewPos.x);
		viewPos.y = Mathf.Clamp01(viewPos.y);



		if(viewPos.y<=0||viewPos.y>=1||viewPos.x<=0||viewPos.x>=1)
			return true;
		else
		return false;*/
	}









	private Texture2D whiteTexture;
//	void OnGUI()
//	{
	//	if(this.whiteTexture==null)
	//		this.whiteTexture=Texture2D.whiteTexture;
		
	//	drawHorizontalLine (new Vector2 (10, 10), new Vector2 (100, 10),Color.red);

	//	drawVerticalLine (new Vector2 (10, 10), new Vector2 (100, 100),Color.red);

	//	GUI.Label (, new Color(0,0,0,1));
//	}

	private void drawHorizontalLine(Vector2 p1,Vector2 p2,Color color)
	{
		GUI.color = color;
		GUI.DrawTexture(new Rect (p1.x, p1.y, p2.x, 2), this.whiteTexture, ScaleMode.StretchToFill);
	}

	private void drawVerticalLine(Vector2 p1,Vector2 p2,Color color)
	{
		GUI.color = color;
		GUI.DrawTexture(new Rect (p1.x, p1.y, 2, p2.y), this.whiteTexture, ScaleMode.StretchToFill);
	}

}
