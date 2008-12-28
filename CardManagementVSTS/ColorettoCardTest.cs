using CardManagement.Coloretto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CardManagementVSTS
{
    /// <summary>
    ///This is a test class for ColorettoCardTest and is intended
    ///to contain all ColorettoCardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ColorettoCardTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        ///A test for ColorettoCard Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ColorettoCard_ConstructorTest0()
        {
            ColorettoCard target = new ColorettoCard(ColorettoCardColors.None);
            Assert.Fail("An invalid ColorettoCard was allowed to be created.");
        }

        /// <summary>
        ///A test To make sure that a none color is allowed 
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ColorettoCard_ConstructorTest1()
        {
            ColorettoCard target = new ColorettoCard(ColorettoCardTypes.Plus2, ColorettoCardColors.Yellow);
            Assert.Fail("An invalid ColorettoCard was allowed to be created.");
        }

        /// <summary>
        ///A test To make sure that a none color is allowed 
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ColorettoCard_ConstructorTest2()
        {
            ColorettoCard target = new ColorettoCard(ColorettoCardTypes.Color, ColorettoCardColors.None);
            Assert.Fail("An invalid ColorettoCard was allowed to be created.");
        }

        /// <summary>
        ///A test for ColorettoCard Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ColorettoCard_ConstructorTest3()
        {
            ColorettoCard target = new ColorettoCard(ColorettoCardTypes.Color);
            Assert.Fail("An invalid ColorettoCard was allowed to be created.");
        }
    }
}
