# openlawnz-web-backend

## Installation

This project uses [https://code.visualstudio.com/docs/remote/containers](https://code.visualstudio.com/docs/remote/containers) - please follow installation instructions there.

In VS Code:

1. Hold `command + shift + p`
2. Select `Remote-Containers: Open Folder in Container...`
3. Select this folder

Wait for it to provision everything:

- .Net Core
- Entity Framework Core
- Postgres server
- Migrations

Then select the Debug and Run option on the left hand side and press the play button.

Visit http://localhost:5000/api/folders - which should show an empty array.