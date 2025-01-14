using DG.Tweening;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Plays backwards all tweens (meaning the tweens that were not already started, playing backwards or rewinded)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsPlayBackwardsAll : FsmStateAction
	{
		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			int num = DOTween.PlayBackwardsAll();
			if (debugThis.Value)
			{
				Debug.Log("GameObject [" + base.State.Fsm.GameObjectName + "] FSM [" + base.State.Fsm.Name + "]  State [" + base.State.Name + "] - DOTween Control Methods Play Backwards All - SUCCESS! - Played " + num + " tweens");
			}
			Finish();
		}
	}
}
