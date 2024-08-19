namespace Proyecto_Web.ViewModels
{
    public class ViewModelBase
    {
        public Exception? Exception { get; set; }

        public bool HasError => Exception != null;
    }
}
