using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace ConvFilter {

    public class ConvolutionProcessor {

        Bitmap map;

        public ConvolutionProcessor(Bitmap bitmap) {
            map = bitmap;
        }


        public Bitmap ComputeWith(Filter filter) {
            var result = new Bitmap(map.Width, map.Height);

            var offset = filter.Size / 2;
            for (int x = 0; x < map.Width; x++) {
                for (int y = 0; y < map.Height; y++) {
                    var colorMap = new Color[filter.Size, filter.Size];

                    for (int filterY = 0; filterY < filter.Size; filterY++) {
                        int pk = (filterY + x - offset <= 0) ? 0 :
                            (filterY + x - offset >= map.Width - 1) ? map.Width - 1 : filterY + x - offset;
                        for (int filterX = 0; filterX < filter.Size; filterX++) {
                            int pl = (filterX + y - offset <= 0) ? 0 :
                                (filterX + y - offset >= map.Height - 1) ? map.Height - 1 : filterX + y - offset;

                            colorMap[filterY, filterX] = map.GetPixel(pk, pl);
                        }
                    }

                    result.SetPixel(x, y, colorMap * filter);
                }
            }

            return result;
        }

        public Bitmap ComputeWith(Filter filter, int threadsCount) {
            if (threadsCount == 0)
                throw new ArgumentException("Thread count shouldn't be zero");

            var result = new Bitmap(map.Width, map.Height);
            var threads = new List<Thread>();

            for (int i = 0, start = 0; i < threadsCount; i++) {
                var size = (map.Width - i + threadsCount - 1) / threadsCount;
                var it = start;

                var thread = new Thread(delegate() {
                    Calculate(it, it + size, filter, result);
                });
                thread.Start();
                threads.Add(thread);

                start += size;
            }

            threads.ForEach(thread => thread.Join());

            return result;
        }

        private void Calculate(int start, int finish, Filter filter, Bitmap result) {
            var offset = filter.Size / 2;
            for (int x = start; x < finish; x++) {
                for (int y = 0; y < map.Height; y++) {
                    var colorMap = new Color[filter.Size, filter.Size];

                    for (int filterY = 0; filterY < filter.Size; filterY++) {
                        int pk = (filterY + x - offset <= 0) ? 0 :
                            (filterY + x - offset >= map.Width - 1) ? map.Width - 1 : filterY + x - offset;

                        for (int filterX = 0; filterX < filter.Size; filterX++) {
                            int pl = (filterX + y - offset <= 0) ? 0 :
                                (filterX + y - offset >= map.Height - 1) ? map.Height - 1 : filterX + y - offset;

                            colorMap[filterY, filterX] = map.GetPixel(pk, pl);
                        }
                    }

                    result.SetPixel(x, y, colorMap * filter);
                }
            }
        }

    }

}