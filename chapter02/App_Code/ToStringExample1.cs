using System;
class ToStringExample1 {
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
      Console.WriteLine(ex.Message);
    }
  }
}