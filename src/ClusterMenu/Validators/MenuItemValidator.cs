using ClusterMenu.Model;
using FluentValidation;

namespace ClusterMenu.Validators {

    /// <summary>
    /// This is a validator for <see cref="MenuItem"/>
    /// </summary>
    public class MenuItemValidator : AbstractValidator<MenuItem> {

        public MenuItemValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => "Name is empty");
            RuleFor(x => x.Price)
                .GreaterThan(decimal.Zero).WithMessage(x => "Price must be greater than zero");
        }

    }
}
