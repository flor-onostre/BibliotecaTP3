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

            Guardar.Click += Guardar_Click; // Asignar evento al botón
            GuardarPrestamo.Click += GuardarPrestamo_Click; // Asignar evento al botón de guardar préstamo
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

                LibrosDataGrid.DisplayMemberPath = "Titulo";
                LibrosDataGrid.SelectedValuePath = "IdLibro";
                LibrosDataGrid.ItemsSource = LibroTabla.DefaultView;
            }
        }


















        //Clase para los prestamos

        // Método para manejar el evento del botón Guardar
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            string titulo = TituloTextBox.Text;
            string autor = AutorTextBox.Text;
            string editorial = EditorialTextBox.Text;
            string ubicacion = UbicacionTextBox.Text;
            Int64 ejemplares = Convert.ToInt64(EjemplaresTextBox.Text);

            string consulta = "INSERT INTO LIBRO (titulo, autor, editorial) VALUES (@titulo, @autor, @editorial)";
            SqlCommand miSqlCommand = new SqlCommand(consulta, miConexionSql); 
            miConexionSql.Open();
            

            // Validar que todos los campos obligatorios estén llenos
            if (string.IsNullOrWhiteSpace(TituloTextBox.Text) || // Verifica que no esté vacío o sólo con espacios
                string.IsNullOrWhiteSpace(AutorTextBox.Text) ||
                string.IsNullOrWhiteSpace(EditorialTextBox.Text) ||
                string.IsNullOrWhiteSpace(UbicacionTextBox.Text) ||
                !int.TryParse(EjemplaresTextBox.Text, out int recibidos)) // Agregado UbiTextBox.Text
            {
                MessageBox.Show("Faltan datos.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Detiene el proceso si falta algún dato
            }

            // Limpiar los campos después de guardar
            TituloTextBox.Clear();
            AutorTextBox.Clear();
            EditorialTextBox.Clear();
            UbicacionTextBox.Clear();
            EjemplaresTextBox.Clear();
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
