﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.PicDeveloper.Data;

namespace TK1.PicDeveloper.Data
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
            PicDirectory = AppFolder.GetAppFolder(Constraints.AppName, "Pictures");
            Prices = new List<PicturePrice>()
            {
                new PicturePrice(){ Price= 0.7f, Type = PaperTypes.Gloss, Size = PaperSizes._10x15},
                new PicturePrice(){ Price= 0.8f, Type = PaperTypes.Gloss, Size = PaperSizes._13x18},
                new PicturePrice(){ Price= 0.9f, Type = PaperTypes.Gloss, Size = PaperSizes._15x21},
                new PicturePrice(){ Price= 1.2f, Type = PaperTypes.Gloss, Size = PaperSizes._20x25},
                new PicturePrice(){ Price= 1.5f, Type = PaperTypes.Gloss, Size = PaperSizes._20x30},
                new PicturePrice(){ Price= 2.8f, Type = PaperTypes.Gloss, Size = PaperSizes._30x45},
                new PicturePrice(){ Price= 0.7f, Type = PaperTypes.Regular, Size = PaperSizes._10x15},
                new PicturePrice(){ Price= 0.8f, Type = PaperTypes.Regular, Size = PaperSizes._13x18},
                new PicturePrice(){ Price= 0.9f, Type = PaperTypes.Regular, Size = PaperSizes._15x21},
                new PicturePrice(){ Price= 1.2f, Type = PaperTypes.Regular, Size = PaperSizes._20x25},
                new PicturePrice(){ Price= 1.5f, Type = PaperTypes.Regular, Size = PaperSizes._20x30},
                new PicturePrice(){ Price= 2.8f, Type = PaperTypes.Regular, Size = PaperSizes._30x45}
            };
            Window = new WindowSettings();
        }
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
