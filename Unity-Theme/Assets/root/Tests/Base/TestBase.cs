using System.Collections;
using UnityEngine;

namespace Unity.Theme.Tests.Base
{
    public partial class TestBase
    {
        DebugLevel startDebugLevel;
        string startThemeName;
        public virtual IEnumerator SetUp()
        {
            Debug.Log($"<b>Test Start </b> ------------------------------------------");
            startDebugLevel = Theme.Instance.debugLevel;
            startThemeName = Theme.Instance.CurrentThemeName;
            Theme.Instance.debugLevel = DebugLevel.Trace;
            TestUtils.BuildTestTheme();
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;
            yield return null;
        }
        public virtual IEnumerator TearDown()
        {
            CleanScene();
            Theme.Instance.CurrentThemeName = startThemeName;
            TestUtils.DeleteTestTheme();
            Theme.Instance.debugLevel = startDebugLevel;
            Debug.Log($"<b>Test End </b> --------------------------------------------");
            yield return null;
        }

        public void CleanScene()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
                GameObject.DestroyImmediate(obj);
        }
    }
}