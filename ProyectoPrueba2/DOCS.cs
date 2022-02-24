using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrueba2.Model
{
    public partial class DOCS : Form
    {
        public DOCS()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
            if (txtRuta.Text.Trim().Equals("") || txtDocumento.Text.Trim().Equals(""))
            {
                MessageBox.Show("Por favor suba el archivo o Agregue el Nombre al Documento");
                return;
            }
            byte[] file = null;
            Stream myStream = openFileDialog1.OpenFile();
            using (MemoryStream ms = new MemoryStream())
            {
                myStream.CopyTo(ms);
                file = ms.ToArray();
            }
            using (Model.promotorEntities4 db = new Model.promotorEntities4())
            {

                Model.documentos document = new Model.documentos();
                document.nombre = txtDocumento.Text.Trim();
                document.RFCProspecto = txtRFCDoc.Text.Trim();
                document.documento = file;
                document.realname = openFileDialog1.SafeFileName;

                db.documentos.Add(document);
                db.SaveChanges();
                    MessageBox.Show("Documento subido");
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("¿Desea seguir subiendo documentos?", "Documentos", MessageBoxButtons.YesNoCancel);
                    if (Opcion == DialogResult.Yes)
                    {
                        txtDocumento.Clear();
                        txtRuta.Clear();
                        MessageBox.Show("Continue subiendo documentos");
                    }
                    if (Opcion == DialogResult.No)
                    {
                        this.Close();
                        //MessageBox.Show("No se envio el Estatus");
                    }
                    if (Opcion == DialogResult.Cancel)
                    {
                        MessageBox.Show("Se cancelo la operación");
                    }
                    //refresh();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void Refresh()
        //{
        //    using(Model.promotorEntities4 db = new Model.promotorEntities4())
        //    {

        //    }
        //}


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Todos los archivos(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = openFileDialog1.FileName;
            }
            //string ruta;
            //OpenFileDialog seleccion = new OpenFileDialog();
            //seleccion.Filter = "Archivo PDF |*.pdf";
            //seleccion.Title = "Seleccionar Plantilla";
            //seleccion.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //if (seleccion.ShowDialog() == DialogResult.OK)
            //{
            //    ruta = seleccion.FileName;
            //    txtRuta.Text = ruta;
            //}
        }


    }
}
