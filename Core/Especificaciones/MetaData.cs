namespace API.Especificaciones
{
    public class MetaData
    {
        public int CurrentPage { get; set; } // Pagina Actual

        public int TotalPages { get; set; } // Total Paginas

        public int PageSize { get; set; } // Tamaño pagina

        public int TotalCount { get; set; } // Total de Registros

        public bool HasPrevious => CurrentPage > 1; //Paginas previas

        public bool HasNext => CurrentPage < TotalPages; // Paginas siguientes
    }
}
