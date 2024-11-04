using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BibliotecaTP3
{
    /// <summary>
    /// Lógica de interacción para VentanaActualizar.xaml
    /// </summary>
    /// 

    public partial class VentanaActualizar : Window
    {

        private int z;

        public VentanaActualizar(int Id)
        {
            InitializeComponent();
            z = Id;
            string miConexion = ConfigurationManager.ConnectionStrings["BibliotecaTP3.Properties.Settings.BibliotecaPruebaConnectionString"].ConnectionString; //Conectar BBDD
            miConexionSql = new SqlConnection(miConexion);
        }



        SqlConnection miConexionSql;




        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TituloActualizaTextBox.Text) ||
            string.IsNullOrEmpty(AutorActualizaTextBox.Text) ||
            string.IsNullOrEmpty(EditorialActualizaTextBox.Text) ||
            string.IsNullOrEmpty(UbicacionActualizaTextBox.Text))
            {
                MessageBox.Show("Faltan datos.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Detiene el proceso si falta algún dato
            }

            try
            {
                miConexionSql.Open();

                // Consulta de actualización correcta
                string consultaLibro = "UPDATE LIBRO SET Título = @titulo, Autor = @autor, Editorial = @editorial, Ubicación = @ubicacion WHERE IdLibro = @idLibro";
                SqlCommand miSqlCommand = new SqlCommand(consultaLibro, miConexionSql);

                miSqlCommand.Parameters.AddWithValue("@titulo", TituloActualizaTextBox.Text);
                miSqlCommand.Parameters.AddWithValue("@autor", AutorActualizaTextBox.Text);
                miSqlCommand.Parameters.AddWithValue("@editorial", EditorialActualizaTextBox.Text);
                miSqlCommand.Parameters.AddWithValue("@ubicacion", UbicacionActualizaTextBox.Text);
                miSqlCommand.Parameters.AddWithValue("@idLibro", z); // Aquí z debe contener el valor del IdLibro a actualizar

                // Ejecutar el comando
                int filasAfectadas = miSqlCommand.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Libro actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el libro a actualizar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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

            this.Close();
        }
    }
}
