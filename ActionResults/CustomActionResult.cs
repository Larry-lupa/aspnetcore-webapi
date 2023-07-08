using Microsoft.AspNetCore.Mvc;
using my_books.Data.ViewModels;

namespace my_books.ActionResults
{
    public class CustomActionResult : IActionResult
    {
        private readonly CustomActionResultVM _customActionResult;

        public CustomActionResult(CustomActionResultVM customActionResult)
        {
            _customActionResult = customActionResult;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_customActionResult.Exception ?? _customActionResult.Publisher as object)
            {
                StatusCode = _customActionResult.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
