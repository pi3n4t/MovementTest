using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals
{
    public static bool CheckCollision(Collision col, GameObject collidingGameObject)
    {
        return col.gameObject == collidingGameObject;
    }

    public static bool CheckCollision(Collider col, GameObject collidingGameObject)
    {
        return col.gameObject == collidingGameObject;
    }

    public static int ChangeValue(int valueToAdd, int currentValue, int maxValue)
    {
        return Mathf.Clamp(currentValue + valueToAdd, 0, maxValue);
    }

    public static float ChangeValue(float valueToAdd, float currentValue, float maxValue)
    {
        return Mathf.Clamp(currentValue + valueToAdd, 0, maxValue);
    }
}