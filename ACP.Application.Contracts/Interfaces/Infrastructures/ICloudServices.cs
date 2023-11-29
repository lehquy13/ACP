namespace ACP.Application.Contracts.Interfaces;

public interface ICloudServices
{
    string GetImage(string fileName);
    string UploadImage(string fileName, Stream stream);
}