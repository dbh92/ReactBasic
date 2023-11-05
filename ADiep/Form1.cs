using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADiep
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<GridControl> grControls = new List<GridControl>();
        public Dictionary<string, Image> _dicPic = new Dictionary<string, Image>();

        private void btn1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 2;

            TableLayoutColumnStyleCollection styles =
     tableLayoutPanel1.ColumnStyles;

            foreach (ColumnStyle style in styles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 50;
            }
            TableLayoutRowStyleCollection styleRows =
       tableLayoutPanel1.RowStyles;

            foreach (RowStyle style in styleRows)
            {
                style.SizeType = SizeType.AutoSize;
                style.Height = 50;

            }
            // thêm tên của từng SAW theo số lượng SAW đã chọn dùng for khi có các số lượng của từng phần nhập SAW
            // Tính được số lượng của từng saw ở phần text và điều kiện check (skip hay ko?)
            // Dùng for từ 0 -> số lượng tìm đc để add tên của saw vào => tìm được list tên saw
            // khi tìm được list tên mình sẽ dùng để đặt tên cho displayText (caption của từng groupBox)

            //Khai báo count để chạy hết list tên int l = 0;
            List<string> nameList = new List<string>() { "0402 Normal", "0402 HQ", "SPW", "SCSP", "SPW" };
            int l = 0;
            for (int j = 0; j < tableLayoutPanel1.RowCount; j++)
            {
                for (int k = 0; k < tableLayoutPanel1.ColumnCount; k++)
                {
                    UserControl1 us1 = new UserControl1(grControls, _dicPic);
                    us1.Dock = DockStyle.Fill;
                   
                    us1.displayText = nameList[l]; // nameList[l].toString();
                 
                   
                    tableLayoutPanel1.Controls.Add(us1, j, k);
                    l++;
                }
            }
        }

        Dictionary<string, string> dic = new Dictionary<string, string> { { "name", "huan" }, { "class","Toan"},{ "age","31"} };
        List<string> nameList = new List<string>() { "0402 Normal", "0402 HQ", "SPW", "SCSP", "SPW" };
        private void btnSave_Click(object sender, EventArgs e)
        {
            POP_SHOW pop = new POP_SHOW(grControls, dic, _dicPic, nameList);
            pop.ShowDialog();
        }
    }
}
