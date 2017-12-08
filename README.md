# Cake.VSProjectProperty

 A Cake AddIn that give Cake a kind of ability to read and write csproj file properties.
 
 【Attention】

    This is an initial version and not tested thoroughly.
    Make sure your have committed your project to git before use this addin,
    because of it will overwrite your .csproj file, and this may be risk.

## API

### **SetVSProjectProperties**

```csharp
/// <summary>
/// set the properties of .csproj file
/// </summary>
/// <param name="context">Cake context</param>
/// <param name="projectFilePath">path of .csproj file</param>
/// <param name="keyValues">properties key values</param>
/// <param name="configure">build configure</param>
[CakeMethodAlias]
public static void SetVSProjectProperties(this ICakeContext context, FilePath projectFilePath, IDictionary<string, string> keyValues, string configure = "Release")
```

### **GetVSProjectProperties**

```csharp
/// <summary>
/// get properties from .csproj file
/// </summary>
/// <param name="context">Cake context</param>
/// <param name="projectFilePath">path of .csproj file</param>
/// <param name="keys">properties keys</param>
/// <param name="configure">build configure</param>
/// <returns></returns>
[CakeMethodAlias]
public static IDictionary<string, string> GetVSProjectProperties(this ICakeContext context, FilePath projectFilePath, IEnumerable<string> keys, string configure = "Release")
```

## Usage

    1. To use this addin, first download the project and then just compile it. Maybe you should sign it.
    2. Copy the compiled Cake.VSProjectProperty.dll to the lib folder in your Cake build folder.
    3. use `#r lib/Cake.VSProjectProperty.dll` at your build.cake file.

## Example

```csharp
#r lib/Cake.VSProjectProperty.dll

...

Task("Build").IsDependentOn("Assembly").Does(()=>{

    var keys = new List<string>(){ "AssemblyName","DefineConstants" };
    var getter = GetVSProjectProperties(proj_path,keys,configuration);
    
    var setter = new Dictionary<string,string>{
        {"AssemblyName",exe_name},
        {"DefineConstants", defines}
    };

    SetVSProjectProperties(proj_path,setter,configuration);

    MSBuild(sln_path, new MSBuildSettings {
            ToolVersion = MSBuildToolVersion.VS2015,
            Configuration = configuration
    });

    SetVSProjectProperties(proj_path,getter,configuration);
});
```