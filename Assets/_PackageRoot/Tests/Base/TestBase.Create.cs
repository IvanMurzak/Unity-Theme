using System;
using System.Collections;
using Unity.Theme.Binders;
using UnityEngine;

namespace Unity.Theme.Tests.Base
{
    public partial class TestBase
    {
        public B CreateColorBinder<T, B>(out T target) where B : GenericColorBinder<T> where T : Component
        {
            var gameObject = new GameObject();

            target = gameObject.AddComponent<T>();
            return gameObject.AddComponent<B>();
        }
    }
}