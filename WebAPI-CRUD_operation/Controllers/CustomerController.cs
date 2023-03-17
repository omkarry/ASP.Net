using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD_operation.Models;
using WebAPI_CRUD_operation.Service.Interfaces;
using WebAPI_CRUD_operation.Service.Models;

namespace WebAPI_CRUD_operation.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerLocationRepository _customerLocation;

        public CustomerController(ICustomerLocationRepository customerLocation)
        {
            _customerLocation = customerLocation;
        }

        /// <summary>
        /// Returns all customers
        /// </summary>
        /// <returns>List of cutomers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Customers
        ///     
        /// </remarks>
        /// <response code="200">Returns List of customers</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("api/Customers")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Customer>> Get()
        {
            try
            {
                var customers = _customerLocation.GetCustomers();
                if (customers.Count == 0)
                {
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ResponseMessages.NoCustomers });
                }
                else
                    return Ok(new ApiResponse<List<Customer>> { StatusCode = 200, Message = ResponseMessages.CustomerList, Result = customers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific customer
        /// </summary>
        /// <returns>A requested customer</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Customer/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested customer</response>
        /// <response code="404">Customer not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("Customer/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult Get(int id)
        {
            try
            {
                Customer customer = _customerLocation.GetCustomer(id);
                if (customer == null)
                    return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.CustomerNotFound });
                else
                    return Ok(new ApiResponse<Customer> { StatusCode = 200, Message = ResponseMessages.CustomerDetails, Result = customer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Creates a Customer.
        /// </summary>
        /// <returns>A newly created Customer</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Customer
        ///     {
        ///        "id": 0,
        ///        "firstName": string,
        ///        "lastName": string,
        ///        "locations": [
        ///             {
        ///                 "id": 0,
        ///                 "addreess": string
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created customer</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("Customer")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult<Customer> Post(Customer newCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ResponseMessages.DataFormat });

                else
                {
                    Customer customer = _customerLocation.AddCustomer(newCustomer);
                    return Ok(new ApiResponse<Customer> { StatusCode = 200, Message = ResponseMessages.CustomerAdd, Result = customer });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <returns>Updated customer</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Customer
        ///     {
        ///        "id": 0,
        ///        "firstName": string,
        ///        "lastName": string,
        ///        "locations": [
        ///             {
        ///                 "id": 0,
        ///                 "addreess": string
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Updated customer</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="404">Customer not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("Customer")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult<Customer> Put(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ResponseMessages.DataFormat });
                else
                {
                    Customer updatedCustomer = _customerLocation.UpdateCustomer(customer);
                    if (updatedCustomer == null)
                        return NotFound(new ApiResponse<Customer> { StatusCode = 404, Message = ResponseMessages.CustomerNotFound, Result = customer });
                    else
                        return Ok(new ApiResponse<Customer> { StatusCode = 200, Message = ResponseMessages.CustomerUpdate, Result = updatedCustomer });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Customer/{id}
        ///
        /// </remarks>
        /// <response code="200">Customer deleted successfully</response>
        /// <response code="404">Customer not found</response>
        /// <response code="500">Internal server Error</response>

        [HttpDelete("Customer/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int id)
        {
            try
            {
                int customerDeleted = _customerLocation.DeleteCustomer(id);
                if (customerDeleted == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ResponseMessages.CustomerDelete });
                else if (customerDeleted == 1)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ResponseMessages.CustomerWithLocations });
                else
                    return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.CustomerNotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a location of particular customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Customer/{customerId}/Location/{locationId}
        ///
        /// </remarks>
        /// <response code="200">Customer's location deleted successfully</response>
        /// <response code="404">Customer with location not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("Customer/{customerId}/Location/{locationId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteCustomerLocation(int customerId, int locationId)
        {
            try
            {
                int locationDeleted = _customerLocation.DeleteCustomerLocation(customerId, locationId);
                if (locationDeleted == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ResponseMessages.CustomerLocationDelete });
                else if(locationDeleted == 1)
                    return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.CustomerLocationNotFound });

                else
                    return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.CustomerNotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}