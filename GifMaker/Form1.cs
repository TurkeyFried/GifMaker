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
using System.Windows.Media.Imaging;
using ImageMagick;

namespace GifMaker
{
    class MovablePicBox : PictureBox
    {
        Point mdLoc;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mdLoc = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mdLoc.X;
                this.Top += e.Y - mdLoc.Y;
            }
        }
    }


    public partial class Form1 : Form
    {
        private const int thumbnailX = 100;
        private const int thumbnailY = 100;

        private int currentColumn = 0;
        private int currentRow = 0;

        private int previewCount = 0;

        private PictureBox activeControl;
        private Point previousLocation;

        private MagickImageCollection libraryList;
        private MagickImageCollection workList;

        public Form1()
        {
            InitializeComponent();

            activeControl = null;
            this.libraryList = new MagickImageCollection();
            this.workList = new MagickImageCollection();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void batchSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Gif Animation (*.gif)|*.gif";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                int numberOfImages = 2;
                int speed = 30;
                MagickImageCollection toSave = new MagickImageCollection();

                string directory = Path.GetDirectoryName(saveDialog.FileName);

                Console.WriteLine("batchSaveButton_Click");
                foreach (MagickImage image in this.libraryList)
                {
                    image.AnimationDelay = speed;
                    toSave.Add(image);

                    if (toSave.Count == numberOfImages)
                    {
                        toSave.Optimize();
                        string ggg = Path.GetFileNameWithoutExtension(toSave[0].FileName);
                        string fff = directory + "\\" + ggg;
                        string hhh = fff.Remove(fff.Length - 1);
                        toSave.Write(hhh + ".gif");
                        toSave.Clear();
                    }
                }

            }
        }

        private void browse_Click(object sender, EventArgs e)
        {
            loadImage();
        }

        void libraryPicture_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as PictureBox;

            PictureBox newPicture = createThumbnail(activeControl.Image);
            this.Controls.Add(newPicture);

            newPicture.Location = activeControl.Location;
            newPicture.BringToFront();

            // http://stackoverflow.com/a/3870225/1978219
            newPicture.MouseDown += new MouseEventHandler(libraryPicture_MouseDown);
            newPicture.MouseUp += new MouseEventHandler(libraryPicture_MouseUp);

            activeControl.BringToFront();
        }

        void libraryPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (activeControl == null)
            {
                return;
            }

            // improve the contains
            if (imageContainer.ClientRectangle.Contains(activeControl.Location))
            {
                imageContainer.Controls.Add(activeControl);
                imageContainer.BringToFront();

                activeControl.Location = new Point(thumbnailX * previewCount + imageContainer.Location.X, thumbnailY * 0);
                previewCount++;

                return;
            }

            activeControl.Dispose();
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
            MovablePicBox newPicture = createThumbnail(fileImage.ToBitmap());
            newPicture.Location = new Point(thumbnailX * currentColumn + imageLibrary.Location.X, thumbnailY * currentRow + imageLibrary.Location.Y);

            if (currentColumn >= 2)
            {
                currentColumn = 0;
                currentRow++;
            }
            else
            {
                currentColumn++;
            }

            this.Controls.Add(newPicture);
            newPicture.BringToFront();

            newPicture.MouseDown += new MouseEventHandler(libraryPicture_MouseDown);
            newPicture.MouseUp += new MouseEventHandler(libraryPicture_MouseUp);

            fileImage.AnimationDelay = 30;
            libraryList.Add(fileImage);

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
                foreach (MagickImage image in this.libraryList)
                {
                    image.AnimationDelay = 30;
                }

                this.libraryList.Write(saveDialog.FileName);
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

        private MovablePicBox createThumbnail(Image image)
        {
            MovablePicBox newPicture = new MovablePicBox();
            newPicture.Image = image;

            newPicture.SizeMode = PictureBoxSizeMode.Zoom;
            newPicture.BorderStyle = BorderStyle.FixedSingle;

            newPicture.Size = new Size(thumbnailX, thumbnailY);
            newPicture.BackColor = Color.White;

            return newPicture;
        }
    }
}
