using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Data; // lógica para manejar la interfaz
using System.Configuration;
using System.Linq;
using System;
using System.Data.SqlClient;
using BibliotecaTP3;

namespace biblioteca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string miConexion = ConfigurationManager.ConnectionStrings["BibliotecaTP3.Properties.Settings.BibliotecaPruebaConnectionString"].ConnectionString; //Conectar BBDD

            miConexionSql = new SqlConnection(miConexion); // Inicialización de clase conexión

            MuestraLibros();

        }




        SqlConnection miConexionSql; //Crear clase de conexión




        //Clases para libros
        private void MuestraLibros()
        {
            string consulta = "SELECT * FROM LIBRO";

            SqlDataAdapter adapter = new SqlDataAdapter(consulta, miConexionSql);

            using (adapter)
            {
                DataTable LibroTabla = new DataTable();

                adapter.Fill(LibroTabla);

                LibrosDataGrid.DisplayMemberPath = "Título";
                LibrosDataGrid.SelectedValuePath = "IdLibro";
                LibrosDataGrid.ItemsSource = LibroTabla.DefaultView;
            }
        }




        // Método para manejar el evento del botón Guardar
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {

            // Validar que todos los campos obligatorios estén llenos antes de realizar el guardado
            if (string.IsNullOrEmpty(TituloTextBox.Text) ||
                string.IsNullOrEmpty(AutorTextBox.Text) ||
                string.IsNullOrEmpty(EditorialTextBox.Text) ||
                string.IsNullOrEmpty(UbicacionTextBox.Text))
            {
                MessageBox.Show("Faltan datos.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Detiene el proceso si falta algún dato
            }

            string titulo = TituloTextBox.Text;
            string autor = AutorTextBox.Text;
            string editorial = EditorialTextBox.Text;
            string ubicacion = UbicacionTextBox.Text;

            try
            {
                miConexionSql.Open();

                // Inserta el libro en la tabla LIBRO
                string consultaLibro = "INSERT INTO LIBRO (Título, Autor, Editorial, Ubicación) VALUES (@titulo, @autor, @editorial, @ubicacion);";
                SqlCommand miSqlCommand = new SqlCommand(consultaLibro, miConexionSql);

                miSqlCommand.Parameters.AddWithValue("@titulo", titulo);
                miSqlCommand.Parameters.AddWithValue("@autor", autor);
                miSqlCommand.Parameters.AddWithValue("@editorial", editorial);
                miSqlCommand.Parameters.AddWithValue("@ubicacion", ubicacion);

                miSqlCommand.ExecuteNonQuery(); // Ejecutar el comando

                miConexionSql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el libro: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Cerrar la conexión
                miConexionSql.Close();
            }

            MuestraLibros(); // Actualiza la lista de libros

            // Limpiar los campos después de guardar
            TituloTextBox.Clear();
            AutorTextBox.Clear();
            EditorialTextBox.Clear();
            UbicacionTextBox.Clear();
        }




        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                miConexionSql.Open();

                // Elimina el libro de la tabla LIBRO
                string consultaLibro = "DELETE FROM LIBRO WHERE IdLibro=@IDLIBRO";
                SqlCommand miSqlCommand = new SqlCommand(consultaLibro, miConexionSql);

                miSqlCommand.Parameters.AddWithValue("@IDLIBRO", LibrosDataGrid.SelectedValue);

                miSqlCommand.ExecuteNonQuery(); // Ejecutar el comando

                miConexionSql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el libro: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Cerrar la conexión
                miConexionSql.Close();
            }

            MuestraLibros(); // Actualiza la lista de libros
        }




        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (LibrosDataGrid.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecciona un libro.");
                return;
            }

            VentanaActualizar ventanaActualizar = new VentanaActualizar((int)LibrosDataGrid.SelectedValue);

            string consulta = "SELECT Título, Autor, Editorial, Ubicación FROM LIBRO WHERE IdLibro=@IDLIBRO";

            SqlCommand miSqlCommand = new SqlCommand(consulta, miConexionSql);

            SqlDataAdapter adapter = new SqlDataAdapter(miSqlCommand);

            using (adapter)
            {
                miSqlCommand.Parameters.AddWithValue("@IDLIBRO", LibrosDataGrid.SelectedValue);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    ventanaActualizar.TituloActualizaTextBox.Text = dataTable.Rows[0]["Título"].ToString();
                    ventanaActualizar.AutorActualizaTextBox.Text = dataTable.Rows[0]["Autor"].ToString();
                    ventanaActualizar.EditorialActualizaTextBox.Text = dataTable.Rows[0]["Editorial"].ToString();
                    ventanaActualizar.UbicacionActualizaTextBox.Text = dataTable.Rows[0]["Ubicación"].ToString();
                }
            }

            ventanaActualizar.ShowDialog();

            MuestraLibros();
        }





        private void GuardarPrestamo_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(TituloPrestamoTextBox.Text) ||
                string.IsNullOrWhiteSpace(AutorPrestamoTextBox.Text) ||
                string.IsNullOrWhiteSpace(EditorialPrestamoTextBox.Text) ||
                string.IsNullOrWhiteSpace(PrestatarioTextBox.Text) ||
                !int.TryParse(CantidadPrestadaTextBox.Text, out int cantidadPrestada) || // Convierte y valida
                cantidadPrestada <= 0)
            {
                MessageBox.Show("Faltan Datos", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Limpiar los campos después de guardar
            TituloPrestamoTextBox.Clear();
            AutorPrestamoTextBox.Clear();
            EditorialPrestamoTextBox.Clear();
            PrestatarioTextBox.Clear();
            CantidadPrestadaTextBox.Clear();

            // Confirmación de que se guardó la información
            MessageBox.Show("Préstamo registrado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void LibrosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

    }


}
