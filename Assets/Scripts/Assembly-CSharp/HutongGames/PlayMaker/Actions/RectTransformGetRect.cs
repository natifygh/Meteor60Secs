using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The calculated rectangle in the local space of the Transform.")]
	public class RectTransformGetRect : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The rect")]
		public FsmRect rect;

		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		[UIHint(UIHint.Variable)]
		public FsmFloat width;

		[UIHint(UIHint.Variable)]
		public FsmFloat height;

		private RectTransform _rt;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			rect = null;
			x = new FsmFloat
			{
				UseVariable = true
			};
			y = new FsmFloat
			{
				UseVariable = true
			};
			width = new FsmFloat
			{
				UseVariable = true
			};
			height = new FsmFloat
			{
				UseVariable = true
			};
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			DoGetValues();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoGetValues();
		}

		private void DoGetValues()
		{
			if (!rect.IsNone)
			{
				rect.Value = _rt.rect;
			}
			if (!x.IsNone)
			{
				x.Value = _rt.rect.x;
			}
			if (!y.IsNone)
			{
				y.Value = _rt.rect.y;
			}
			if (!width.IsNone)
			{
				width.Value = _rt.rect.width;
			}
			if (!height.IsNone)
			{
				height.Value = _rt.rect.height;
			}
		}
	}
}
