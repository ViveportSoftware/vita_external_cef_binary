#addin "nuget:?package=Cake.CMake&version=1.3.1"
#addin "nuget:?package=Cake.FileHelpers&version=4.0.1"
#addin "nuget:?package=Cake.Git&version=1.1.0"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var configuration = Argument("configuration", "Debug");
var revision = EnvironmentVariable("BUILD_NUMBER") ?? Argument("revision", "9999");
var target = Argument("target", "Default");
var cefWithAutomateGitCommitId = EnvironmentVariable("CEF_WITH_AUTOMATE_GIT_COMMIT_ID") ?? "NOTSET";
var cefWithAutomateGitRoot = EnvironmentVariable("CEF_WITH_AUTOMATE_GIT_ROOT") ?? "NOTSET";
var cefWithChromeDepotToolsCommitId = EnvironmentVariable("CEF_WITH_CHROME_DEPOT_TOOLS_COMMIT_ID") ?? "NOTSET";
var cefWithChromeDepotToolsRoot = EnvironmentVariable("CEF_WITH_CHROME_DEPOT_TOOLS_ROOT") ?? "NOTSET";
var cefWithChromeDepotToolsWithUpdate = EnvironmentVariable("CEF_WITH_CHROME_DEPOT_TOOLS_WITH_UPDATE") ?? "ON";
var cefWithChromiumBranchName = EnvironmentVariable("CEF_WITH_CHROMIUM_BRANCH_NAME") ?? "5304";
var cefWithCleanBuild = EnvironmentVariable("CEF_WITH_CLEAN_BUILD") ?? "ON";
var cefWithMsvsRoot = EnvironmentVariable("CEF_WITH_MSVS_ROOT") ?? "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community";
var cefWithMsvsVcRedistCrtName = EnvironmentVariable("CEF_WITH_MSVS_VC_REDIST_CRT_NAME") ?? "Microsoft.VC142.CRT";
var cefWithMsvsVcRedistVersion = EnvironmentVariable("CEF_WITH_MSVS_VC_REDIST_VERSION") ?? "14.29.30133";
var cefWithMsvsVcToolsVersion = EnvironmentVariable("CEF_WITH_MSVS_VC_TOOLS_VERSION") ?? "14.29.30133";
var cefWithMsvsVersion = EnvironmentVariable("CEF_WITH_MSVS_VERSION") ?? "2019";
var cefWithProprietaryCodecs = EnvironmentVariable("CEF_WITH_PROPRIETARY_CODECS") ?? "OFF";
var cefWithSdkCmakeMsbuildVersion = EnvironmentVariable("CEF_WITH_SDK_CMAKE_MSBUILD_VERSION") ?? "default";
var cefWithSdkCmakeToolsetV120 = EnvironmentVariable("CEF_WITH_SDK_CMAKE_TOOLSET_V120") ?? "OFF";
var cefWithSdkCmakeToolsetV140 = EnvironmentVariable("CEF_WITH_SDK_CMAKE_TOOLSET_V140") ?? "OFF";
var cefWithSdkCmakeToolsetV141 = EnvironmentVariable("CEF_WITH_SDK_CMAKE_TOOLSET_V141") ?? "OFF";
var cefWithSdkCmakeToolsetV142 = EnvironmentVariable("CEF_WITH_SDK_CMAKE_TOOLSET_V142") ?? "ON";
var cefWithSdkCmakeToolsetV143 = EnvironmentVariable("CEF_WITH_SDK_CMAKE_TOOLSET_V143") ?? "OFF";
var cefWithSourceRoot = EnvironmentVariable("CEF_WITH_SOURCE_ROOT") ?? "NOTSET";
var cefWithWinArm64Binary = EnvironmentVariable("CEF_WITH_WIN_ARM64_BINARY") ?? "ON";
var cefWithWinSdkRoot = EnvironmentVariable("CEF_WITH_WIN_SDK_ROOT") ?? "C:\\Program Files (x86)\\Windows Kits\\10";
var cefWithWinSdkVersion = EnvironmentVariable("CEF_WITH_WIN_SDK_VERSION") ?? "10.0.20348.0";
var cefWithWinX64Binary = EnvironmentVariable("CEF_WITH_WIN_X64_BINARY") ?? "ON";
var cefWithWinX86Binary = EnvironmentVariable("CEF_WITH_WIN_X86_BINARY") ?? "ON";


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define git commit id
var commitId = "SNAPSHOT";

// Define product name and version
var product = "htc_vita_external_cef";
var productDescription = "HTC Vita External Library for WebView (CEF)";
var companyName = "HTC";
var version = "";
var distribVersion = "";
var semanticVersion = "";
var buildVersion = "";
var nugetAuthors = new [] {"HTC"};
var nugetTags = new [] {"htc", "vita", "cef"};
var projectUrl = "https://github.com/ViveportSoftware/vita_external_cef_binary/";
var isCleanBuild = "ON".Equals(cefWithCleanBuild);
var isReleaseBuild = "Release".Equals(configuration);
var cefAutomateGitCommitIdMap = new Dictionary<string, string>
{
        { "3945", "3afa29d499b0ce4dcb847459a79143dee347fa86" },
        { "3987", "3afa29d499b0ce4dcb847459a79143dee347fa86" },
        { "4044", "06a5ef3cd80370ab8fa6ee27975d87d3568c4dd2" },
        { "4103", "047e8f9349ecd635b1146e4a44de46a38b8a64f8" },
        { "4147", "75e0f9f60ea8b4124ac9588954a4679d3fa03552" },
        { "4183", "75e0f9f60ea8b4124ac9588954a4679d3fa03552" },
        { "4240", "24c2f2fa3816c61e6996473e7cc5bc18b155f17c" },
        { "4324", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4389", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4430", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4472", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4515", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4577", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4606", "b798147c33913a5500704892d3f57853f72ad353" },
        { "4638", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "4664", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "4692", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "4758", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "4844", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "4896", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "4951", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "5005", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "5060", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "5112", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "5195", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "5249", "0a10fd4506c6753b38a00182c60de65c9ea30a22" },
        { "5304", "60ee4a34aa542005901114b8bfe8b891b3873f38" }
};
var cefSdkCmakeOptions = new List<string>();
var cefLocaleFileNames = new []
{
        "af.pak", "am.pak", "ar.pak",
        "bg.pak", "bn.pak",
        "ca.pak", "cs.pak",
        "da.pak", "de.pak",
        "el.pak", "en-GB.pak", "en-US.pak", "es.pak", "es-419.pak", "et.pak",
        "fa.pak", "fi.pak", "fil.pak", "fr.pak",
        "gu.pak",
        "he.pak", "hi.pak", "hr.pak", "hu.pak",
        "id.pak", "it.pak",
        "ja.pak",
        "kn.pak", "ko.pak",
        "lt.pak", "lv.pak",
        "ml.pak", "mr.pak", "ms.pak",
        "nb.pak", "nl.pak",
        "pl.pak", "pt-BR.pak", "pt-PT.pak",
        "ro.pak", "ru.pak",
        "sk.pak", "sl.pak", "sr.pak", "sv.pak", "sw.pak",
        "ta.pak", "te.pak", "th.pak", "tr.pak",
        "uk.pak", "ur.pak",
        "vi.pak",
        "zh-CN.pak", "zh-TW.pak"
};
cefSdkCmakeOptions.Add("-DUSE_SANDBOX=OFF");
cefSdkCmakeOptions.Add("-DCEF_DEBUG_INFO_FLAG=/Z7");
cefSdkCmakeOptions.Add("-DCEF_RUNTIME_LIBRARY_FLAG=/MD");
var chromeDepotToolsUrl = "https://storage.googleapis.com/chrome-infra/depot_tools.zip";
var cefSdkMsbuildSettingsDebug = new MSBuildSettings()
{
        Configuration = "Debug",
        MaxCpuCount = 0
}
.WithTarget("cefsimple");
var cefSdkMsbuildSettingsRelease = new MSBuildSettings()
{
        Configuration = "Release",
        MaxCpuCount = 0
}
.WithTarget("cefsimple");
var shouldBuildSdkV120 = "ON".Equals(cefWithSdkCmakeToolsetV120);
var shouldBuildSdkV140 = "ON".Equals(cefWithSdkCmakeToolsetV140);
var shouldBuildSdkV141 = "ON".Equals(cefWithSdkCmakeToolsetV141);
var shouldBuildSdkV142 = "ON".Equals(cefWithSdkCmakeToolsetV142);
var shouldBuildSdkV143 = "ON".Equals(cefWithSdkCmakeToolsetV143);
var shouldBuildWinArm64Binary = "ON".Equals(cefWithWinArm64Binary);
var shouldBuildWinX64Binary = "ON".Equals(cefWithWinX64Binary);
var shouldBuildWinX86Binary = "ON".Equals(cefWithWinX86Binary);

