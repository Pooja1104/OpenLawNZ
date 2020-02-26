cd api
dotnet restore
dotnet tool install --global dotnet-ef --version 3.1.0
cat << \EOF >> ~/.bashrc
# Add .NET Core SDK tools
export PATH="$PATH:/home/vscode/.dotnet/tools"
EOF
export PATH="$PATH:/home/vscode/.dotnet/tools"
if [ ! -d "Migrations" ]; then
dotnet-ef migrations add InitialCreate
dotnet-ef database update
fi
