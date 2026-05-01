# Download Organizer

Automatically organizes your Downloads folder by moving files into categorized subfolders based on their extension.

---

## How to use

1. Download `download-organizer.exe` from the latest release
2. Run it as **Administrator** for scheduling features
3. Choose an option from the menu:
```
========= Download Organizer =========
1 - Organize files
2 - Settings
3 - Schedule Task
4 - Exit
```

**1 - Organize files** — moves all files in your Downloads folder to subfolders automatically.

**2 - Settings** — remove scheduled tasks.

**3 - Schedule Task** — schedule the organizer to run automatically:
- Daily at 9:00 AM
- Weekly on Mondays
- On login
- On system startup

**4 - Exit** — closes the program.

> Note: Scheduling requires Administrator privileges.

---

## Default categories

| Folder | Extensions |
|--------|-----------|
| Images | .jpg .jpeg .png .gif .webp |
| Documents | .pdf .docx .txt .doc .xls .xlsx .ppt .pptx |
| Videos | .mp4 .mkv .avi .mov .wmv .flv |
| Music | .mp3 .wav .aac .flac .ogg |
| Archives | .zip .rar .7z .tar .gz |
| Executables | .exe .msi .bat .sh |
| Others | everything else |

---

## For developers

### How it works

- Files are scanned with `Directory.GetFiles()` and matched by extension
- Subfolders are created automatically with `Directory.CreateDirectory()`
- Scheduling uses the `TaskScheduler` NuGet package to register Windows tasks
- When called with `--auto` argument, runs silently without UI (used by scheduler)

### Requirements

- .NET SDK 10.0 or higher
- Windows only

### Run locally

```bash
git clone https://github.com/martimfm1/download-organizer
cd download-organizer
dotnet run
```

### Build

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true
```

---

## Technologies

- C# / .NET 10
- [TaskScheduler](https://github.com/dahall/TaskScheduler) NuGet package