using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	public Sun[] SunsToTestIfLevelIsComplete;

	public string NextLevelSceneName;

	private bool _levelIsComplete = false;
	public bool LevelIsComplete
	{
		get
		{
			if (_levelIsComplete)
			{
				return true;
			}
			this._levelIsComplete = this.AllSunsIndicateLevelIsComplete;
			if (this._levelIsComplete)
			{
				this.StartCoroutine("EndLevelAndLoadNextLevel");
			}
			return _levelIsComplete;
		}
	}

	private bool AllSunsIndicateLevelIsComplete
	{
		get
		{
			foreach (Sun sun in this.SunsToTestIfLevelIsComplete)
			{
				if (!sun.LevelIsComplete)
				{
					return false;
				}
			}
			return true;
		}
	}

	public static LevelManager Instance
	{
		get
		{
			return GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		}
	}

	IEnumerator EndLevelAndLoadNextLevel ()
	{
		float timeToStopWaiting = Time.time + 10.0f;
		while (GameObject.FindGameObjectsWithTag("Bouncer").Length > 0 && timeToStopWaiting >= Time.time)
		{
			yield return null;
		}

		Application.LoadLevel(this.NextLevelSceneName);
	}
}
