using System;
using System.IO;

class Program
{
    static void Main()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            // Clear previous log before each run
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("███╗░░░███╗██╗███╗░░██╗███████╗░█████╗░██████╗░░█████╗░███████╗████████╗\n" +
                "████╗░████║██║████╗░██║██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔════╝╚══██╔══╝\n" +
                "██╔████╔██║██║██╔██╗██║█████╗░░██║░░╚═╝██████╔╝███████║█████╗░░░░░██║░░░\n" +
                "██║╚██╔╝██║██║██║╚████║██╔══╝░░██║░░██╗██╔══██╗██╔══██║██╔══╝░░░░░██║░░░\n" +
                "██║░╚═╝░██║██║██║░╚███║███████╗╚█████╔╝██║░░██║██║░░██║██║░░░░░░░░██║░░░\n" +
                "╚═╝░░░░░╚═╝╚═╝╚═╝░░╚══╝╚══════╝░╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░░░░░░░╚═╝░░░\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("██████╗░░█████╗░░█████╗░██╗░░██╗  ██████╗░███████╗██╗░░░░░███████╗████████╗██╗░█████╗░███╗░░██╗\n" +
                "██╔══██╗██╔══██╗██╔══██╗██║░██╔╝  ██╔══██╗██╔════╝██║░░░░░██╔════╝╚══██╔══╝██║██╔══██╗████╗░██║\n" +
                "██████╔╝███████║██║░░╚═╝█████═╝░  ██║░░██║█████╗░░██║░░░░░█████╗░░░░░██║░░░██║██║░░██║██╔██╗██║\n" +
                "██╔═══╝░██╔══██║██║░░██╗██╔═██╗░  ██║░░██║██╔══╝░░██║░░░░░██╔══╝░░░░░██║░░░██║██║░░██║██║╚████║\n" +
                "██║░░░░░██║░░██║╚█████╔╝██║░╚██╗  ██████╔╝███████╗███████╗███████╗░░░██║░░░██║╚█████╔╝██║░╚███║\n" +
                "╚═╝░░░░░╚═╝░░╚═╝░╚════╝░╚═╝░░╚═╝  ╚═════╝░╚══════╝╚══════╝╚══════╝░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝\n");

            Console.ForegroundColor = ConsoleColor.White;

            // Get all user profiles from the User directory
            string userProfilePath = @"C:\Users";
            string[] users = Directory.GetDirectories(userProfilePath);
            string[] userNames = Array.ConvertAll(users, Path.GetFileName);

            // Display user selection
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Select a user to delete resource and behavior packs:");
            for (int i = 0; i < userNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {userNames[i]}");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter the number of the user: ");
            string input = Console.ReadLine();
            int selectedUserIndex;

            // Validate input and select user
            if (int.TryParse(input, out selectedUserIndex) && selectedUserIndex > 0 && selectedUserIndex <= userNames.Length)
            {
                string selectedUserName = userNames[selectedUserIndex - 1];
                Console.WriteLine($"\"{selectedUserName}\": Has been selected.");

                // Paths to the directories
                string[] paths = {
                    @$"C:\Users\{selectedUserName}\AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\resource_packs",
                    @$"C:\Users\{selectedUserName}\AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\development_resource_packs",
                    @$"C:\Users\{selectedUserName}\AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\development_behavior_packs",
                    @$"C:\Users\{selectedUserName}\AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\behavior_packs"
                };

                Console.WriteLine("Are you sure you want to delete resouce and behaviour packs for this user? (Y/N)");
                string confirmation = Console.ReadLine();

                if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (string path in paths)
                    {
                        Console.WriteLine($"Checking directory: {path}");
                        if (Directory.Exists(path))
                        {
                            DeleteContentsInsideDirectory(path);
                        }
                        else
                        {
                            Console.WriteLine($"Directory does not exist: {path}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Operation cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number.");
            }

            // Prompt the user whether to rerun the program
            Console.WriteLine("\nDo you want to run the program again? (Y/N)");
            string rerunInput = Console.ReadLine(); // renamed to avoid conflict

            if (!rerunInput.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                keepRunning = false; // Exit the loop
            }
        }
    }

    static void DeleteContentsInsideDirectory(string path)
    {
        // Delete all files in the directory
        string[] files = Directory.GetFiles(path);
        if (files.Length > 0)
        {
            foreach (string file in files)
            {
                try
                {
                    Console.WriteLine($"Deleting file: {file}");
                    File.Delete(file);
                    Console.WriteLine($"Deleted file: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete file {file}: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine($"No files to delete in: {path}");
        }

        // Delete all subdirectories in the directory
        string[] directories = Directory.GetDirectories(path);
        if (directories.Length > 0)
        {
            foreach (string dir in directories)
            {
                try
                {
                    Console.WriteLine($"Deleting folder: {dir}");
                    Directory.Delete(dir, true);
                    Console.WriteLine($"Deleted folder: {dir}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete folder {dir}: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine($"No folders to delete in: {path}");
        }
    }
}
