using NUnit.Framework;

namespace RobotCleaner
{
    [TestFixture]
    public class PerimeterHuggerStrategyTests
    {
        [Test]
        public void Robot_Cleans_Perimeter_Successfully()
        {
         
            Map map = new Map(5, 5);
            map.AddDirt(0, 0);
            map.AddDirt(4, 0);
            map.AddDirt(4, 4);
            map.AddDirt(0, 4);

            IStrategy strategy = new PerimeterHuggerStrategy();
            Robot robot = new Robot(map, strategy);

            robot.Move(0, 0);
            robot.StartCleaning();

            Assert.That(map.IsDirt(0, 0), Is.False, "Top-left corner should be cleaned");
            Assert.That(map.IsDirt(4, 0), Is.False, "Top-right corner should be cleaned");
            Assert.That(map.IsDirt(4, 4), Is.False, "Bottom-right corner should be cleaned");
            Assert.That(map.IsDirt(0, 4), Is.False, "Bottom-left corner should be cleaned");
        }
    }
}
