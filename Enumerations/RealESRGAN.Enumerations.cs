

namespace DataJuggler.RealESRGAN.Enumerations
{

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

    #region enum UpscaleModelEnum
    /// <summary>
    /// This is the choices for models
    /// </summary>
   public enum UpscaleModelEnum : int
    {
        NotSet = 0,
        Standard = 1,
        Anime = 2,
        Fast = 3,
        Remacri = 4,
        UltraSharp = 5,
        UltraMix = 6
    }
    #endregion

}
