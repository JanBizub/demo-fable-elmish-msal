# demo-fable-elmish-msal

# Backend and installation
- Create App Registration for your web app for your single page applciation (AADDemo.App)
- Azure Portal > Home > Default Directory - App registrations > AADDemo - Manifest: set "oauth2AllowImplicitFlow": true,
- Clone this repo
- Open cmd in root
- .\.paket\paket.bootstrapper.exe
- .\.paket\paket.exe install
- cd .\src\AADDemo.App\
- npm install
- open Solution
- AADDemo.API > Startup.cs
- put authority guid (line 27)
- put client id guid (line 28)
- in properties > debug > check enable ssl
- Run AADDemo.API project

# Front end
- AADDemo.App > App.fs
- fill lines 15 and 16
- add ssl address of AADDemo.API to line 69
- open cmd in folder demo-fable-elmish-msal\src\AADDemo.App 
- npx webpack-dev-server --https

Be sure your browser trusts certificates of both frontend and backend
