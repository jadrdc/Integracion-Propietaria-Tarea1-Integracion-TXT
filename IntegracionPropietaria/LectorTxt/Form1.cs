using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LectorTxt.Utils;

namespace LectorTxt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form_DragEnter);
            this.DragDrop += new DragEventHandler(Form_DragDrop);
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
            string message;

            message = (FileController.ReadFile(file) == true) ? "Se han agregado los registros a la BD" : "No se han agregado nada a BD debido a un fallo";

            MessageBox.Show(message);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
