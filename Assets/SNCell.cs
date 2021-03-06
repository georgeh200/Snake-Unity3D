﻿using UnityEngine;
using System.Collections;

public class SNCell  {
	public Vector3 direction=Vector3.zero	;
	public float x;
	public float y;
	public float width;
	public float height;
	public int row;
	public int column;		

	public GameObject 	runningPiece;
	public GameObject exitPiece;
	public bool hasFood = false;

	public Vector3 getCenter()
	{
		return new Vector3 (this.x + this.width / 2, this.y + this.height / 2, 0);
	}

	public void reset()
	{
		this.runningPiece = null;
		this.exitPiece = null;
		this.hasFood = false;
	}
}
