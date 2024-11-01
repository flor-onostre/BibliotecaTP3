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

namespace biblioteca
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /*public ObservableCollection<Libro> libros { get; set; } // Colección de libros
        public ObservableCollection<Prestamo> prestamos { get; set; }*/

        //cargar archivo
        /*public ObservableCollection<Libro> Cargarlibro(string RutaArchivo)
        {
            var libroscargados = new ObservableCollection<Libro>(); ;
            if (File.Exists(RutaArchivo))
            {
                using (var reader = new StreamReader(RutaArchivo))
                {
                    string linea;
                    bool PrimeraLinea = true; //Saltarse la cabecera del csv
                    while (true)
                    {
                        var nuevalinea = reader.ReadLine(); //leer nueva linea
                        if (nuevalinea == null) break; //salir si se alcanza el final del archivo
                        linea = nuevalinea; //asignar a linea solo si no es nulo
                        if (PrimeraLinea)
                        {
                            PrimeraLinea = false; //Saltar
                            continue;
                        }
                        //divir las lineas del cvs
                        var partes = linea.Split(',');
                        //asaegurar que tenga los 6 elementos
                        if (partes.Length == 4)
                        {
                            var libro = new Libro
                            {
                                Titulo = partes[0],
                                Autor = partes[1],
                                Editorial = partes[2],
                                Ubicacion = partes[3],
                                //Estado = partes[4],
                                //Reponer = bool.Parse(partes[5])
                            };
                            libroscargados.Add(libro);
                        }
                    }
                }
            }
            return libroscargados;
        }*/

        //Funcion para guardar los datos en un archivo cvs
       /* public void GuardarLibros(ObservableCollection<Libro> libros, string RutaArchivo)
        {
            //StreamWriter heredado de TextWriter
            using (TextWriter writer = new StreamWriter(RutaArchivo))
            {
                //Cabecera.
                writer.WriteLine("Título,Autor,Editorial,Ubicacion");
                //Datos para cada libro en formato cvs
                foreach (var libro in libros)
                {
                    writer.WriteLine($"{libro.Titulo},{libro.Autor},{libro.Editorial},{libro.Ubicacion}");
                }
            }
        }*/

        public MainWindow()
        {
            InitializeComponent();
            Guardar.Click += Guardar_Click; // Asignar evento al botón
            GuardarPrestamo.Click += GuardarPrestamo_Click; // Asignar evento al botón de guardar préstamo
            /*libros = new ObservableCollection<Libro>(); // Inicializar la colección de libros
            prestamos = new ObservableCollection<Prestamo>(); // Inicializar la colección de préstamos
            */
            // Llamar al archivo
            /*string RutaArchivo = "libro.csv";
            var librosCargados = Cargarlibro(RutaArchivo);
            foreach (var libro in librosCargados)
            {
                libros.Add(libro); // Agregar los libros
            }

            LibrosDataGrid.ItemsSource = libros; // Vincular la colección al DataGrid
            this.DataContext = this; // Establecer conexión con DataContext para el binding en XAML*/
        }

        // Clase para almacenar los datos del libro
        /*public class Libro
        {
            public required string Titulo { get; set; }
            public required string Autor { get; set; }
            public required string Editorial { get; set; }
            public required string Ubicacion { get; set; }
            public int Recibidos { get; set; }
            //public string? Estado{ get; set; }
            //public bool Reponer { get; set; }
        }*/
        //Clase para los prestamos¿

        // Método para manejar el evento del botón Guardar
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("solo un click deberia salir");
            // Validar que todos los campos obligatorios estén llenos
            if (string.IsNullOrWhiteSpace(TituloTextBox.Text) || // Verifica que no esté vacío o sólo con espacios
                string.IsNullOrWhiteSpace(AutorTextBox.Text) ||
                string.IsNullOrWhiteSpace(EditorialTextBox.Text) ||
                string.IsNullOrWhiteSpace(UbiTextBox.Text) ||
                !int.TryParse(RecibidosTextBox.Text, out int recibidos)) // Agregado UbiTextBox.Text
            {
                MessageBox.Show("Faltan datos.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Detiene el proceso si falta algún dato
            }
            // Crear un nuevo libro con los datos ingresados
            /*var nuevoLibro = new Libro
            {
                Titulo = TituloTextBox.Text,
                Autor = AutorTextBox.Text,
                Editorial = EditorialTextBox.Text,
                Ubicacion = UbiTextBox.Text,
                Recibidos = recibidos
                //Estado = (EstadoComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                //Reponer = ReponerCheckBox.IsChecked ?? false
            };*/

            // ruta donde guardar los datos
            string RutaArchivo = "libros.csv";
            /*GuardarLibros(libros, RutaArchivo); // Guardar el nuevo libro*/

            // Agregar el nuevo libro a la colección
            /*libros.Add(nuevoLibro);*/

            // Limpiar los campos después de guardar
            TituloTextBox.Clear();
            AutorTextBox.Clear();
            EditorialTextBox.Clear();
            UbiTextBox.Clear();
            RecibidosTextBox.Clear();
            //EstadoComboBox.SelectedItem = null;
            //ReponerCheckBox.IsChecked = false;

            // Confirmación de que se guardó la información
            MessageBox.Show("Información guardada en " + RutaArchivo, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /*public class Prestamo
        {
            public required string Titulo { get; set; }
            public required string Autor { get; set; }
            public required DateTime FechaRetiro { get; set; }
            public int CantidadPrestada { get; set; } // Cantidad de libros prestados
        }*/
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

            // Buscar el libro correspondiente
           /* var libro = libros.FirstOrDefault(l => l.Titulo == TituloPrestamoTextBox.Text && l.Autor == AutorPrestamoTextBox.Text);
            if (libro == null)
            {
                MessageBox.Show("El Libro no existe.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }*/

            // Verificar la disponibilidad de copias
            /*if (libro.Recibidos < cantidadPrestada)
            {
                MessageBox.Show("Sin suficientes copias disponibles.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }*/

            // Crear el préstamo
            /*var nuevoPrestamo = new Prestamo
            {
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                FechaRetiro = DateTime.Now,
                CantidadPrestada = cantidadPrestada
            };*/

            // Actualizar la cantidad de libros
            /*libro.Recibidos -= cantidadPrestada;*/

            // Agregar el nuevo préstamo a la colección
            /*prestamos.Add(nuevoPrestamo);*/

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
