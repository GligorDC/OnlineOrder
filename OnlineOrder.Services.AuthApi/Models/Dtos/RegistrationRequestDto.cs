﻿namespace OnlineOrder.Services.AuthApi.Models.Dtos
{
    public class RegistrationRequestDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password {  get; set; }
        public string RoleName { get; set; }
    }
}