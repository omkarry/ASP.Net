using WebAPI_CRUD_operation.Service.Models;

namespace WebAPI_CRUD_operation.Service.Interfaces
{
    public interface ICustomerLocationRepository
    {
        public Customer GetCustomer(int customerId);
        public List<Customer> GetCustomers();
        public Customer AddCustomer(Customer customer);
        public Customer UpdateCustomer(Customer customer);
        public int DeleteCustomer(int customerId);
        public int DeleteCustomerLocation(int customerId, int locationId);
        public void DeleteCustomerLocation(int locationId);
        public void UpdateCustomerLocation(Location location);
        public Location GetLocation(int locationId);
        public List<Location> GetLocations();
        public Location AddLocation(Location location);
        public Location UpdateLocation(Location location);
        public bool DeleteLocation(int locationId);
    }
}
