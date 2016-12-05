/*using System;
using System.ComponentModel.DataAnnotations;
using ADL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static ADL.Models.EnumCollection;

public class AssignmentModelValidation : ValidationAttribute
{
    private Assignment assignment;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        bool success = false;
        switch (assignment.Type)
        {
            case AssignmentType.Textual:
                {
                    Assignment newAssignment = (Assignment)validationContext.ObjectInstance;
                    success = true;
                    break;
                }
                case AssignmentType.ExclusiveChoice:
                {
                    ExclusiveChoiceAssignment newAssignment = (ExclusiveChoiceAssignment)validationContext.ObjectInstance;
                    success = true;
                    break;
                }
                case AssignmentType.MultipleChoice:
                {
                    MultipleChoiceAssignment newAssignment = (MultipleChoiceAssignment)validationContext.ObjectInstance;
                    success = true;
                    break;
                }
        }


        if (!success)
        {
            return new ValidationResult("ee");
        }

        return ValidationResult.Success;
    }
}
*/