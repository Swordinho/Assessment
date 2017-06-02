using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assessment.Tests
{
    [TestClass]
    public class AssessmentTest
    {
        [TestMethod]
        public void ReadData_WhenFileNotExist()
        {
            BL.DataFile = "data.csv";

            try
            {
                BL.ReadData();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
                return;
            }
        }
    }
}
