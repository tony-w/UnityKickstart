using UnityEngine;
using System.Collections;

public class HeadsUpDisplay : MonoBehaviour
{
	public Texture ScoreBackground;
	public GameObject BluePlayer;
	public GameObject RedPlayer;
	public int WinDistance;

	private bool HasWon = false;
	private bool BlueHasEndocron = false;
	private bool RedHasEndocron = false;

	void OnGUI ()
	{
		const float height = 47.0f;
		const float width = 225.0f;
		this.DrawScore(TextAnchor.UpperLeft, this.BluePlayer.GetComponent<Player>(), new Vector2(10.0f, Screen.height - height - 10.0f), width, height);
		this.DrawScore(TextAnchor.UpperRight, this.RedPlayer.GetComponent<Player>(), new Vector2(Screen.width - 10.0f - width, Screen.height - height - 10.0f), width, height);
		
		GUIStyle locationStyle = new GUIStyle("label");
		locationStyle.fontSize = 14;
		locationStyle.alignment = TextAnchor.UpperLeft;
		locationStyle.normal.textColor = this.BluePlayer.GetComponent<Player>().HUDColor;
		GUI.Label (new Rect( 0, 0, width, height), this.BluePlayer.GetComponent<Player>().transform.position.ToString(), locationStyle);
		
		locationStyle = new GUIStyle("label");
		locationStyle.fontSize = 14;
		locationStyle.alignment = TextAnchor.UpperRight;
		locationStyle.normal.textColor = this.RedPlayer.GetComponent<Player>().HUDColor;
		GUI.Label (new Rect( Screen.width - width, 0, width, height), this.RedPlayer.GetComponent<Player>().transform.position.ToString(), locationStyle);

		const float restartButtonWidth = 100.0f;
		const float restartButtonHeight = 40.0f;

		if (GUI.Button (new Rect((Screen.width - restartButtonWidth)/2, Screen.height - restartButtonHeight - 10.0f, restartButtonWidth, restartButtonHeight), "Restart Game"))
		{
			Application.LoadLevel("ingame");
		}
	}

	/**
	 * Check for win-game conditions.
	 */
	void Update ()
	{
		BlueHasEndocron = this.BluePlayer.GetComponent<Player> ().IsHoldingItem ("Endocron");
		RedHasEndocron = this.RedPlayer.GetComponent<Player> ().IsHoldingItem ("Endocron");
		HasWon = Vector3.Distance (this.BluePlayer.GetComponent<Player> ().transform.position, this.RedPlayer.GetComponent<Player> ().transform.position) < WinDistance
			&& BlueHasEndocron && RedHasEndocron;

		// We Won :D
		if (HasWon) Application.Quit ();
	}

	void DrawScore(TextAnchor alignment, Player player, Vector2 topLeft, float width, float height)
	{
		bool HasEndocron = player.IsHoldingItem ("Endocron");

		GUI.DrawTexture(new Rect(topLeft.x, topLeft.y, width, height), this.ScoreBackground);

		const float topMargin = 10.0f;
		const float outerMargin = 35.0f;
		const float scoreHeight = 40.0f;

		GUIStyle scoreStyle = new GUIStyle("label");
		scoreStyle.fontSize = 10;
		scoreStyle.alignment = alignment;
		scoreStyle.normal.textColor = player.HUDColor;
		GUI.Label (new Rect(topLeft.x + outerMargin, topLeft.y + topMargin, width - outerMargin * 2, height), HasEndocron ? "You found the Endocron, find your partner." : "Keep looking for the Endocron.", scoreStyle);

		GUIStyle nameStyle = new GUIStyle("label");
		nameStyle.fontSize = 14;
		nameStyle.alignment = alignment;
		nameStyle.normal.textColor = player.HUDColor;
		GUI.Label (new Rect(topLeft.x + outerMargin, topLeft.y + topMargin + scoreHeight, width - outerMargin * 2, height), player.name, nameStyle);
	}
}
