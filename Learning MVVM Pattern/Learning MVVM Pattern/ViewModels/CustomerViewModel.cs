namespace Learning_MVVM_Pattern.ViewModels
{
    using System.Windows;
    using Models;
    
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            Customer customer = new Customer();
            
            customer.Details.SetName("Steve");
            customer.Details.Address.SetFullAddress("69 Zoo Lane", "Zootopia", "ID6 9FK");
            MessageBox.Show(customer.IsValid.ToString());
        }
    }
    
}