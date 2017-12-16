using System;

using Color = System.Drawing.Color;

namespace ConvFilter {

    abstract public class Filter {

        abstract public double[,] Data { get; }

        public int Size => (int) Math.Sqrt(Data.Length);

        public double this[int i, int j] => Data[i, j];

        abstract public int NormalizationRate { get; }

        abstract public int Bias { get; }

        public static Color operator *(Color[,] map, Filter filter) {
            if ((int) Math.Sqrt(map.Length) != filter.Size)
                throw new ArgumentException("Different sizes in multiplication");

            double red = 0;
            double green = 0;
            double blue = 0;

            for (int y = 0; y < filter.Size; y++) {
                for (int x = 0; x < filter.Size; x++) {
                    red += map[y, x].R * filter[y, x];
                    green += map[y, x].G * filter[y, x];
                    blue += map[y, x].B * filter[y, x];
                }
            }


            return Color.FromArgb(Normalize(red, filter), Normalize(green, filter), Normalize(blue, filter));
        }

        protected static int Normalize(double value, Filter filter) {
            return Math.Min(Math.Max((int) (value / filter.NormalizationRate + filter.Bias), 0), 255);
        }

        abstract public string ToStringAction();

    }


    public class ReliefFilter : Filter {

        public override int NormalizationRate { get; } = 1;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                -2, -1, 0
            }, {
                -1, 1, 1
            }, {
                0, 1, 2
            }
        };

        public override string ToString() {
            return "Emboss (Relief)";
        }

        public override string ToStringAction() {
            return "embossed";
        }

    }

    public class EdgeFilter : Filter {

        public override int NormalizationRate { get; } = 1;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                0, 1, 0
            }, {
                1, -4, 1
            }, {
                0, 1, 0
            }
        };


        public override string ToString() {
            return "Edge Detect";
        }


        public override string ToStringAction() {
            return "edged";
        }

    }

    public class BoxBlurFilter : Filter {

        public override int NormalizationRate { get; } = 16;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                1, 2, 1
            }, {
                2, 4, 2
            }, {
                1, 2, 1
            }
        };

        public override string ToString() {
            return "Box Blur";
        }

        public override string ToStringAction() {
            return "box-blured";
        }

    }

    public class SharpenFilter : Filter {

        public override int NormalizationRate { get; } = 1;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                0, 0, 0, 0, 0
            }, {
                0, 0, 5, 0, 0
            }, {
                0, -1, 5, -1, 0
            }, {
                0, 0, 1, 0, 0
            }, {
                0, 0, 0, 0, 0
            }
        };

        public override string ToString() {
            return "Sharpen";
        }

        public override string ToStringAction() {
            return "sharpened";
        }

    }


    public class IdentityFilter : Filter {

        public override int NormalizationRate { get; } = 1;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                0, 0, 0
            }, {
                0, 1, 0
            }, {
                0, 0, 0
            }
        };

        public override string ToString() {
            return "Identity";
        }

        public override string ToStringAction() {
            return "identity";
        }

    }


    public class GaussianBlurFilter : Filter {

        public override int NormalizationRate { get; } = 256;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                1, 4, 6, 4, 1
            }, {
                4, 16, 24, 16, 4
            }, {
                6, 24, 36, 24, 6
            }, {
                4, 16, 24, 16, 4
            }, {
                1, 4, 6, 4, 1
            }
        };

        public override string ToString() {
            return "Gaussian Blur";
        }

        public override string ToStringAction() {
            return "gaussian-blured";
        }

    }

    public class MotionBlurFilter : Filter {

        public override int NormalizationRate { get; } = 9;

        public override int Bias { get; } = 0;

        public override double[,] Data { get; } = {
            {
                1, 0, 0, 0, 0, 0, 0, 0, 0
            }, {
                0, 1, 0, 0, 0, 0, 0, 0, 0
            }, {
                0, 0, 1, 0, 0, 0, 0, 0, 0
            }, {
                0, 0, 0, 1, 0, 0, 0, 0, 0
            }, {
                0, 0, 0, 0, 1, 0, 0, 0, 0
            }, {
                0, 0, 0, 0, 0, 1, 0, 0, 0
            }, {
                0, 0, 0, 0, 0, 0, 1, 0, 0
            }, {
                0, 0, 0, 0, 0, 0, 0, 1, 0
            }, {
                0, 0, 0, 0, 0, 0, 0, 0, 1
            }
        };

        public override string ToString() {
            return "Motion Blur";
        }

        public override string ToStringAction() {
            return "motion-blured";
        }

    }

}