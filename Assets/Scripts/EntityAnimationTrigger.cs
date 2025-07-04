using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour
{
    private Entity entity;

    void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }
}