// Define copyright
var copyright = $"Copyright © 2022 - {DateTime.Now.Year}";

// Define timestamp for signing
var lastSignTimestamp = DateTime.Now;
var signIntervalInMilli = 1000 * 5;

// Define directories.
var sourceDir = Directory("./source");
var distDir = Directory("./dist");
var tempDir = Directory("./temp");
var packagesDir = Directory("./source/packages");
var nugetDir = Directory("./dist") + Directory(configuration) + Directory("nuget");
var homeDir = Directory(EnvironmentVariable("USERPROFILE") ?? EnvironmentVariable("HOME"));
var cefAutomateGitDir = sourceDir + Directory("automate");
var cefAutomateGitFile = cefAutomateGitDir + File("automate-git.py");
var cefSoureDir = sourceDir + Directory("chromium_git");
var chromeDepotToolsDir = sourceDir + Directory("depot_tools");
var sdkTargetDir = tempDir + Directory($"{product}.sdk");
var binaryDistribRootDir = cefSoureDir + Directory("chromium") + Directory("src") + Directory("cef") + Directory("binary_distrib");
var binaryDistribWinArm64Dir = binaryDistribRootDir + Directory($"cef_binary_{distribVersion}_windowsarm64");
var binaryDistribWinX64Dir = binaryDistribRootDir + Directory($"cef_binary_{distribVersion}_windows64");
var binaryDistribWinX86Dir = binaryDistribRootDir + Directory($"cef_binary_{distribVersion}_windows32");

// Define signing key, password and timestamp server
var signKeyEnc00 = EnvironmentVariable("SIGNKEYENC00");
var signKeyEnc01 = EnvironmentVariable("SIGNKEYENC01");
var signKeyEnc02 = EnvironmentVariable("SIGNKEYENC02");
var signKeyEnc03 = EnvironmentVariable("SIGNKEYENC03");
var signKeyEnc04 = EnvironmentVariable("SIGNKEYENC04");
var signKeyEnc05 = EnvironmentVariable("SIGNKEYENC05");
var signKeyEnc06 = EnvironmentVariable("SIGNKEYENC06");
var signKeyEnc07 = EnvironmentVariable("SIGNKEYENC07");
var signKeyEnc = EnvironmentVariable("SIGNKEYENC") ?? signKeyEnc00 + signKeyEnc01 + signKeyEnc02 + signKeyEnc03 + signKeyEnc04 + signKeyEnc05 + signKeyEnc06 + signKeyEnc07 ?? "NOTSET";
var signPass = EnvironmentVariable("SIGNPASS") ?? "NOTSET";
var signSha1Uri = new Uri("http://timestamp.digicert.com");
var signSha256Uri = new Uri("http://timestamp.digicert.com");

// Define nuget push source and key
var nugetApiKey = EnvironmentVariable("NUGET_PUSH_TOKEN") ?? EnvironmentVariable("NUGET_APIKEY") ?? "NOTSET";
var nugetSource = EnvironmentVariable("NUGET_PUSH_PATH") ?? EnvironmentVariable("NUGET_SOURCE") ?? "NOTSET";


//////////////////////////////////////////////////////////////////////
// METHODS
//////////////////////////////////////////////////////////////////////

void DetectVersionInSource()
{
    if (!string.IsNullOrWhiteSpace(version) && !string.IsNullOrWhiteSpace(distribVersion))
    {
        return;
    }

    var cefVersionFile = cefSoureDir
                       + Directory("chromium")
                       + Directory("src")
                       + Directory("cef")
                       + Directory("include")
                       + File("cef_version.h");
    if (!FileExists(cefVersionFile))
    {
        throw new Exception($"Can not find {cefVersionFile}");
    }

    var lines = FileReadLines(cefVersionFile);
    var unknownVersionPart = "9999";
    var majorPart = unknownVersionPart;
    var minorPart = unknownVersionPart;
    var patchPart = unknownVersionPart;
    foreach (var line in lines)
    {
        var prefix = "#define CEF_VERSION_MAJOR ";
        if (line.StartsWith(prefix))
        {
            majorPart = line.Substring(prefix.Length);
            continue;
        }
        prefix = "#define CEF_VERSION_MINOR ";
        if (line.StartsWith(prefix))
        {
            minorPart = line.Substring(prefix.Length);
            continue;
        }
        prefix = "#define CEF_VERSION_PATCH ";
        if (line.StartsWith(prefix))
        {
            patchPart = line.Substring(prefix.Length);
            continue;
        }
        prefix = "#define CEF_VERSION ";
        if (line.StartsWith(prefix))
        {
            distribVersion = line.Substring(prefix.Length).Trim('"');
            continue;
        }
    }
    version = $"{majorPart}.{minorPart}.{patchPart}";

    semanticVersion = $"{version}.{revision}";
    buildVersion = isReleaseBuild ? semanticVersion : $"{version}.0-CI{revision}";
    Information("Build version: {0}", buildVersion);
    Information("Build distrib version: {0}", distribVersion);
}

