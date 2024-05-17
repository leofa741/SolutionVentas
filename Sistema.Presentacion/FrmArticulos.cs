

using Sistema.Datos;
using Sistema.Negocio;
using Sistema.Negocio.Observers;
using Sistema.Presentacion.utilities.impl;

using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.IO;
using System.Linq;


using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sistema.Presentacion
    {
    public partial class FrmArticulos : Form
        {
        private int totalRegistros = 0;
        private int tamañoPagina = 10; // Número de registros por página
        private int numeroPaginas = 0;
        private int paginaActual = 1;
        private string NombreAnt;
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio = "C:\\Users\\leonardo\\source\\repos\\leofa741\\SolutionVentas\\imsges\\";
        private BarcodeGenerator generator;

        // Define el texto de marcador de posición

        private PlaceholderTextBox placeholderTextBoxPrecio;
        private PlaceholderTextBox placeholderTextStock;
        public FrmArticulos()
            {
            InitializeComponent();
            btnAnterior.Click += btnAnterior_Click; // Asigna el evento clic al botón "Anterior"
            btnSiguiente.Click += btnSiguiente_Click; // Asigna el evento clic al botón "Siguiente"
            btnBuscar.Click += btnBuscar_Click;
            Listar();
            // Inicializar el generador y agregar observadores
            SqlConnection sqlconn = new SqlConnection();
            sqlconn = Conexion.getInstancia().CrearConexion();
            DatosArticulos articuloData = new DatosArticulos(sqlconn);
            generator = new BarcodeGenerator();
            generator.AddObserver(new DisplayBarcode(panelCodigobarra));
            generator.AddObserver(new DatabaseSaver(articuloData));
            generator.AddObserver(new BarcodeValidator());


            // Inicializar los PlaceholderTextBox
            placeholderTextBoxPrecio = new PlaceholderTextBox(txtPrecioProducto, "Ingrese el precio", Color.Gray, Color.Black);
            placeholderTextStock = new PlaceholderTextBox(txtStock, "Ingrese Stock", Color.Gray, Color.Black);

            this.Load += new System.EventHandler(this.FrmArticulos_Load);

            }




        private void Listar()
            {
            try
                {
                DataTable datos = NegocioArticulo.Listar(); // Suponiendo que NegocioArticulo.Listar() devuelve un DataTable

                List<DataRow> listaDatos = new List<DataRow>(); // Lista para almacenar las filas del DataTable

                foreach (DataRow fila in datos.Rows)
                    {
                    listaDatos.Add(fila);
                    }

                totalRegistros = listaDatos.Count;
                numeroPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);

                // Obtener los datos de la página actual
                var datosPagina = listaDatos.Skip((paginaActual - 1) * tamañoPagina).Take(tamañoPagina).ToList();

                // Bind the data to the DataGridView
                DgvListado.DataSource = datosPagina.CopyToDataTable(); // Convertir la lista de DataRow a DataTable
                this.Limpiar();
                this.Formato();

                lblTotal.Text = $"Página {paginaActual} de {numeroPaginas}, Total de registros: {totalRegistros}";

                // Habilitar/deshabilitar botones de navegación
                btnAnterior.Enabled = paginaActual > 1;
                btnSiguiente.Enabled = paginaActual < numeroPaginas;
                }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

        private void btnAnterior_Click(object sender, EventArgs e)
            {
            if (paginaActual > 1)
                {
                paginaActual--;
                Listar();
                }
            }

        private void btnSiguiente_Click(object sender, EventArgs e)
            {
            if (paginaActual < numeroPaginas)
                {
                paginaActual++;
                Listar();
                }
            }

        private void Buscar()
            {
            try
                {
                DataTable datos = NegocioArticulo.Buscar(txtBuscar.Text); // Obtener los datos sin filtrar

                if (datos != null && datos.Rows.Count > 0)
                    {
                    // Aplicar el filtro de búsqueda a las filas del DataTable
                    DataRow[] filasFiltradas = datos.Select($"Nombre LIKE '%{txtBuscar.Text}%'");

                    if (filasFiltradas.Length > 0) // Verificar si hay filas después del filtro
                        {
                        totalRegistros = filasFiltradas.Length;
                        numeroPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);

                        // Obtener los datos de la página actual
                        var datosPagina = filasFiltradas.Skip((paginaActual - 1) * tamañoPagina)
                                                         .Take(tamañoPagina)
                                                         .CopyToDataTable();

                        // Mostrar los resultados en el DataGridView
                        DgvListado.DataSource = datosPagina;
                        this.Formato();
                        lblTotal.Text = $"Página {paginaActual} de {numeroPaginas}, Total de registros: {totalRegistros}";
                        }
                    else
                        {
                        // Mostrar un mensaje indicando que no se encontraron resultados para la búsqueda realizada
                        MessageBox.Show("No se encontraron resultados para la búsqueda realizada.");

                        // Limpiar el DataGridView
                        DgvListado.DataSource = null;
                        lblTotal.Text = "Total de registros: 0";
                        }
                    }
                else
                    {
                    // Mostrar un mensaje indicando que no se encontraron resultados para la búsqueda realizada
                    MessageBox.Show("No se encontraron resultados para la búsqueda realizada.");

                    // Limpiar el DataGridView
                    DgvListado.DataSource = null;
                    lblTotal.Text = "Total de registros: 0";
                    }
                }
            catch (Exception ex)
                {
                // Manejar cualquier excepción que pueda ocurrir
                MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }


        private void Formato()
            {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[2].Width = 60;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Categoria";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Codigo";
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio venta";
            DgvListado.Columns[7].Width = 100;
            DgvListado.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DgvListado.Columns[8].HeaderText = "Descripción";
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;

            }

        private void Limpiar()
            {
            txtBuscar.Clear();
            txtId.Clear();
            txtIdArticulo.Clear();
            txtNombre.Clear();
            txtCodigoBarras.Clear();
            txtPrecioProducto.Clear();
            txtStock.Clear();
            txtImagen.Clear();
            pictureBoxImagen.Image = null;
            panelCodigobarra.BackgroundImage = null;
            this.RutaOrigen = "";
            this.RutaDestino = "";
            btnGuardarCodigoBarras.Enabled = true;
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



        private void MensajeOk(string msg)
            {
            MessageBox.Show(msg, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        private void CargarCategorias()
            {
            try
                {
                cboCategorias.DataSource = NegocioCategorias.Listar_categoria_selec();
                cboCategorias.ValueMember = "idcategoria";
                cboCategorias.DisplayMember = "nombre";

                }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message + ex.StackTrace);
                }

            }


        private void FrmArticulos_Load(object sender, EventArgs e)
            {

            this.Listar();
            this.CargarCategorias();
            // Aplicar el marcador de posición inicialmente
            placeholderTextBoxPrecio.Apply();
            placeholderTextStock.Apply();

            }

        private void btnBuscar_Click(object sender, EventArgs e)
            {
            this.Buscar();

            }

        private void label4_Click(object sender, EventArgs e)
            {

            }

        private void btnCargarImagen_Click(object sender, EventArgs e)
            {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files(*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (file.ShowDialog() == DialogResult.OK)
                {
                pictureBoxImagen.Image = Image.FromFile(file.FileName);
                txtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                this.RutaOrigen = file.FileName;

                }
            }

        private void btnGenerarCodigobarras_Click(object sender, EventArgs e)
            {
            int idarticulo;
            if (!int.TryParse(txtId.Text, out idarticulo))
                {
                idarticulo = -1; // Un ID temporal o indicativo de que el ID aún no está establecido
                }

            string nombreCodigo = txtCodigoBarras.Text;
            string precio = txtPrecioProducto.Text;
            string descripcion = txtNombre.Text;

            string textoCodigoBarras = nombreCodigo + "$" + precio + descripcion;

            generator.GenerateBarcode(idarticulo, textoCodigoBarras);
            btnGuardarCodigoBarras.Enabled = true;
            }


        private void btnGuardarCodigoBarras_Click(object sender, EventArgs e)
            {
            Image imagenCodigoBarra = (Image)panelCodigobarra.BackgroundImage.Clone();

            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.Filter = "Image PNG (*.png) | *.png";
            dialogoGuardar.FileName = txtCodigoBarras.Text;
            dialogoGuardar.ShowDialog();

            if (!string.IsNullOrEmpty(dialogoGuardar.FileName))
                {
                imagenCodigoBarra.Save(dialogoGuardar.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }

            imagenCodigoBarra.Dispose();
            }


        private void label6_Click(object sender, EventArgs e)
            {

            }

        private void btnInsertar_Click(object sender, EventArgs e)
            {
            try
                {
                string Respuesta = "";

                if (string.IsNullOrEmpty(cboCategorias.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecioProducto.Text) || string.IsNullOrEmpty(txtStock.Text))
                    {
                    IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                    errorHandler.ShowErrorMessage("Falta ingresar datos");
                    errorIcono.SetError(cboCategorias, "Ingrese categoría");
                    errorIcono.SetError(txtNombre, "Ingrese nombre");
                    errorIcono.SetError(txtPrecioProducto, "Ingrese precio");
                    errorIcono.SetError(txtStock, "Ingrese stock");
                    }
                else
                    {
                    string nombreArchivoConContador = txtImagen.Text.Trim();

                    // Verifica si txtImagen.Text no está vacío
                    if (!string.IsNullOrEmpty(nombreArchivoConContador))
                        {
                        // Construye la ruta de destino inicial
                        string rutaDestino = Path.Combine(this.Directorio, nombreArchivoConContador);
                        string rutaDirectorio = this.Directorio;
                        string nombreArchivo = Path.GetFileNameWithoutExtension(nombreArchivoConContador);
                        string extensionArchivo = Path.GetExtension(nombreArchivoConContador);
                        int contador = 1;

                        // Verifica si el archivo ya existe y genera un nuevo nombre si es necesario
                        while (File.Exists(rutaDestino))
                            {
                            // Genera un nuevo nombre de archivo con un contador
                            nombreArchivoConContador = $"{nombreArchivo}({contador}){extensionArchivo}";
                            rutaDestino = Path.Combine(rutaDirectorio, nombreArchivoConContador);
                            contador++;
                            }

                        try
                            {
                            // Copia el archivo desde la ruta de origen a la ruta de destino
                            File.Copy(this.RutaOrigen, rutaDestino);
                            Console.WriteLine("Archivo copiado exitosamente.");
                            }
                        catch (Exception ex)
                            {
                            // Maneja cualquier excepción que ocurra durante la operación de copiado
                            Console.WriteLine($"Error al copiar el archivo: {ex.Message}");
                            }
                        }

                    // Inserta el registro en la base de datos con el nombre del archivo actualizado
                    Respuesta = NegocioArticulo.Insertar(Convert.ToInt32(cboCategorias.SelectedValue), txtCodigoBarras.Text.Trim(), txtNombre.Text.Trim(), Convert.ToDecimal(txtPrecioProducto.Text), Convert.ToInt32(txtStock.Text), txtDescripcion.Text.Trim(), nombreArchivoConContador);

                    if (Respuesta.Equals("Ok"))
                        {
                        this.MensajeOk("Registro insertado");
                        this.Listar();
                        }
                    else
                        {
                        IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                        errorHandler.ShowErrorMessage(Respuesta);
                        }
                    }
                }
            catch (Exception ex)
                {
                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                errorHandler.ShowErrorMessage(ex.Message + ex.StackTrace);
                }
            }

        private void btnLimpiar_Click(object sender, EventArgs e)
            {
            // Limpiar el cuadro de texto de búsqueda
            txtBuscar.Text = string.Empty;

            // Llamar al método Listar para mostrar todos los registros nuevamente
            Listar();
            }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
            try
                {
                if (DgvListado.CurrentRow != null)
                    {
                    this.Limpiar();
                    btnUpdate.Visible = true;
                    btnInsertar.Visible = false;

                    txtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                    cboCategorias.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idcategoria"].Value);
                    txtCodigoBarras.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Codigo"].Value);
                    this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                    txtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                    txtPrecioProducto.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Precio_Venta"].Value);
                    txtStock.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Stock"].Value);
                    txtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);

                    string Imagen = Convert.ToString(DgvListado.CurrentRow.Cells["Imagen"].Value);
                    if (!string.IsNullOrEmpty(Imagen))
                        {
                        string rutaImagen = Path.Combine(this.Directorio, Imagen);
                        if (File.Exists(rutaImagen))
                            {
                            pictureBoxImagen.Image = Image.FromFile(rutaImagen);
                            txtImagen.Text = Imagen;
                            }
                        else
                            {
                            pictureBoxImagen.Image = null;
                            txtImagen.Text = "";
                            MessageBox.Show("La imagen no se pudo encontrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    else
                        {
                        pictureBoxImagen.Image = null;
                        txtImagen.Text = "";
                        }

                    tabControl1.SelectedIndex = 1;
                    }
                else
                    {
                    MessageBox.Show("No hay ninguna fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error al seleccionar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void btnUpdate_Click(object sender, EventArgs e)
            {
            try
                {
                if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(cboCategorias.Text) ||
                    string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecioProducto.Text) ||
                    string.IsNullOrEmpty(txtStock.Text))
                    {
                    IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                    errorHandler.ShowErrorMessage("Falta ingresar datos");

                    errorIcono.SetError(txtId, "Ingrese ID");
                    errorIcono.SetError(cboCategorias, "Ingrese categoría");
                    errorIcono.SetError(txtNombre, "Ingrese nombre");
                    errorIcono.SetError(txtPrecioProducto, "Ingrese precio");
                    errorIcono.SetError(txtStock, "Ingrese stock");
                    return;
                    }

                string nombreArchivoConContador = txtImagen.Text.Trim();

                // Si se especifica un archivo de imagen nuevo
                if (!string.IsNullOrEmpty(this.RutaOrigen))
                    {
                    string rutaDestino = Path.Combine(this.Directorio, nombreArchivoConContador);
                    string rutaDirectorio = this.Directorio;
                    string nombreArchivo = Path.GetFileNameWithoutExtension(nombreArchivoConContador);
                    string extensionArchivo = Path.GetExtension(nombreArchivoConContador);
                    int contador = 1;

                    // Generar un nuevo nombre de archivo si el archivo ya existe
                    while (File.Exists(rutaDestino))
                        {
                        nombreArchivoConContador = $"{nombreArchivo}({contador}){extensionArchivo}";
                        rutaDestino = Path.Combine(rutaDirectorio, nombreArchivoConContador);
                        contador++;
                        }

                    try
                        {
                        // Copiar el archivo desde la ruta de origen a la ruta de destino
                        File.Copy(this.RutaOrigen, rutaDestino, true); // Permitir sobreescritura si el archivo ya existe
                        Console.WriteLine("Archivo copiado exitosamente.");

                        // Actualizar la ruta de origen con la nueva ruta
                        this.RutaOrigen = rutaDestino;
                        }
                    catch (Exception ex)
                        {
                        // Manejar cualquier excepción que ocurra durante la operación de copiado de archivos
                        Console.WriteLine($"Error al copiar el archivo: {ex.Message}");
                        MessageBox.Show($"Error al copiar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                        }
                    }

                // Actualizar el registro en la base de datos con el nuevo nombre de archivo de imagen o el existente
                string respuesta = NegocioArticulo.Actualizar(
                    Convert.ToInt32(txtId.Text),
                    Convert.ToInt32(cboCategorias.SelectedValue),
                    txtCodigoBarras.Text.Trim(),
                    this.NombreAnt,
                    txtNombre.Text.Trim(),
                    Convert.ToDecimal(txtPrecioProducto.Text),
                    Convert.ToInt32(txtStock.Text),
                    txtDescripcion.Text.Trim(),
                    nombreArchivoConContador
                );

                if (respuesta.Equals("Ok"))
                    {
                    this.MensajeOk("Registro actualizado correctamente");
                    this.Listar(); // Actualizar la lista
                    tabControl1.SelectedIndex = 0; // Volver a la pestaña de lista
                    }
                else
                    {
                    IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                    errorHandler.ShowErrorMessage(respuesta);
                    }
                }
            catch (Exception ex)
                {
                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                errorHandler.ShowErrorMessage("Error al actualizar el registro: " + ex.Message);
                }
            }

        private void panelCodigobarra_Paint(object sender, PaintEventArgs e)
            {

            }



        private void txtPrecioProducto_KeyPress(object sender, KeyPressEventArgs e)
            {
            // Permitir solo números, punto decimal y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números y un punto decimal.", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }

            // Permitir solo un punto decimal
            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
                {
                e.Handled = true;
                MessageBox.Show("Solo se permite un punto decimal.", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }
            }



        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
            {
            // Permitir solo números y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                e.Handled = true;
                MessageBox.Show("Solo se permite Numeros", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }
            }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
                {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
                }
            }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
            {
            if (chkSeleccionar.Checked)
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
                Opocion = MessageBox.Show("¿Deseas eliminar el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opocion == DialogResult.OK)
                    {
                    int codigo;
                    string Rpta = "";
                    string Imagen = "";

                    foreach (DataGridViewRow item in DgvListado.Rows)
                        {
                        if (Convert.ToBoolean(item.Cells[0].Value))
                            {

                            codigo = Convert.ToInt32(item.Cells[1].Value);
                            Imagen = Convert.ToString(item.Cells[9].Value);
                            Rpta = NegocioArticulo.Eliminar(codigo);

                            if (Rpta.Equals("Ok"))
                                {
                                this.MensajeOk("Se elimino correctamente: " + Convert.ToString(item.Cells[5].Value));
                                File.Delete(this.Directorio + Imagen);
                                }
                            else
                                {
                                //this.MensajeError(Rpta);
                                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                                errorHandler.ShowErrorMessage(Rpta);
                                }

                            }

                        }
                    this.Listar();
                    }

                }
            catch (Exception ex)
                {

                //MessageBox.Show(ex.Message + ex.StackTrace);
                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("categorias");
                errorHandler.ShowErrorMessage(ex.Message + ex.StackTrace);
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
                    MessageBox.Show("Por favor, seleccione al menos un registro para eliminar.", "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // No hay registros seleccionados, salimos del método
                    }

                // Continuamos con el proceso de activación
                DialogResult Opocion;
                Opocion = MessageBox.Show("¿Deseas eliminar el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opocion == DialogResult.OK)
                    {
                    int codigo;
                    string Rpta = "";


                    foreach (DataGridViewRow item in DgvListado.Rows)
                        {
                        if (Convert.ToBoolean(item.Cells[0].Value))
                            {

                            codigo = Convert.ToInt32(item.Cells[1].Value);

                            Rpta = NegocioArticulo.Desactivar(codigo);

                            if (Rpta.Equals("Ok"))
                                {
                                this.MensajeOk("Se desactivo correctamente: " + Convert.ToString(item.Cells[5].Value));

                                }
                            else
                                {
                                //this.MensajeError(Rpta);
                                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                                errorHandler.ShowErrorMessage(Rpta);
                                }

                            }

                        }
                    this.Listar();
                    }

                }
            catch (Exception ex)
                {

                //MessageBox.Show(ex.Message + ex.StackTrace);
                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("categorias");
                errorHandler.ShowErrorMessage(ex.Message + ex.StackTrace);
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
                    MessageBox.Show("Por favor, seleccione al menos un registro para eliminar.", "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // No hay registros seleccionados, salimos del método
                    }

                // Continuamos con el proceso de activación
                DialogResult Opocion;
                Opocion = MessageBox.Show("¿Deseas eliminar el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opocion == DialogResult.OK)
                    {
                    int codigo;
                    string Rpta = "";


                    foreach (DataGridViewRow item in DgvListado.Rows)
                        {
                        if (Convert.ToBoolean(item.Cells[0].Value))
                            {

                            codigo = Convert.ToInt32(item.Cells[1].Value);

                            Rpta = NegocioArticulo.Activar(codigo);

                            if (Rpta.Equals("Ok"))
                                {
                                this.MensajeOk("Se activo correctamente: " + Convert.ToString(item.Cells[5].Value));

                                }
                            else
                                {
                                //this.MensajeError(Rpta);
                                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("articulos");
                                errorHandler.ShowErrorMessage(Rpta);
                                }

                            }

                        }
                    this.Listar();
                    }

                }
            catch (Exception ex)
                {

                //MessageBox.Show(ex.Message + ex.StackTrace);
                IErrorHandler errorHandler = ErrorHandlerFactory.GetErrorHandler("categorias");
                errorHandler.ShowErrorMessage(ex.Message + ex.StackTrace);
                }
            }







        }
    }
