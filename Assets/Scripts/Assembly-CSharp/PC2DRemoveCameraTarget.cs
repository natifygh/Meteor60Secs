using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;

[Tooltip("Remove a target from the camera")]
public class PC2DRemoveCameraTarget : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[Tooltip("The Transform of the target")]
	public FsmGameObject target;

	[Tooltip("The time it takes for this target to reach a zero influence. Use for a more progressive transition.")]
	public FsmFloat duration = 0f;

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null && (bool)target.Value)
		{
			ProCamera2D.Instance.RemoveCameraTarget(target.Value.transform, duration.Value);
		}
		Finish();
	}
}
