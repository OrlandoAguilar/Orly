using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class Pinky : MonoBehaviour
{

	public int score = 0;
	public AudioClip die;
	public AudioClip punch;
	public Vector3 checkPoint;
	public float factor = 1.0f;
	private SpriteRenderer myRenderer;
	private Color atack = new Color (0.0f, 1.0f, 0.0f);
	private Color inmuneColor = new Color (1.0f, 0.0f, 0.0f, 0.5f);
	public GameObject objTrial;
	private TrailRenderer trail;
	private float inmune = 0.0f;
	private AudioSource sourcePunch;



	public float time{ get; protected set; }

	public GUISkin GuiSkin;
	public Texture2D jewel;
	public Texture2D barTime;
	public Sprite bocaAbierta;
	public int dieds{ get; protected set; }
	//private TrailRenderer trail;


	public enum state
	{
			NORMAL,
			ATACK,
			DIED,
			PAUSED,
			INMUNE
									}
	;

	//state _actualState;
	public state actualState {
			get;
			protected set;
	}


	// Use this for initialization
	void Start ()
	{
			checkPoint = transform.position;
			Globals.pinky = this;
			Application.targetFrameRate = 30;
			myRenderer = gameObject.GetComponent<SpriteRenderer> ();
			Physics2D.gravity = Vector2.up * -3;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			actualState = state.NORMAL;
			//GameObject trailO=GameObject.Find("trail");
			GameObject trailg = ((GameObject)GameObject.Instantiate (objTrial, transform.position, transform.rotation));
			trail = trailg.GetComponent<TrailRenderer> ();
			trail.transform.parent = transform;
			time = 0;
			dieds = 0;
		sourcePunch=GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update ()
	{

		/*if (Input.GetKeyDown(KeyCode.A)){
			Application.CaptureScreenshot(Application.loadedLevelName+".png");
		}*/
			switch (actualState) {
			case state.DIED:
					if (renderer.isVisible == false) {
							//Application.LoadLevel(Application.loadedLevel);
							rigidbody2D.collider2D.isTrigger = false;
							Controls cntrols = gameObject.GetComponent<Controls> ();
							cntrols.enabled = true;
							transform.position = checkPoint;
							rigidbody2D.velocity = Vector2.zero;
							actualState = state.NORMAL;

							AudioSource au = Camera.main.gameObject.GetComponent<AudioSource> ();
							//au.enabled = true;
							au.Play ();

							ScrollCamera sc = Camera.main.GetComponent<ScrollCamera> ();
							ScrollCamera scam = sc.GetComponent<ScrollCamera> ();
							scam.enabled = true;
							SpriteRenderer sprite=GetComponent<SpriteRenderer> ();
							sprite.sortingOrder = 6;
							transform.localScale=Vector3.one;
							sprite.color=Color.white;
							becomeInmune ();
					}
					break;
			case state.PAUSED:
					rigidbody2D.velocity = Vector3.zero;
					break;
			default:
					time += Time.deltaTime * Time.timeScale;

					if (Controls.Deaccel) {
							rigidbody2D.velocity *= 0.8f;
					}

					if (Controls.Accel) {
							rigidbody2D.velocity *= 1.09f;
					}



					if (inmune > 0.0f) {
							inmune -= Time.deltaTime * Time.timeScale;
							if (inmune < 0) {
									actualState = state.NORMAL;
									myRenderer.color = Color.white;
							}
					} else {
							if (rigidbody2D.velocity.sqrMagnitude > 7.0f) {
									myRenderer.color = atack;
									actualState = state.ATACK;
									if (!trail.enabled)
											trail.enabled = true;
							} else {
									myRenderer.color = Color.white;
									actualState = state.NORMAL;
									if (trail.enabled)
											trail.enabled = false;
							}
					}


					break;
			}

	}

	public void Die ()
	{
		if (actualState != state.DIED && actualState != state.PAUSED && inmune <= 0.0f) {
#if UNITY_ANDROID ||UNITY_IPHONE
			if (Globals.supportVibration) {
				Handheld.Vibrate ();
			}
#endif
			AudioSource.PlayClipAtPoint (die, transform.position);
			actualState = state.DIED;
			rigidbody2D.velocity = Vector2.up * 3;
			Physics2D.gravity = Vector2.up * -5;

			rigidbody2D.collider2D.isTrigger = true;
			Controls cntrols = gameObject.GetComponent<Controls> ();
			cntrols.enabled = false;

			SpriteRenderer sprite=GetComponent<SpriteRenderer> ();
			sprite.sortingOrder = 30;
			sprite.sprite=bocaAbierta;
			transform.localScale=new Vector3(1.5f,1.5f,1.5f);
			sprite.color=new Color(1.0f,1.0f,1.0f,0.8f);

			AudioSource au = Camera.main.gameObject.GetComponent<AudioSource> ();
			//au.enabled = false;
			au.Pause ();

			ScrollCamera sc = Camera.main.GetComponent<ScrollCamera> ();
			ScrollCamera scam = sc.GetComponent<ScrollCamera> ();
			scam.enabled = false;
			dieds++;
		}
	}

	public bool isDied ()
	{
			return actualState == state.DIED;
	}

	public void Paused (bool stateb)
	{
			if (stateb) {
					actualState = state.PAUSED;
			} else {
					actualState = state.NORMAL;
					becomeInmune ();
			}
	}

	public void becomeInmune (float cant=4.0f)
	{
		if (enabled){
			inmune = cant;
			myRenderer.color = inmuneColor;
			trail.enabled = false;
		}
	}

	public void Gana ()
	{
			//Application.LoadLevel (0);//Application.loadedLevel+1);
		GetComponent<EndLevel> ().enabled = true;
		GetComponent<Controls> ().enabled = false;	
		GetComponent<PauseMenu>().enabled=false;
		enabled = false;
								
	}

	void OnGUI ()
	{
		GUI.skin = GuiSkin;
		Vector3 aux = Camera.main.WorldToScreenPoint (transform.position);
		//GUI.Label(new Rect(100,100,100,100),aux.ToString());
		if (aux.x < Screen.width - 190 || aux.y < Screen.height - 100) {
			GUI.Label (new Rect (Screen.width - 190, 5, 195, 100), "");
			
			//GUI.skin.box.fontSize = 15;
			GUI.Box (new Rect (Screen.width - 110, 40, 50, 50), jewel);
			GUI.Box (new Rect (Screen.width - 75, 17, 90, 90), score.ToString ());

			GUI.Box (new Rect (Screen.width - 180, 7, 100, 30), Globals.texts.time);
			GUI.Box (new Rect (Screen.width - 90, 7, 90, 40), Format.FormatTime (time));
			if (inmune>0)
				GUI.DrawTexture(new Rect(Screen.width - 185, 93, Mathf.Min(180,inmune*20), 5), barTime);
			//GUI.Label (new Rect (Screen.width - 185, 85, Mathf.Min(180,inmune*20), 10), barTime);
		}
	}


	void OnCollisionEnter2D(Collision2D col){
		if (!isDied()){// && col.relativeVelocity!=null){
			sourcePunch.volume=Mathf.Min(col.relativeVelocity.sqrMagnitude/20.0f,1);
			sourcePunch.Play();
		}
	}

	public void Checkpoint(Vector3 checkpoint){
		this.checkPoint=checkpoint;
	}

}
