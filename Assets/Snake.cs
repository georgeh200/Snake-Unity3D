using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {



	public GameObject prfFood;
	public GameObject prfPiece;


	private ArrayList listCells;
	private ArrayList listPieces;

	private int	colums;
	private int rows;
	private static Snake instance;


	private Vector3 direction=Vector3.up;
	private float speed=0.9f;
	private float gameTime=0;
	private float snakeScreenWidth;
	private float snakeScreenHeight;
	private float gameWidth;
	private float gameHeight;
	private float startX;
	private float startY;


	private float cellWidth=0;
	private float cellHeight=0;
	private Rect rect1;


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


		//instantiate some pieces

		GameObject piece=(GameObject)Instantiate(this.prfPiece, new Vector3 (0, 0, 0), Quaternion.identity);

		this.cellWidth = piece.transform.localScale.x;
		this.cellHeight = piece.transform.localScale.y;

		listPieces.Add (piece);




		setupGame ();

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


		SNCell cell = null;

		for (int j = 0; j < colums; j++)
			for (int i = 0; i < rows; i++) {
				cell = new SNCell ();
				cell.x = startX + j * cellWidth;
				cell.y = startY + i * cellHeight;
				cell.width = cellWidth;
				cell.height = cellHeight;
				listCells.Add (cell);
			}
	//	Debug.Log ("cell count:"+rows);
	//	Debug.Log ("cell count:"+colums);
	//	Debug.Log ("cell count:"+listCells.Count);


		drawGrid ();

	
		addInitialPieces ();





	}

	private void addInitialPieces()
	{
		GameObject piece =(GameObject) listPieces [0];
		int rr = rows / 2;
		int cc = colums / 2;
		piece.transform.position = new Vector3 (startX+ cc * cellWidth+cellWidth/2,startY+ rr * cellHeight+cellHeight/2, 0);

		Vector3 p = Vector3.zero;
		for(int j=rr+1;j<rr+6;j++)
		{
			p=new Vector3 (startX+ cc * cellWidth+cellWidth/2,startY+ j * cellHeight+cellHeight/2, 0);;
			piece=(GameObject)Instantiate(this.prfPiece, p, Quaternion.identity);
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

	// Update is called once per frame
	void Update () {
		if (true)
			return;
		this.handleInput ();
		gameTime += Time.deltaTime;
		Vector3 newPos = transform.position + (this.direction * cellWidth);


		if (!this.boundariesCollide (newPos,this.gameObject))
			this.transform.position = newPos;
	}

	private void handleInput()
	{
		if (Input.GetKey ("up"))
			this.direction = Vector3.up;
		else if (Input.GetKey ("down"))
			this.direction = Vector3.down;
		else if (Input.GetKey ("left"))
			this.direction = Vector3.left;
		else if (Input.GetKey ("right"))
			this.direction = Vector3.right;
		
	}



	bool boundariesCollide(Vector3 p,GameObject gameObject)
	{
		//startX startY 


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
