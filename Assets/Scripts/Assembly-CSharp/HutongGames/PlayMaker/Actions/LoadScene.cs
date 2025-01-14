using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Loads the scene by its name or index in Build Settings. ")]
	public class LoadScene : FsmStateAction
	{
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		[Tooltip("Allows you to specify whether or not to load the scene additively. See LoadSceneMode Unity doc for more information about the options.")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadSceneMode;

		[ActionSection("Result")]
		[Tooltip("True if the scene was loaded")]
		public FsmBool success;

		[Tooltip("Event sent if the scene was loaded")]
		public FsmEvent successEvent;

		[Tooltip("Event sent if a problem occured, check log for information")]
		public FsmEvent failureEvent;

		private Scene _scene;

		private bool _sceneFound;

		public override void Reset()
		{
			sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtIndex = null;
			loadSceneMode = null;
			success = null;
			successEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			bool flag = DoLoadScene();
			if (!success.IsNone)
			{
				success.Value = flag;
			}
			if (flag)
			{
				base.Fsm.Event(successEvent);
			}
			else
			{
				base.Fsm.Event(failureEvent);
			}
			Finish();
		}

		private bool DoLoadScene()
		{
			if (sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex)
			{
				if (SceneManager.GetActiveScene().buildIndex == sceneAtIndex.Value)
				{
					return false;
				}
				SceneManager.LoadScene(sceneAtIndex.Value, (LoadSceneMode)(object)loadSceneMode.Value);
			}
			else
			{
				if (SceneManager.GetActiveScene().name == sceneByName.Value)
				{
					return false;
				}
				SceneManager.LoadScene(sceneByName.Value, (LoadSceneMode)(object)loadSceneMode.Value);
			}
			return true;
		}
	}
}
