using UnityEditor;
using System.Collections;
using UnityEngine;
using Jolly;

[CustomEditor(typeof(JollyDebug))] 
public class JollyDebugInspector : Editor {
	
	public override void OnInspectorGUI()
	{
		JollyDebug jollyDebug = (JollyDebug)this.target;

		this.DisplayExpressions(jollyDebug);

		base.OnInspectorGUI();
	}

	private void DisplayExpressions (JollyDebug jollyDebug)
	{
		IEnumerator enumerator = jollyDebug.ExpressionsByOwnerEnumerator;
		while (enumerator.MoveNext())
		{
			JollyDebug.ExpressionsByOwner expressionsByOwner = (JollyDebug.ExpressionsByOwner)enumerator.Current;
			expressionsByOwner.Enabled = EditorGUILayout.InspectorTitlebar(expressionsByOwner.Enabled, expressionsByOwner.Owner);
			if (expressionsByOwner.Enabled)
			{
				foreach (JollyDebug.Expression expression in expressionsByOwner.Expressions)
				{
					EditorGUILayout.LabelField(expression.Name, expression.LastValue);
				}
			}
		}
	}
	
	public override bool RequiresConstantRepaint ()
	{
		return true;
	}
}
