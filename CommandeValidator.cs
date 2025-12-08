using FluentValidation;
using ClientApi.Models;

public class CommandeDtoValidator : AbstractValidator<Commande>
{
    public CommandeDtoValidator()
    {
        RuleFor(c => c.NumeroCommande).NotEmpty().WithMessage("Le numéro de commande est obligatoire.");

        RuleFor(c => c.MontantTotal)
            .GreaterThan(0).WithMessage("Le montant total doit être supérieur à 0.");

        RuleFor(c => c.ClientId)
            .GreaterThan(0).WithMessage("Le ClientId doit être renseigné.");

        RuleFor(c => c.DateCommande)
            .NotEmpty().WithMessage("La date de commande est obligatoire.");

        RuleFor(c => c.Statut)
            .MaximumLength(50).WithMessage("Le statut ne peut pas dépasser 50 caractères.");
    }
}
