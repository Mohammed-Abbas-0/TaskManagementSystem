using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Interface.Dtos
{
    public class RefreshTokenRequestModel
    {
        public required string Token {  get; set; }  
        public required DateTime ExpireDate { get; set; }    
    }
}
