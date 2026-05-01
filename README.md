# Download Organizer

An automatic download organizer for Windows.

## For Users

### Description

This project is a console application written in C# that automatically organizes files in the user's Downloads folder into subfolders based on file extensions. It categorizes files into folders such as Images, Documents, Videos, Music, Archives, Executables, and Others.

### Features

- **Automatic Organization**: Organizes files by type based on extensions.
- **Console Interface**: Simple menu for interaction.
- **Task Scheduling**: Allows scheduling automatic organization using Windows Task Scheduler (daily, weekly, on login, on system startup).
- **Command Line Execution**: Support for automatic execution with the `--auto` parameter.
- **Administrator Check**: Warns if not running as administrator for scheduling features.

### Requirements

- .NET 10.0 or higher
- Windows operating system (requires Task Scheduler)
- Administrator privileges recommended for task scheduling

### Installation

Download the latest release from the [Releases](https://github.com/martimfm1/download-organizer/releases) page.

### Usage

#### Interactive Interface

Run the program without arguments to access the main menu:

```
========= Download Organizer =========
1 - Organize files
2 - Settings
3 - Schedule Task
4 - Exit
```

- **1 - Organize files**: Immediately organizes files in the Downloads folder.
- **2 - Settings**: Access settings, such as removing scheduled tasks.
- **3 - Schedule Task**: Schedule automatic tasks.
- **4 - Exit**: Exit the program.

#### Automatic Execution

To run organization without interface:
```
download-manager.exe --auto
```

#### Scheduling

In the "Schedule Task" menu, you can choose:
- **1 - Schedule daily**: Runs daily at 9:00 AM.
- **2 - Schedule weekly**: Runs weekly on Mondays at 9:00 AM.
- **3 - Schedule on login**: Runs when the user logs in.
- **4 - Schedule on system startup**: Runs on system startup.

Note: Requires administrator privileges.

### Folder Structure

After organization, files will be moved to subfolders in the Downloads folder:
- `Images/`: .jpg, .jpeg, .png, .gif, .webp
- `Documents/`: .pdf, .docx, .txt, .doc, .xls, .xlsx, .ppt, .pptx
- `Videos/`: .mp4, .mkv, .avi, .mov, .wmv, .flv
- `Music/`: .mp3, .wav, .aac, .flac, .ogg
- `Archives/`: .zip, .rar, .7z, .tar, .gz
- `Executables/`: .exe, .msi, .bat, .sh
- `Others/`: Files that do not fit the above categories

## For Developers

### Dependencies

- Microsoft.Win32.TaskScheduler: For integration with Windows Task Scheduler.

### Building from Source

1. Clone this repository:
   ```
   git clone https://github.com/martimfm1/download-organizer.git
   cd download-organizer
   ```

2. Restore dependencies:
   ```
   dotnet restore
   ```

3. Build the project:
   ```
   dotnet build --configuration Release
   ```

4. Run the application:
   ```
   dotnet run
   ```

   Or use the compiled executable in `bin/Release/net10.0/win-x64/download-manager.exe`.

### Contributing

Contributions are welcome! Feel free to open issues or pull requests.

### License

This project is licensed under the MIT License.