string GetAutomateGitCommitId()
{
    if (!"NOTSET".Equals(cefWithAutomateGitCommitId))
    {
        return cefWithAutomateGitCommitId;
    }
    if (cefAutomateGitCommitIdMap.ContainsKey(cefWithChromiumBranchName))
    {
        return cefAutomateGitCommitIdMap[cefWithChromiumBranchName];
    }
    return "master";
}

List<NuSpecContent> GetNuSpecContentList(ConvertableDirectoryPath basePath, string[] pathesToPack, string target)
{
    var nuSpecContentList = new List<NuSpecContent>();
    foreach(var pathToPack in pathesToPack)
    {
        var pathTokens = pathToPack.Split('/');
        var pathTokensLength = pathTokens.Length;
        FilePath fileToPack = null;
        for(var i = pathTokensLength - 1; i >= 0; i--)
        {
            if (fileToPack == null)
            {
                fileToPack = File(pathTokens[i]);
            }
            else
            {
                fileToPack = Directory(pathTokens[i]) + fileToPack;
            }
        }
        fileToPack = basePath + fileToPack;
        if (!FileExists(fileToPack))
        {
            Warning($"Binary {fileToPack} does not exist. Skipped.");
        }
        else
        {
            nuSpecContentList.Add(new NuSpecContent
            {
                    Source = pathToPack,
                    Target = target
            });
        }
    }

    return nuSpecContentList;
}

List<string> GetCefSdkCmakeToolsetList()
{
    var toolsetList = new List<string>();
    if (shouldBuildSdkV120)
    {
        toolsetList.Add("v120");
    }
    if (shouldBuildSdkV140)
    {
        toolsetList.Add("v140");
    }
    if (shouldBuildSdkV141)
    {
        toolsetList.Add("v141");
    }
    if (shouldBuildSdkV142)
    {
        toolsetList.Add("v142");
    }
    if (shouldBuildSdkV143)
    {
        toolsetList.Add("v143");
    }
    return toolsetList;
}

IDictionary<string, string> GetWindowsEnvironmentVariables(string platform)
{
    var ninjaArguments = new List<string>();
    ninjaArguments.Add($"--ide=vs{cefWithMsvsVersion}");
    ninjaArguments.Add("--sln=cef");
    ninjaArguments.Add("--filters=//cef/*");

    var ninjaDefines = new List<string>();
    ninjaDefines.Add("ffmpeg_branding=\"Chrome\"");
    ninjaDefines.Add("is_component_build=false");
    ninjaDefines.Add("is_official_build=true");
    if ("ON".Equals(cefWithProprietaryCodecs))
    {
        ninjaDefines.Add("proprietary_codecs=true");
    }
    if (isReleaseBuild)
    {
        ninjaDefines.Add("blink_symbol_level=0");
        ninjaDefines.Add("symbol_level=1");
        ninjaDefines.Add("v8_symbol_level=0");
    }

    var env = EnvironmentVariables();
    env["CEF_ARCHIVE_FORMAT"] = "tar.bz2";
    env["CEF_USE_GN"] = "1";
    env["GN_ARGUMENTS"] = string.Join(" ", ninjaArguments);
    env["GN_DEFINES"] = string.Join(" ", ninjaDefines);
    env["GYP_MSVS_VERSION"] = cefWithMsvsVersion;

    if (string.IsNullOrWhiteSpace(platform))
    {
        return env;
    }

    var include = new List<string>();
    include.Add($"{cefWithWinSdkRoot}\\Include\\{cefWithWinSdkVersion}\\um");
    include.Add($"{cefWithWinSdkRoot}\\Include\\{cefWithWinSdkVersion}\\ucrt");
    include.Add($"{cefWithWinSdkRoot}\\Include\\{cefWithWinSdkVersion}\\shared");
    include.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\include");
    include.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\atlmfc\\include");

    var lib = new List<string>();
    lib.Add($"{cefWithWinSdkRoot}\\Lib\\{cefWithWinSdkVersion}\\um\\{platform}");
    lib.Add($"{cefWithWinSdkRoot}\\Lib\\{cefWithWinSdkVersion}\\ucrt\\{platform}");
    lib.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\lib\\{platform}");
    lib.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\atlmfc\\lib\\{platform}");

    var path = new List<string>();
    path.Add($"{cefWithWinSdkRoot}\\bin\\{cefWithWinSdkVersion}\\{platform}");
    if ("arm64".Equals(platform))
    {
        path.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\bin\\Hostx64\\{platform}");
        path.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\bin\\Hostx64\\x64");
    }
    if ("x64".Equals(platform))
    {
        path.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\bin\\Hostx64\\{platform}");
    }
    if ("x86".Equals(platform))
    {
        path.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\bin\\Hostx86\\{platform}");
        path.Add($"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\bin\\Hostx64\\{platform}");
    }
    path.Add($"{cefWithMsvsRoot}\\VC\\Redist\\MSVC\\{cefWithMsvsVcRedistVersion}\\{platform}\\{cefWithMsvsVcRedistCrtName}");

    env["CEF_ENABLE_ARM64"] = "1";
    env["CEF_VCVARS"] = "none";
    env["DEPOT_TOOLS_WIN_TOOLCHAIN"] = "0";
    env["GYP_MSVS_OVERRIDE_PATH"] = cefWithMsvsRoot;

    var oldInclude = "";
    if (env.ContainsKey("INCLUDE"))
    {
        oldInclude = env["INCLUDE"];
    }
    var newInclude = string.Join(";", include);
    if (!string.IsNullOrWhiteSpace(oldInclude))
    {
        newInclude += $";{oldInclude}";
    }
    env["INCLUDE"] = newInclude;

    var oldLib = "";
    if (env.ContainsKey("LIB"))
    {
        oldLib = env["LIB"];
    }
    var newLib = string.Join(";", lib);
    if (!string.IsNullOrWhiteSpace(oldLib))
    {
        newLib += $";{oldLib}";
    }
    env["LIB"] = newLib;

    var oldPath = "";
    if (env.ContainsKey("PATH"))
    {
        oldPath = env["PATH"];
    }
    var newPath = string.Join(";", path);
    if (!string.IsNullOrWhiteSpace(oldPath))
    {
        newPath += $";{oldPath}";
    }
    env["PATH"] = newPath;

    env["SDK_ROOT"] = cefWithWinSdkRoot;
    env["VS_CRT_ROOT"] = $"{cefWithMsvsRoot}\\VC\\Tools\\MSVC\\{cefWithMsvsVcToolsVersion}\\crt\\src\\vcruntime";
    env["WIN_CUSTOM_TOOLCHAIN"] = "1";
    return env;
}

