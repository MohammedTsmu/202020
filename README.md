
# 202020 Eye Protection App

## Overview

The **202020 Eye Protection App** is a C# desktop application designed to enforce the 20-20-20 rule, a guideline for preventing eye strain caused by prolonged screen time. The app runs silently in the system tray and reminds users to take a 20-second break every 20 minutes by displaying a full-screen, non-closable rest screen. 

This project was developed to promote healthy screen usage habits, especially for people who spend long hours working on computers.

## Features

- **20-20-20 Rule Enforcement**: Automatically reminds the user to take a 20-second break every 20 minutes.
- **Non-Closable Rest Screen**: During the rest time, the app displays a full-screen overlay that cannot be closed, minimized, or bypassed by the user.
- **System Tray Application**: The app runs quietly in the system tray without displaying a main window.
- **Automatic Startup**: The app automatically starts when the user logs in or starts their computer.
- **No Alt+Tab Visibility**: The main window does not appear in the Alt+Tab window switcher.
- **Customizable and Lightweight**: Simple and clean interface with minimal resource usage.

## Installation

### Prerequisites

- Windows OS
- .NET Framework 4.7.2 or later
- Visual Studio (optional for modifications)

### Steps

1. **Clone the repository**:
   ```bash
   git clone https://github.com/MohammedTsmu/202020.git
   ```

2. **Run the executable**:
   Once cloned, you can directly run the pre-built executable (`202020.exe`), or you can build the solution in Visual Studio if you'd like to make any modifications.

3. **Building from source**:
   - Open the project in **Visual Studio**.
   - Restore dependencies and build the project.
   - Run the application.

## Usage

1. After starting the application, it will run quietly in the **system tray**.
2. Every 20 minutes, a full-screen **rest screen** will appear for 20 seconds, encouraging you to look away from your screen.
3. You will not be able to close or bypass the rest screen until the timer finishes.
4. The app will automatically restart the 20-minute timer after each rest period.

## Customization

If you would like to modify the rest time or other behavior, you can do so by editing the `MainForm.cs` file in Visual Studio. 

```csharp
private int workTime = 20 * 60; // Change the 20 minutes to any desired time
private int restTime = 20; // Change the rest duration from 20 seconds to any desired time
```

## License

This project is licensed under the AGPL-3.0 license. See the `LICENSE` file for details.

## Developer
Dr. Mohammed
## Contributing

Contributions are welcome! Feel free to submit issues or pull requests to improve this project.

## Contact

If you have any questions, you can reach out to the project maintainer via [GitHub](https://github.com/your-username).
