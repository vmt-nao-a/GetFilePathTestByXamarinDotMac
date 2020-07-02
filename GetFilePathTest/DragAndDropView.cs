using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CoreGraphics;

namespace GetFilePathTest
{
    public interface DragAndDropViewDelegate
    {
        /// <summary>
        /// ビュー上にファイルがD&Dされた場合に呼ばれる
        /// </summary>
        /// <param name="files">ファイルパス一覧</param>
        void DragAndDropViewGetDraggingFiles(NSArray files);
    }

    public partial class DragAndDropView : AppKit.NSView
    {
        /// <summary>
        /// マウスドラッグアンドドロップ時のフォーカスフラグ
        /// </summary>
        private bool _isHighlight = false;

        /// <summary>
        /// ドラッグアンドドロップされた後の処理を移譲するためのインターフェースオブジェクト
        /// </summary>
        public DragAndDropViewDelegate Delegate;

        #region Constructors

        // Called when created from unmanaged code
        public DragAndDropView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public DragAndDropView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
            _isHighlight = false;
            RegisterForDraggedTypes(new string[] { NSPasteboard.NSFilenamesType });
        }

        /// <summary>
        /// Viewの描画処理
        /// View上にファイルがドラッグされているならば、ハイライトをつける
        /// </summary>
        /// <param name="dirtyRect"></param>
        public override void DrawRect(CGRect dirtyRect)
        {
            base.DrawRect(dirtyRect);
            if (_isHighlight)
            {
                NSColor.SystemBlueColor.Set();
                NSBezierPath.DefaultLineWidth = 5;
            }
            else
            {
                NSColor.SystemGrayColor.Set();
                NSBezierPath.DefaultLineWidth = 1;
            }
            NSBezierPath.StrokeRect(Bounds);
        }

        /// <summary>
        /// Viewの境界にファイルがドラッグされるときに呼ばれる
        /// 宛先がどのドラッグ操作を実行するのかを示す値を返す必要があります。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public override NSDragOperation DraggingEntered(NSDraggingInfo sender)
        {
            _isHighlight = true;
            NeedsDisplay = true;
            return NSDragOperation.Generic;
        }

        /// <summary>
        /// View上にファイルがドラッグで保持されている間、短い間隔毎に呼ばれるメソッド
        /// 宛先がどのドラッグ操作を実行するのかを示す値を返す必要があります。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public override NSDragOperation DraggingUpdated(NSDraggingInfo sender)
        {
            _isHighlight = true;
            NeedsDisplay = true;
            return NSDragOperation.Generic;
        }

        /// <summary>
        /// View上にファイルがドラッグされなくなった際に呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        public override void DraggingExited(NSDraggingInfo sender)
        {
            _isHighlight = false;
            NeedsDisplay = true;
        }

        /// <summary>
        /// View上でファイルがドロップされた際に呼ばれる
        /// メッセージが返された場合はtrue、performDragOperation:メッセージが送信されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public override bool PrepareForDragOperation(NSDraggingInfo sender)
        {
            _isHighlight = false;
            NeedsDisplay = true;
            return true;
        }

        /// <summary>
        /// View上でファイルがドロップされた後の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public override bool PerformDragOperation(NSDraggingInfo sender)
        {
            NSArray draggedFilenames = (NSArray)sender.DraggingPasteboard.GetPropertyListForType(NSPasteboard.NSFilenamesType);

            for (uint i=0; i<draggedFilenames.Count; i++)
            {
                bool isDir = false;
                if (!NSFileManager.DefaultManager.FileExists(draggedFilenames.GetItem<NSString>(i), ref isDir))
                {
                    return false;
                }
                if (isDir == true)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 一連のドラッグ操作が完了したときに呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        public override void ConcludeDragOperation(NSDraggingInfo sender)
        {
            NSArray filePathes = (NSArray)sender.DraggingPasteboard.GetPropertyListForType(NSPasteboard.NSFilenamesType);
            Delegate.DragAndDropViewGetDraggingFiles(filePathes);
        }

        #endregion
    }
}
