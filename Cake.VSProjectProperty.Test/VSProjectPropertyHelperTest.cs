using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cake.VSProjectProperty.Test
{
    [TestClass]
    public class VSProjectPropertyHelperTest
    {
        [TestMethod]
        public void TestCsprojGlobal()
        {
            ProjectFileHelper helper = new ProjectFileHelper("test.csproj");

            string config = "debug";

            //read tests 
            string AssemblyName = helper.GetProperty("AssemblyName", config);
            Assert.AreEqual(AssemblyName, "AppExeName");

            string RootNamespace = helper.GetProperty("RootNamespace", config);
            Assert.AreEqual(RootNamespace, "Com.App.Desktop");

            string PublishUrl = helper.GetProperty("PublishUrl", config);
            Assert.AreEqual(PublishUrl, "publish\\");

            //set tests
            helper.SetProperty("AssemblyName", "test assmbly name", config);
            helper.SetProperty("RootNamespace", "test root namespace", config);
            helper.SetProperty("PublishUrl", "test publish urls", config);

            helper.Save();
            helper.Reload();

            AssemblyName = helper.GetProperty("AssemblyName", config);
            Assert.AreEqual(AssemblyName, "test assmbly name");

            RootNamespace = helper.GetProperty("RootNamespace", config);
            Assert.AreEqual(RootNamespace, "test root namespace");

            PublishUrl = helper.GetProperty("PublishUrl", config);
            Assert.AreEqual(PublishUrl, "test publish urls");

            //reset to default value 
            helper.SetProperty("AssemblyName", "AppExeName", config);
            helper.SetProperty("RootNamespace", "Com.App.Desktop", config);
            helper.SetProperty("PublishUrl", "publish\\", config);
            helper.Save();
        }

        [TestMethod]
        public void TestCsprojDebug()
        {
            ProjectFileHelper helper = new ProjectFileHelper("test.csproj");

            string config = "debug";
            //read tests 

            string DefineConstants = helper.GetProperty("DefineConstants", config);
            Assert.AreEqual(DefineConstants, "DEBUG;TRACE");

            string WarningLevel = helper.GetProperty("WarningLevel", config);
            Assert.AreEqual(WarningLevel, "7");

            //set tests
            helper.SetProperty("DefineConstants", "Test Debug Defines", config);
            helper.SetProperty("WarningLevel", "8", config);

            helper.Save();
            helper.Reload();

            DefineConstants = helper.GetProperty("DefineConstants", config);
            Assert.AreEqual(DefineConstants, "Test Debug Defines");

            WarningLevel = helper.GetProperty("WarningLevel", config);
            Assert.AreEqual(WarningLevel, "8");

            //reset to default value 
            helper.SetProperty("DefineConstants", "DEBUG;TRACE", config);
            helper.SetProperty("WarningLevel", "7", config);
            helper.Save();
        }

        [TestMethod]
        public void TestRelease()
        {
            ProjectFileHelper helper = new ProjectFileHelper("test.csproj");

            string config = "release";
            //read tests 

            string DefineConstants = helper.GetProperty("DefineConstants",config);
            Assert.AreEqual(DefineConstants, "TRACE");

            string WarningLevel = helper.GetProperty("WarningLevel", config);
            Assert.AreEqual(WarningLevel, "4");

            //set tests
            helper.SetProperty("DefineConstants", "Test Release Defines", config);
            helper.SetProperty("WarningLevel", "5", config);

            helper.Save();
            helper.Reload();

            DefineConstants = helper.GetProperty("DefineConstants", config);
            Assert.AreEqual(DefineConstants, "Test Release Defines");

            WarningLevel = helper.GetProperty("WarningLevel", config);
            Assert.AreEqual(WarningLevel, "5");

            //reset to default value 
            helper.SetProperty("DefineConstants", "TRACE", config);
            helper.SetProperty("WarningLevel", "4", config);
            helper.Save();
        }


        [TestMethod]
        public void TestVcxprojReleaseX64()
        {
            ProjectFileHelper helper = new ProjectFileHelper("cpulspuls.vcxproj");

            string config = "release";
            string platform = "x64";

            //read tests 
            string def = helper.GetProperty("PreprocessorDefinitions", config, platform);

            string newdef = "DX_11_M;" + def;

            //set tests
            helper.SetProperty("PreprocessorDefinitions", newdef, config, platform);

            helper.Save();

            helper.Reload();

            string rdef = helper.GetProperty("PreprocessorDefinitions", config, platform);
            Assert.AreEqual(rdef, newdef);
        }
    }
}
