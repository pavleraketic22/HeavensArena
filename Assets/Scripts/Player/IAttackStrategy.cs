using UnityEngine;

public interface IAttackStrategy
{
    void Attack(Vector3? position = null);
}