void GZipFile(FilePath source, DirectoryPath destination)
{
    byte[] contents = System.IO.File.ReadAllBytes(source.FullPath);
    FilePath output = destination.CombineWithFilePath($"{source.GetFilename()}.gz");
    Information($"Compressing {source} to {output}");

    using (var gzipStream = new System.IO.Compression.GZipStream(
            System.IO.File.Create(output.FullPath),
            System.IO.Compression.CompressionLevel.Optimal))
    {
        gzipStream.Write(contents, 0, contents.Length);
    }
}


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Fetch-Git-Commit-ID")
    .ContinueOnError()
    .Does(() =>
{
    var lastCommit = GitLogTip(MakeAbsolute(Directory(".")));
    commitId = lastCommit.Sha;
});

Task("Display-Config")
    .IsDependentOn("Fetch-Git-Commit-ID")
    .Does(() =>
{
    Information($"Build target:        {target}");
    Information($"Build configuration: {configuration}");
    Information($"Build commitId:      {commitId}");

    if("vs2022".Equals(cefWithSdkCmakeMsbuildVersion))
    {
        cefSdkMsbuildSettingsDebug.ToolVersion = MSBuildToolVersion.VS2022;
        cefSdkMsbuildSettingsRelease.ToolVersion = MSBuildToolVersion.VS2022;
    }
    Information($"MSBuild version:     {cefSdkMsbuildSettingsRelease.ToolVersion}");
});

Task("Clean-Workspace")
    .IsDependentOn("Display-Config")
    .Does(() =>
{
    CleanDirectory(distDir);
    CleanDirectory(tempDir);
});

Task("Prepare-Depot-Tools")
    .IsDependentOn("Clean-Workspace")
    .Does(() =>
{
    if (!"NOTSET".Equals(cefWithChromeDepotToolsRoot))
    {
        chromeDepotToolsDir = Directory(cefWithChromeDepotToolsRoot);
    }
    if (!DirectoryExists(chromeDepotToolsDir))
    {
        var chromeDepotToolsTempFile = tempDir + File("depot_tools.zip");
        Information("Try to download {0} to \"{1}\"", chromeDepotToolsUrl, chromeDepotToolsTempFile);
        DownloadFile(chromeDepotToolsUrl, chromeDepotToolsTempFile);
        Information("Try to extract {0} to {1}", chromeDepotToolsTempFile, chromeDepotToolsDir);
        Unzip(chromeDepotToolsTempFile, chromeDepotToolsDir);
    }
    Information("Locate depot tools: {0}", chromeDepotToolsDir);

    if ("ON".Equals(cefWithChromeDepotToolsWithUpdate))
    {
        var updateScriptFileName = "update_depot_tools.bat";
        var exitCodeWithArgument = StartProcess(
                "cmd.exe",
                new ProcessSettings
                {
                        Arguments = new ProcessArgumentBuilder()
                                .Append("/c")
                                .Append(updateScriptFileName),
                        WorkingDirectory = chromeDepotToolsDir
                }
        );
        Information("Update depot tools via {0}, exit code: {1}", updateScriptFileName, exitCodeWithArgument);
    }

    if (!"NOTSET".Equals(cefWithChromeDepotToolsCommitId))
    {
        GitCheckout(chromeDepotToolsDir, cefWithChromeDepotToolsCommitId);
    }

    var newChromeDepotToolsCommit = GitLogTip(MakeAbsolute(chromeDepotToolsDir));
    var newcefWithChromeDepotToolsCommitId = newChromeDepotToolsCommit.Sha;
    Information("Detect depot tools commit id: {0}", newcefWithChromeDepotToolsCommitId);
});

Task("Prepare-Automate-Git")
    .IsDependentOn("Prepare-Depot-Tools")
    .Does(() =>
{
    var automateGitCommitId = GetAutomateGitCommitId();
    cefAutomateGitDir = sourceDir + Directory("automate") + Directory(automateGitCommitId);
    if (!"NOTSET".Equals(cefWithAutomateGitRoot))
    {
        cefAutomateGitDir = Directory(cefWithAutomateGitRoot) + Directory(automateGitCommitId);
    }
    cefAutomateGitFile = cefAutomateGitDir + File("automate-git.py");
    if (!DirectoryExists(cefAutomateGitDir))
    {
        var cefAutomateGitTempFile = tempDir + File("automate-git.py");
        var cefAutomateGitUrl = $"https://bitbucket.org/chromiumembedded/cef/raw/{automateGitCommitId}/tools/automate/automate-git.py";
        Information("Try to download {0} to \"{1}\"", cefAutomateGitUrl, cefAutomateGitTempFile);
        DownloadFile(cefAutomateGitUrl, cefAutomateGitTempFile);
        CreateDirectory(cefAutomateGitDir);
        Information("Try to copy {0} to {1}", cefAutomateGitTempFile, cefAutomateGitFile);
        CopyFile(cefAutomateGitTempFile, cefAutomateGitFile);
    }
    Information("Locate automate git: {0}", cefAutomateGitDir);
});

Task("Fetch-Source")
    .IsDependentOn("Prepare-Automate-Git")
    .Does(() =>
{
    if (!"NOTSET".Equals(cefWithSourceRoot))
    {
        cefSoureDir = Directory(cefWithSourceRoot);
    }
    if (!DirectoryExists(cefSoureDir))
    {
        CreateDirectory(cefSoureDir);
    }

    var exitCodeWithArgument = StartProcess(
            "cmd.exe",
            new ProcessSettings
            {
                    Arguments = new ProcessArgumentBuilder()
                            .Append("/c")
                            .Append("python3.bat")
                            .Append(MakeAbsolute(cefAutomateGitFile).ToString())
                            .Append($"--branch={cefWithChromiumBranchName}")
                            .Append($"--depot-tools-dir=\"{MakeAbsolute(chromeDepotToolsDir).ToString()}\"")
                            .Append($"--download-dir=\"{MakeAbsolute(cefSoureDir).ToString()}\"")
                            .Append(isCleanBuild ? "--force-clean" : "")
                            .Append("--no-build")
                            .Append("--no-depot-tools-update")
                            .Append("--no-distrib"),
                    EnvironmentVariables = GetWindowsEnvironmentVariables(null),
                    WorkingDirectory = chromeDepotToolsDir
            }
    );
    if (exitCodeWithArgument != 0)
    {
        throw new Exception($"Can not fetch source code. exit code: {exitCodeWithArgument}");
    }
});

