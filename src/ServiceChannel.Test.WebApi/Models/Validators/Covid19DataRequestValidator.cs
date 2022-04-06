﻿using FluentValidation;

using ServiceChannel.Test.WebApi.Models.Requests;

namespace ServiceChannel.Test.WebApi.Models.Validators;

public class Covid19DataRequestValidator : AbstractValidator<Covid19DataRequest>
{
    public Covid19DataRequestValidator()
    {
        this.RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        this.RuleFor(x => x.Location.County)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .When(x => string.IsNullOrWhiteSpace(x.Location?.State));
        this.RuleFor(x => x.Location.State)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .When(x => string.IsNullOrWhiteSpace(x.Location?.County));
    }
}