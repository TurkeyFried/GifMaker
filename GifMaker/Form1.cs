using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageMagick;

namespace GifMaker
{
    public partial class Form1 : Form
    {
        private const int thumbnailX = 100;
        private const int thumbnailY = 100;

        private int currentColumn = 0;
        private int currentRow = 0;

        private PictureBox activeControl;
        private Point previousLocation;

        private MagickImageCollection libraryList;
        private MagickImageCollection workList;

        public Form1()
        {
            InitializeComponent();

            activeControl = null;
            libraryList = new MagickImageCollection();
            workList = new MagickImageCollection();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            loadImage();
        }

        private void imageContainer_DragDrop(object sender, DragEventArgs e)
        {
            Console.Write("imageContainer_DragDrop");
        }

        void libraryPicture_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as PictureBox;

            PictureBox newPicture = new PictureBox();
            newPicture.Image = activeControl.Image;
            newPicture.SizeMode = activeControl.SizeMode;
            newPicture.BorderStyle = activeControl.BorderStyle;
            newPicture.Size = activeControl.Size;
            newPicture.Location = activeControl.Location;
            newPicture.BringToFront();
            imageLibrary.Controls.Add(newPicture);

            activeControl.BringToFront();

            // http://stackoverflow.com/a/3870225/1978219
            newPicture.MouseDown += new MouseEventHandler(libraryPicture_MouseDown);
            newPicture.MouseMove += new MouseEventHandler(libraryPicture_MouseMove);
            newPicture.MouseUp += new MouseEventHandler(libraryPicture_MouseUp);

            this.Controls.Add(activeControl);

            previousLocation = e.Location;
            Cursor = Cursors.Hand;
        }

        void libraryPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl.Image == null)
            {
                return;
            }

            var location = activeControl.Location;
            location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            activeControl.Location = location;
        }

        void libraryPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl.Image == null)
            {
                return;
            }
            activeControl.Dispose();
            Cursor = Cursors.Default;
        }

        private void loadImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in fileDialog.FileNames)
                {
                    // http://stackoverflow.com/a/17736176/1978219
                    if (fileName.EndsWith(".gif"))
                    {
                        MagickImageCollection frames = new MagickImageCollection(fileName);

                        foreach (MagickImage frame in frames)
                        {
                            addToLibrary(frame);
                        }

                        continue;
                    }

                    addToLibrary(new MagickImage(fileName));
                }
            }
        }

        private void addToLibrary(MagickImage fileImage)
        {
            if (libraryList.Contains(fileImage))
            {
                return;
            }

            PictureBox newPicture = new PictureBox();
            newPicture.Image = fileImage.ToBitmap();
            newPicture.SizeMode = PictureBoxSizeMode.Zoom;
            newPicture.BorderStyle = BorderStyle.FixedSingle;
            newPicture.Size = new Size(thumbnailX, thumbnailY);

            newPicture.Location = new Point(thumbnailX * currentColumn, thumbnailY * currentRow);

            if (currentColumn >= 2)
            {
                currentColumn = 0;
                currentRow++;
            }
            else
            {
                currentColumn++;
            }

            imageLibrary.Controls.Add(newPicture);

            newPicture.BringToFront();

            // http://stackoverflow.com/a/3870225/1978219
            newPicture.MouseDown += new MouseEventHandler(libraryPicture_MouseDown);
            newPicture.MouseMove += new MouseEventHandler(libraryPicture_MouseMove);
            newPicture.MouseUp += new MouseEventHandler(libraryPicture_MouseUp);

            //fileImage.AnimationDelay = 300;
            libraryList.Add(fileImage);

            //MagickImageCollection previewGif = createGif(libraryList);
            previewBox.Image = libraryList.ToBitmap(ImageFormat.Gif);
        }

        // https://magick.codeplex.com/wikipage?title=Combining%20images&referringTitle=Documentation
        private MagickImageCollection createGif(MagickImageCollection collection)
        {

            //foreach (MagickImage image in collection)
            //{
            //    image.AnimationDelay = 3;
            //}
            //collection[0].AnimationDelay = 300;

            // Optionally optimize the images (images should have the same size).
            //collection.Optimize();

            return collection;
        }

        private void saveImage()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "GIF Images|*.gif";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {


                foreach (MagickImage image in libraryList)
                {
                    image.AnimationDelay = 3;
                }

                libraryList.Write(saveDialog.FileName);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown thing = sender as NumericUpDown;

            foreach (MagickImage image in libraryList)
            {
                image.AnimationDelay = (int)thing.Value;
            }

            previewBox.Image = libraryList.ToBitmap(ImageFormat.Gif);
        }
    }
}
