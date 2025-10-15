using System;
using NUnit.Framework;


namespace RobotCleaner
{

  public interface IStrategy
  {
    void Clean(Robot robot);
  }

  public class Robot
  {
    private readonly Map _map;
    private readonly IStrategy _strategy;

    public int X { get; set; }
    public int Y { get; set; }

    public Map Map { get { return _map; } }

    public Robot(Map map, IStrategy strategy)
    {
      _map = map;
      _strategy = strategy;
      X = 0;
      Y = 0;
    }

    public bool Move(int newX, int newY)
    {
      if (_map.IsInBounds(newX, newY) && !_map.IsObstacle(newX, newY))
      {
        // set the new location
        X = newX;
        Y = newY;
        // display the map with the robot in its location in the grid
        _map.Display(X, Y);
        return true;
      }
      // it cannot move
      return false;
    }// Move method

    public void CleanCurrentSpot()
    {
      if (_map.IsDirt(X, Y))
      {
        _map.Clean(X, Y);
        _map.Display(X, Y);
      }
    }

    public void StartCleaning()
    {
      _strategy.Clean(this);
    }
  }

  public class SomeStrategy : IStrategy
  {
    public void Clean(Robot robot)
    {
      int direction = 1; // 1 = right, -1 = left
      for (int y = 0; y < robot.Map.Height; y++)
      {
        int startX = (direction == 1) ? 0 : robot.Map.Width - 1;
        int endX = (direction == 1) ? robot.Map.Width : -1;

        for (int x = startX; x != endX; x += direction)
        {
          robot.Move(x, y);
          robot.CleanCurrentSpot();
        }
        direction *= -1; // Reverse direction for the next row
      }
    }
  }



  public class PerimeterHuggerStrategy : IStrategy
  {
    public void Clean(Robot robot)
    {
      int direction = 1;
      while (robot.Move(robot.X + 1, robot.Y))
      {
        robot.CleanCurrentSpot();
      }


      while (robot.Move(robot.X, robot.Y + 1))
      {
        robot.CleanCurrentSpot();
      }


      while (robot.Move(robot.X - 1, robot.Y))
      {
        robot.CleanCurrentSpot();
      }
      while (robot.Move(robot.X, robot.Y - 1))
      {
        robot.CleanCurrentSpot();
      }

      direction *= -1;
    }
  }


  // public class Program
  // {

  //   public static void Main(string[] args)
  //   {
  //     Console.WriteLine("Initialize robot");


  //     // IStrategy some_strategy = new SomeStrategy();
  //     IStrategy perimeterHuggerStrategy = new PerimeterHuggerStrategy();

  //     Map map = new Map(20, 10);
  //     // map.Display( 10,10);

  //     map.AddDirt(5, 3);
  //     map.AddDirt(10, 8);
  //     map.AddDirt(1, 1);
  //     map.AddObstacle(2, 5);
  //     map.AddObstacle(12, 1);
  //     map.Display(11, 8);

  //     Robot robot = new Robot(map, perimeterHuggerStrategy);
  //     //   robot.Move(11, 5);
  //     robot.Move(0, 0);
  //     robot.StartCleaning();

  //     Console.WriteLine("Done.");
  //   }
  // }
}

