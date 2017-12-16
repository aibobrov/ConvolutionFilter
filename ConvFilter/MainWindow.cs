using System;
using System.Diagnostics;

using Gtk;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using ConvFilter;

using Gdk;

using GLib;

public partial class MainWindow : Gtk.Window {

    private ImageFile _originalFile = null;

    private ImageFile _filteredFile = null;

    private Filter[] _filters = new Filter[] {
        new IdentityFilter(), new BoxBlurFilter(), new EdgeFilter(), new ReliefFilter(), new SharpenFilter(),
        new GaussianBlurFilter(), new MotionBlurFilter()
    };

    public MainWindow() : base(Gtk.WindowType.Toplevel) {
        Build();

        var store = new ListStore(GType.String);

        foreach (var filter in _filters) {
            store.AppendValues(filter.ToString());
        }

        filterComboBox.Model = store;
        filterComboBox.Active = 0;
    }


    protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OpenButtonClicked(object sender, EventArgs e) {
        var fileDialog = new FileChooserDialog("Choose image",
            this,
            FileChooserAction.Open,
            "Cancel",
            ResponseType.Cancel,
            "Open",
            ResponseType.Accept);
        fileDialog.SetCurrentFolder("/Users/artembobrov");
        var response = fileDialog.Run();

        if (response == (int) ResponseType.Accept) {
            if (_originalFile == null || _originalFile.Path != fileDialog.Filename) {
                _originalFile = new ImageFile(fileDialog.Filename);
                _filteredFile = null;

                originalImage.Pixbuf = _originalFile.PixBuf;
                filteredImage.Clear();
                informationLabel.Text = "";
            }
        }

        fileDialog.Destroy();
    }

    protected void ProcessButton(object sender, EventArgs e) {
        if (_originalFile == null) {
            informationLabel.Text = "Firslty, choose Image";
            return;
        }

        var processor = new ConvolutionProcessor(_originalFile.Map);
        Task.Run(delegate {
            try {
                var filter = _filters[filterComboBox.Active];
                var stopwatch= new Stopwatch();
                stopwatch.Start();
                var result = processor.ComputeWith(filter, threadCountSpin.ValueAsInt);
                stopwatch.Stop();
                var path =
                    $"{System.IO.Path.GetDirectoryName(_originalFile.Path)}/{System.IO.Path.GetFileNameWithoutExtension(_originalFile.Path)}_{filter.ToStringAction()}{System.IO.Path.GetExtension(_originalFile.Path)}";

                result.Save(path);
                _filteredFile = new ImageFile(path);
                filteredImage.Pixbuf = _filteredFile.PixBuf;
                informationLabel.Text = $"Filter {filter.ToString()} has been applied for {stopwatch.ElapsedMilliseconds} ms";
            }
            catch (Exception exception) {
                informationLabel.Text = exception.Message;
                return;
            }
        });
    }

}