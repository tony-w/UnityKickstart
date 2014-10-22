using UnityEngine;
using System.Collections;
using Jolly;

public class Sun : MonoBehaviour
{
	public Color UnpoweredColor;
	public Color PoweredColor;

	public float TimeToFullPower;
	private float PowerIncreasePerUnit;
	private float Power = 0.0f;
	private float LerpedPower = 0.0f;


	private ArrayList timesOfLastUnits = new ArrayList();

	void Start ()
	{
		float totalObjectsPerSecond = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("SpawnPoint"))
		{
			SpawnsObjectStream spawnsObjectStream = go.GetComponent<SpawnsObjectStream>();
			totalObjectsPerSecond += spawnsObjectStream.ObjectsPerSecond;
		}
		JollyDebug.Assert (totalObjectsPerSecond > 0);
		Debug.Log (string.Format ("tops = {0}", totalObjectsPerSecond));
		this.PowerIncreasePerUnit = 1.0f / totalObjectsPerSecond;
	}

	void Update ()
	{
		float sum = 0.0f;
		for (int i = this.timesOfLastUnits.Count - 1; i >= 0; --i)
		{
			float unitTime = (float)this.timesOfLastUnits[i];
			if (Time.time - unitTime > 1.0f)
			{
				this.timesOfLastUnits.RemoveRange(0, i+1);
				break;
			}
			sum += this.PowerIncreasePerUnit;
		}
		this.Power = sum;
		this.LerpedPower = Mathf.Lerp (this.LerpedPower, this.Power, Time.deltaTime);
		JollyDebug.Watch (this, "Power", this.Power);
		JollyDebug.Watch (this, "LerpedPower", this.LerpedPower);
	}

	public Color Color
	{
		get
		{
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
			return spriteRenderer.color;
		}
		set
		{
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
			spriteRenderer.color = value;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Bouncer bouncer = other.gameObject.GetComponent<Bouncer>();
		if (null != bouncer)
		{
			this.timesOfLastUnits.Add (Time.time);
			GameObject.Destroy(other.gameObject);
		}
	}
}
