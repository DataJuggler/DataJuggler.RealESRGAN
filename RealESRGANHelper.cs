

#region using statements

using DataJuggler.UltimateHelper;
using System.Diagnostics;
using DataJuggler.RealESRGAN.Enumerations;

#endregion

namespace DataJuggler.RealESRGAN
{

    #region class RealESRGANHelper
    /// <summary>
    /// This class is used to upscale Images
    /// </summary>
    public static class RealESRGANHelper
    {
    
        #region Methods
            
            #region GetModelPath(UpscaleModelEnum model)
            /// <summary>
            /// returns the Model Path for the UpscaleModelEnum option
            /// </summary>
            public static string GetModelPath(UpscaleModelEnum model)
            {
                // initial value
                string modelPath = "";

                switch (model)
                {
                    case UpscaleModelEnum.Standard:

                        // Set the return value
                        modelPath = "realesrgan-x4plus";

                        // required
                        break;

                    case UpscaleModelEnum.Anime:

                        // Set the return value
                        modelPath = "realesrgan-x4plus-anime";

                        // required
                        break;

                    case UpscaleModelEnum.Fast:

                        // Set the return value
                        modelPath = "realesrgan-x4fast";

                        // required
                        break;

                    case UpscaleModelEnum.Remacri:

                        // Set the return value
                        modelPath = "remacri";

                        // required
                        break;

                    case UpscaleModelEnum.UltraSharp:

                        // Set the return value
                        modelPath = "ultrasharp";

                        // required
                        break;

                    case UpscaleModelEnum.UltraMix:

                        // Set the return value
                        modelPath = "ultramix_balanced";

                        // required
                        break;
                }

                // return value
                return modelPath;
            }
            #endregion

            #region GetRealESRGANPath
            /// <summary>
            /// Returns the full path to realesrgan-ncnn-vulkan.exe installed by the NuGet package.
            /// </summary>
            public static string GetRealESRGANPath()
            {
                // return the path to the RealESRGAN executable
                return Path.Combine(AppContext.BaseDirectory, "RealESRGAN", "realesrgan-ncnn-vulkan.exe");
            }
            #endregion
            
            #region UpscaleImage(string inputPath, string outputPath, UpscaleModelEnum model)
            /// <summary>
            /// Upscales an image using RealESRGAN and writes output to specified location.
            /// </summary>
            public static bool UpscaleImage(string inputPath, string outputPath, UpscaleModelEnum model)
            {
                // initial value
                bool success = false;

                try
                {
                    // get the path to the executable
                    string executablePath = GetRealESRGANPath();

                    // get the path for the model
                    string modelPath = GetModelPath(model);

                    // Ensure the executable and input file exist
                    if (FileHelper.Exists(executablePath) && FileHelper.Exists(inputPath))
                    {
                        using (Process process = new Process())
                        {
                            process.StartInfo.FileName = executablePath;
                            process.StartInfo.Arguments = $"-i \"{inputPath}\" -o \"{outputPath}\" -n {modelPath}";
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.RedirectStandardError = true;

                            process.Start();

                            // Optional: capture output if you ever want to log
                            string output = process.StandardOutput.ReadToEnd();
                            string error = process.StandardError.ReadToEnd();

                            process.WaitForExit();

                            // Did this work?
                            success = FileHelper.Exists(outputPath);
                        }
                    }
                }
                catch (Exception error)
                {
                    // For debugging only for now
                    DebugHelper.WriteDebugError("UpscaleImage", "RealESRGANHelper", error);
                }

                // return value
                return success;
            }
            #endregion

        #endregion
        
    }
    #endregion

}