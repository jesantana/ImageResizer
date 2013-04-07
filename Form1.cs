using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class Form1 : Form
    {
        Resizer1 changePhot;
        string[] imageSet;
            

        public Form1()
        {
            InitializeComponent();
            changePhot = new Resizer1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (imageSet != null && textBox1.Text != "")
            {
                button1.Enabled = false;
                label3.Text = "Cambiando tamaño a las imágenes .....";
                this.Refresh();
                changePhot.ResizeAll(imageSet, int.Parse(textBox1.Text), progressBar1);
                imageSet = null;
                listBox1.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                MessageBox.Show("El tamaño de las fotos fue cambiado con éxito");
                progressBar1.Value = 0;
                label3.Text = "";
                button1.Enabled = true;
            }
            else 
            {
                MessageBox.Show("Debes especificar las fotos y el nuevo tamaño para ella");
            }
        }

    private void cargarCarpetaDeImágenesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        fbd1.ShowDialog();
        string path=fbd1.SelectedPath;
        DirectoryInfo dinfo = new DirectoryInfo(path);
        FileInfo[] finfo = dinfo.GetFiles();
        List<string> fotos = new List<string>();
        listBox1.Items.Clear();
        for (int i = 0; i < finfo.Length; i++) 
        {
            if (finfo[i].Extension.ToLower() == ".jpg") 
            {
                fotos.Add(finfo[i].FullName);
                listBox1.Items.Add(finfo[i].FullName);
            }
        }
        imageSet = fotos.ToArray();
    }


    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        string cad = textBox1.Text;
        for (int i = 0; i < cad.Length; i++)
        {
            if (!char.IsNumber(cad[i]))
            {
                textBox1.Text = cad.Remove(i, 1);
                break;
            }
        }

        if (textBox1.Text != "")
        {
            int dim = int.Parse(textBox1.Text);
            dim = (int)(dim * 6 / 8);
            textBox2.Text = dim.ToString();
        }
        else 
        {
            textBox2.Text ="";
        }
    }

    private void cargarConjuntoDeImágenesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (ofd.ShowDialog() == DialogResult.OK) 
        {
            FileInfo fi = null;
            for (int i = 0;i< ofd.FileNames.Length; i++) 
            {
                fi =new FileInfo(ofd.FileNames[i]);
                if (fi.Extension.ToLower() != ".jpg") 
                {
                    MessageBox.Show("Debes seleccionar solo imágenes. Imposible cargar "+ofd.FileNames[i]);
                    listBox1.Items.Clear();
                    return;
                }
                listBox1.Items.Add(ofd.FileNames[i]);
            }
            imageSet = ofd.FileNames;
        }
        
    }

    private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    //    public static byte[] ResizeImageFile(byte[] imageFile, int targetSize)
    //    {
    //        Image original = Image.FromStream(new MemoryStream(imageFile));
    //        int targetH, targetW;
    //        if (original.Height > original.Width)
    //        {
    //            targetH = targetSize;
    //            targetW = (int)(original.Width * ((float)targetSize / (float)original.Height));
    //        }
    //        else
    //        {
    //            targetW = targetSize;
    //            targetH = (int)(original.Height * ((float)targetSize / (float)original.Width));
    //        }
    //        Image imgPhoto = Image.FromStream(new MemoryStream(imageFile));
    //        // Create a new blank canvas.  The resized image will be drawn on this canvas.
    //        Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
    //        bmPhoto.SetResolution(72, 72);
    //        Graphics grPhoto = Graphics.FromImage(bmPhoto);
    //        grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
    //        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
    //        grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
    //        grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
    //        // Save out to memory and then to a file.  We dispose of all objects to make sure the files don't stay locked.
    //        MemoryStream mm = new MemoryStream();
    //        bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
    //        original.Dispose();
    //        imgPhoto.Dispose();
    //        bmPhoto.Dispose();
    //        grPhoto.Dispose();
    //        return mm.GetBuffer();
    //}




}
