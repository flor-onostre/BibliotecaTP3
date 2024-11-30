# BibliotecaTP3

TRABAJO PRÁCTICO N°3:
Bases de Datos - SQL Server - CRUD

Carrera: Tecnicatura Universitaria en Programación

Materia: Programación II

Comisión: 2C

Docente: Francisco Díaz

Grupo: G

Integrantes:
	- ONOSTRE, María Florencia

ADMINISTRACIÓN DE BIBLIOTECAS DE LIBROS



PLANTEO GENERAL

Se realiza un programa para la administración de bibliotecas de libros. Para ello se utiliza el lenguaje C#, haciendo uso del paradigma de programación orientada a objetos y WPF para la interfaz gráfica. Este programa tiene dos funcionalidades, la de almacenar los libros que la biblioteca recibe y la de registrar los préstamos. Sin embargo, por motivos de tiempo se llegó a realizar el CRUD con la SQL Server.



REALIZACIÓN


Creación BBDD
Se creó con el siguiente script de SQL: https://drive.google.com/file/d/1ijxJOjJBdF4WC_dly4MooJPM4i_4bgfR/view?usp=drive_link
Realización del código en C#


MainWindows:

Constructor de MainWindow:

InitializeComponent(): Inicializa los componentes gráficos de la ventana principal.

Cadena de conexión: Obtiene la cadena de conexión de la configuración de la aplicación, que permite conectarse a la base de datos de la biblioteca.

Inicialización de conexión: Se crea una instancia de SqlConnection para manejar las conexiones a la base de datos.

MuestraLibros(): Llama a este método para cargar los libros desde la base de datos y mostrarlos en la interfaz.

Variable de conexión SQL:
Se declara una variable de tipo SqlConnection que se usará para manejar la conexión con la base de datos.

Método MuestraLibros:
Este método consulta todos los libros de la tabla LIBRO en la base de datos y los muestra en un control DataGrid llamado LibrosDataGrid.

Consulta SQL: La consulta SQL SELECT * FROM LIBRO selecciona todos los registros de la tabla LIBRO.

SqlDataAdapter: Usa SqlDataAdapter para ejecutar la consulta y llenar un DataTable.

Enlace a la interfaz: Se establece el enlace de datos (ItemsSource) de LibrosDataGrid con el DataTable llenado.

Método Guardar_Click:
Este método maneja el evento de clic del botón Guardar, que guarda un nuevo libro en la base de datos.

Validación de datos: Comprueba si los campos requeridos están vacíos. Si falta algún dato, se muestra un mensaje de advertencia.

Recopilación de datos: Si los campos están llenos, se recopilan los datos del libro de los TextBox correspondientes (TituloTextBox, AutorTextBox, EditorialTextBox, UbicacionTextBox).

Guardado en la Base de Datos:

Apertura de conexión: Abre la conexión a la base de datos.

Consulta SQL: Inserta un nuevo registro en la tabla LIBRO utilizando los parámetros recibidos.

Manejo de excepciones: Si ocurre un error, se muestra un mensaje.

Cierre de conexión: Asegura que la conexión se cierra.

Actualización de la lista: Llama a MuestraLibros() para actualizar la lista de libros.

Limpieza de campos: Borra los TextBox después de guardar.

Método Eliminar_Click:
Este método maneja el evento del botón Eliminar para eliminar un libro seleccionado.

Validación de selección: Comprueba si hay un libro seleccionado en el DataGrid.

Eliminación en la Base de Datos:

Consulta SQL: Elimina el libro seleccionado usando IdLibro como criterio.

Manejo de excepciones y cierre de conexión: Similar al método de guardado.

Actualización de la lista: Llama a MuestraLibros() para actualizar la lista de libros.


VentanaActualizar:

Variable z y Constructor de VentanaActualizar:

z: Esta variable privada almacena el ID del libro que se va a actualizar. Es un valor que se pasa al constructor.

Constructor VentanaActualizar(int Id): Inicializa la ventana, asigna el ID recibido a z, y establece una conexión a la base de datos.

Cadena de conexión: Obtiene la cadena de conexión desde la configuración de la aplicación.

Inicialización de conexión SQL: Crea una instancia de SqlConnection para conectarse a la base de datos.

Declaración de miConexionSql:
Esta es una variable de tipo SqlConnection, que se usará para manejar la conexión a la base de datos dentro de esta ventana.

Método Confirmar_Click:
Este método se ejecuta cuando el usuario hace clic en el botón Confirmar para actualizar un libro.

Validación de datos: Comprueba que todos los campos de texto estén llenos (TituloActualizaTextBox, AutorActualizaTextBox, EditorialActualizaTextBox, UbicacionActualizaTextBox). Si alguno está vacío, muestra un mensaje de advertencia y detiene el proceso.

Conexión a la base de datos y actualización de datos:

Apertura de conexión: Abre la conexión con la base de datos.

Consulta SQL de actualización: Actualiza el registro del libro con los datos ingresados por el usuario, usando @idLibro para identificar el libro por su ID.

Parámetros de consulta: Añade los valores de los TextBox de la ventana a la consulta usando AddWithValue para evitar inyecciones SQL.

Ejecución de la actualización y cierre de la ventana:

ExecuteNonQuery: Ejecuta la consulta de actualización en la base de datos. filasAfectadas contiene el número de filas afectadas por la operación.

Comprobación de éxito: Si el número de filas afectadas es mayor a 0, muestra un mensaje de éxito. Si no, informa que el libro no fue encontrado.

Manejo de excepciones: Si ocurre un error durante la actualización, muestra un mensaje de error detallando la excepción.

Cierre de conexión: Cierra la conexión SQL en el bloque finally para asegurar que siempre se liberen los recursos.

Cierre de ventana: Después de realizar la actualización, cierra la ventana VentanaActualizar.



LINKS

Archivos xaml: 
MainWindow: https://drive.google.com/file/d/1Dq6wTRlHxut71BGx3hk1NaEBQY6bT9FY/view?usp=drive_link

VentanaActualizar:
https://drive.google.com/file/d/1qgGJmjwGk0DgiqDuwSSOADMxBv3gLhB-/view?usp=drive_link

Archivos cs: 

MainWindow:
https://drive.google.com/file/d/1OF1fR1gX60_rXQszhZy8v8Wwsh7-gsxm/view?usp=drive_link

VentanaActualizar:
https://drive.google.com/file/d/1SQAvrOWdn2fArR6XkvyvK-Em5ckkNC3b/view?usp=drive_link 

Archivo exe:
https://drive.google.com/file/d/1LCIdEmu0nDdHI87EXk90bxBNWXTg3aBF/view?usp=drive_link

Video Explicación: Onostre-María-Florencia.mp4
