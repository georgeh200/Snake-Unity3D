using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {


	public GameObject food;
	public float spawnTime;

	private Vector3 direction=Vector3.up;
	private float speed=0.9f;
	private float gameTime=0;
	private float snakeScreenWidth;
	private float snakeScreenHeight;
	private float gameWidth;
	private float gameHeight;
	private float startX;
	private float startY;

	private ArrayList listEdges;  // for edge lines
	private ArrayList listGrid; // for grid lines
	private float cellWidth=0;
	private Rect rect1;


	public Snake()
	{
		listGrid = new ArrayList ();
		listEdges = new ArrayList ();
	}

	// Use this for initialization
	void Start () {
		this.cellWidth = this.transform.localScale.x;

	//	Vector3 p1 = Camera.main.ScreenToWorldPoint(new Vector3(1,Screen.height,1));
	//	Debug.logger.Log ("p1p1p1p1p1:" + p1.y);

		this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.5f,6));

		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,6));
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0.0f,6));
		float unit = Vector3.Distance(p1, p2);


		//Vector3 p2 = Camera.main.ScreenToWorldPoint(Vector3.right);
		//float unit = Vector3.Distance(p1, p2);
		//Debug.Log ("unitunitunit:"+unit);

	//	Vector3 p = Camera.main.WorldToScreenPoint (transform.position);
	//	p=Camera.main.ScreenToViewportPoint (new Vector3 (Screen.width/2, 0, 0));
	//	Debug.logger.Log ("Screen.geo:"+p.y);
	//	Debug.logger.Log ("Screen.width:" + Screen.height);

	//	this.transform.position = p;

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

		int	colums =  (int)Mathf.Floor( (snakeScreenWidth-0.5f) / cellWidth);
		int	rows= (int)Mathf.Floor((snakeScreenHeight-0.5f) / cellWidth);

		gameWidth = colums * cellWidth;
		gameHeight= rows * cellWidth;
		startX = (snakeScreenWidth - gameWidth) / 2;
		startY = (snakeScreenHeight - gameHeight) / 2;

		Debug.Log ("snakeScreenWidth:"+snakeScreenWidth);
		Debug.Log ("snakeScreenHeight:"+snakeScreenHeight);


		Debug.Log ("gameWidth:"+gameWidth);
		Debug.Log ("gameHeight:"+gameHeight);

		Debug.Log ("startXstartX:"+startX);
		Debug.Log ("startXstartyy:"+startY);


	}

	// Update is called once per frame
	void Update () {
		if (true)
			return;
		this.handleInput ();
		gameTime += Time.deltaTime;
		Vector3 newPos = transform.position + (this.direction * Time.deltaTime * speed);


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
		Renderer renderer= gameObject.GetComponent<Renderer> ();
		p += new Vector3 (renderer.bounds.size.x / 2, renderer.bounds.size.y / 2, renderer.bounds.size.z / 2);

		Vector3 viewPos = Camera.main.WorldToViewportPoint(p);
		viewPos.x = Mathf.Clamp01(viewPos.x);
		viewPos.y = Mathf.Clamp01(viewPos.y);



		if(viewPos.y<=0||viewPos.y>=1||viewPos.x<=0||viewPos.x>=1)
			return true;
		else
		return false;
	}


	private Texture2D whiteTexture;
	void OnGUI()
	{
		if(this.whiteTexture==null)
			this.whiteTexture=Texture2D.whiteTexture;
		
	//	drawHorizontalLine (new Vector2 (10, 10), new Vector2 (100, 10),Color.red);

	//	drawVerticalLine (new Vector2 (10, 10), new Vector2 (100, 100),Color.red);

	//	GUI.Label (, new Color(0,0,0,1));
	}

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
