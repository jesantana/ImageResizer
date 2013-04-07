using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class Resizer1
{
    public Resizer1() 
    { 
        
    }

    public void ResizeAll(string[] conjuntoArchivos, int sizeFin,ProgressBar pb) 
    {
        pb.Value = 0;
        for (int i = 0; i < conjuntoArchivos.Length; i++) 
        {
            this.ResizeImageFile(conjuntoArchivos[i],sizeFin);
            pb.Value = (i + 1) * 100 / conjuntoArchivos.Length;
        }
    }

    public void ResizeImageFile(string imageFile, int targetSize)
    {
        Image original = Image.FromFile(imageFile);
        int targetH, targetW;
        if (original.Height > original.Width)
        {
            targetH = targetSize;
            targetW = (int)(original.Width * ((float)targetSize / (float)original.Height));
        }
        else
        {
            targetW = targetSize;
            targetH = (int)(original.Height * ((float)targetSize / (float)original.Width));
        }
        Image imgPhoto = Image.FromFile(imageFile);
        // Create a new blank canvas.  The resized image will be drawn on this canvas.
        Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(72, 72);
        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
        grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
        // Save out to memory and then to a file.  We dispose of all objects to make sure the files don't stay locked.
        //
        MemoryStream mm = new MemoryStream();

        bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
        original.Dispose();
        imgPhoto.Dispose();
        bmPhoto.Dispose();
        grPhoto.Dispose();

        File.Delete(imageFile);
        FileStream fs = new FileStream(imageFile, FileMode.OpenOrCreate);
        fs.Write(mm.GetBuffer(), 0, mm.GetBuffer().Length);
        mm.Close();
        fs.Close();
        //return mm.GetBuffer();
    }
}
