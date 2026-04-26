using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBLibrary.SceneManager.MapGeneration
{
    //using Player;
    using Raycast.Axis;
    using RCBLibrary.SceneManagement;

    public class IntelligentSpawn
    {
        public int x;
        public int y;
        public List<Point> FFPoints = new List<Point>();
        public void Player(PSC psc)
        {
            Random rnd = new Random();
            HashSet<Point> visited = new HashSet<Point>();

            // 1. Get a list of all potential walkable points to avoid constant looping
            // We exclude the borders (1 to Width-1) as per your original logic
            List<Point> availablePoints = Enumerable.Range(1, psc.Width - 2)
                .SelectMany(x => Enumerable.Range(1, psc.Height - 2), (x, y) => new Point(x, y))
                .Where(p => !psc.BlockedPoints.Contains(p))
                .ToList();

            while (availablePoints.Count > 0)
            {
                // 2. Pick a random starting point from available tiles
                int index = rnd.Next(availablePoints.Count);
                Point startPoint = availablePoints[index];

                // 3. Run FloodFill once for this specific area
                // Note: Assuming FloodFill populates FFPoints and returns the count
                FloodFill(startPoint.X, startPoint.Y, psc);

                if (FFPoints.Count >= 50)
                {
                    // 4. Success! Pick a random point from this valid region
                    psc.SpawnPoint = FFPoints[rnd.Next(FFPoints.Count)];
                    psc.spawnablePoints = FFPoints;
                    return;
                }
                else
                {
                    // 5. Optimization: If the area is too small (< 50), 
                    // remove all those points from availablePoints so we don't try them again.
                    availablePoints.RemoveAll(p => FFPoints.Contains(p));
                    FFPoints.Clear();
                }
            }

            // Fallback if no area >= 50 exists
            psc.SpawnPoint = new Point(1, 1);
        }

        public void FloodFill(int startX, int startY, PSC psc)
        {
            FFPoints.Clear();
            Point start = new Point(startX, startY);

            // Check if start is valid
            if (psc.BlockedPoints.Contains(start)) return;

            // Use a Stack (DFS) or Queue (BFS) on the Heap
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(start);

            // Track visited locally so we don't loop forever
            HashSet<Point> visited = new HashSet<Point>();
            visited.Add(start);

            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                FFPoints.Add(a);

                // Define the 4 neighbors (Up, Down, Left, Right)
                Point[] neighbors = {
            new Point(a.X, a.Y + 1),
            new Point(a.X, a.Y - 1),
            new Point(a.X - 1, a.Y),
            new Point(a.X + 1, a.Y)
        };

                foreach (Point next in neighbors)
                {
                    // Check bounds and if it's blocked or already visited
                    if (next.X >= 1 && next.X < psc.Width - 1 &&
                        next.Y >= 1 && next.Y < psc.Height - 1 &&
                        !psc.BlockedPoints.Contains(next) &&
                        !visited.Contains(next))
                    {
                        pixels.Push(next);
                        visited.Add(next);
                    }
                }
            }
        }

        /// <summary>
        /// Recursive version (not recommended for large maps due to stack overflow risk)
        /// </summary>
        public void FloodFillR(int X, int Y, PSC psc)
        {
            if (psc.BlockedPoints.Contains(new Point() { X = X, Y = Y }) || FFPoints.Contains(new Point() { X = X, Y = Y }))
                return;
            if (X < 1 || X >= psc.Width)
                return;
            if (Y < 1 || Y >= psc.Width)
                return;

            FFPoints.Add(new Point() { X = X, Y = Y });

            FloodFill(X + 1, Y, psc);
            FloodFill(X - 1, Y, psc);
            FloodFill(Y + 1, X, psc);
            FloodFill(Y - 1, X, psc);
        }

        public void Materials()
        {
            Random rnd = new Random();
            int ItemsToSpawn = rnd.Next(10, 50);
            for (int i = 0; i < ItemsToSpawn; i++)
            {
                //PSC.StonePoints.Add(new Player.UI.Item("Stone", rnd.Next(1, 5), "collectable", "Mountains;None;None;None", FFPoints[rnd.Next(0, FFPoints.Count)]));
            }
            
        }
    }
}
