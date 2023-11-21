namespace GSI.ViewModels
{
    public class AjustarStockViewModel
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public int CantidadStock { get; set; }
        public int NewCantidadStock { get; set; } = 0;
    }
}
