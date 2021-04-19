using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDDATABARANG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dataGridView.DataSource = tblbarangBindingSource;
                }
                else
                {
                    var query = from o in this.appData.tbl_barang
                                where o.NamaBarang.Contains(txtSearch.Text) || o.HargaBarang == txtSearch.Text || o.JumlahBarang == txtSearch.Text || o.Satuan.Contains(txtSearch.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    tblbarangBindingSource.RemoveCurrent();
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNama.Focus();
                this.appData.tbl_barang.Addtbl_barangRow(this.appData.tbl_barang.Newtbl_barangRow());
                tblbarangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tblbarangBindingSource.ResetBindings(false);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtNama.Focus();
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda akan menghapus datanya?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                tblbarangBindingSource.RemoveCurrent();
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                tblbarangBindingSource.EndEdit();
                tbl_barangTableAdapter.Update(this.appData.tbl_barang);
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tblbarangBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.tbl_barang' table. You can move, or remove it, as needed.
            this.tbl_barangTableAdapter.Fill(this.appData.tbl_barang);
            tblbarangBindingSource.DataSource = this.appData.tbl_barang;
        }
    }
}
