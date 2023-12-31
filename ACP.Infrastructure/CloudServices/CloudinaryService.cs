﻿using ACP.Application.Contracts.Interfaces;
using ACP.Domain.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace ACP.Infrastructure.CloudServices;

public class CloudinaryService(IOptions<CloudinarySetting> cloudinarySetting, IAppLogger<CloudinaryService> logger)
    : ICloudService
{
    private Cloudinary Cloudinary { get; set; } = new(
        new Account(
            cloudinarySetting.Value.CloudName,
            cloudinarySetting.Value.ApiKey,
            cloudinarySetting.Value.ApiSecret
        ))
    {
        Api =
        {
            Secure = true
        }
    };
    
    

    public string GetImage(string fileName)
    {
        try
        {
            var getResourceParams = new GetResourceParams("fileName")
            {
                QualityAnalysis = true
            };
            var getResourceResult = Cloudinary.GetResource(getResourceParams);
            var resultJson = getResourceResult.JsonObj;

            // Log quality analysis score to the console
            logger.LogInformation("{Message}", resultJson["quality_analysis"] ?? "No quality analysis");

            return resultJson["url"]?.ToString() ??
                   "https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";
        }
        catch (Exception ex)
        {
            logger.LogError("{ExMessage}", ex.Message);
            return @"https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";
        }
    }

   
    /// <summary>
    /// Recommended to use this method
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    public string UploadImage(string fileName, Stream stream)
    {
        try
        {
            var imageUploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };
            var result = Cloudinary.Upload(imageUploadParams);

            return result.Url.ToString();
        }
        catch (Exception ex)
        {
            logger.LogError("{ExMessage}", ex.Message);
            return string.Empty;
        }
    }
}