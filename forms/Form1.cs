using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiestroPrintFeature
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CalculateTotalPrice()
        {
            double total = 0;
            for (int i = 0; i < this.gridProducts.Rows.Count; i++)
            {
                total += double.Parse(this.gridProducts.Rows[i].Cells[1].Value.ToString()) * int.Parse(this.gridProducts.Rows[i].Cells[2].Value.ToString());
            }

            this.lblTotalPrice.Text = string.Format("Total Price: {0:n}", total);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 46)
            {
                if ((sender as Guna.UI2.WinForms.Guna2TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 46)
            {
                if ((sender as Guna.UI2.WinForms.Guna2TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int n = this.gridProducts.Rows.Add();
            this.gridProducts.Rows[n].Cells[0].Value = this.txtProductName.Text;

            double price = double.Parse(this.txtPrice.Text);
            this.gridProducts.Rows[n].Cells[1].Value = string.Format("{0:n}", price);

            this.gridProducts.Rows[n].Cells[2].Value = this.txtQuantity.Text;

            double totalPriceEachItem = double.Parse(this.txtPrice.Text) * int.Parse(this.txtQuantity.Text);
            this.gridProducts.Rows[n].Cells[3].Value = string.Format("{0:n}", totalPriceEachItem);

            this.gridProducts.ClearSelection();
            CalculateTotalPrice();

            this.txtProductName.ResetText();
            this.txtPrice.ResetText();
            this.txtQuantity.ResetText();

            this.txtProductName.Focus();
        }

        private void gridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.gridProducts.Rows[e.RowIndex];

            if (row.Cells[4].Selected)
            {
                this.gridProducts.Rows.Remove(row);
            }

            this.gridProducts.ClearSelection();
            CalculateTotalPrice();

            this.txtProductName.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn clmBtnRemove = new DataGridViewButtonColumn();
            clmBtnRemove.HeaderText = "Action";
            clmBtnRemove.Name = "clmBtnRemove";
            clmBtnRemove.Text = "Remove";
            clmBtnRemove.UseColumnTextForButtonValue = true;
            this.gridProducts.Columns.Add(clmBtnRemove);
        }
    }
}
