using DG.Tweening;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Sends all tweens to their end position (has no effect with tweens that have infinite loops).")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsCompleteAll : FsmStateAction
	{
		[UIHint(UIHint.FsmBool)]
		[Tooltip("For Sequences only: if TRUE internal Sequence callbacks will be fired, otherwise they will be ignored.")]
		public FsmBool withCallbacks;

		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			withCallbacks = new FsmBool
			{
				UseVariable = false,
				Value = false
			};
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			int num = DOTween.CompleteAll(withCallbacks.Value);
			if (debugThis.Value)
			{
				Debug.Log("GameObject [" + base.State.Fsm.GameObjectName + "] FSM [" + base.State.Fsm.Name + "]  State [" + base.State.Name + "] - DOTween Control Methods Complete All - SUCCESS! - Completed " + num + " tweens");
			}
			Finish();
		}
	}
}