Task("Build-Binary-win-x86")
    .WithCriteria(() => shouldBuildWinX86Binary)
    .IsDependentOn("Fetch-Source")
    .Does(() =>
{
    var exitCodeWithArgument = StartProcess(
            "cmd.exe",
            new ProcessSettings
            {
                    Arguments = new ProcessArgumentBuilder()
                            .Append("/c")
                            .Append("python3.bat")
                            .Append(MakeAbsolute(cefAutomateGitFile).ToString())
                            .Append("--build-log-file")
                            .Append($"--branch={cefWithChromiumBranchName}")
                            .Append($"--depot-tools-dir=\"{MakeAbsolute(chromeDepotToolsDir).ToString()}\"")
                            .Append($"--download-dir=\"{MakeAbsolute(cefSoureDir).ToString()}\"")
                            .Append("--force-build")
                            .Append("--force-distrib")
                            .Append("--no-depot-tools-update")
                            .Append("--no-update")
                            .Append("--verbose-build"),
                    EnvironmentVariables = GetWindowsEnvironmentVariables("x86"),
                    WorkingDirectory = chromeDepotToolsDir
            }
    );
    if (exitCodeWithArgument != 0)
    {
        throw new Exception($"Can not build binary. exit code: {exitCodeWithArgument}");
    }

    DetectVersionInSource();

    // Update path
    binaryDistribRootDir = cefSoureDir
            + Directory("chromium")
            + Directory("src")
            + Directory("cef")
            + Directory("binary_distrib");
    binaryDistribWinX86Dir = binaryDistribRootDir + Directory($"cef_binary_{distribVersion}_windows32");
    if (!DirectoryExists(binaryDistribWinX86Dir))
    {
        throw new Exception($"Can not find binary distrib directory: {binaryDistribWinX86Dir}");
    }
});

Task("Build-Binary-win-x64")
    .WithCriteria(() => shouldBuildWinX64Binary)
    .IsDependentOn("Build-Binary-win-x86")
    .Does(() =>
{
    var exitCodeWithArgument = StartProcess(
            "cmd.exe",
            new ProcessSettings
            {
                    Arguments = new ProcessArgumentBuilder()
                            .Append("/c")
                            .Append("python3.bat")
                            .Append(MakeAbsolute(cefAutomateGitFile).ToString())
                            .Append("--build-log-file")
                            .Append($"--branch={cefWithChromiumBranchName}")
                            .Append($"--depot-tools-dir=\"{MakeAbsolute(chromeDepotToolsDir).ToString()}\"")
                            .Append($"--download-dir=\"{MakeAbsolute(cefSoureDir).ToString()}\"")
                            .Append("--force-build")
                            .Append("--force-distrib")
                            .Append("--no-depot-tools-update")
                            .Append("--no-update")
                            .Append("--verbose-build")
                            .Append("--x64-build"),
                    EnvironmentVariables = GetWindowsEnvironmentVariables("x64"),
                    WorkingDirectory = chromeDepotToolsDir
            }
    );
    if (exitCodeWithArgument != 0)
    {
        throw new Exception($"Can not fetch source code. exit code: {exitCodeWithArgument}");
    }

    DetectVersionInSource();

    // Update path
    binaryDistribRootDir = cefSoureDir
            + Directory("chromium")
            + Directory("src")
            + Directory("cef")
            + Directory("binary_distrib");
    binaryDistribWinX64Dir = binaryDistribRootDir + Directory($"cef_binary_{distribVersion}_windows64");
    if (!DirectoryExists(binaryDistribWinX64Dir))
    {
        throw new Exception($"Can not find binary distrib directory: {binaryDistribWinX64Dir}");
    }
});

Task("Build-Binary-win-arm64")
    .WithCriteria(() => shouldBuildWinArm64Binary)
    .IsDependentOn("Build-Binary-win-x64")
    .Does(() =>
{
    var exitCodeWithArgument = StartProcess(
            "cmd.exe",
            new ProcessSettings
            {
                    Arguments = new ProcessArgumentBuilder()
                            .Append("/c")
                            .Append("python3.bat")
                            .Append(MakeAbsolute(cefAutomateGitFile).ToString())                            
                            .Append("--arm64-build")
                            .Append("--build-log-file")
                            .Append("--build-target=cefsimple")
                            .Append($"--branch={cefWithChromiumBranchName}")
                            .Append($"--depot-tools-dir=\"{MakeAbsolute(chromeDepotToolsDir).ToString()}\"")
                            .Append($"--download-dir=\"{MakeAbsolute(cefSoureDir).ToString()}\"")
                            .Append("--force-build")
                            .Append("--force-distrib")
                            .Append("--no-depot-tools-update")
                            .Append("--no-update")
                            .Append("--verbose-build"),
                    EnvironmentVariables = GetWindowsEnvironmentVariables("arm64"),
                    WorkingDirectory = chromeDepotToolsDir
            }
    );
    if (exitCodeWithArgument != 0)
    {
        throw new Exception($"Can not fetch source code. exit code: {exitCodeWithArgument}");
    }

    DetectVersionInSource();

    // Update path
    binaryDistribRootDir = cefSoureDir
            + Directory("chromium")
            + Directory("src")
            + Directory("cef")
            + Directory("binary_distrib");
    binaryDistribWinArm64Dir = binaryDistribRootDir + Directory($"cef_binary_{distribVersion}_windowsarm64");
    if (!DirectoryExists(binaryDistribWinArm64Dir))
    {
        throw new Exception($"Can not find binary distrib directory: {binaryDistribWinArm64Dir}");
    }
});

Task("Build-SDK-win-x86")
    .WithCriteria(() => shouldBuildWinX86Binary)
    .IsDependentOn("Build-Binary-win-arm64")
    .Does(() =>
{
    foreach(var toolset in GetCefSdkCmakeToolsetList())
    {
        var sdkTargetBuildDir = sdkTargetDir
                + Directory(toolset)
                + Directory("win32");
        CreateDirectory(sdkTargetBuildDir);
        CMake(
                binaryDistribWinX86Dir,
                new CMakeSettings
                {
                        Options = cefSdkCmakeOptions.ToArray(),
                        OutputPath = sdkTargetBuildDir,
                        Platform = "Win32",
                        Toolset = toolset
                }
        );
        MSBuild(
                sdkTargetBuildDir + File("cef.sln"),
                cefSdkMsbuildSettingsDebug
        );
        MSBuild(
                sdkTargetBuildDir + File("cef.sln"),
                cefSdkMsbuildSettingsRelease
        );
    }
});

Task("Build-SDK-win-x64")
    .WithCriteria(() => shouldBuildWinX64Binary)
    .IsDependentOn("Build-SDK-win-x86")
    .Does(() =>
{
    foreach(var toolset in GetCefSdkCmakeToolsetList())
    {
        var sdkTargetBuildDir = sdkTargetDir
                + Directory(toolset)
                + Directory("x64");
        CreateDirectory(sdkTargetBuildDir);
        CMake(
                binaryDistribWinX64Dir,
                new CMakeSettings
                {
                        Options = cefSdkCmakeOptions.ToArray(),
                        OutputPath = sdkTargetBuildDir,
                        Platform = "x64",
                        Toolset = toolset
                }
        );
        MSBuild(
                sdkTargetBuildDir + File("cef.sln"),
                cefSdkMsbuildSettingsDebug
        );
        MSBuild(
                sdkTargetBuildDir + File("cef.sln"),
                cefSdkMsbuildSettingsRelease
        );
    }
});

