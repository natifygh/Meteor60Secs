using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("iTween")]
	[Tooltip("Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.")]
	public class iTweenPunchPosition : iTweenFsmAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		[RequiredField]
		[Tooltip("A vector punch range.")]
		public FsmVector3 vector;

		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		public Space space;

		[Tooltip("Restricts rotation to the supplied axis only.")]
		public AxisRestriction axis;

		public override void Reset()
		{
			base.Reset();
			id = new FsmString
			{
				UseVariable = true
			};
			time = 1f;
			delay = 0f;
			loopType = iTween.LoopType.none;
			vector = new FsmVector3
			{
				UseVariable = true
			};
			space = Space.World;
			axis = AxisRestriction.none;
		}

		public override void OnEnter()
		{
			OnEnteriTween(gameObject);
			if (loopType != 0)
			{
				IsLoop(true);
			}
			DoiTween();
		}

		public override void OnExit()
		{
			OnExitiTween(gameObject);
		}

		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				Vector3 vector = Vector3.zero;
				if (!this.vector.IsNone)
				{
					vector = this.vector.Value;
				}
				itweenType = "punch";
				iTween.PunchPosition(ownerDefaultTarget, iTween.Hash("amount", vector, "name", (!id.IsNone) ? id.Value : string.Empty, "time", (!time.IsNone) ? time.Value : 1f, "delay", (!delay.IsNone) ? delay.Value : 0f, "looptype", loopType, "oncomplete", "iTweenOnComplete", "oncompleteparams", itweenID, "onstart", "iTweenOnStart", "onstartparams", itweenID, "ignoretimescale", !realTime.IsNone && realTime.Value, "space", space, "axis", (axis != 0) ? Enum.GetName(typeof(AxisRestriction), axis) : string.Empty));
			}
		}
	}
}
