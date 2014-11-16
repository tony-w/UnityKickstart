using UnityEngine;
using System.Collections;

public class HeadsUpDisplay : MonoBehaviour
{
	public Texture ScoreBackground;
	public GameObject BluePlayer;
	public GameObject RedPlayer;
	public int WinDistance;

	private bool HasWon = false;

	void OnGUI ()
	{/*
		const float height = 97.0f;
		const float width = 175.0f;
		this.DrawScore(TextAnchor.UpperLeft, this.BluePlayer.GetComponent<Player>(), new Vector2(10.0f, Screen.height - height - 10.0f), width, height);
		this.DrawScore(TextAnchor.UpperRight, this.RedPlayer.GetComponent<Player>(), new Vector2(Screen.width - 10.0f - width, Screen.height - height - 10.0f), width, height);

		const float restartButtonWidth = 100.0f;
		const float restartButtonHeight = 40.0f;

		if (GUI.Button (new Rect((Screen.width - restartButtonWidth)/2, Screen.height - restartButtonHeight - 10.0f, restartButtonWidth, restartButtonHeight), "Restart Game"))
		{
			Application.LoadLevel("ingame");
		}*/
	}

	/**
	 * Check for win-game conditions.
	 */
	void Update ()
	{
		HasWon = Vector3.Distance (this.BluePlayer.GetComponent<Player> ().transform.position, this.RedPlayer.GetComponent<Player> ().transform.position) < WinDistance;
	}
	/*
	void DrawScore(TextAnchor alignment, Player player, Vector2 topLeft, float width, float height)
	{
		GUI.DrawTexture(new Rect(topLeft.x, topLeft.y, width, height), this.ScoreBackground);

		const float topMargin = 15.0f;
		const float outerMargin = 35.0f;
		const float scoreHeight = 40.0f;

		GUIStyle scoreStyle = new GUIStyle("label");
		scoreStyle.fontSize = 20;
		scoreStyle.alignment = alignment;
		scoreStyle.normal.textColor = player.HUDColor;
		GUI.Label (new Rect(topLeft.x + outerMargin, topLeft.y + topMargin, width - outerMargin * 2, height), HasWon ? "You Win!" : "Keep Looking", scoreStyle);

		GUIStyle nameStyle = new GUIStyle("label");
		nameStyle.fontSize = 14;
		nameStyle.alignment = alignment;
		nameStyle.normal.textColor = player.HUDColor;
		GUI.Label (new Rect(topLeft.x + outerMargin, topLeft.y + topMargin + scoreHeight, width - outerMargin * 2, height), player.name, nameStyle);
	}*/
}
