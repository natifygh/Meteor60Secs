using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sends event when Called by the EventSystem when a drag has been found, but before it is valid to begin the drag.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class uGuiOnInitializePotentialDragEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnInitializePotentialDrag is called")]
		public FsmEvent onInitializePotentialDragEvent;

		private GameObject _go;

		private EventTrigger _trigger;

		private EventTrigger.Entry entry;

		public override void Reset()
		{
			gameObject = null;
			onInitializePotentialDragEvent = null;
		}

		public override void OnEnter()
		{
			_go = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(_go == null))
			{
				_trigger = _go.GetComponent<EventTrigger>();
				if (_trigger == null)
				{
					_trigger = _go.AddComponent<EventTrigger>();
				}
				if (entry == null)
				{
					entry = new EventTrigger.Entry();
				}
				entry.eventID = EventTriggerType.InitializePotentialDrag;
				entry.callback.AddListener(delegate(BaseEventData data)
				{
					OnInitializePotentialDragDelegate((PointerEventData)data);
				});
				_trigger.triggers.Add(entry);
			}
		}

		public override void OnExit()
		{
			entry.callback.RemoveAllListeners();
			_trigger.triggers.Remove(entry);
		}

		private void OnInitializePotentialDragDelegate(PointerEventData data)
		{
			GetLastPointerDataInfo.lastPointeEventData = data;
			base.Fsm.Event(onInitializePotentialDragEvent);
		}
	}
}
