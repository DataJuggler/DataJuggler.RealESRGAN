# DataJuggler.RealESRGAN

This project supports Leonard — a video upscaler that:

1. Converts MP4 to an image sequence using FFmpeg  
2. Upscales the images with Real-ESRGAN Vulkan (this project)  
3. Reassembles the images into an MP4 using FFmpeg

This NuGet package provides a static helper class called RealESRGANHelper to upscale 
images using Real-ESRGAN Vulkan.

Project Leonard - A Video Upscaler will be released by Thursday, July 31, 2025.
https://github.com/DataJuggler/Leonard

---

DataJuggler.RealESRGAN/  
├── RealESRGANHelper.cs  
├── Enumerations/  
│   └── UpscaleModelEnum.cs  
├── RealESRGAN/  
│   ├── realesrgan.exe  
│   └── models/  
│       ├── realesrgan-x4plus  
│       ├── realesrgan-x4plus-anime  
│       ├── realesrgan-x4fast  
│       ├── remacri  
│       ├── ultrasharp  
│       └── ultramix_balanced  
├── Dependencies/  
│   └── DataJuggler.PixelDatabase  

Note: The RealESRGAN folder is automatically included and copied to the output folder 
when the project builds.

## This Process Is Slow!

Upscaling can be extremely slow when processing large batches of images. For example, 
a 2-minute video at 30 frames per second will produce 3,600 images. Depending on your 
system, GPU, and source image resolution, each image can take anywhere from 
**1 to 15 seconds**  or more to upscale.

To speed up the workflow, it’s recommended to split longer videos into smaller chunks.  
The `DataJuggler.FFmpeg` package includes a class called `FFmpegHelper` which provides
a `SplitVideo` method, or you can use your preferred video editing software to manually 
divide the video into shorter segments.

# Warning: If you split your video into parts, make sure to use the same upscale model for best results

---

## StatusUpdate Delegate (from DataJuggler.FFmpeg)

This project supports progress notifications via the StatusUpdate delegate.  
Note: This delegate is not defined in this project — it comes from the DataJuggler.FFmpeg package, 
which is installed in this project as a required dependency for this project.

    public delegate void StatusUpdate(string sender, string data);

To use it, define a method like this in your project:

    public void Callback(string senderName, string data)
    {
        // sample log
        string logFile = @"c:\Temp\RealESRGANLog.txt";
        File.AppendAllText(logFile, data);
    }

Pass this method as a delegate to any RealESRGAN or FFmpeg methods that supports callbacks.  
If you don’t need notifications, pass null.

---

## Usage

### RealESRGANHelper

This class provides two public methods.

---

### UpscaleImage

Upscales a single image using a specified model.
    
    string input = @"C:\Images\Frame001.png";
    string output = @"C:\Upscaled\Frame001.png";

    // select your model
    UpscaleModelEnum model = UpscaleModelEnum.UltraSharp;

    // perform the upscale
    bool result = RealESRGANHelper.UpscaleImage(input, output, model);

    // Perform the Upscale with width & height
    int width = 1920;
    int height = 1080;

    // perform the upscale
    bool result = RealESRGANHelper.UpscaleImage(input, output, mode, width, height);

# Note: How Resizing To A Specific Size Works

At the time of this writing (July 2025) The Vulkan upscaler models used in this project
will upscale the image by four times. Meaning an 800 x 600 image will becomes 3200 x 2400.

To convert to a specific size, my NuGet package DataJuggler.PixelDatabase will resize the image
to the specific height and width. Under the hood DataJuggler.PixelDatabase using System.Drawing.
This can and will stretch your image.

## A Note About Scaling
RealESRGAN upscaler has an option for Scale, but after trying found an issue on GitHub that is known. The upscaled
images had problems with Scale and didn't work, so I took the scale out. If this issue is ever fixed I will add
the scale option back.

### GetModelPath

Returns the actual model name string for the given enum.

    string modelName = RealESRGANHelper.GetModelPath(UpscaleModelEnum.UltraMix);
    // returns "ultramix_balanced"

---

## Enum to Model Name Mapping

When calling UpscaleImage, the enum values map to Real-ESRGAN model names as shown below:

UpscaleModelEnum → Model Name Mapping  
--------------------------------------
Standard                   realesrgan-x4plus  
Anime                       realesrgan-x4plus-anime  
Fast                          realesrgan-x4fast  
Remacri                    remacri  
UltraSharp                ultrasharp  
UltraMix                    ultramix_balanced  

---

# Model Guide

RealESRGAN includes several model types, each optimized for different kinds of images. Use the guide 
below to choose the best model for your needs.

    Standard (realesrgan-x4plus)

        The original and most balanced model. Best for general photos, videos, and realistic content.
    
    Anime (realesrgan-x4plus-anime)

        Designed for upscaling anime-style artwork, line art, and 2D illustrations with flat colors and 
        hard edges.
    
    Fast (realesrgan-x4fast)

        A lighter model for faster performance. Lower quality than Standard, but good for quick previews
        or processing large batches with lower resource requirements.
    
    Remacri (remacri)

        Produces sharp, contrasty output with less blurring. Great for enhancing detail in already sharp 
        images or text-heavy graphics.
    
    UltraSharp (ultrasharp)

        Prioritizes edge definition and texture clarity. Useful for upscaling small, blurry images where 
        detail matters most.
    
    UltraMix (ultramix_balanced)

        A hybrid model that balances sharpness, smoothness, and noise reduction. Good for real-world 
        photography and complex scenes with mixed content.

---

## Change Log

8.2.2025: Version 1.0.5

Scale Option didn't work, and resulted in broken images, so scale is gone for now

7.30.2025: Version 1.0.1
I added a Scale option. You must now pass in the ScaleEnum parameter for scale

    // namespace
    using DataJuggler.RealESRGAN.Enumerations;

    #region enum ScaleEnum
    /// <summary>
    /// This is the option for scaling. I usually use 2x because it's faster
    /// and 2x is closer to my usual target size of 1920 x 1080. 
    /// </summary>
    public enum ScaleEnum : int
    {
        Two_X = 2,
        Three_X = 3,
        Four_X = 4
    }
    #endregion

7.29.2025: Version 1.0.0

Initial Release

7.29.2025: Version 1.0.1:

I added two optional parameters to the UpscaleImage method for width & height. 
If you leave this parameters blank the image will be upscaled to 4 x the original size.

## Credits

Real-ESRGAN Vulkan by Xintao  
https://github.com/xinntao/Real-ESRGAN

---

## Questions or Feedback

Create an issue on GitHub:  
https://github.com/DataJuggler/DataJuggler.RealESRGAN

If you find this project worth the price, please leave a star.