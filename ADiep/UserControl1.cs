using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADiep
{
    public partial class UserControl1 : UserControl
    {
        const string connect_WHC = @"Data Source = DESKTOP-OI9PL9F\ADMIN;Initial Catalog = Test;User Id = sa;Password = 1;Connect Timeout=3";
        //const string connect_MES = @"Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
        public string displayText { get { return displayText; } set => groupControl1.Text = value; }

        public List<GridControl> gridList { get; set; }
        public Dictionary<string, Image> dicPic = new Dictionary<string, Image>();
        public UserControl1()
        {
            InitializeComponent();
        }
        public UserControl1(List<GridControl> _gridList, Dictionary<string, Image> _dicPic)
        {
            InitializeComponent();
            this.gridList = _gridList;
            this.dicPic = _dicPic;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Dựa vào tên của groupbox và số lượng PAD và số chamfer và check SR để hiển thị grid
            var nameGroup = groupControl1.Text.Trim();
            var pad = txtPAD.Value;
            var chamfer = txtChamfer.Text.Trim();
            var sr = ckSr.Checked;

            if(pad > 0)
            {
                if (nameGroup.Equals("0402 Normal"))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("");
                    dt.Columns.Add("PAD");
                    dt.Columns.Add("MASK");
                    dt.Columns.Add("RATE(%)");
                    dt.Rows.Add("X(um)","", "140","");
                    dt.Rows.Add("Y(um)", "", "180", "");
                    dt.Rows.Add("AREA(um2)", "", "25200", "");
                    gridControl1.DataSource = dt;
                    this.gridList.Add(gridControl1);
                }else if (nameGroup.Equals("0402 HQ"))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("");
                    dt.Columns.Add("PAD");
                    dt.Columns.Add("MASK");
                    dt.Columns.Add("RATE(%)");
                    dt.Rows.Add("X(um)", "", 140, "");
                    dt.Rows.Add("Y(um)", "", 140, "");
                    dt.Rows.Add("AREA(um2)", "", 19600, "");
                    gridControl1.DataSource = dt;
                    this.gridList.Add(gridControl1);
                }
                else if(nameGroup.Equals("SPW"))
                {
                    if (sr)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(chamfer);
                        dt.Columns.Add("PAD");
                        dt.Columns.Add("MASK");
                        dt.Columns.Add("RATE(%)");
                        dt.Rows.Add("X(um)", "", 130, "");
                        dt.Rows.Add("Y(um)", "", 130, "");
                        dt.Rows.Add("AREA(um2)", "", 13266.5, "");
                        dt.Rows.Add("", "PAD", "Mask", "RATE(%)");
                        dt.Rows.Add("X(um)", "", 120, "");
                        dt.Rows.Add("Y(um)", "", 120, "");
                        dt.Rows.Add("AREA(um2)", "", 11304, "");
                        dt.Rows.Add("", "PAD", "Mask", "RATE(%)");
                        dt.Rows.Add("X(um)", "", 130, "");
                        dt.Rows.Add("Y(um)", "", 130, "");
                        dt.Rows.Add("AREA(um2)", "", 13266.5, "");
                        gridControl1.DataSource = dt;
                        this.gridList.Add(gridControl1);
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("1");
                        dt.Columns.Add("PAD");
                        dt.Columns.Add("MASK");
                        dt.Columns.Add("RATE(%)");
                        dt.Rows.Add("X(um)", "", 130, "");
                        dt.Rows.Add("Y(um)", "", 130, "");
                        dt.Rows.Add("AREA(um2)", "", 13266.5, "");
                        dt.Rows.Add("Other", "PAD", "Mask", "RATE(%)");
                        dt.Rows.Add("X(um)", "", 130, "");
                        dt.Rows.Add("Y(um)", "", 130, "");
                        dt.Rows.Add("AREA(um2)", "", 13266.5, "");
                        gridControl1.DataSource = dt;
                        this.gridList.Add(gridControl1);
                    }
                }
                else
                {
                    if (sr)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(chamfer);
                        dt.Columns.Add("PAD");
                        dt.Columns.Add("MASK");
                        dt.Columns.Add("RATE(%)");
                        dt.Rows.Add("X(um)", "", 150, "");
                        dt.Rows.Add("Y(um)", "", 200, "");
                        dt.Rows.Add("AREA(um2)", "", 30000, "");
                        dt.Rows.Add("", "PAD", "Mask", "RATE(%)");
                        dt.Rows.Add("X(um)", "", 120, "");
                        dt.Rows.Add("Y(um)", "", 120, "");
                        dt.Rows.Add("AREA(um2)", "", 11304, "");
                        dt.Rows.Add("", "PAD", "Mask", "RATE(%)");
                        dt.Rows.Add("X(um)", "", 150, "");
                        dt.Rows.Add("Y(um)", "", 200, "");
                        dt.Rows.Add("AREA(um2)", "", 30000, "");
                        gridControl1.DataSource = dt;
                        this.gridList.Add(gridControl1);
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("1");
                        dt.Columns.Add("PAD");
                        dt.Columns.Add("MASK");
                        dt.Columns.Add("RATE(%)");
                        dt.Rows.Add("X(um)", "", 150, "");
                        dt.Rows.Add("Y(um)", "", 200, "");
                        dt.Rows.Add("AREA(um2)", "", 30000, "");
                        dt.Rows.Add("Other", "PAD", "Mask", "RATE(%)");
                        dt.Rows.Add("X(um)", "", 150, "");
                        dt.Rows.Add("Y(um)", "", 200, "");
                        dt.Rows.Add("AREA(um2)", "", 30000, "");
                        gridControl1.DataSource = dt;
                        this.gridList.Add(gridControl1);
                    }
                }
            }
        }
        private static DataTable ExcuteQuery(string query, string connect)
        {
            DataTable vdt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, cnn);
                sda.Fill(vdt);
                cnn.Close();
                cnn.Dispose();
                return vdt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Image files |*.jpg;*.jpeg;*.png";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            //openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        var path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        var name = System.IO.Path.GetFileName(openFileDialog1.FileName);
                        pic1.Image = new Bitmap(openFileDialog1.FileName);
                        pic1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                        if (pic1.Image != null)
                        {
                            this.dicPic.Add(groupControl1.Text.Trim(), pic1.Image);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string ImageToBase64(Image _imagePath)
        {
            string _base64String = null;

            using (System.Drawing.Image _image = _imagePath)
            {
                using (MemoryStream _mStream = new MemoryStream())
                {
                    _image.Save(_mStream, ImageFormat.Jpeg);
                    byte[] _imageBytes = _mStream.ToArray();
                    _base64String = Convert.ToBase64String(_imageBytes);

                    //return "data:image/jpg;base64," + _base64String;
                    return _base64String;
                }
            }
        }
        public Image stringToImage(string inputString)
        {
            byte[] imageBytes = Convert.FromBase64String(inputString);
            MemoryStream ms = new MemoryStream(imageBytes);

            Image image = Image.FromStream(ms, true, true);

            return image;
        }
    }
}
