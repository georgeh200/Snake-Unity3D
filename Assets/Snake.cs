using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

	public GameObject snakeBit;
	public float spawnTime;

	private Vector3 direction=Vector3.forward;
	private float speed=0.9f;
	private float gameTime=0;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameTime += Time.deltaTime;
		Vector3 newPos = transform.position + (this.direction * Time.deltaTime * speed);


		if (!this.boundariesCollide (newPos,this.gameObject))
			this.transform.position = newPos;
	}



	bool boundariesCollide(Vector3 p,GameObject gameObject)
	{
		Renderer renderer= gameObject.GetComponent<Renderer> ();
		p += new Vector3 (renderer.bounds.size.x / 2, renderer.bounds.size.y / 2, renderer.bounds.size.z / 2);

		Vector3 viewPos = Camera.main.WorldToViewportPoint(p);
		viewPos.x = Mathf.Clamp01(viewPos.x);
		viewPos.y = Mathf.Clamp01(viewPos.y);

		Debug.Log ("viewPos.yviewPos.y:"+viewPos.y);

		if(viewPos.y<=0||viewPos.y>=1||viewPos.x<=0||viewPos.x>=1)
			return true;
		else
		return false;
	}


}
