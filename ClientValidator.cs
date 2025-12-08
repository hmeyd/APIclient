using FluentValidation;
using ClientApi.Models;

public class ClientDtoValidator : AbstractValidator<Client>
{
    public ClientDtoValidator()
    {
        RuleFor(c => c.Nom).NotEmpty().WithMessage("Le nom est obligatoire.");
        RuleFor(c => c.Prenom).NotEmpty().WithMessage("Le prénom est obligatoire.");
        RuleFor(c => c.Email).EmailAddress().WithMessage("L'email doit être valide.");
        RuleFor(c => c.Telephone).NotEmpty().WithMessage("Le téléphone doit être renseigné.");
        RuleFor(c => c.Adresse).NotEmpty().WithMessage("L'adresse est obligatoire.");
    }
}

