namespace BuyIt.Infrastructure.Services.FileSystem;

public sealed class RelativePathConstructor
{
    private string GetBaseDirectory()
    {
        var currentDomainDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        var splitIndex = currentDomainDirectory.LastIndexOf("BuyIt.Domain\\") + "BuyIt.Domain\\".Length;
        
        return currentDomainDirectory[..splitIndex].Replace('\\', '/');
    }
    
    public string CreateFilePath(string solutionRootPath)
    {
        return GetBaseDirectory() + solutionRootPath;
    }
}