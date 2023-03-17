using WebAPI_CRUD_operation.Service.Models;
using WebAPI_CRUD_operation.Service.Interfaces;

namespace WebAPI_CRUD_operation.Service.Services
{
    public class CustomerLocationService : ICustomerLocationRepository
    {
        private readonly List<Customer> _customers = new();
        private readonly List<Location> _locations = new();

        private int _nextCustomerId = 0;
        private int _nextLocationId = 0;

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = _customers;
            return customers;
        }

        public Customer GetCustomer(int customerId)
        {
            int customerIndex = _customers.FindIndex(x => x.Id == customerId);
            if (customerIndex == -1)
                return null;
            else
                return (_customers[customerIndex]);
        }

        public Customer AddCustomer(Customer customer)
        {
            customer.Id = ++_nextCustomerId;
            foreach (Location loc in customer.Locations)
            {
                int locationIndex = _locations.FindIndex(l => l.Address.ToLower() == loc.Address.ToLower());
                if (locationIndex != -1)
                    loc.Id = _locations[locationIndex].Id;
                else
                {
                    loc.Id = ++_nextCustomerId;
                    _locations.Add(loc);
                }
            }
            _customers.Add(customer);
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            int customerIndex = _customers.FindIndex(x => x.Id == customer.Id);
            if (customerIndex != -1)
            {
                foreach(Location loc in customer.Locations)
                {
                    int locationIndex = _locations.FindIndex(l =>  l.Address.ToLower() == loc.Address.ToLower());
                    if(locationIndex != -1)
                        loc.Id = _locations[locationIndex].Id;
                    else
                    {
                        loc.Id = ++_nextCustomerId;
                        _locations.Add(loc);
                    }
                }
                _customers[customerIndex] = customer;
                return _customers[customerIndex];
            }
            else
                return null;
        }

        public int DeleteCustomer(int customerId)
        {
            int customerIndex = _customers.FindIndex(x => x.Id == customerId);
            if (customerIndex != -1)
            {
                if (_customers[customerIndex].Locations.Count > 0)
                    return 1;
                else
                {
                    _customers.RemoveAt(customerIndex);
                    return 0;
                }
            }
            else
                return -1;
        }

        public int DeleteCustomerLocation(int customerId, int locationId)
        {
            int customerIndex = _customers.FindIndex(x => x.Id == customerId);
            if (customerIndex != -1)
            {
                int locationIndex = _customers[customerIndex].Locations.FindIndex(l => l.Id == locationId);
                if (locationIndex != -1)
                {
                    _customers[customerIndex].Locations.RemoveAt(locationIndex);
                    return 0;
                }
                else
                    return 1;
            }
            else
                return -1;
        }

        public void DeleteCustomerLocation(int locationId)
        {
            foreach (Customer customer in _customers)
            {
                int locationIndex = customer.Locations.FindIndex(x => x.Id == locationId);
                if (locationIndex != -1)
                {
                    customer.Locations.RemoveAt(locationIndex);
                }
            }
        }

        public void UpdateCustomerLocation(Location location)
        {
            foreach (Customer customer in _customers)
            {
                int locationIndex = customer.Locations.FindIndex(x => x.Id == location.Id);
                if (locationIndex != -1)
                {
                    customer.Locations[locationIndex] = location;
                }
            }
        }

        public List<Location> GetLocations()
        {
            List<Location> locations = _locations;
            return locations;
        }

        public Location GetLocation(int locationId)
        {
            int locationIndex = _locations.FindIndex(x => x.Id == locationId);
            if (locationIndex == -1)
                return null;
            else
                return (_locations[locationIndex]);
        }

        public Location AddLocation(Location location)
        {
            int locationIndex = _locations.FindIndex(x => x.Address.ToLower() == location.Address.ToLower());
            if (locationIndex == -1)
            {
                location.Id = ++_nextLocationId;
                _locations.Add(location);
                return location;
            }
            else
                return null;
        }

        public Location UpdateLocation(Location location)
        {
            int locationIndex = _locations.FindIndex(x => x.Id == location.Id);
            if (locationIndex != -1)
            {
                UpdateCustomerLocation(location);
                _locations[locationIndex] = location;
                return _locations[locationIndex];
            }
            else
                return null;
        }

        public bool DeleteLocation(int locationId)
        {
            int locationIndex = _locations.FindIndex(x => x.Id == locationId);
            if (locationId != -1)
            {
                DeleteCustomerLocation(locationId);
                _locations.RemoveAt(locationIndex);
                return true;
            }
            else
                return false;
        }
    }
}
