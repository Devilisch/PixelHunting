using UnityEngine;

public class EntityController
{
    public Vector3 endPoint;
    
    protected EntityView entityView;

    public float Speed { get; protected set; }



    public void OnMove(Vector3 point)
    {
        if (entityView.State == EntityState.DEATH)
            return;
        
        entityView.SetState(EntityState.WALK);
        
        endPoint = point;
    }
    
    public void CustomUpdate(float deltaTime)
    {
        if (entityView.State != EntityState.WALK)
            return;

        var movementVector = endPoint - entityView.transform.position;
        
        entityView.SetSpriteFlip(movementVector.x < 0);

        if (movementVector.magnitude < 0.1f)
        {
            entityView.SetState(EntityState.IDLE);

            entityView.transform.position = endPoint;
        }
        else
        {
            entityView.transform.position += movementVector.normalized * Speed * deltaTime;
        }
    }

    protected void OnHitCollision(Collision2D col)
    {
        var entity = col.gameObject.GetComponent<EntityView>();

        if (entity != null)
            entityView.transform.position += (entityView.transform.position - col.transform.position).normalized;
    }

    public void Show() => entityView.Show();
    public void Hide() => entityView.Hide();
}
