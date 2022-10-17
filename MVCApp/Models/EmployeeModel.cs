using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace MVCApp.Models
{
    public class EmployeeModel
    {
        // Data annotation allows us to modify our model
        [Display(Name = "Employee ID")]
        // [Range] says EmployeeId is somewhere in the 6-digit range
        [Range(100000, 999999, ErrorMessage = "You need to enter a valid Employee ID that's 6 digits long.")]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to provide your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to provide your first name.")]
        public string LastName { get; set; }

        // [DataType] tells the app that this is an email address, so the app will VALIDATE the info given
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "You need to provide a valid email address.")]
        public string EmailAddress { get; set; }

        // Use [Compare("varName")] to force a comparison to another var
        [Display(Name = "Confirm Email")]
        [Compare("EmailAddress", ErrorMessage = "The Email and Confirm Email must match to proceed.")]
        public string ConfirmEmail { get; set; }

        // [DataType] lets us identify what kind of value we're getting from the user
        // [StringLength] sets a maxi value of 100 char and a MinimumLength of 10 char
        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must have a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Passwords must be at least 10 characters long.")]
        public string Password { get; set; }

        // ConfirmPassword doesn't need [Required] or [StringLength] because it has [Compare].
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

// This is using jQuery validation and tracking the data on the CLIENT side