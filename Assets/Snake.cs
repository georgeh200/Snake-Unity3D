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

	// Use this for initialization
	void Start () {

	//	Vector3 p1 = Camera.main.ScreenToWorldPoint(new Vector3(1,Screen.height,1));
	//	Debug.logger.Log ("p1p1p1p1p1:" + p1.y);

		this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.5f,6));

		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,6));
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0.0f,6));
		float unit = Vector3.Distance(p1, p2);
		Debug.Log ("unitunitunit:"+unit);

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
		snakeScreenWidth = Vector3.Distance(p1, p2)-0.5f;	

		p2 = Camera.main.ViewportToWorldPoint(new Vector3(0,1,6));

		snakeScreenHeight = Vector3.Distance(p1, p2) - 0.5f;	

		//this.transform.position = new Vector3 (width / 2 - 0.5f, height / 2 - 0.5f, 0);

		//Debug.Log ("x:"+width/2);
		//Debug.Log ("y:"+height/2);

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


}
