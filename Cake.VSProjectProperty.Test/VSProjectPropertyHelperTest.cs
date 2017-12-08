using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cake.VSProjectProperty.Test
{
    [TestClass]
    public class VSProjectPropertyHelperTest
    {
        [TestMethod]
        public void TestGlobal()
        {
            VSProjectPropertyHelper helper = new VSProjectPropertyHelper("test.csproj", "Debug");

            //read tests 
            string AssemblyName = helper.GetProperty("AssemblyName");
            Assert.AreEqual(AssemblyName, "AppExeName");

            string RootNamespace = helper.GetProperty("RootNamespace");
            Assert.AreEqual(RootNamespace, "Com.App.Desktop");

            string PublishUrl = helper.GetProperty("PublishUrl");
            Assert.AreEqual(PublishUrl, "publish\\");

            //set tests
            helper.SetProperty("AssemblyName", "test assmbly name");
            helper.SetProperty("RootNamespace", "test root namespace");
            helper.SetProperty("PublishUrl", "test publish urls");

            helper.Save();
            helper.Reload();

            AssemblyName = helper.GetProperty("AssemblyName");
            Assert.AreEqual(AssemblyName, "test assmbly name");

            RootNamespace = helper.GetProperty("RootNamespace");
            Assert.AreEqual(RootNamespace, "test root namespace");

            PublishUrl = helper.GetProperty("PublishUrl");
            Assert.AreEqual(PublishUrl, "test publish urls");

            //reset to default value 
            helper.SetProperty("AssemblyName", "AppExeName");
            helper.SetProperty("RootNamespace", "Com.App.Desktop");
            helper.SetProperty("PublishUrl", "publish\\");
            helper.Save();
        }

        [TestMethod]
        public void TestDebug()
        {
            VSProjectPropertyHelper helper = new VSProjectPropertyHelper("test.csproj", "Debug");

            //read tests 

            string DefineConstants = helper.GetProperty("DefineConstants");
            Assert.AreEqual(DefineConstants, "DEBUG;TRACE");

            string WarningLevel = helper.GetProperty("WarningLevel");
            Assert.AreEqual(WarningLevel, "7");

            //set tests
            helper.SetProperty("DefineConstants", "Test Debug Defines");
            helper.SetProperty("WarningLevel", "8");

            helper.Save();
            helper.Reload();

            DefineConstants = helper.GetProperty("DefineConstants");
            Assert.AreEqual(DefineConstants, "Test Debug Defines");

            WarningLevel = helper.GetProperty("WarningLevel");
            Assert.AreEqual(WarningLevel, "8");

            //reset to default value 
            helper.SetProperty("DefineConstants", "DEBUG;TRACE");
            helper.SetProperty("WarningLevel", "7");
            helper.Save();
        }

        [TestMethod]
        public void TestRelease()
        {
            VSProjectPropertyHelper helper = new VSProjectPropertyHelper("test.csproj", "Debug");

            //read tests 

            string DefineConstants = helper.GetProperty("DefineConstants","Release");
            Assert.AreEqual(DefineConstants, "TRACE");

            string WarningLevel = helper.GetProperty("WarningLevel", "Release");
            Assert.AreEqual(WarningLevel, "4");

            //set tests
            helper.SetProperty("DefineConstants", "Test Release Defines", "Release");
            helper.SetProperty("WarningLevel", "5", "Release");

            helper.Save();
            helper.Reload();

            DefineConstants = helper.GetProperty("DefineConstants", "Release");
            Assert.AreEqual(DefineConstants, "Test Release Defines");

            WarningLevel = helper.GetProperty("WarningLevel", "Release");
            Assert.AreEqual(WarningLevel, "5");

            //reset to default value 
            helper.SetProperty("DefineConstants", "TRACE", "Release");
            helper.SetProperty("WarningLevel", "4", "Release");
            helper.Save();
        }




    }
}
