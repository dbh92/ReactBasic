using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADiep
{
    public partial class POP_SHOW : DevExpress.XtraEditors.XtraForm
    {
        const string connect_WHC = @"Data Source = DESKTOP-OI9PL9F\ADMIN;Initial Catalog = Test;User Id = sa;Password = 1;Connect Timeout=3";
        //const string connect_MES = @"Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
        List<GridControl> listGrid { get; set; }
        Dictionary<string, string> dic { get; set; }
        Dictionary<string, Image> dicPic { get; set; }
        List<string> nameList { get; set; }



        public POP_SHOW()
        {
            InitializeComponent();
        }
        public POP_SHOW(List<GridControl> _listGrid, Dictionary<string, string> _dic, Dictionary<string, Image> _dicPic, List<string> _nameList)
        {
            InitializeComponent();
            this.listGrid = _listGrid;
            this.dic = _dic;
            this.dicPic = _dicPic;
            this.nameList = _nameList;
            showPreview();
        }

        private void showPreview()
        {
            spreadsheetControl1.ActiveViewZoom = 73;
            spreadsheetControl1.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            spreadsheetControl1.Options.HorizontalScrollbar.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetScrollbarVisibility.Hidden;
            spreadsheetControl1.Options.VerticalScrollbar.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetScrollbarVisibility.Hidden;
            spreadsheetControl1.Options.View.ShowColumnHeaders = false;
            spreadsheetControl1.Options.View.ShowRowHeaders = false;
            spreadsheetControl1.Options.TabSelector.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetElementVisibility.Hidden;

            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.DocumentSettings.R1C1ReferenceStyle = true;
            Worksheet worksheet = workbook.Worksheets[0];
           

            workbook.BeginUpdate();
            worksheet.Clear(worksheet.GetUsedRange());
            worksheet.UnMergeCells(worksheet.GetUsedRange());

            //worksheet.ActiveView.ShowGridlines = false;

            try
            {
                worksheet.Cells[1, 10].Value = "Thông số chế tạo MASK";
                
                worksheet.Cells[3, 1].Value = "Ngày cải tiến";
                worksheet.Cells[3, 2].Value = "Frame Size";
                worksheet.Cells[3, 3].Value = "Vị trí gia công";
                worksheet.Cells[3, 4].Value = "Chiều dày SUS";
                worksheet.Cells[3, 5].Value = "Loại sản phẩm";
                worksheet.Cells[3, 6].Value = "Model";
                worksheet.Cells[3, 7].Value = "PCB_RER NO";
                worksheet.Cells[3, 8].Value = "Độ co sản phẩm";
                worksheet.Cells[3, 9].Value = "Màu nhãn";
                worksheet.Cells[6, 1].Value = "1. PCB Design";

                System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\Capture.png");

                foreach (var item in dicPic)
                {
                    if (item.Key.Equals("0402 Normal"))
                    {
                        System.Drawing.Image image = ResizeImage(item.Value, new Size(400, 300));
                        worksheet.Pictures.AddPicture(image, worksheet.Cells[7, 1]);
                    }
                    else if(item.Key.Equals("0402 HQ"))
                    {
                        System.Drawing.Image image = ResizeImage(item.Value, new Size(400, 300));
                        worksheet.Pictures.AddPicture(image, worksheet.Cells[15, 20]);
                    }
                    
                }
                for (int i = 0; i < listGrid.Count(); i++)
                {
                    GridView gv = (GridView)listGrid[i].MainView;

                    worksheet.Cells["E2"].Value = gv.Columns[0].FieldName;
                    worksheet.Cells["F2"].Value = gv.Columns[1].FieldName;
                    worksheet.Cells["G2"].Value = gv.Columns[2].FieldName;
                    for (int j = 0; j < gv.RowCount; j++)
                    {
                        worksheet.Cells["E23"].Value = gv.GetRowCellDisplayText(j, gv.Columns[0].FieldName).ToString();
                        worksheet.Cells["F23"].Value = gv.GetRowCellDisplayText(j, "PAD").ToString();
                        worksheet.Cells["G23"].Value = gv.GetRowCellDisplayText(j, "MASK").ToString();
                    }

                }



            }
            finally
            {
                workbook.EndUpdate();
            }
            worksheet.GetUsedRange().Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.GetUsedRange().Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            workbook.EndUpdate();
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

        private static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            // Get the image current width
            int sourceWidth = imgToResize.Width;
            // Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            // Calculate width and height with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);
            // New Width and Height
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
    }
}