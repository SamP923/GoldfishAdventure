using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {


	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 10f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform leftWallCheck;

	public GameObject Flag;
	public UIManager uiManagerScript;
	public GameManager gameManagerScript;

	//public AudioSource flag_sound;
	public AudioSource collect_sound;

	public int potions = 0;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		transform.position = new Vector3(22f, -11.5f, 0);

		uiManagerScript = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		if (uiManagerScript != null) {
			uiManagerScript.UpdatePotions (potions);
		}

		gameObject.GetComponent<Renderer>().enabled = false;
		gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

		AudioSource[]audios = GetComponents<AudioSource>();
		collect_sound = audios[0];

	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Return)) {
			uiManagerScript.HideTitle();
			gameObject.GetComponent<Renderer>().enabled = true;
			transform.position = new Vector3(22f, -11.5f, 0);
		}

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			anim.SetBool ("walk", true);
		} else {
			anim.SetBool ("walk", false);
		}


		if (transform.position.x > 23f)
			transform.position = new Vector3 (23f, transform.position.y, 0);
		else if (transform.position.x < -23f)
			transform.position = new Vector3 (-23f, transform.position.y, 0);
	

		/*if (Input.GetKeyDown (KeyCode.B)) {
			if (potions != 3) {
				potions += 1;
				uiManagerScript.UpdatePotions (potions);
			}
		}*/
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");

		//anim.SetFloat ("Speed", Mathf.Abs (h));
		if (h * rb2d.velocity.x < maxSpeed) {
			rb2d.AddForce (Vector2.right * h * moveForce);
		}


		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		

		if (h > 0 && facingRight)
			Flip ();
		else if (h < 0 && !facingRight)
			Flip ();

		if (jump)
		{
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Equals ("Flag")) {
			uiManagerScript.EndTitle();
			Destroy( gameObject );

		}
		if (other.name.Contains ("Potion")) {
			collect_sound.Play();
			Destroy(other.gameObject);
			potions += 1;
			uiManagerScript.UpdatePotions (potions);
		}
	}
}


/*if (Input.GetButtonUp ("Horizontal") && grounded) {
				Debug.Log ("lol");
				rb2d.velocity = Vector3.zero;
				float angularVelocity = 0f;
				rb2d.angularVelocity = angularVelocity;
			}
*/