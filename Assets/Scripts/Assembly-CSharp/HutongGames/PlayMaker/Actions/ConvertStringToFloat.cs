namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an String value to an Float value.")]
	public class ConvertStringToFloat : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The String variable to convert to a float value.")]
		public FsmString stringVariable;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an Float variable.")]
		public FsmFloat floatVariable;

		[Tooltip("Repeat every frame. Useful if the String variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			stringVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertStringToFloat();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoConvertStringToFloat();
		}

		private void DoConvertStringToFloat()
		{
			floatVariable.Value = float.Parse(stringVariable.Value);
		}
	}
}
