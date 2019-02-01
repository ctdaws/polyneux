using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Polyneux
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class DI : System.Attribute
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void AfterSceneLoad()
        {
            var types = GetClassesWithAnnotation();
            var scene = SceneManager.GetActiveScene();
            var rootGameObjects = new List<GameObject>(scene.rootCount);
            scene.GetRootGameObjects(rootGameObjects);

            // Find MonoBehaviours instances of classes with annotations
            rootGameObjects.SelectMany(g => g.GetComponents<MonoBehaviour>())
                           .Where(b => types.Any(t => b.GetType().IsAssignableFrom(t)))
                           .ToList()
                           .ForEach(b =>
                           {
                               b.GetType()
                                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                .Where(f => f.IsDefined(typeof(Autowire), true))
                                .ToList()
                                .ForEach(f =>
                                {
                                    var gameObject = b.gameObject;
                                    var type = f.FieldType;
                                    Debug.Log("Creating new " + type);
                                    f.SetValue(b, gameObject.AddComponent(type));
                                });
                           });
        }

        private static List<Type> GetClassesWithAnnotation()
        {
            return Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(t =>
                               t.IsClass &&
                               t.Namespace != "Polyneux" &&
                               Attribute.GetCustomAttribute(t, typeof(DI)) != null)
                           .ToList();

            // Alternative with more scope. Hold in reserve in case needed!

            // return AppDomain.CurrentDomain.GetAssemblies()
            //                .SelectMany(t => t.GetTypes())
            //                .Where(t => t.IsClass)
            //                .ToList();
        }
    }

}