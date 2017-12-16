using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
	public static CharacterManager instance;

	public float move_speed;

	public Vector2 limit;

	public float shoot_interval = 0.1f;
	protected float shoot_timer = 0;

	protected static List<Bullet> bulletPool = new List<Bullet>();

	[SerializeField]
	protected Bullet bullet01;

	[SerializeField]
	Transform muzzle;

	protected  void Awake () 
	{
		instance = this;
	}

	// Update is called once per frame
	protected virtual void Update ()
	{
		CheckInput();
		UpdateRoll();
		UpdateCharge();
	}

	public void CheckInput()
	{
		Move();
		CheckShoot();
		UpdateTimer();
	}

	void UpdateRoll()
	{

	}

	protected void UpdateTimer()
	{
		if( shoot_timer > 0 )
		{
			shoot_timer -= Time.deltaTime;
		}
	}

	protected void Move()
	{
		// 入力から移動量を計算
		Vector2 movement = new Vector2(	Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * move_speed;
		// 移動量を適用
		transform.position += new Vector3( movement.x, 0, movement.y ) * Time.deltaTime;
		// 制限を超えないようにする
		transform.position = new Vector3( Mathf.Clamp( transform.position.x, -limit.x, limit.x), 0, Mathf.Clamp( transform.position.z, -limit.y, limit.y) );
	}

	protected bool charging = false;
	protected float charge_rate = 1f;
	protected float charge_rate_max = 3f;
	protected float charge_speed = 1f;

	protected void CheckShoot()
	{
		charging = Input.GetKey( KeyCode.C );

		if( Input.GetKeyUp( KeyCode.C ) )
		{
			Shoot();
		}

		if( Input.GetKey( KeyCode.X ) )
		{
			Shoot();
		}
	}

	protected void Shoot()
	{
		if( shoot_timer > 0 )
		{
			return;
		}

		shoot_timer = shoot_interval;

		Bullet bullet = null;

		foreach( Bullet b in bulletPool )
		{
			if( !b.isActiveAndEnabled )
			{
				bullet = b;
				bullet.gameObject.SetActive( true );
				break;
			}
		}

		if( bullet == null ){
			bullet = Instantiate<Bullet>(bullet01);
			bulletPool.Add( bullet );
		}

		bullet.Initialize( muzzle.position, Vector3.forward, charge_rate );
		//		bullet.transform.position = transform.position;	
	}

	protected void UpdateCharge()
	{
		if( !charging )
		{
			charge_rate = 1f;
		}
		else
		{
			if( charge_rate < charge_rate_max )
				charge_rate += charge_speed * Time.deltaTime;
		}
	}

}
