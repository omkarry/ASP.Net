using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD_operation.Models;
using WebAPI_CRUD_operation.Service.Interfaces;
using WebAPI_CRUD_operation.Service.Models;

namespace WebAPI_CRUD_operation.Controllers
{
    [ApiController]
    [Route("api")]
    public class LocationController : ControllerBase
    {
        private readonly ICustomerLocationRepository _customerLocation;

        public LocationController(ICustomerLocationRepository customerLocation)
        {
            _customerLocation = customerLocation;
        }

        /// <summary>
        /// Returns all Locations
        /// </summary>
        /// <returns>List of locations</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Locations
        ///     
        /// </remarks>
        /// <response code="200">Returns List of locations</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("Locations")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Location>> GetLocations()
        {
            try
            {
                List<Location> locations = _customerLocation.GetLocations();
                if (locations.Count == 0)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ResponseMessages.NoLocations });
                else
                    return Ok(new ApiResponse<List<Location>> { StatusCode = 200, Message = ResponseMessages.CustomerUpdate, Result = locations });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns specific location
        /// </summary>
        /// <returns>A requested location</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Location/{id}
        ///
        /// </remarks>
        /// <response code="200">Returns the requested location</response>
        /// <response code="404">Location not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("Location/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult GetLocation(int id)
        {
            try
            {
                Location location = _customerLocation.GetLocation(id);
                if (location == null)
                    return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.LocationNotFound });
                else
                    return Ok(new ApiResponse<Location> { StatusCode = 200, Message = ResponseMessages.LocationList, Result = location });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Creates a Location.
        /// </summary>
        /// <returns>A newly created location</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Location
        ///     {
        ///         "id": 0,
        ///         "address": string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created location</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost("Location")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult<Location> Post(Location newLocation)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ResponseMessages.DataFormat });
                else
                {
                    Location location = _customerLocation.AddLocation(newLocation);
                    if (location != null)
                        return Ok(new ApiResponse<Location> { StatusCode = 200, Message = ResponseMessages.LocationAdd, Result = location });
                    else
                        return Ok(new ApiResponse<Location> { StatusCode = 200, Message = ResponseMessages.LocationExist });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a location.
        /// </summary>
        /// <returns>Updated location</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Location
        ///     {
        ///         "id": 0,
        ///         "address": string
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Updated location</response>
        /// <response code="400">Entered data is not in correct format</response>
        /// <response code="404">Location not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpPut("Location")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult<Location> Put(Location location)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ApiResponse<object> { StatusCode = 400, Message = ResponseMessages.DataFormat });
                else
                {
                    Location updatedLocation = _customerLocation.UpdateLocation(location);
                    if (updatedLocation == null)
                        return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.LocationNotFound });
                    else
                        return Ok(new ApiResponse<Location> { StatusCode = 200, Message = ResponseMessages.LocationNotFound, Result = updatedLocation }); ;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes a location.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Location/{id}
        ///
        /// </remarks>
        /// <response code="200">Location deleted successfully</response>
        /// <response code="404">Location not found</response>
        /// <response code="500">Internal server Error</response>
        [HttpDelete("Location/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteLocation(int id)
        {
            try
            {
                bool locationDeleted = _customerLocation.DeleteLocation(id);
                if (locationDeleted)
                    return Ok(new ApiResponse<object> { StatusCode = 200, Message = ResponseMessages.LocationDelete });
                else
                    return NotFound(new ApiResponse<object> { StatusCode = 404, Message = ResponseMessages.LocationNotFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
