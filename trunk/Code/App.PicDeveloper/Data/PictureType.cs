using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.PicDeveloper.Data
{
    public enum PictureType
    {
        Gloss = 1,
        Regular = 2
    }

    public class PictureTypeConverter
    {
        public static PictureType StringToPictureType(string pictureType)
        {
            switch (pictureType)
            {
                case "Gloss":
                    return PictureType.Gloss;
                case "Regular":
                    return PictureType.Regular;
                default:
                    return PictureType.Regular;

                    
            }
        }
    }
}
