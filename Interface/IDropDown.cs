using FICCI_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FICCI_API.Interface
{
    public interface IDropDown
    {
        public Task<IActionResult> GetRole(int id);
    }
}
