// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Uniflowprinterinstaller
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSSecureTextField password { get; set; }

		[Outlet]
		AppKit.NSTextField username { get; set; }

		[Action ("click:")]
		partial void click (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (username != null) {
				username.Dispose ();
				username = null;
			}

			if (password != null) {
				password.Dispose ();
				password = null;
			}
		}
	}
}