Task("Build-SDK-win-arm64")
    .WithCriteria(() => shouldBuildWinArm64Binary)
    .IsDependentOn("Build-SDK-win-x64")
    .Does(() =>
{
    foreach(var toolset in GetCefSdkCmakeToolsetList())
    {
        var sdkTargetBuildDir = sdkTargetDir
                + Directory(toolset)
                + Directory("arm64");
        CreateDirectory(sdkTargetBuildDir);
        CMake(
                binaryDistribWinArm64Dir,
                new CMakeSettings
                {
                        Options = cefSdkCmakeOptions.ToArray(),
                        OutputPath = sdkTargetBuildDir,
                        Platform = "arm64",
                        Toolset = toolset
                }
        );
        MSBuild(
                sdkTargetBuildDir + File("cef.sln"),
                cefSdkMsbuildSettingsDebug
        );
        MSBuild(
                sdkTargetBuildDir + File("cef.sln"),
                cefSdkMsbuildSettingsRelease
        );
    }
});

Task("Sign-Binaries")
    .WithCriteria(() => isReleaseBuild && !"NOTSET".Equals(signPass) && !"NOTSET".Equals(signKeyEnc))
    .IsDependentOn("Build-SDK-win-arm64")
    .Does(() =>
{
    var signKey = "./temp/key.pfx";
    System.IO.File.WriteAllBytes(signKey, Convert.FromBase64String(signKeyEnc));

    var dirsToSign = new List<ConvertableDirectoryPath>();
    if (shouldBuildWinX86Binary)
    {
        dirsToSign.Add(binaryDistribWinX86Dir + Directory("Release"));
    }
    if (shouldBuildWinX64Binary)
    {
        dirsToSign.Add(binaryDistribWinX64Dir + Directory("Release"));
    }
    if (shouldBuildWinArm64Binary)
    {
        dirsToSign.Add(binaryDistribWinArm64Dir + Directory("Release"));
    }
    foreach (var dirToSign in dirsToSign)
    {
        var filesToSign = new[]
        {
                dirToSign + File("chrome_elf.dll"),
                dirToSign + File("libcef.dll"),
                dirToSign + File("libEGL.dll"),
                dirToSign + File("libGLESv2.dll"),
                dirToSign + File("vk_swiftshader.dll"),
                dirToSign + File("vulkan-1.dll")
        };
        foreach (var fileToSign in filesToSign)
        {
            if (!FileExists(fileToSign))
            {
                Warning("Binary {0} does not exist. Skipped.", fileToSign);
            }
            else
            {
                Sign(
                        fileToSign,
                        new SignToolSignSettings
                        {
                                AppendSignature = true,
                                TimeStampUri = signSha256Uri,
                                DigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                                TimeStampDigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                                CertPath = signKey,
                                Password = signPass
                        }
                );
                System.Threading.Thread.Sleep(signIntervalInMilli);
            }
        }
    }

    dirsToSign = new List<ConvertableDirectoryPath>();
    if (shouldBuildWinX86Binary)
    {
        dirsToSign.Add(binaryDistribWinX86Dir + Directory("Release") + Directory("swiftshader"));
    }
    if (shouldBuildWinX64Binary)
    {
        dirsToSign.Add(binaryDistribWinX64Dir + Directory("Release") + Directory("swiftshader"));
    }
    if (shouldBuildWinArm64Binary)
    {
        dirsToSign.Add(binaryDistribWinArm64Dir + Directory("Release") + Directory("swiftshader"));
    }
    foreach (var dirToSign in dirsToSign)
    {
        var filesToSign = new[]
        {
                dirToSign + File("libEGL.dll"),
                dirToSign + File("libGLESv2.dll")
        };
        foreach (var fileToSign in filesToSign)
        {
            if (!FileExists(fileToSign))
            {
                Warning("Binary {0} does not exist. Skipped.", fileToSign);
            }
            else
            {
                Sign(
                        fileToSign,
                        new SignToolSignSettings
                        {
                                AppendSignature = true,
                                TimeStampUri = signSha256Uri,
                                DigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                                TimeStampDigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                                CertPath = signKey,
                                Password = signPass
                        }
                );
                System.Threading.Thread.Sleep(signIntervalInMilli);
            }
        }
    }
});

Task("Gzip-Binaries")
    .IsDependentOn("Sign-Binaries")
    .Does(() =>
{
    var dirsToGzip = new List<ConvertableDirectoryPath>();
    if (shouldBuildWinX86Binary)
    {
        dirsToGzip.Add(binaryDistribWinX86Dir + Directory("Release"));
    }
    if (shouldBuildWinX64Binary)
    {
        dirsToGzip.Add(binaryDistribWinX64Dir + Directory("Release"));
    }
    if (shouldBuildWinArm64Binary)
    {
        dirsToGzip.Add(binaryDistribWinArm64Dir + Directory("Release"));
    }
    foreach (var dirToGzip in dirsToGzip)
    {
        var filesToGzip = new[]
        {
                dirToGzip + File("chrome_elf.dll"),
                dirToGzip + File("d3dcompiler_47.dll"),
                dirToGzip + File("libcef.dll"),
                dirToGzip + File("libEGL.dll"),
                dirToGzip + File("libGLESv2.dll"),
                dirToGzip + File("snapshot_blob.bin"),
                dirToGzip + File("v8_context_snapshot.bin"),
                dirToGzip + File("vk_swiftshader.dll"),
                dirToGzip + File("vk_swiftshader_icd.json"),
                dirToGzip + File("vulkan-1.dll")
        };
        foreach (var fileToGzip in filesToGzip)
        {
            if (!FileExists(fileToGzip))
            {
                Warning("Binary {0} does not exist. Skipped.", fileToGzip);
            }
            else
            {
                GZipFile(
                        fileToGzip,
                        dirToGzip
                );
            }
        }
    }

    dirsToGzip = new List<ConvertableDirectoryPath>();
    if (shouldBuildWinX86Binary)
    {
        dirsToGzip.Add(binaryDistribWinX86Dir + Directory("Release") + Directory("swiftshader"));
    }
    if (shouldBuildWinX64Binary)
    {
        dirsToGzip.Add(binaryDistribWinX64Dir + Directory("Release") + Directory("swiftshader"));
    }
    if (shouldBuildWinArm64Binary)
    {
        dirsToGzip.Add(binaryDistribWinArm64Dir + Directory("Release") + Directory("swiftshader"));
    }
    foreach (var dirToGzip in dirsToGzip)
    {
        var filesToGzip = new[]
        {
                dirToGzip + File("libEGL.dll"),
                dirToGzip + File("libGLESv2.dll")
        };
        foreach (var fileToGzip in filesToGzip)
        {
            if (!FileExists(fileToGzip))
            {
                Warning("Binary {0} does not exist. Skipped.", fileToGzip);
            }
            else
            {
                GZipFile(
                        fileToGzip,
                        dirToGzip
                );
            }
        }
    }

    dirsToGzip = new List<ConvertableDirectoryPath>();
    if (shouldBuildWinX86Binary)
    {
        dirsToGzip.Add(binaryDistribWinX86Dir + Directory("Resources"));
    }
    if (shouldBuildWinX64Binary)
    {
        dirsToGzip.Add(binaryDistribWinX64Dir + Directory("Resources"));
    }
    if (shouldBuildWinArm64Binary)
    {
        dirsToGzip.Add(binaryDistribWinArm64Dir + Directory("Resources"));
    }
    foreach (var dirToGzip in dirsToGzip)
    {
        var filesToGzip = new[]
        {
                dirToGzip + File("cef.pak"),
                dirToGzip + File("cef_100_percent.pak"),
                dirToGzip + File("cef_200_percent.pak"),
                dirToGzip + File("cef_extensions.pak"),
                dirToGzip + File("chrome_100_percent.pak"),
                dirToGzip + File("chrome_200_percent.pak"),
                dirToGzip + File("devtools_resources.pak"),
                dirToGzip + File("icudtl.dat"),
                dirToGzip + File("resources.pak")
        };
        foreach (var fileToGzip in filesToGzip)
        {
            if (!FileExists(fileToGzip))
            {
                Warning("Binary {0} does not exist. Skipped.", fileToGzip);
            }
            else
            {
                GZipFile(
                        fileToGzip,
                        dirToGzip
                );
            }
        }
    }

    dirsToGzip = new List<ConvertableDirectoryPath>();
    if (shouldBuildWinX86Binary)
    {
        dirsToGzip.Add(binaryDistribWinX86Dir + Directory("Resources") + Directory("locales"));
    }
    if (shouldBuildWinX64Binary)
    {
        dirsToGzip.Add(binaryDistribWinX64Dir + Directory("Resources") + Directory("locales"));
    }
    if (shouldBuildWinArm64Binary)
    {
        dirsToGzip.Add(binaryDistribWinArm64Dir + Directory("Resources") + Directory("locales"));
    }
    foreach (var dirToGzip in dirsToGzip)
    {
        foreach (var cefLocaleFileName in cefLocaleFileNames)
        {
            var fileToGzip = dirToGzip + File(cefLocaleFileName); 
            if (!FileExists(fileToGzip))
            {
                Warning("Binary {0} does not exist. Skipped.", fileToGzip);
            }
            else
            {
                GZipFile(
                        fileToGzip,
                        dirToGzip
                );
            }
        }
    }
});

