using System;
using System.Net;
using System.Diagnostics;
using System.Threading;
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
        static string konami;
        public override void ViewDidLoad()
        {
            login.Enabled = false;
            //Console.WriteLine(PingHost("10.0.5.18"));
            if (PingHost("10.0.5.18") == true)
            {


                base.ViewDidLoad();
                //var feidepass = password.StringValue;
                NSEvent.AddLocalMonitorForEventsMatchingMask(NSEventMask.KeyDown, Enter);
            }
            else
            {
                var alert = new NSAlert();
                alert.MessageText = "Feil!";
                alert.AlertStyle = NSAlertStyle.Critical;
                alert.InformativeText = "Får ingen tilkobling til NFK.";
                alert.RunModal();
                base.ViewDidLoad();
            }
            progressBar.StartAnimation(new NSObject());
            var urlgit = "https://github.com/danielmb/MAC-Uniflow/raw/master/Uniflowprinterinstaller/Resources/CNADVC5045X1.PPD.gz";

            WebClient Client = new WebClient();
            
                
                Client.DownloadProgressChanged += Client_DownloadProgressChanged;   
                Client.DownloadFileAsync(new System.Uri(urlgit), System.IO.Path.GetTempPath() + "CNADVC5045X1.PPD.gz"); // Kan endres eller fjernes
                
         
            username.SelectText(new NSObject());


        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            progressLog.StringValue = String.Format("Laster ned {0}/{1} Kb ({2}%)",
            unchecked((int)e.BytesReceived)/1000,
            unchecked((int)e.TotalBytesToReceive)/1000,            e.ProgressPercentage);
            progressBar.DoubleValue = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                Thread.Sleep(200);
                login.Enabled = true;
                progressLog.StringValue = "";
                progressBar.Hidden = true;
            }
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
        public NSEvent Enter(NSEvent keyevent)
        {
            konami += keyevent.KeyCode;
            

            if ("12612612512512312412312411053".Contains(konami))
            {
                if (konami.Length == 29)
                {
                    if (okonami.Hidden == true) 
                    {
                        okonami.Hidden = false;
                    } else
                    {
                        okonami.Hidden = true;
                    }
                }
            } else
            {
                konami = keyevent.KeyCode + "";

            }
            progressBar.DoubleValue = konami.Length * (100 / 28);
            Console.WriteLine(konami);

            if ((login.Enabled == true) && (keyevent.KeyCode == 36))
            {
                Console.WriteLine(keyevent.KeyCode);
                Actionhero();
            }
            return keyevent;
        }

        partial void click(Foundation.NSObject sender)
        {
            Actionhero();
            /*var feideuser = username.StringValue;
            var feidepass = password.StringValue;
            //username.Enabled = false;
            //password.Enabled = false;
            //login.Enabled = false;
            
            Bash("/usr/sbin/lpadmin -p NFK_Print -E -v smb://10.0.5.18/NFK%20print -P CNADVC5045X1.PPD.gz -o auth-info-required=negotiate");
            //Console.WriteLine(Bash("pwd"));
            Bash("security delete-internet-password -s 10.0.5.18");
            var test = Bash("ldapsearch -x -LLL -h 10.0.5.30 -D \"nfkad\\" + feideuser + "\" -w " + feidepass + " -p 389 -b \"dc=nfkad,dc=local\" | grep \"danbre14\"");
            //Console.WriteLine(test.Length);
            if (test.Length > 1)
            {
                Bash("security add-internet-password -a \"nfkad\\" + feideuser + "\" -D \"network password\"  -w " + feidepass + " -s \"10.0.5.18\" -T /System/Library/CoreServices/NetAuthAgent.app/Contents/MacOS/NetAuthSysAgent -r \"smb \" -l \"NFK_print\"");
                Bash("security find-internet-password -s 10.0.5.18");
                var Bsearch = Bash("security find-internet-password -s 10.0.5.18 | grep \"acct\"");
                if (Bsearch.Contains(feideuser) == true)
                {

                    var alert = new NSAlert()
                    {
                        AlertStyle = NSAlertStyle.Critical,
                        InformativeText = "Suksess!",
                        MessageText = "Skriveren er nå lagt til!",
                    };
                    alert.RunModal();
                }
            } else
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Critical,
                    InformativeText = "Feil!",
                    MessageText = "Feil Passord eller Brukernavn!",
                };
                alert.RunModal();
            }*/
        }

        public void Actionhero()
        {
            
            var feideuser = username.StringValue;
            var feidepass = password.StringValue;
            username.Enabled = false;
            password.Enabled = false;
            login.Enabled = false;
            var driver = System.IO.Path.GetTempPath() + "CNADVC5045X1.PPD.gz";
            if (username.StringValue.Length > 5)
            {
                if (password.StringValue.Length > 9)
                {
                    var test = Bash("ldapsearch -x -LLL -h 10.0.5.30 -D \"nfkad\\" + feideuser + "\" -w " + feidepass + " -p 389 -b \"dc=nfkad,dc=local\"");
                    //Console.WriteLine(test.Length);
                    if (test.Length > 1)
                    {
                        Bash("/usr/sbin/lpadmin -p NFK_Print -E -v smb://10.0.5.18/NFK%20print -P "+driver+" -o auth-info-required=negotiate");
                        //Console.WriteLine(Bash("pwd"));
                        Bash("security delete-internet-password -s 10.0.5.18");
                        Bash("security add-internet-password -a \"nfkad\\" + feideuser + "\" -D \"network password\"  -w " + feidepass + " -s \"10.0.5.18\" -T /System/Library/CoreServices/NetAuthAgent.app/Contents/MacOS/NetAuthSysAgent -r \"smb \" -l \"NFK_print\"");
                        Bash("security find-internet-password -s 10.0.5.18");

                        var Bsearch = Bash("security find-internet-password -s 10.0.5.18 | grep \"acct\"");
                        if (Bsearch.Contains(feideuser) == true)
                        {

                            var alert = new NSAlert()
                            {
                                AlertStyle = NSAlertStyle.Informational,
                                MessageText = "Suksess!",
                                InformativeText = "Skriveren er nå lagt til! Du kan nå lukke programmet!",
                            };
                            alert.RunModal();
                        }
                    }
                    else
                    {
                        var alert = new NSAlert()
                        {
                            AlertStyle = NSAlertStyle.Critical,
                            MessageText = "Feil!",
                            InformativeText = "Feil Passord eller Brukernavn!",
                        };
                        alert.RunModal();
                    }

                }
                else
                {
                    var alert = new NSAlert()
                    {
                        AlertStyle = NSAlertStyle.Critical,
                        MessageText = "Feil!",
                        InformativeText = "Passorded er for kort",
                    };
                    alert.RunModal();
                }
            }
            else
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Critical,
                    MessageText = "Feil!",
                    InformativeText = "Brukernavnet er for kort",
                };
                alert.RunModal();
            }
            username.Enabled = true;
            password.Enabled = true;
            password.StringValue = "";
            login.Enabled = true;
        }
        public static string Bash(string cmd)
        {
            
            //Console.WriteLine(cmd);
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
                PingReply reply = pinger.Send(nameOrAddress, 1000);
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
