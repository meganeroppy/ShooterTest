using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float move_speed = 4f;

	private Vector3 movement = Vector3.forward;

	private Vector3 baseScale = new Vector3(.1f, .2f, .1f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Move();

	}

	void Move()
	{
		transform.position += movement * move_speed * Time.deltaTime;

		if( Mathf.Abs( transform.position.x ) > PlayerManager.instance.limit.x * 1.2f || Mathf.Abs( transform.position.z ) > PlayerManager.instance.limit.y * 1.2f )
		{
			gameObject.SetActive( false );
		}
	}

	public void Initialize( Vector3 position, Vector3 movement, float charge_rate=1f)
	{
		transform.position = position;
		this.movement = movement;
		transform.localScale = baseScale * charge_rate;
	}

	/// <summary>
	/// カメラ外いいった時
	/// </summary>
	void OnBecomeInvisible()
	{
		System.Reflection.MethodBase.GetCurrentMethod();
	}
}
