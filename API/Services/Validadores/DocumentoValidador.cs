using Core.Entidades;
using FluentValidation;

namespace Services.Validadores
{
    public class DocumentoValidador : AbstractValidator<Documento>
    {
        public DocumentoValidador() {
            RuleFor(x => x.IdTipo);
            RuleFor(x => x.IdPrestamo);
            RuleFor(x => x.documento);
        }
    }
}