Task("Build-NuGet-Package")
    .IsDependentOn("Gzip-Binaries")
    .Does(() =>
{
    CreateDirectory(nugetDir);

    var pathesToCef = new []
    {
            "Release/chrome_elf.dll",
            "Release/chrome_elf.dll.gz",
            "Release/d3dcompiler_47.dll",
            "Release/d3dcompiler_47.dll.gz",
            "Release/libcef.dll",
            "Release/libcef.dll.gz",
            "Release/libEGL.dll",
            "Release/libEGL.dll.gz",
            "Release/libGLESv2.dll",
            "Release/libGLESv2.dll.gz",
            "Release/snapshot_blob.bin",
            "Release/snapshot_blob.bin.gz",
            "Release/v8_context_snapshot.bin",
            "Release/v8_context_snapshot.bin.gz",
            "Release/vk_swiftshader.dll",
            "Release/vk_swiftshader.dll.gz",
            "Release/vk_swiftshader_icd.json",
            "Release/vk_swiftshader_icd.json.gz",
            "Release/vulkan-1.dll",
            "Release/vulkan-1.dll.gz",
            "Resources/cef.pak",
            "Resources/cef.pak.gz",
            "Resources/cef_100_percent.pak",
            "Resources/cef_100_percent.pak.gz",
            "Resources/cef_200_percent.pak",
            "Resources/cef_200_percent.pak.gz",
            "Resources/cef_extensions.pak",
            "Resources/cef_extensions.pak.gz",
            "Resources/chrome_100_percent.pak",
            "Resources/chrome_100_percent.pak.gz",
            "Resources/chrome_200_percent.pak",
            "Resources/chrome_200_percent.pak.gz",
            "Resources/devtools_resources.pak",
            "Resources/devtools_resources.pak.gz",
            "Resources/icudtl.dat",
            "Resources/icudtl.dat.gz",
            "Resources/resources.pak",
            "Resources/resources.pak.gz"
    };

    var pathListToCefLocales = new List<string>();
    foreach (var cefLocaleFileName in cefLocaleFileNames)
    {
        pathListToCefLocales.Add($"Resources/locales/{cefLocaleFileName}");
        pathListToCefLocales.Add($"Resources/locales/{cefLocaleFileName}.gz");
    }
    var pathesToCefLocales = pathListToCefLocales.ToArray();

    var pathesToCefSwiftshader = new []
    {
            "Release/swiftshader/libEGL.dll",
            "Release/swiftshader/libEGL.dll.gz",
            "Release/swiftshader/libGLESv2.dll",
            "Release/swiftshader/libGLESv2.dll.gz"
    };

    CopyDirectory(
            sourceDir + Directory("nuget"),
            binaryDistribRootDir
    );

    if (shouldBuildWinX86Binary)
    {
        var nuSpecContentListWinX86 = GetNuSpecContentList(
                binaryDistribWinX86Dir,
                pathesToCef,
                "CEF"
        );
        nuSpecContentListWinX86.AddRange(GetNuSpecContentList(
                binaryDistribWinX86Dir,
                pathesToCefLocales,
                "CEF\\locales"
        ));
        nuSpecContentListWinX86.AddRange(GetNuSpecContentList(
                binaryDistribWinX86Dir,
                pathesToCefSwiftshader,
                "CEF\\swiftshader"
        ));
        nuSpecContentListWinX86.Add(new NuSpecContent
        {
                Source = $"../{product}.redist.win-x86.props",
                Target = "build"
        });

        NuGetPack(new NuGetPackSettings
        {
                Id = $"{product}.redist.win-x86",
                Version = buildVersion,
                Authors = nugetAuthors,
                Description = $"{productDescription} [CommitId: {commitId}]",
                Copyright = copyright,
                ProjectUrl = new Uri(projectUrl),
                Tags = nugetTags,
                RequireLicenseAcceptance= false,
                Files = nuSpecContentListWinX86.ToArray(),
                Properties = new Dictionary<string, string>
                {
                        {"Configuration", configuration}
                },
                BasePath = binaryDistribWinX86Dir,
                OutputDirectory = nugetDir
        });
    }

    if (shouldBuildWinX64Binary)
    {
        var nuSpecContentListWinX64 = GetNuSpecContentList(
                binaryDistribWinX64Dir,
                pathesToCef,
                "CEF"
        );
        nuSpecContentListWinX64.AddRange(GetNuSpecContentList(
                binaryDistribWinX64Dir,
                pathesToCefLocales,
                "CEF\\locales"
        ));
        nuSpecContentListWinX64.AddRange(GetNuSpecContentList(
                binaryDistribWinX64Dir,
                pathesToCefSwiftshader,
                "CEF\\swiftshader"
        ));
        nuSpecContentListWinX64.Add(new NuSpecContent
        {
                Source = $"../{product}.redist.win-x64.props",
                Target = "build"
        });

        NuGetPack(new NuGetPackSettings
        {
                Id = $"{product}.redist.win-x64",
                Version = buildVersion,
                Authors = nugetAuthors,
                Description = $"{productDescription} [CommitId: {commitId}]",
                Copyright = copyright,
                ProjectUrl = new Uri(projectUrl),
                Tags = nugetTags,
                RequireLicenseAcceptance= false,
                Files = nuSpecContentListWinX64.ToArray(),
                Properties = new Dictionary<string, string>
                {
                        {"Configuration", configuration}
                },
                BasePath = binaryDistribWinX64Dir,
                OutputDirectory = nugetDir
        });
    }

    if (shouldBuildWinArm64Binary)
    {
        var nuSpecContentListWinArm64 = GetNuSpecContentList(
                binaryDistribWinArm64Dir,
                pathesToCef,
                "CEF"
        );
        nuSpecContentListWinArm64.AddRange(GetNuSpecContentList(
                binaryDistribWinArm64Dir,
                pathesToCefLocales,
                "CEF\\locales"
        ));
        nuSpecContentListWinArm64.AddRange(GetNuSpecContentList(
                binaryDistribWinArm64Dir,
                pathesToCefSwiftshader,
                "CEF\\swiftshader"
        ));
        nuSpecContentListWinArm64.Add(new NuSpecContent
        {
                Source = $"../{product}.redist.win-arm64.props",
                Target = "build"
        });

        NuGetPack(new NuGetPackSettings
        {
                Id = $"{product}.redist.win-arm64",
                Version = buildVersion,
                Authors = nugetAuthors,
                Description = $"{productDescription} [CommitId: {commitId}]",
                Copyright = copyright,
                ProjectUrl = new Uri(projectUrl),
                Tags = nugetTags,
                RequireLicenseAcceptance= false,
                Files = nuSpecContentListWinArm64.ToArray(),
                Properties = new Dictionary<string, string>
                {
                        {"Configuration", configuration}
                },
                BasePath = binaryDistribWinArm64Dir,
                OutputDirectory = nugetDir
        });
    }

    foreach(var toolset in GetCefSdkCmakeToolsetList())
    {
        var nuSpecContentListWinSdk = new List<NuSpecContent>();
        var sdkPropName = $"{product}.sdk.{toolset}.props";
        FileWriteText(
                sdkTargetDir + File(sdkPropName),
                ""
                        + "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
                        + "<Project ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n"
                        + "  <PropertyGroup>\n"
                        + "    <CefSdkVer>" + $"{product}.sdk.{toolset}.{buildVersion}" + "</CefSdkVer>\n"
                        + "  </PropertyGroup>\n"
                        + "</Project>"
        );
        nuSpecContentListWinSdk.Add(new NuSpecContent
        {
                Source = sdkPropName,
                Target = "build"
        });

        var includeDir = binaryDistribWinArm64Dir + Directory("include");
        if (!DirectoryExists(includeDir))
        {
            includeDir = binaryDistribWinX64Dir + Directory("include");
        }
        if (!DirectoryExists(includeDir))
        {
            includeDir = binaryDistribWinX86Dir + Directory("include");
        }
        if (!DirectoryExists(includeDir))
        {
            throw new Exception("Can not find include directory.");
        }
        CopyDirectory(
                includeDir,
                sdkTargetDir + Directory("include")
        );
        nuSpecContentListWinSdk.Add(new NuSpecContent
        {
                Source = "include/**",
                Target = "CEF"
        });

        var libcefConfigList = new []
        {
                "Debug",
                "Release"
        };
        foreach (var libcefConfig in libcefConfigList)
        {
            var libcefLibFileWinX86 = binaryDistribWinX86Dir + Directory(libcefConfig) + File("libcef.lib");
            if (FileExists(libcefLibFileWinX86))
            {
                CopyFile(
                        libcefLibFileWinX86,
                        sdkTargetDir + Directory(toolset) + Directory("win32") + File("libcef.lib")
                );
                nuSpecContentListWinSdk.Add(new NuSpecContent
                {
                        Source = $"{toolset}/win32/libcef.lib",
                        Target = $"CEF\\win32\\{libcefConfig.ToLower()}"
                });
                nuSpecContentListWinSdk.Add(new NuSpecContent
                {
                        Source = $"{toolset}/win32/libcef_dll_wrapper/{libcefConfig}/libcef_dll_wrapper.lib",
                        Target = $"CEF\\win32\\{libcefConfig.ToLower()}\\{toolset}"
                });
            }
            var libcefLibFileWinX64 = binaryDistribWinX64Dir + Directory(libcefConfig) + File("libcef.lib");
            if (FileExists(libcefLibFileWinX64))
            {
                CopyFile(
                        libcefLibFileWinX64,
                        sdkTargetDir + Directory(toolset) + Directory("x64") + File("libcef.lib")
                );
                nuSpecContentListWinSdk.Add(new NuSpecContent
                {
                        Source = $"{toolset}/x64/libcef.lib",
                        Target = $"CEF\\x64\\{libcefConfig.ToLower()}"
                });
                nuSpecContentListWinSdk.Add(new NuSpecContent
                {
                        Source = $"{toolset}/x64/libcef_dll_wrapper/{libcefConfig}/libcef_dll_wrapper.lib",
                        Target = $"CEF\\x64\\{libcefConfig.ToLower()}\\{toolset}"
                });
            }
            var libcefLibFileWinArm64 = binaryDistribWinArm64Dir + Directory(libcefConfig) + File("libcef.lib");
            if (FileExists(libcefLibFileWinArm64))
            {
                CopyFile(
                        libcefLibFileWinArm64,
                        sdkTargetDir + Directory(toolset) + Directory("arm64") + File("libcef.lib")
                );
                nuSpecContentListWinSdk.Add(new NuSpecContent
                {
                        Source = $"{toolset}/arm64/libcef.lib",
                        Target = $"CEF\\arm64\\{libcefConfig.ToLower()}"
                });
                nuSpecContentListWinSdk.Add(new NuSpecContent
                {
                        Source = $"{toolset}/arm64/libcef_dll_wrapper/{libcefConfig}/libcef_dll_wrapper.lib",
                        Target = $"CEF\\arm64\\{libcefConfig.ToLower()}\\{toolset}"
                });
            }
        }

        NuGetPack(new NuGetPackSettings
        {
                Id = $"{product}.sdk.{toolset}",
                Version = buildVersion,
                Authors = nugetAuthors,
                Description = $"{productDescription} [CommitId: {commitId}]",
                Copyright = copyright,
                ProjectUrl = new Uri(projectUrl),
                Tags = nugetTags,
                RequireLicenseAcceptance= false,
                Files = nuSpecContentListWinSdk.ToArray(),
                Properties = new Dictionary<string, string>
                {
                        {"Configuration", configuration}
                },
                BasePath = sdkTargetDir,
                OutputDirectory = nugetDir
        });
    }
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build-NuGet-Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
