using System;

using AppKit;
using Foundation;

namespace GetFilePathTest
{
    public partial class ViewController : NSViewController, DragAndDropViewDelegate
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            dragAndDropView.Delegate = this;
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void OnClickButtonFileOpen(Foundation.NSObject sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "txt", "html", "md", "css" };

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    var path = url.Path;
                    labelFilePath.StringValue = path;
                }
            }
        }

        public void DragAndDropViewGetDraggingFiles(NSArray files)
        {
            string pathes = "";
            for (uint i=0; i<files.Count; i++)
            {
                pathes += files.GetItem<NSString>(i) + ", ";
            }

            labelFilePath.StringValue = pathes;
        }
    }
}
