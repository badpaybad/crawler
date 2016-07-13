using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using badpaybad.Scraper.DTO;

namespace ProjectTest
{
    public partial class frmMapInfo : Form
    {
        public event Action<Maping> MapInfo;

        public frmMapInfo()
        {
            InitializeComponent();
        }

        private void frmMapInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var maping = new Maping();
            maping.ElementByClassName = txtByClass.Text;
            maping.ElementById = txtById.Text;
            maping.ElementByIndex = (int)numByIndex.Value;
            maping.ElementByTagName = txtByTag.Text;
            maping.FieldInDb = txtFieldDb.Text;

            MapInfo(maping);
            DialogResult = DialogResult.OK;

        }
    }
}
