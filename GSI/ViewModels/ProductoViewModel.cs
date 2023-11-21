namespace GSI.ViewModels
{
    public class ProductoViewModel
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadStock { get; set; } // Asumiendo que quieres mostrar esto
        public string Categoria { get; set; }
        // Agrega cualquier otro detalle que quieras mostrar, como Estado de la transacción
    }

}
