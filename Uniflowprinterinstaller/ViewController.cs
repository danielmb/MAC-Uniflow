using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using AppKit;
using Foundation;

namespace Uniflowprinterinstaller
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            Console.WriteLine(PingHost("10.0.5.18"));
            if (PingHost("10.0.5.18") == true)
            {
                base.ViewDidLoad();
            } else
            {
                var alert = new NSAlert();
                alert.MessageText = "Feil!";
                alert.InformativeText = "Får ingen tilkobling til NFK, ta kontakt med IT-Avdelingen";
                alert.RunModal();
                base.ViewDidLoad();
            }
            

            // Do any additional setup after loading the view.
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

        partial void click(Foundation.NSObject sender)
        {
            Bash("/usr/sbin/lpadmin -p NFK_Print -E -v smb://10.0.5.18/NFK%20print -P CNADVC5045X1.PPD.gz -o auth-info-required=negotiate");
            Console.WriteLine(Bash("pwd"));
            Bash("security delete-internet-password -s 10.0.5.18");
            Bash("security add-internet-password -a \"nfkad\\"+username.StringValue+"\" -D \"network password\"  -w "+password.StringValue+" -s \"10.0.5.18\" -T /System/Library/CoreServices/NetAuthAgent.app/Contents/MacOS/NetAuthSysAgent -r \"smb \" -l \"NFK_print\"");
            var alert = new NSAlert();
            alert.MessageText = "Feil!";
            alert.InformativeText = "Får ingen tilkobling til NFK, ta kontakt med IT-Avdelingen";
            alert.RunModal();

        }
        public static string Bash(string cmd)
        {
            Console.WriteLine(cmd);
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process()
            {   
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

    }
}
