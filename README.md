# CnCNet.Launcher
Launcher for Windows for the XNA / MonoGame client that automatically selects the correct executable to run for the user's platform.

Also checks and notifies about any needed client runtimes.

The launcher itself relies on .NET Framework 4.0.

You can find the [dedicated project development chat](https://discord.gg/M5gGdBYG5m) at C&C Mod Haven Discord server.

## Optional arguments

### Flag arguments
Prefer .NET 8 clients to .NET Framework 4.8 clients:
```
-NET8
```

If this flag is not set, the launcher will run .NET Framework 4.8 clients.

### Standalone arguments
Do not autoselect a version, run the WinForms DirectX client:
```
-DX
```
Do not autoselect a version, run the WinForms OpenGL client:
```
-OGL
```
Do not autoselect a version, run the WinForms XNA client:
```
-XNA
```
Do not autoselect a version, run the cross-platform OpenGL client in .NET 8:
```
-UGL
```
