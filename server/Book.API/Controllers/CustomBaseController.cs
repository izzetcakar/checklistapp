using Book.Core.Dtos.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PageApp.API.Controllers
{
    public class CustomBaseController:ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
