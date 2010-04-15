using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.PicDeveloper.Data
{
    public enum PictureSize
    {
        _10x15 = 1,
        _13x18 = 2,
        _15x21 = 4,
        _20x25 = 8,
        _20x30 = 16,
        _30x45 = 32
    }

    public class PictureSizeConverter
    {
        public static PictureSize StringToPictureType(string pictureSize)
        {
            switch (pictureSize)
            {
                case "10x15":
                    return PictureSize._10x15;
                case "13x18":
                    return PictureSize._13x18;
                case "15x21":
                    return PictureSize._15x21;
                case "20x25":
                    return PictureSize._20x25;
                case "20x30":
                    return PictureSize._20x30;
                case "30x45":
                    return PictureSize._30x45;
                default:
                    return PictureSize._10x15;


            }
        }
    }

}
