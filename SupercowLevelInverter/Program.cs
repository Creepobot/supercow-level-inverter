using Nevosoft.Supercow;
using System;
using System.Drawing;

namespace SupercowLevelInverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start...");
            try
            {
                Console.WriteLine("Search for arguments...");
                if (args == null || args.Length == 0) throw new IndexOutOfRangeException("args");
                foreach (var arg in args)
                {
                    Console.WriteLine($"Edit file {arg}");
                    if (!arg.EndsWith(".lev")) { Console.WriteLine($"Error: File {arg} is not .lev"); continue; }
                    Level l = new Level(arg);

                    Console.WriteLine($"Edit objects...");
                    for (int i = 0; i < l.Objects.Count; i++)
                    {
                        LevelObject lo = l.Objects[i];
                        lo.Rectangle = RectangleF.FromLTRB(-lo.Rectangle.Left, lo.Rectangle.Top, -lo.Rectangle.Right, lo.Rectangle.Bottom);
                        lo.EndPosition.X = -lo.EndPosition.X;
                        lo.Rotation = -lo.Rotation;
                        lo.Inverted = !lo.Inverted;
                        l.Objects[i] = lo;
                    }

                    Console.WriteLine($"Edit grounds...");
                    foreach (var ground in l.Grounds)
                        Reverse2DimArray(ground);

                    l.Save(arg);
                    Console.WriteLine($"File {arg} done");
                }

                Console.WriteLine("Done");
            }
            catch (IndexOutOfRangeException ex)
            {
                switch (ex.Message)
                {
                    case "args":
                        Console.WriteLine("Error: No arguments were found. Please open the .lev file from the Supercow game with this .exe");
                        break;
                    default:
                        Console.WriteLine(ex.Source);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Source);
            }
            finally
            {
                Console.WriteLine("Press any button to close application.");
                Console.ReadKey();
            }
        }

        public static void Reverse2DimArray(int[,] theArray)
        {
            for (int rowIndex = 0;
                 rowIndex <= (theArray.GetUpperBound(0) / 2); rowIndex++)
            {
                for (int colIndex = 0;
                     colIndex <= (theArray.GetUpperBound(1)); colIndex++)
                {
                    int tempHolder = theArray[rowIndex, colIndex];
                    theArray[rowIndex, colIndex] =
                      theArray[theArray.GetUpperBound(0) - rowIndex, colIndex];
                    theArray[theArray.GetUpperBound(0) - rowIndex, colIndex] =
                      tempHolder;
                }
            }
        }
    }
}