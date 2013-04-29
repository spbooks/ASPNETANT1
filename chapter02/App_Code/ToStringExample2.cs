using System;
class ToStringExample2 {
  public static void Main()
  {
    int x = 0;
    int y = 0;

    try
    {
      y = 10/x;
    }
    catch (DivideByZeroException ex)
    {
      Console.WriteLine(ex.ToString());
    }
  }
}