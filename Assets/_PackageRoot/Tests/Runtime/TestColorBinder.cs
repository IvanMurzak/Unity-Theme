using System.Collections;
using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine.TestTools;

namespace Unity.Theme.Tests.Runtime
{
    public partial class TestColorBinder : TestBase
    {
        [UnitySetUp] public override IEnumerator SetUp() => base.SetUp();
        [UnityTearDown] public override IEnumerator TearDown() => base.TearDown();
    }
}