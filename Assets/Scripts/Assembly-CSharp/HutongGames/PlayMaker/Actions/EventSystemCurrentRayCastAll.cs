using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("The eventType will be executed on all components on the GameObject that can handle it.")]
	public class EventSystemCurrentRayCastAll : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The ScreenPosition in pixels")]
		public FsmVector3 screenPosition;

		[Tooltip("The ScreenPosition in a Vector2")]
		public FsmVector2 orScreenPosition2d;

		[Tooltip("GameObjects hit by the raycast")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray gameObjectList;

		[Tooltip("Number of hits")]
		[UIHint(UIHint.Variable)]
		public FsmInt hitCount;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private PointerEventData pointer;

		private List<RaycastResult> raycastResults = new List<RaycastResult>();

		public override void Reset()
		{
			screenPosition = null;
			orScreenPosition2d = new FsmVector2
			{
				UseVariable = true
			};
			gameObjectList = null;
			hitCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			ExecuteRayCastAll();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			ExecuteRayCastAll();
		}

		private void ExecuteRayCastAll()
		{
			pointer = new PointerEventData(EventSystem.current);
			if (!orScreenPosition2d.IsNone)
			{
				pointer.position = orScreenPosition2d.Value;
			}
			else
			{
				pointer.position = new Vector2(screenPosition.Value.x, screenPosition.Value.y);
			}
			EventSystem.current.RaycastAll(pointer, raycastResults);
			if (!hitCount.IsNone)
			{
				hitCount.Value = raycastResults.Count;
			}
			gameObjectList.Resize(raycastResults.Count);
			int index = 0;
			foreach (RaycastResult raycastResult in raycastResults)
			{
				if (!gameObjectList.IsNone)
				{
					gameObjectList.Set(index, raycastResult.gameObject);
				}
			}
		}
	}
}
