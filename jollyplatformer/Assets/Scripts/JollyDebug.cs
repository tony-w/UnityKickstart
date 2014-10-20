using UnityEngine;
using System.Collections;
using Jolly;


namespace Jolly
{
	public class JollyDebug : MonoBehaviour
	{
		public class Expression
		{
			public string Name;
			public string LastValue;

			public System.Func<float> ReturnsFloat;
			public System.Func<string> ReturnsString;
			public System.Func<bool> ReturnsBool;

			public Expression(string name)
			{
				this.Name = name;
			}

			public Expression(string name, System.Func<float> returnsFloat)
			{
				this.Name = name;
				this.ReturnsFloat = returnsFloat;
			}

			public Expression(string name, System.Func<string> returnsString)
			{
				this.Name = name;
				this.ReturnsString = returnsString;
			}

			public Expression(string name, System.Func<bool> returnsBool)
			{
				this.Name = name;
				this.ReturnsBool = returnsBool;
			}

			public void Update()
			{
				if (null != this.ReturnsFloat)
				{
					this.SetLastValue(this.ReturnsFloat());
				}
				else if (null != this.ReturnsString)
				{
					this.SetLastValue(this.ReturnsString());
				}
				else if (null != this.ReturnsBool)
				{
					this.SetLastValue(this.ReturnsBool());
				}
			}

			public void SetLastValue(float floatValue)
			{
				this.LastValue = floatValue.ToString("0.00");
			}

			public void SetLastValue(string stringValue)
			{
				this.LastValue = stringValue;
			}

			public void SetLastValue(bool boolValue)
			{
				this.LastValue = boolValue.ToString();
			}
		};

		public class ExpressionsByOwner
		{
			public MonoBehaviour Owner;
			public ArrayList Expressions = new ArrayList();
			public bool Enabled = true;

			public ExpressionsByOwner(MonoBehaviour owner)
			{
				this.Owner = owner;
			}

			public void Add(Expression expression)
			{
				this.Expressions.Add (expression);
			}

			public Expression GetExpression(string name)
			{
				foreach (Expression expression in this.Expressions)
				{
					if (expression.Name.Equals(name))
					{
						return expression;
					}
				}
				Expression newExpression = new Expression(name);
				this.Add (newExpression);
				return newExpression;
			}

			public void Update()
			{
				if (!this.Enabled)
				{
					return;
				}
				JollyDebug.Assert(!this.OwnerIsMissing);
				foreach (Expression expression in this.Expressions)
				{
					expression.Update();
				}
			}

			public bool OwnerIsMissing
			{
				get
				{
					return null == this.Owner;
				}
			}
		}


		private ArrayList ExpressionsByOwnerList = new ArrayList();
		public IEnumerator ExpressionsByOwnerEnumerator
		{
			get
			{
				return this.ExpressionsByOwnerList.GetEnumerator();
			}
		}

		private static JollyDebug _instance = null;
		public static JollyDebug Instance
		{
			get
			{
				if (null != JollyDebug._instance)
				{
					return JollyDebug._instance;
				}

				GameObject go = GameObject.Find("JollyDebug");
				JollyDebug instance = null;

				if (go)
				{
					instance = go.GetComponent<JollyDebug>();
				}
				else
				{
					go = new GameObject("JollyDebug");
				}
				if (!instance)
				{
					instance = go.AddComponent("JollyDebug") as JollyDebug;
				}

				JollyDebug._instance = instance;
				return instance;
			}
		}

		private ExpressionsByOwner GetExpressionsForOwner(MonoBehaviour owner)
		{
			foreach (ExpressionsByOwner expressionsByOwner in this.ExpressionsByOwnerList)
			{
				if (expressionsByOwner.Owner == owner)
				{
					return expressionsByOwner;
				}
			}
			ExpressionsByOwner newExpressionsByOwner = new ExpressionsByOwner(owner);
			this.ExpressionsByOwnerList.Add (newExpressionsByOwner);
			return newExpressionsByOwner;
		}


		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Watch (MonoBehaviour owner, string name, System.Func<float> returnsFloat)
		{
			JollyDebug self = JollyDebug.Instance;
			self.GetExpressionsForOwner(owner).Add (new Expression(name, returnsFloat));
		}

		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Watch (MonoBehaviour owner, string name, System.Func<string> returnsString)
		{
			JollyDebug self = JollyDebug.Instance;
			self.GetExpressionsForOwner(owner).Add (new Expression(name, returnsString));
		}

		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Watch (MonoBehaviour owner, string name, System.Func<bool> returnsBool)
		{
			JollyDebug self = JollyDebug.Instance;
			self.GetExpressionsForOwner(owner).Add (new Expression(name, returnsBool));
		}

		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Watch (MonoBehaviour owner, string name, float floatValue)
		{
			JollyDebug self = JollyDebug.Instance;
			self.GetExpressionsForOwner(owner).GetExpression(name).SetLastValue(floatValue);
		}

		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Watch (MonoBehaviour owner, string name, string stringValue)
		{
			JollyDebug self = JollyDebug.Instance;
			self.GetExpressionsForOwner(owner).GetExpression(name).SetLastValue(stringValue);
		}

		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Watch (MonoBehaviour owner, string name, bool boolValue)
		{
			JollyDebug self = JollyDebug.Instance;
			self.GetExpressionsForOwner(owner).GetExpression(name).SetLastValue(boolValue);
		}

		void Update ()
		{
			for (int i = this.ExpressionsByOwnerList.Count - 1; i >= 0; --i)
			{
				ExpressionsByOwner expressionsByOwner = (ExpressionsByOwner)this.ExpressionsByOwnerList[i];
				if (expressionsByOwner.OwnerIsMissing)
				{
					this.ExpressionsByOwnerList.RemoveAt (i);
				}
				else
				{
					expressionsByOwner.Update();
				}
			}
		}

		[System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Assert(bool expression)
		{
			if (!expression)
			{
				throw new UnityException("Assertion failed!");
			}
		}
	}
}
