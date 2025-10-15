using NUnit.Framework;

namespace RobotCleaner
{
    [TestFixture]
    public class SpiralStrategyTests
    {
        [Test]
        public void Robot_Cleans_Spiral_Successfully()
        {
            // Arrange
            Map map = new Map(5, 5);
            map.AddDirt(0, 0);
            map.AddDirt(1, 0);
            map.AddDirt(2, 0);
            map.AddDirt(2, 1);
            map.AddDirt(2, 2);
            map.AddDirt(1, 2);
            map.AddDirt(0, 2);
            map.AddDirt(0, 1);

            IStrategy spiralStrategy = new SpiralStrategy();
            Robot robot = new Robot(map, spiralStrategy);

            robot.Move(11, 5);
            robot.StartCleaning();

            bool allClean = true;
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.IsDirt(x, y))
                    {
                        allClean = true;
                        break;
                    }
                }
            }

            Assert.That(allClean, Is.True, "Not all dirt was cleaned by SpiralStrategy.");
        }
    }
}
