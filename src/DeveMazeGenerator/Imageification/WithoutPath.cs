﻿using DeveMazeGenerator.InnerMaps;
using ImageSharp;
using ImageSharp.Formats;
using System.IO;

namespace DeveMazeGenerator.Imageification
{
    public class WithoutPath
    {
        public static void MazeToImage(InnerMap map, Stream stream)
        {
            var image = new Image(map.Width - 1, map.Height - 1);
            using (var pixels = image.Lock())
            {
                for (int y = 0; y < map.Height - 1; y++)
                {
                    for (int x = 0; x < map.Width - 1; x++)
                    {
                        var wall = map[x, y];

                        if (!wall)
                        {
                            pixels[x, y] = Color.Black;
                        }
                        else
                        {
                            pixels[x, y] = Color.White;
                        }
                    }
                }
            }

            image.Save(stream, new PngEncoder());
        }

        public static void SaveMaze(string fileName, InnerMap maze)
        {
            var dir = "Images";
            Directory.CreateDirectory(dir);
            var totalFile = Path.Combine(dir, fileName);

            using (var fs = new FileStream(totalFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                MazeToImage(maze, fs);
            }
        }
    }
}
