using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {


	public const int STATUS_RUNNING = 1;
	public const int STATUS_GAME_OVER = 2;

	public GameObject prfFood;
	public GameObject prfPiece;




	public int gameStatus;

	private GameObject head;


	private SNCell [,] arrCells;
	private ArrayList listPieces;

	public int	colums;	
	public int rows;
	private static Snake instance;

	private int foodNumber;


	public Vector3 direction=Vector3.down;
	[HideInInspector] 

	public float speed;

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
		


		listPieces = new ArrayList ();
		instance = this;
	}

	public static Snake getInstance()
	{
		return instance;
	}

	// Use this for initialization
	void Start () {
		this.speed = 0.1f;
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



	private void setupGame()
	{
		//get width and heigh of screen in
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0,0,6));
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0,6));
		snakeScreenWidth = Vector3.Distance(p1, p2);	

		p2 = Camera.main.ViewportToWorldPoint(new Vector3(0,1,6));

		snakeScreenHeight = Vector3.Distance(p1, p2) ;	

			colums =  (int)Mathf.Floor( (snakeScreenWidth-1.0f) / cellWidth);
			rows= (int)Mathf.Floor((snakeScreenHeight-1.0f) / cellHeight);

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




	


		drawGrid ();

	
		addInitialPieces ();





	}

	public bool lastPieceInSnake(GameObject piece)
	{
		return (listPieces [listPieces.Count - 1] == piece);
	}


	private void addInitialPieces()
	{
		
		int rr = rows / 2;
		int cc = colums / 2;
		head.transform.position = new Vector3 (startX+ cc * cellWidth+cellWidth/2,startY+ rr * cellHeight+cellHeight/2, 0);
		SNPiece pp = head.GetComponent<SNPiece> ();
		pp.row = rr;
		pp.column = cc;
		getCell (rr, cc).runningPiece = head;

	//	Debug.Log ("head.transform.position:" + head.transform.position.x);	

		GameObject piece = null;
		Vector3 p = Vector3.zero;
		for(int j=rr+1;j<rr+5;j++)
		{
			p=new Vector3 (startX+ cc * cellWidth+cellWidth/2,startY+ j * cellHeight+cellHeight/2, 0);
			piece=(GameObject)Instantiate(this.prfPiece, p, Quaternion.identity);
			pp = piece.GetComponent<SNPiece> ();
			pp.row = j;
			pp.column = cc;
			getCell (j, cc).runningPiece = piece;
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


	public void snakeCollideitSelf()
	{
		this.gameStatus = Snake.STATUS_GAME_OVER;
		Debug.Break ();
	}

	public void eatFood()
	{
		this.foodNumber++;
		SNFood.getInstance ().eaten ();
		//Debug.Break ();
		GameObject newPiece=(GameObject)Instantiate(this.prfPiece, new Vector3 (0, 0, 0), Quaternion.identity);

		GameObject lastPiece = (GameObject)listPieces [listPieces.Count - 1];
		lastPiece.GetComponent<SNPiece> ().attachPiece (newPiece);

		listPieces.Add (newPiece);

	}
	void FixedUpdate () {


		if (this.gameStatus != Snake.STATUS_RUNNING)
			return;

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
			this.dist = 0;
		}

		if (this.boundariesCollide (this.head)) {
			this.gameStatus = Snake.STATUS_GAME_OVER;
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
		SNFood.getInstance ().generate ();


	

	}

	private void handleInput()
	{
		
		SNPiece sn = head.GetComponent<SNPiece> ();
		SNCell cell = sn.getNextCell ();

		if (sn.direction != Vector3.up && sn.direction != Vector3.down) {
			if (Input.GetKey ("up")) {
				cell.direction = Vector3.up;

			} else if (Input.GetKey ("down")) {
				cell.direction = Vector3.down;
			
			} 
		}

		if (sn.direction != Vector3.left && sn.direction != Vector3.right) {
			if (Input.GetKey ("left")) {
				cell.direction = Vector3.left;
			
			} else if (Input.GetKey ("right")) {

				cell.direction = Vector3.right;

			}
		}


		//Debug.Break ();
		
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


	
	}

	private GUIStyle guiStyle = new GUIStyle();
	void OnGUI()
	{
		guiStyle.fontSize = 15;

		GUI.Label(new Rect(5, 5, 100, 30), "Food: "+this.foodNumber, guiStyle);


		if (this.gameStatus == Snake.STATUS_GAME_OVER) {
			guiStyle.fontSize = 30;
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2, 100, 30), "GAME OVER", guiStyle);

			guiStyle.fontSize = 15;
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2+30, 100, 30), "Click here to restart", guiStyle);
		}
	}










}
