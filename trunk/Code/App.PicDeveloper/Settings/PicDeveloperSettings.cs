using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.PicDeveloper.Settings
{
    public class PicDeveloperSettings
    {
        public string PicDirectory { get; set; }
        public PaperSizes DefaultSize { get; set; }
        public PaperTypes DefaultType { get; set; }
        public List<PicturePrice> Prices { get; set; }
        public WindowSettings Window { get; set; }

        public PicDeveloperSettings()
        {
            DefaultSize = PaperSizes._10x15;
            DefaultType = PaperTypes.Regular;
            Prices = new List<PicturePrice>()
            {
                new PicturePrice(){ Price= 1, Type = PaperTypes.Glow, Size = PaperSizes._10x15},
                new PicturePrice(){ Price= 2, Type = PaperTypes.Glow, Size = PaperSizes._13x18},
                new PicturePrice(){ Price= 3, Type = PaperTypes.Glow, Size = PaperSizes._15x21},
                new PicturePrice(){ Price= 4, Type = PaperTypes.Glow, Size = PaperSizes._20x25},
                new PicturePrice(){ Price= 5, Type = PaperTypes.Glow, Size = PaperSizes._20x30},
                new PicturePrice(){ Price= 6, Type = PaperTypes.Glow, Size = PaperSizes._30x45},
                new PicturePrice(){ Price= 1, Type = PaperTypes.Regular, Size = PaperSizes._10x15},
                new PicturePrice(){ Price= 2, Type = PaperTypes.Regular, Size = PaperSizes._13x18},
                new PicturePrice(){ Price= 3, Type = PaperTypes.Regular, Size = PaperSizes._15x21},
                new PicturePrice(){ Price= 4, Type = PaperTypes.Regular, Size = PaperSizes._20x25},
                new PicturePrice(){ Price= 5, Type = PaperTypes.Regular, Size = PaperSizes._20x30},
                new PicturePrice(){ Price= 6, Type = PaperTypes.Regular, Size = PaperSizes._30x45}
            };
            Window = new WindowSettings();
        }
    }

    public enum PaperSizes
    {
        _10x15,
        _13x18,
        _15x21,
        _20x25,
        _20x30,
        _30x45
    }
    public enum PaperTypes
    {
        Regular, Glow
    }
    public class PicturePrice
    {
        public PaperSizes Size { get; set; }
        public PaperTypes Type { get; set; }
        public float Price { get; set; }

    }
    public class WindowSettings
    {
        public string CustomMessage { get; set; }
        public string WindowName { get; set; }

        public string BottonLeftImage { get; set; }
        public string TopLeftImage { get; set; }

        public string ButtonIconCamera { get; set; }
        public string ButtonIconCD { get; set; }
        public string ButtonIconFolder { get; set; }
        public string ButtonIconPenDrive { get; set; }


        public WindowSettings()
        {
            CustomMessage = "CustomMessage";
            WindowName = "PicDeveloper";

            BottonLeftImage = string.Empty;
            TopLeftImage = string.Empty;

            ButtonIconCamera = string.Empty;
            ButtonIconCD = string.Empty;
            ButtonIconFolder = string.Empty;
            ButtonIconPenDrive = string.Empty;
        }
    }
    
}
