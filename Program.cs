using System.Diagnostics;
using Microsoft.Win32.TaskScheduler;

namespace download_manager
{
    class Program
    {
        static Dictionary<string, string[]> rules = new Dictionary<string, string[]>()
        {
            { "Images", new string[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" } },
            { "Documents", new string[] { ".pdf", ".docx", ".txt", ".doc", ".xls", ".xlsx", ".ppt", ".pptx" } },
            { "Videos", new string[] { ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv" } },
            { "Music", new string[] { ".mp3", ".wav", ".aac", ".flac", ".ogg" } },
            { "Archives", new string[] { ".zip", ".rar", ".7z", ".tar", ".gz" } },
            { "Executables", new string[] { ".exe", ".msi", ".bat", ".sh" } },
            { "Others", new string[] { "*" } }
        };

        static bool IsAdministrator()
        {
            using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
        }

        static void Main(string[] args)
        {
            if (args.Contains("--auto"))
            {
                OrganizeDownloads();
                return;
            }

            if (!IsAdministrator())
            {
                Console.WriteLine("Warning: This application is not running with administrator privileges. Scheduling tasks may fail.");
                Console.WriteLine("Please run the application as an administrator to ensure all features work correctly.");
                Thread.Sleep(5000);
            }

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("========= Download Organizer =========");
                Console.WriteLine("1 - Organize files");
                Console.WriteLine("2 - Settings");
                Console.WriteLine("3 - Schedule Task");
                Console.WriteLine("4 - Exit");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Organizing files in the Downloads folder...");
                    OrganizeDownloads();
                    Thread.Sleep(1500);
                    Console.Clear();
                    Console.WriteLine("Returning to main menu...");
                    Thread.Sleep(1500);
                    Console.Clear();
                }
                if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("=========Settings =========");
                    Console.WriteLine("1 - Remove scheduled tasks");
                    string settingsChoice = Console.ReadLine() ?? "";
                    if (settingsChoice == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("Removing scheduled tasks...");
                        RemoveScheduledTasks();
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                }
                if (choice == "3")
                {
                    Console.Clear();
                    Console.WriteLine("========= Schedule Task =========");
                    Console.WriteLine("1 - Schedule daily");
                    Console.WriteLine("2 - Schedule weekly");
                    Console.WriteLine("3 - Schedule on login");
                    Console.WriteLine("4 - Schedule on system startup");
                    Console.WriteLine("5 - Back to main menu");

                    string scheduleChoice = Console.ReadLine() ?? "";
                    
                    if (scheduleChoice == "1")
                    {
                        Console.Clear();
                        ScheduleDailyTask();
                        Console.WriteLine("Scheduling task...");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    else if (scheduleChoice == "2")
                    {
                        Console.Clear();
                        ScheduleWeeklyTask();
                        Console.WriteLine("Scheduling task...");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    else if (scheduleChoice == "3")
                    {
                        Console.Clear();
                        ScheduleOnLoginTask();
                        Console.WriteLine("Scheduling task...");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    else if (scheduleChoice == "4")
                    {
                        Console.Clear();
                        ScheduleOnStartupTask();
                        Console.WriteLine("Scheduling task...");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    if (scheduleChoice == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("Returning to main menu...");
                        Thread.Sleep(1500);
                        Console.Clear();
                        continue; 
                    }
                }
                if (choice == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(1500);
                    isRunning = false;
                }

            }
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        static void RemoveScheduledTasks()
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(@"DownloadManagerDaily", false);
                ts.RootFolder.DeleteTask(@"DownloadManagerWeekly", false);
                ts.RootFolder.DeleteTask(@"DownloadManagerOnLogin", false);
                ts.RootFolder.DeleteTask(@"DownloadManagerOnStartup", false);
            }
            Thread.Sleep(1500);
            Console.WriteLine("Tasks removed successfully!");
            Thread.Sleep(1500);
        }

        static void ScheduleDailyTask()
        {
            try{
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Download Organizer - Daily";
                td.Triggers.Add(new DailyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(9) });

                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                td.Actions.Add(new ExecAction(exePath, "--auto"));

                ts.RootFolder.RegisterTaskDefinition(@"DownloadManagerDaily", td);
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
        }

        static void ScheduleWeeklyTask()
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Download Organizer - Weekly";
                td.Triggers.Add(new WeeklyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(9), DaysOfWeek = DaysOfTheWeek.Monday });
                
                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                td.Actions.Add(new ExecAction(exePath, "--auto"));

                ts.RootFolder.RegisterTaskDefinition(@"DownloadManagerWeekly", td);
            }
        }

        static void ScheduleOnLoginTask()
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Download Organizer - On Login";
                td.Triggers.Add(new LogonTrigger());
                
                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                td.Actions.Add(new ExecAction(exePath, "--auto"));

                ts.RootFolder.RegisterTaskDefinition(@"DownloadManagerOnLogin", td);
            }
        }
        static void ScheduleOnStartupTask()
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Download Organizer - On Startup";
                td.Triggers.Add(new BootTrigger());
                
                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                td.Actions.Add(new ExecAction(exePath, "--auto"));

                ts.RootFolder.RegisterTaskDefinition(@"DownloadManagerOnStartup", td);
            }
        }
        static void OrganizeDownloads()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string[] files = Directory.GetFiles(folder);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                bool moved = false;

                foreach (var rule in rules)
                {
                    if (rule.Value.Contains(extension) || rule.Value.Contains("*"))
                    {
                        string destinationFolder = Path.Combine(folder, rule.Key);
                        Directory.CreateDirectory(destinationFolder);
                        string destinationPath = Path.Combine(destinationFolder, Path.GetFileName(file));
                        File.Move(file, destinationPath);
                        moved = true;
                        break;
                    }
                }

                if (!moved)
                {
                    string destinationFolder = Path.Combine(folder, "Others");
                    Directory.CreateDirectory(destinationFolder);
                    string destinationPath = Path.Combine(destinationFolder, Path.GetFileName(file));
                    File.Move(file, destinationPath);
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine("Files organized successfully!");
        }
    }
}
