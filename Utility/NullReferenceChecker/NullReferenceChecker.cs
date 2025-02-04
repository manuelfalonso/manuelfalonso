using System.Reflection;
using UnityEngine;

namespace SombraStudios.Shared.Utility.NullReferenceChecker
{
    /// <summary>
    /// Utility class to check for unassigned required fields in MonoBehaviours and ScriptableObjects.
    /// Logs an error if a <see cref="SerializeField"/> is not assigned in the Inspector.
    /// Ignores fields marked with the <see cref="OptionalAttribute"/>.
    /// </summary>
    public static class NullReferenceChecker
    {
        /// <summary>
        /// Validates all serialized fields in the given instance and logs errors for unassigned fields.
        /// </summary>
        /// <param name="instance">The object to validate. Can be a MonoBehaviour, ScriptableObject, 
        /// or other class.</param>
        public static void Validate(object instance)
        {
            if (instance == null)
            {
                Debug.LogError("NullReferenceChecker: Cannot validate a null instance.");
                return;
            }

            System.Type instanceType = instance.GetType();

            // Cache all non-static fields (both public and private) in the instance
            FieldInfo[] fields = instanceType.GetFields(
                BindingFlags.Instance | 
                BindingFlags.NonPublic | 
                BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                // Ignore non-serialized fields and those marked as optional
                if (!field.IsDefined(typeof(SerializeField), inherit: true) 
                    || field.IsDefined(typeof(OptionalAttribute), inherit: true))
                {
                    continue;
                }

                // If the field is unassigned, log an error
                if (field.GetValue(instance) == null)
                {
                    string message = $"Missing assignment for field: {field.Name} in {instance.GetType().Name}";

                    // Determine the type of instance and log accordingly
                    if (instance is MonoBehaviour monoBehaviour)
                    {
                        Debug.LogError($"{message} on GameObject: {monoBehaviour.gameObject.name}", 
                            monoBehaviour.gameObject);
                    }
                    else if (instance is ScriptableObject scriptableObject)
                    {
                        Debug.LogError($"{message} on ScriptableObject: {scriptableObject.name}");
                    }
                    else
                    {
                        Debug.LogError(message);
                    }
                }
            }
        }
    }
}
