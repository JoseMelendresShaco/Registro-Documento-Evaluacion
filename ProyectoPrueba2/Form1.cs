using ProyectoPrueba2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrueba2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
 
            //SqlConnection conexionBD = Conexion.conexion();

            //SqlCommand com = new SqlCommand("insert into prospecto (Nombre, PrimerApellido, SegundoApellido, Calle, Numero, CodigoPostal, Telefono, RFC, Documento) values ('XXXX','ZZZZZ','AAAAAAAA', 'cale', 99, 81141, '6871235030', 'RFCRFCRFC',@doc)",conexionBD);

            //FileStream stream = new FileStream(@"C:\Users\melen\Desktop\SISTEMAS.pdf",FileMode.Open, FileAccess.Read);

            //byte[] binData = new byte[stream.Length];

            //stream.Read(binData, 0, Convert.ToInt32(stream.Length));

            //com.Parameters.AddWithValue("@doc", binData);
            //conexionBD.Open();
            //com.ExecuteNonQuery();
            //conexionBD.Close();
            //if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(txtRuta.Text))
            //{
            //    MessageBox.Show("Por favor complete los campos vacios");
            //}
            //convertir el archivo a un array de bytes
            /*byte[] file = null;
            Stream myStream = Convert. txtRuta.Text;//openFileDialog1.OpenFile();
            using (MemoryStream ms= new MemoryStream())
            {
                myStream.CopyTo(ms);
                file = ms.ToArray();
                textBox10.Text = Convert.ToString(file);
            }
            
            //contexto para guardar los datos

            //using (Model.prospecto db = new Model.prospecto())
            //{

            //}*/
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("Por favor complete los campos vacios");
            }
            else
            {
                String nombreP = textBox1.Text;
                String primerA = textBox2.Text;
                String segundoA = textBox3.Text;
                String calle = textBox4.Text;
                int numero = Convert.ToInt32(textBox5.Text);
                String colonia = textBox6.Text;
                int codigoPostal = Convert.ToInt32(textBox7.Text);
                String telefono = textBox8.Text;
                String RFC = textBox9.Text;
                //String Documento = txtRuta.Text;
                String estatus = "Enviado";
                //sentencia para agregar a base de datos
                string sql = "INSERT INTO prospecto2 (Nombre, PrimerApellido, SegundApellido, Calle, Numero, CodigoPostal, Telefono, RFC,estatus,observaciones) " +
                    "VALUES ('" + nombreP + "','" + primerA + "','" + segundoA + "', '" + calle + "', " + numero + ", " + codigoPostal + ", '" + telefono + "', '" + RFC + "', '" + estatus + "', '')";
                //Usando la clase de conexion
                SqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();

                try
                {
                    SqlCommand comando = new SqlCommand(sql, conexionBD);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Guardado");
                    cargarProspectosEnviados();
                    DOCS d = new DOCS();
                    d.txtRFCDoc.Text = textBox9.Text;
                    d.ShowDialog();
                    
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                    //txtRuta.Clear();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al guardar" + ex.Message);
                }

                finally
                {
                    conexionBD.Close();
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            cargarProspectos();
            cargarProspectosEnviados();
            cargarProspectosAutorizado();
            cargarProspectosRechazados();
        }
        public void cargarDocumentos()
        {
            SqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            SqlCommand cmd = conexionBD.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id, RFCProspecto, nombre, realname FROM documentos where RFCProspecto= '" + txtRFC.Text + "'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView5.DataSource = dt;

            conexionBD.Close();
        }
        public void cargarProspectos()
        {
            SqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            SqlCommand cmd = conexionBD.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id, Nombre, PrimerApellido, SegundApellido, estatus FROM prospecto2";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            conexionBD.Close();
        }
        public void cargarProspectosEnviados()
        {
            SqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            SqlCommand cmd = conexionBD.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id, Nombre, PrimerApellido, SegundApellido, estatus FROM prospecto2 Where estatus = 'Enviado'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView2.DataSource = dt;

            conexionBD.Close();
        }
        public void cargarProspectosAutorizado()
        {
            SqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            SqlCommand cmd = conexionBD.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id, Nombre, PrimerApellido, SegundApellido, estatus FROM prospecto2 Where estatus = 'Autorizado'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView3.DataSource = dt;

            conexionBD.Close();
        }
        public void cargarProspectosRechazados()
        {
            SqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            SqlCommand cmd = conexionBD.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id, Nombre, PrimerApellido, SegundApellido, estatus FROM prospecto2 Where estatus = 'Rechazado'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView4.DataSource = dt;

            conexionBD.Close();
        }
        public void cargarObservaciones()
        {
            try
            {
                String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                SqlDataReader reader = null;
                string sql = "SELECT observaciones, Nombre, PrimerApellido, SegundApellido, Calle, Numero, CodigoPostal, Telefono, RFC FROM Prospecto2 WHERE id = " + id + ";";
                //abrir la conexion
                SqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();

                try
                {
                    SqlCommand comando = new SqlCommand(sql, conexionBD);
                    reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtObersvaciones.Text = Convert.ToString(reader.GetString(0));
                            txtNombre.Text = Convert.ToString(reader.GetString(1));
                            txtPrimerA.Text = Convert.ToString(reader.GetString(2));
                            txtSegundoA.Text = Convert.ToString(reader.GetString(3));
                            txtCalle.Text = Convert.ToString(reader.GetString(4));
                            txtNumero.Text = Convert.ToString(reader.GetInt32(5));
                            txtCP.Text = Convert.ToString(reader.GetInt32(6));
                            txtTelefono.Text = Convert.ToString(reader.GetString(7));
                            txtRFC.Text = Convert.ToString(reader.GetString(8));
                            //txtDocumento.Text = Convert.ToString(reader.GetString(9));
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al buscar" + ex.Message);
                }
                finally
                {
                    conexionBD.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Ocurrio un error" + ex); };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarObservaciones();
            cargarDocumentos();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Por favor selecciones al Prospecto");
            }
            else { 
            DialogResult Opcion;
            Opcion = MessageBox.Show("Esta seguro de enviar al prospecto: " + textBox10.Text + "Con el Estatus de: " + comboBox1.Text, "Actualizar Grupo", MessageBoxButtons.YesNoCancel);
            if (Opcion == DialogResult.Yes)
            {
                try
                {
                    String ID = txtID.Text;
                    //String nombre = comboNombre.Text;

                    string sql = "UPDATE prospecto2 SET estatus= '" + comboBox1.Text + "', observaciones= '" + txtObserv.Text + "'WHERE id= '" + ID + "'";

                    //Usando la clase de conexion
                    SqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();

                    try
                    {
                        SqlCommand comando = new SqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Estatus Actualizado");
                        cargarProspectos();
                        cargarProspectosEnviados();
                        cargarProspectosAutorizado();
                        cargarProspectosRechazados();
                            txtID.Clear();
                            textBox10.Clear();
                            textBox11.Clear();
                            textBox12.Clear();
                            txtObserv.Clear();
                        //this.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error al Actualizar el Estatus" + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Ocurrio un error" + ex); };
            }
            if (Opcion == DialogResult.No)
            {
                MessageBox.Show("No se envio el Estatus");
            }
            if (Opcion == DialogResult.Cancel)
            {
                MessageBox.Show("Se cancelo la operación");
            }
        }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text=dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox10.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox11.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox12.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Rechazado")
            {
                txtObserv.Visible = true;
                label29.Visible = true;
            }
            if (comboBox1.Text != "Rechazado"){
                txtObserv.Visible = false;
                label29.Visible = false;
            }
        }



        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            //Model.documentos dd = new Model.documentos();
            int id = int.Parse( dataGridView5.CurrentRow.Cells[0].Value.ToString());
            using (Model.promotorEntities4 db = new promotorEntities4())
            {
                var document = db.documentos.Find(id);
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string folder = path + "/temp/";
                string fullFilePath = folder + document.id;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (File.Exists(fullFilePath))
                    Directory.Delete(fullFilePath);
                File.WriteAllBytes(fullFilePath, document.documento);

                Process.Start(fullFilePath);
            }
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult Opcion;
            Opcion = MessageBox.Show("¿Esta seguro que desea salir?, todos los datos de la captura", "Documentos", MessageBoxButtons.YesNoCancel);
            if (Opcion == DialogResult.Yes)
            {
                this.Close();
            }
            if (Opcion == DialogResult.No)
            {
                
                MessageBox.Show("Continue...");
            }
            if (Opcion == DialogResult.Cancel)
            {
                //MessageBox.Show("Se cancelo la operación");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumeros(e);
        }
    }
}
