using Sistema.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmCategorias : Form
    {

        private string NombreAnt;

        public FrmCategorias()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NegocioCategorias.Listar();
                this.Formato();
                this.Limpiar();
                lblTotal.Text = "Total de registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NegocioCategorias.Buscar(txtBuscar.Text);
                this.Formato();
                lblTotal.Text = "Total de registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            DgvListado.Columns[0].Visible = true;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Width = 100;
            DgvListado.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DgvListado.Columns[3].HeaderText = "Descripción";
            DgvListado.Columns[4].Width = 150;

        }

        private void MensajeError(string msg)
        {
            MessageBox.Show(msg, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOk(string msg)
        {
            MessageBox.Show(msg, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar()
        {
            txtBuscar.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();

            DgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
            chkSeleccionar.Checked = false;
        }
        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Respuesta = "";

                if (txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar datos");
                    errorIcono.SetError(txtNombre, "Ingrsese en nombre");
                }
                else
                {
                    Respuesta = NegocioCategorias.Insertar(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());

                    if (Respuesta.Equals("Ok"))
                    {
                        this.MensajeOk("registro insertado");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Respuesta);
                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void btnCanselar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            this.Listar();
            tabControl1.SelectedIndex = 0;

        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnInsertar.Visible = false;
                btnActualizar.Visible = true;
                txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                txtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);
                tabControl1.SelectedIndex = 1;
            }
            catch(Exception)
            {
                MessageBox.Show("Seleccione desde la celda nombre");
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Respuesta = "";

                if (txtNombre.Text == string.Empty || txtId.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar datos");
                    errorIcono.SetError(txtNombre, "Ingrsese en nombre");
                }
                else
                {
                    Respuesta = NegocioCategorias.Actualizar(Convert.ToInt32(txtId.Text), this.NombreAnt, txtNombre.Text.Trim(), txtDescripcion.Text.Trim());

                    if (Respuesta.Equals("Ok"))
                    {
                        this.MensajeOk("registro actualizado correctamente");
                        this.Limpiar();
                        this.Listar();
                        tabControl1.SelectedIndex = 0;
                    }
                    else
                    {
                        this.MensajeError(Respuesta);
                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }

        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);

            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
                {
                // Verificar si al menos un registro está seleccionado
                bool alMenosUnoSeleccionado = false;
                foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                    if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                        alMenosUnoSeleccionado = true;
                        break; // No necesitamos continuar verificando una vez que se encuentre uno seleccionado
                        }
                    }

                if (!alMenosUnoSeleccionado)
                    {
                    MessageBox.Show("Por favor, seleccione al menos un registro para eliminar.", "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // No hay registros seleccionados, salimos del método
                    }

                // Continuamos con el proceso de activación
                DialogResult Opocion;
                Opocion = MessageBox.Show("¿Deseas elimino el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opocion == DialogResult.OK)
                    {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow item in DgvListado.Rows)
                        {
                        if (Convert.ToBoolean(item.Cells[0].Value))
                            {
                            codigo = Convert.ToInt32(item.Cells[1].Value);
                            Rpta = NegocioCategorias.Eliminar(codigo);

                            if (Rpta.Equals("Ok"))
                                {
                                this.MensajeOk("Se eliminó correctamente: " + Convert.ToString(item.Cells[2].Value));
                                }
                            else
                                {
                                this.MensajeError(Rpta);
                                }
                            }
                        }
                    this.Listar();
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

       private void btnActivar_Click(object sender, EventArgs e)
         {
    try
    {         
        // Verificar si al menos un registro está seleccionado
        bool alMenosUnoSeleccionado = false;
        foreach (DataGridViewRow fila in DgvListado.Rows)
        {
            if (Convert.ToBoolean(fila.Cells[0].Value))
            {
                alMenosUnoSeleccionado = true;
                break; // No necesitamos continuar verificando una vez que se encuentre uno seleccionado
            }
        }

        if (!alMenosUnoSeleccionado)
        {
            MessageBox.Show("Por favor, seleccione al menos un registro para activar.", "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return; // No hay registros seleccionados, salimos del método
        }
        
        // Continuamos con el proceso de activación
        DialogResult Opocion;
        Opocion = MessageBox.Show("¿Deseas activar el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        if (Opocion == DialogResult.OK)
        {
            int codigo;
            string Rpta = "";

            foreach (DataGridViewRow item in DgvListado.Rows)
            {              
                if (Convert.ToBoolean(item.Cells[0].Value))
                {
                    codigo = Convert.ToInt32(item.Cells[1].Value);
                    Rpta = NegocioCategorias.Activar(codigo);

                    if (Rpta.Equals("Ok"))
                    {
                        this.MensajeOk("Se activó correctamente: " + Convert.ToString(item.Cells[2].Value));
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }
            }
            this.Listar();
        }
                }
    catch (Exception ex)
          {
        MessageBox.Show(ex.Message + ex.StackTrace);
            }

            }


        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
                {
                // Verificar si al menos un registro está seleccionado
                bool alMenosUnoSeleccionado = false;
                foreach (DataGridViewRow fila in DgvListado.Rows)
                    {
                    if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                        alMenosUnoSeleccionado = true;
                        break; // No necesitamos continuar verificando una vez que se encuentre uno seleccionado
                        }
                    }

                if (!alMenosUnoSeleccionado)
                    {
                    MessageBox.Show("Por favor, seleccione al menos un registro para desactivar.", "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // No hay registros seleccionados, salimos del método
                    }

                // Continuamos con el proceso de activación
                DialogResult Opocion;
                Opocion = MessageBox.Show("¿Deseas desactivar el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opocion == DialogResult.OK)
                    {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow item in DgvListado.Rows)
                        {
                        if (Convert.ToBoolean(item.Cells[0].Value))
                            {
                            codigo = Convert.ToInt32(item.Cells[1].Value);
                            Rpta = NegocioCategorias.Desactivar(codigo);

                            if (Rpta.Equals("Ok"))
                                {
                                this.MensajeOk("Se desactivó correctamente: " + Convert.ToString(item.Cells[2].Value));
                                }
                            else
                                {
                                this.MensajeError(Rpta);
                                }
                            }
                        }
                    this.Listar();
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

        private void tabGeneral_Click(object sender, EventArgs e)
        {

        }






    }
}
