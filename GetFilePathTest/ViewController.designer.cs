// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GetFilePathTest
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton buttonOpenFile { get; set; }

		[Outlet]
		GetFilePathTest.DragAndDropView dragAndDropView { get; set; }

		[Outlet]
		AppKit.NSTextField labelFilePath { get; set; }

		[Action ("OnClickButtonFileOpen:")]
		partial void OnClickButtonFileOpen (Foundation.NSObject sender);

		[Action ("OnClickButtonOpenFIle:")]
		partial void OnClickButtonOpenFIle (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonOpenFile != null) {
				buttonOpenFile.Dispose ();
				buttonOpenFile = null;
			}

			if (labelFilePath != null) {
				labelFilePath.Dispose ();
				labelFilePath = null;
			}

			if (dragAndDropView != null) {
				dragAndDropView.Dispose ();
				dragAndDropView = null;
			}
		}
	}
}
