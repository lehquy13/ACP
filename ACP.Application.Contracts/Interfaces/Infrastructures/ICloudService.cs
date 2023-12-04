namespace ACP.Application.Contracts.Interfaces;

public interface ICloudService
{
    string GetImage(string fileName);
    string UploadImage(string fileName, Stream stream);
}