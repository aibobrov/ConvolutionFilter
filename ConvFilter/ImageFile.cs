using System.Drawing;

using Gdk;

namespace ConvFilter {

    public class ImageFile {

        public string Path;

        public Bitmap Map;

        public Pixbuf PixBuf => new Pixbuf(Path);

        public ImageFile(string path) {
            Path = path;
            Map = new Bitmap(path);
        }

    }

}