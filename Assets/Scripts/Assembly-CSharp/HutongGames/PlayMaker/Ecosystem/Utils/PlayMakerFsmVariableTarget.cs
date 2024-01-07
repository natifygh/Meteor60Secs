using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	[Serializable]
	public class PlayMakerFsmVariableTarget
	{
		public ProxyFsmVariableTarget variableTarget;

		public GameObject gameObject;

		public string fsmName;

		[SerializeField]
		private PlayMakerFSM _fsmComponent;

		private FsmVariables _fsmVariables;

		[NonSerialized]
		private bool _initialized;

		public bool isTargetAvailable
		{
			get
			{
				return FsmVariables != null;
			}
		}

		public FsmVariables FsmVariables
		{
			get
			{
				if (_fsmVariables == null || !_initialized)
				{
					Initialize();
				}
				return _fsmVariables;
			}
		}

		public PlayMakerFSM fsmComponent
		{
			get
			{
				Initialize();
				return _fsmComponent;
			}
			set
			{
				_fsmComponent = value;
			}
		}

		public PlayMakerFsmVariableTarget()
		{
		}

		public PlayMakerFsmVariableTarget(ProxyFsmVariableTarget target)
		{
			variableTarget = target;
		}

		public void Initialize(bool forceRefresh = false)
		{
			if (_initialized && !forceRefresh)
			{
				return;
			}
			_initialized = true;
			_fsmVariables = null;
			if (variableTarget == ProxyFsmVariableTarget.GlobalVariable)
			{
				_fsmVariables = FsmVariables.GlobalVariables;
			}
			else if (variableTarget == ProxyFsmVariableTarget.FsmComponent)
			{
				if (_fsmComponent != null)
				{
					_fsmVariables = _fsmComponent.FsmVariables;
					Debug.LogWarning(string.Concat("Setting FsmVariables for ", variableTarget, " _fsmComponent= ", _fsmComponent));
				}
			}
			else
			{
				if (gameObject != null)
				{
					fsmComponent = PlayMakerUtils.FindFsmOnGameObject(gameObject, fsmName);
				}
				if (fsmComponent == null)
				{
					_fsmVariables = null;
				}
				else
				{
					_fsmVariables = _fsmComponent.FsmVariables;
				}
			}
		}

		public override string ToString()
		{
			return string.Format("[PlayMakerFsmVariableTarget: FsmVariables={0}, fsmComponent={1}]", FsmVariables, fsmComponent);
		}
	}
}